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

    private GameObject currentModel;
    private Vector3 rotationInput;
    public float rotationSpeed = 100f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        panel.SetActive(false);
    }

    public void InspectItem(string itemName, GameObject modelPrefab)
    {
        if (currentModel != null)
            Destroy(currentModel);

        itemNameText.text = itemName;

        currentModel = Instantiate(modelPrefab, modelAnchor);
        currentModel.transform.localPosition = Vector3.zero;
        currentModel.transform.localRotation = Quaternion.Euler(20, 45, 0);

        NormalizeModelScale(currentModel); // ← Add this line

        currentModel.layer = LayerMask.NameToLayer("Default");
        foreach (Transform child in currentModel.GetComponentsInChildren<Transform>())
        {
            child.gameObject.layer = LayerMask.NameToLayer("Default");
        }

        panel.SetActive(true);
    }

    public void CloseInspection()
    {
        panel.SetActive(false);
        if (currentModel != null)
            Destroy(currentModel);
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
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");
            currentModel.transform.Rotate(Vector3.up, -x * rotationSpeed * Time.deltaTime, Space.World);
            currentModel.transform.Rotate(Vector3.right, y * rotationSpeed * Time.deltaTime, Space.World);
        }

        if (panel.activeSelf && Input.GetKeyDown(KeyCode.C))
        {
            CloseInspection();
        }
    }
}