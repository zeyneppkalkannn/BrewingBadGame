using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class OrderManager3 : MonoBehaviour
{
    public TextMeshProUGUI orderListText; // TextMeshPro referansı
    private List<string> itemList = new List<string> { "Limonata", "kahve", "meyvesuyu", "Çay", "Çörek", "kurabiye", "Limonlu", "Vișneli", "cupcake" }; // Sipariş oluşturulacak öğeler
    public Dictionary<string, int> currentOrder = new Dictionary<string, int>();
    public TextMeshProUGUI gameOverText; // Oyun sonu mesajı için TextMeshPro referansı
    public CoinManager coinManager; // CoinManager referansı
    public string mainMenuSceneName = "MainMenu"; // Ana menü sahnesinin adı
    public float gameOverDelay = 3f; // Game over ekranının gösterilme süresi

    private int wrongDeliveryCount = 0; // Yanlış teslimat sayacı

    void Start()
    {
        GenerateNewOrder();
        gameOverText.gameObject.SetActive(false); // Başlangıçta oyun sonu mesajını gizle
    }

    public void GenerateNewOrder()
    {
        currentOrder.Clear();
        int numberOfItems = Random.Range(1, 5); // 1 ile 4 arasında rastgele çeşit sayısı

        List<string> selectedItems = new List<string>(itemList);
        for (int i = 0; i < numberOfItems; i++)
        {
            string selectedItem = selectedItems[Random.Range(0, selectedItems.Count)];
            selectedItems.Remove(selectedItem); // Seçilen öğeyi listeden kaldırarak tekrar seçilmesini önle
            int quantity = Random.Range(1, 4); // 1 ile 3 arasında rastgele miktar
            currentOrder[selectedItem] = quantity;
        }
        UpdateOrderListText();
    }

    public void UpdateOrderListText()
    {
        if (orderListText != null)
        {
            orderListText.text = "Siparişler:\n";
            foreach (var orderItem in currentOrder)
            {
                orderListText.text += $"{orderItem.Key} x {orderItem.Value}\n";
            }
        }
        else
        {
            Debug.LogError("Order List Text is not assigned.");
        }
    }

    public bool CheckOrder(Dictionary<string, int> deliveredItems)
    {
        foreach (var orderItem in currentOrder)
        {
            if (!deliveredItems.ContainsKey(orderItem.Key) || deliveredItems[orderItem.Key] < orderItem.Value)
            {
                return false;
            }
        }
        return true;
    }

    public void HandleDelivery(bool isOrderCorrect)
    {
        if (isOrderCorrect)
        {
            Debug.Log("TESLİMAT DOĞRU");
            coinManager.AddCoins(10);
            wrongDeliveryCount = 0; // Yanlış teslimat sayacını sıfırla
        }
        else
        {
            Debug.Log("TESLİMAT YANLIŞ");
            coinManager.AddCoins(-10); // Yanlış teslimatta -10 coin
            wrongDeliveryCount++;
            if (wrongDeliveryCount >= 2)
            {
                GameOver();
            }
        }
    }

    public void GameOver()
    {
        // Oyun sonu işlemlerini burada yap
        gameOverText.text = "İFLAS ETTİN!";
        gameOverText.gameObject.SetActive(true);
        Debug.Log("Oyun Bitti!");
        // Fare imlecini görünür ve serbest yap
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // Belirli bir süre sonra ana menü sahnesine geçiş yap
        Invoke("LoadMainMenu", gameOverDelay);
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
