using UnityEngine;
using TMPro;

public class ClipboardInteraction : MonoBehaviour
{
    public GameObject orderPanel;
    public GameObject cursorImage;
    public Camera playerCamera;
    public float interactionDistance = 5f;
    public LayerMask interactableLayer;
    public OrderManager orderManager;
    public TextMeshProUGUI orderList; // TextMeshPro referansý

    void Start()
    {
        // Baþlangýçta paneli ve text'i gizle
        orderPanel.SetActive(false);
        cursorImage.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
            {
                if (hit.collider.gameObject.name == "OrderClipboard")
                {
                    ToggleOrderPanel();
                }
            }
        }
    }

    void ToggleOrderPanel()
    {
        bool isActive = orderPanel.activeSelf;
        orderPanel.SetActive(!isActive);
        orderList.gameObject.SetActive(!isActive);
        cursorImage.SetActive(isActive);

        if (!isActive)
        {
            orderManager.UpdateOrderListText();
        }
    }
}
