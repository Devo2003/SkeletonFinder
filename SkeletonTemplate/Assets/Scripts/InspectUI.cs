using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InspectUI : MonoBehaviour
{
    public static InspectUI Instance;

    public GameObject panel;
    public TextMeshProUGUI itemNameText;
    public Transform modelAnchor;
    public Button closeInspect;

    private GameObject currentModel;
    private Vector3 rotationInput;
    public float rotationSpeed = 100f;
    private bool isDragging = false;
    public float scaleSpeed = 0.1f;

    [System.Serializable]
    public class InspectableItem
    {
        public string itemName;
        public Vector3 rotationOffset;
    }

    public List<InspectableItem> inspectableItems = new List<InspectableItem>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        panel.SetActive(false);
        closeInspect.enabled = false;
    }

    public void InspectItem(string itemName, GameObject modelPrefab)
    {
        if (currentModel != null)
            Destroy(currentModel);

        //Disable Player Movement and Dialogue
        PlayerController.Instance.EnableMovement(false);
        DialogueManager.Instance.EnableDialogue(false);

        itemNameText.text = itemName;

        currentModel = Instantiate(modelPrefab, modelAnchor);
        currentModel.transform.localPosition = Vector3.zero;
        Vector3 defaultRotation = new Vector3(20, 45, 0); // Fallback rotation
        Vector3 rotationOffset = GetRotationOffset(itemName);

        // Apply rotation offset
        currentModel.transform.localRotation = Quaternion.Euler(defaultRotation + rotationOffset);
        NormalizeModelScale(currentModel); // ← Add this line

        currentModel.layer = LayerMask.NameToLayer("Default");
        foreach (Transform child in currentModel.GetComponentsInChildren<Transform>())
        {
            child.gameObject.layer = LayerMask.NameToLayer("Default");
        }

        panel.SetActive(true);
        closeInspect.enabled = true;
    }

    public void CloseInspection()
    {
        panel.SetActive(false);
        closeInspect.enabled = false;
        if (currentModel != null)
            Destroy(currentModel);

        //Enable Player Movement and Dialogue
        PlayerController.Instance.EnableMovement(true);
        DialogueManager.Instance.EnableDialogue(true);
    }

    private void NormalizeModelScale(GameObject model)
    {
        Renderer[] renderers = model.GetComponentsInChildren<Renderer>();

        if (renderers.Length == 0) return;

        Bounds bounds = renderers[0].bounds;
        foreach (Renderer rend in renderers)
        {
            bounds.Encapsulate(rend.bounds);
        }

        float maxDimension = Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z);
        float desiredSize = 129.0f; // Tweak this to your liking

        if (maxDimension > 0f)
        {
            float scaleFactor = desiredSize / maxDimension;
            model.transform.localScale = Vector3.one * scaleFactor;
        }
    }

    private void Update()
    {
        if (panel.activeSelf && currentModel != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDragging = true; //Left mouse button clicked, dragging starts
            }

            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false; //Left mouse button released, dragging stops
            }

            if (isDragging)
            {
                float x = Input.GetAxis("Mouse X");
                float y = Input.GetAxis("Mouse Y");
                currentModel.transform.Rotate(Vector3.up, -x * rotationSpeed * Time.deltaTime, Space.World);
                currentModel.transform.Rotate(Vector3.right, y * rotationSpeed * Time.deltaTime, Space.World);
            }

            //Scale model based on mouse wheel scroll
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            if (scrollInput != 0f)
            {
                float scaleChange = scrollInput * scaleSpeed;

                // Increase or decrease the current scale
                Vector3 newScale = currentModel.transform.localScale + new Vector3(scaleChange, scaleChange, scaleChange);

                //Ensure the new scale is not too small
                newScale = Vector3.Max(newScale, Vector3.one * 0.1f); //Prevent item from becoming too small
                currentModel.transform.localScale = newScale;
            }

        }

        if (panel.activeSelf && Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Escape))
        {
            CloseInspection();
        }
    }

    private Vector3 GetRotationOffset(string itemName)
    {
        foreach (InspectableItem item in inspectableItems)
        {
            if (item.itemName == itemName)
            {
                return item.rotationOffset;
            }
        }
        return Vector3.zero; // Default if no item is found
    }
}