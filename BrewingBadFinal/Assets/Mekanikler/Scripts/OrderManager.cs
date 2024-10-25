using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class OrderManager : MonoBehaviour
{
    public TextMeshProUGUI orderListText; // TextMeshPro referansý
    private List<string> itemList = new List<string> { "Cola", "Pizza", "Hamburger" }; // Sipariþ oluþturulacak öðeler
    public Dictionary<string, int> currentOrder = new Dictionary<string, int>();
    public TextMeshProUGUI gameOverText; // Oyun sonu mesajý için TextMeshPro referansý
    public CoinManager coinManager; // CoinManager referansý
    public string mainMenuSceneName = "Main Menu"; // Ana menü sahnesinin adý
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
        // Rastgele sipariþler oluþturun
        foreach (string item in itemList)
        {
            int quantity = Random.Range(1, 3); // 1 ile 2 arasýnda rastgele miktar
            currentOrder[item] = quantity;
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
