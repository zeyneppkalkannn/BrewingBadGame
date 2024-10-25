using UnityEngine;
using TMPro;

public class ClipboardInteraction2 : MonoBehaviour
{
    public GameObject orderPanel;
    public GameObject cursorImage;
    public Camera playerCamera;
    public float interactionDistance = 5f;
    public TextMeshProUGUI orderList;
    public LayerMask interactableLayer;
    public OrderManager2 orderManager;

    void Start()
    {
        // Baþlangýçta paneli gizle
        orderPanel.SetActive(false);
        orderList.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
            {
                Debug.Log("Raycast hit: " + hit.collider.gameObject.name); // Raycast sonucu logla

                if (hit.collider.gameObject.name == "OrderClipboard")
                {
                    ToggleOrderPanel();
                }
            }
            else
            {
                Debug.Log("Raycast missed");
            }
        }
    }

    void ToggleOrderPanel()
    {
        bool isActive = orderPanel.activeSelf;
        orderPanel.SetActive(!isActive);
        orderList.enabled = !isActive;
        cursorImage.SetActive(isActive);

        if (!isActive)
        {
            orderManager.UpdateOrderListText();
        }
    }
}
