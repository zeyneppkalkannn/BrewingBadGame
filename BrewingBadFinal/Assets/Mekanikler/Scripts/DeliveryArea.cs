using UnityEngine;
using System.Collections.Generic;

public class DeliveryArea : MonoBehaviour
{
    public LayerMask itemLayer; // Teslimat alanýnda algýlanacak nesnelerin katmaný
    public Transform containerTransform; // Konteynerin transform referansý
    public float checkRadius = 2f; // Kontrol yarýçapý
    public OrderManager orderManager; // OrderManager referansý
    public Timer timer; // Timer referansý

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) // Teslimatý kontrol etmek için 'T' tuþu
        {
            CheckForItems();
        }
    }

    void CheckForItems()
    {
        Collider[] hitColliders = Physics.OverlapSphere(containerTransform.position, checkRadius, itemLayer);
        Dictionary<string, int> deliveredItems = new Dictionary<string, int>();

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Cola") || hitCollider.CompareTag("Hamburger") || hitCollider.CompareTag("Pizza"))
            {
                string itemName = hitCollider.GetComponent<ObjectGrabbable>().itemName; // itemName kullanarak kontrol
                if (deliveredItems.ContainsKey(itemName))
                {
                    deliveredItems[itemName]++;
                }
                else
                {
                    deliveredItems[itemName] = 1;
                }
            }
        }

        bool isOrderCorrect = orderManager.CheckOrder(deliveredItems);
        orderManager.HandleDelivery(isOrderCorrect);

        // Teslimat alanýndaki nesneleri sil
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Cola") || hitCollider.CompareTag("Hamburger") || hitCollider.CompareTag("Pizza"))
            {
                Destroy(hitCollider.gameObject);
            }
        }

        // Süreyi yeniden baþlat
        timer.ResetTimer();

        // Yeni sipariþ oluþtur
        orderManager.GenerateNewOrder();
    }
}
