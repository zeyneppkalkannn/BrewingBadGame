using UnityEngine;
using System.Collections.Generic;

public class DeliveryArea2 : MonoBehaviour
{
    public LayerMask itemLayer; // Teslimat alan�nda alg�lanacak nesnelerin katman�
    public Transform containerTransform; // Konteynerin transform referans�
    public float checkRadius = 2f; // Kontrol yar��ap�
    public OrderManager2 orderManager; // OrderManager2 referans�
    public Timer timer; // Timer referans�

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) // Teslimat� kontrol etmek i�in 'T' tu�u
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
            // Y�kseklik kontrol�
            if (hitCollider.transform.position.y >= containerTransform.position.y)
            {
                if (hitCollider.CompareTag("PortakalSuyu") || hitCollider.CompareTag("Limonata") || hitCollider.CompareTag("Sandvi�") || hitCollider.CompareTag("Kruvasan") || hitCollider.CompareTag("Donut"))
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

        // Teslimat alan�ndaki nesneleri sil
        foreach (Collider hitCollider in hitColliders)
        {
            // Y�kseklik kontrol�
            if (hitCollider.transform.position.y >= containerTransform.position.y)
            {
                if (hitCollider.CompareTag("PortakalSuyu") || hitCollider.CompareTag("Limonata") || hitCollider.CompareTag("Sandvi�") || hitCollider.CompareTag("Kruvasan") || hitCollider.CompareTag("Donut"))
                {
                    Destroy(hitCollider.gameObject);
                }
            }
        }

        // S�reyi yeniden ba�lat
        timer.ResetTimer();

        // Yeni sipari� olu�tur
        orderManager.GenerateNewOrder();
    }
}
