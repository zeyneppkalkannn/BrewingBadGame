using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class OrderManager2 : MonoBehaviour
{
    public TextMeshProUGUI orderListText; // TextMeshPro referansý
    private List<string> itemList = new List<string> { "PortakalSuyu", "Limonata", "Sandviç", "Kruvasan", "Donut" }; // Sipariþ oluþturulacak öðeler
    public Dictionary<string, int> currentOrder = new Dictionary<string, int>();
    public TextMeshProUGUI gameOverText; // Oyun sonu mesajý için TextMeshPro referansý
    public CoinManager coinManager; // CoinManager referansý
    public string mainMenuSceneName = "MainMenu"; // Ana menü sahnesinin adý
    public float gameOverDelay = 3f; // Game over ekranýnýn gösterilme süresi

    private int wrongDeliveryCount = 0; // Yanlýþ teslimat sayacý

    void Start()
    {
        GenerateNewOrder();
        gameOverText.gameObject.SetActive(false); // Baþlangýçta oyun sonu mesajýný gizle
    }

    public void GenerateNewOrder()
    {
        currentOrder.Clear();
        int numberOfItems = Random.Range(1, 5); // 1 ile 4 arasýnda rastgele çeþit sayýsý

        List<string> selectedItems = new List<string>(itemList);
        for (int i = 0; i < numberOfItems; i++)
        {
            string selectedItem = selectedItems[Random.Range(0, selectedItems.Count)];
            selectedItems.Remove(selectedItem); // Seçilen öðeyi listeden kaldýrarak tekrar seçilmesini önle
            int quantity = Random.Range(1, 4); // 1 ile 3 arasýnda rastgele miktar
            currentOrder[selectedItem] = quantity;
        }
        UpdateOrderListText();
    }

    public void UpdateOrderListText()
    {
        if (orderListText != null)
        {
            orderListText.text = "Sipariþler:\n";
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
            Debug.Log("TESLÝMAT DOÐRU");
            coinManager.AddCoins(10);
            wrongDeliveryCount = 0; // Yanlýþ teslimat sayacýný sýfýrla
        }
        else
        {
            Debug.Log("TESLÝMAT YANLIÞ");
            coinManager.AddCoins(-10); // Yanlýþ teslimatta -10 coin
            wrongDeliveryCount++;
            if (wrongDeliveryCount >= 2)
            {
                GameOver();
            }
        }
    }

    public void GameOver()
    {
        // Oyun sonu iþlemlerini burada yap
        gameOverText.text = "ÝFLAS ETTÝN!";
        gameOverText.gameObject.SetActive(true);
        Debug.Log("Oyun Bitti!");
        // Fare imlecini görünür ve serbest yap
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // Belirli bir süre sonra ana menü sahnesine geçiþ yap
        Invoke("LoadMainMenu", gameOverDelay);
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
