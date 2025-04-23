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
        {
            Destroy(currentModel);
        }

        itemNameText.text = itemName;

        currentModel = Instantiate(modelPrefab, modelAnchor);

        // Reset local position, rotation, and scale
        currentModel.transform.localPosition = Vector3.zero;
        currentModel.transform.localRotation = Quaternion.Euler(20, 45, 0); // slight angle
        currentModel.transform.localScale = Vector3.one * 100f; // make it bigger if it's tiny

        panel.SetActive(true);
    }

    public void CloseInspection()
    {
        panel.SetActive(false);
        if (currentModel != null)
            Destroy(currentModel);
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