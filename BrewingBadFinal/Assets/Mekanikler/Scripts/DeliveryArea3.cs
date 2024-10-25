using UnityEngine;
using System.Collections.Generic;

public class DeliveryArea3 : MonoBehaviour
{
    public LayerMask itemLayer; // Teslimat alanýnda algýlanacak nesnelerin katmaný
    public Transform containerTransform; // Konteynerin transform referansý
    public float checkRadius = 2f; // Kontrol yarýçapý
    public OrderManager3 orderManager; // OrderManager3 referansý
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
                if (hitCollider.CompareTag("Limonata") || hitCollider.CompareTag("kahve") || hitCollider.CompareTag("meyvesuyu") || hitCollider.CompareTag("Çay") || hitCollider.CompareTag("Çörek") || hitCollider.CompareTag("kurabiye") || hitCollider.CompareTag("Limonlu") || hitCollider.CompareTag("Viþneli") || hitCollider.CompareTag("cupcake"))
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
                if (hitCollider.CompareTag("Limonata") || hitCollider.CompareTag("kahve") || hitCollider.CompareTag("meyvesuyu") || hitCollider.CompareTag("Çay") || hitCollider.CompareTag("Çörek") || hitCollider.CompareTag("kurabiye") || hitCollider.CompareTag("Limonlu") || hitCollider.CompareTag("Viþneli") || hitCollider.CompareTag("cupcake"))
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
