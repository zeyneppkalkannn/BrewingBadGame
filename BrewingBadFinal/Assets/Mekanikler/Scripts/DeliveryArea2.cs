using UnityEngine;
using System.Collections.Generic;

public class DeliveryArea2 : MonoBehaviour
{
    public LayerMask itemLayer; // Teslimat alanýnda algýlanacak nesnelerin katmaný
    public Transform containerTransform; // Konteynerin transform referansý
    public float checkRadius = 2f; // Kontrol yarýçapý
    public OrderManager2 orderManager; // OrderManager2 referansý
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
            // Yükseklik kontrolü
            if (hitCollider.transform.position.y >= containerTransform.position.y)
            {
                if (hitCollider.CompareTag("PortakalSuyu") || hitCollider.CompareTag("Limonata") || hitCollider.CompareTag("Sandviç") || hitCollider.CompareTag("Kruvasan") || hitCollider.CompareTag("Donut"))
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
        }

        bool isOrderCorrect = orderManager.CheckOrder(deliveredItems);
        orderManager.HandleDelivery(isOrderCorrect);

        // Teslimat alanýndaki nesneleri sil
        foreach (Collider hitCollider in hitColliders)
        {
            // Yükseklik kontrolü
            if (hitCollider.transform.position.y >= containerTransform.position.y)
            {
                if (hitCollider.CompareTag("PortakalSuyu") || hitCollider.CompareTag("Limonata") || hitCollider.CompareTag("Sandviç") || hitCollider.CompareTag("Kruvasan") || hitCollider.CompareTag("Donut"))
                {
                    Destroy(hitCollider.gameObject);
                }
            }
        }

        // Süreyi yeniden baþlat
        timer.ResetTimer();

        // Yeni sipariþ oluþtur
        orderManager.GenerateNewOrder();
    }
}
