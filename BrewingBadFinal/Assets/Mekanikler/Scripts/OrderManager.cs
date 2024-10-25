using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class OrderManager : MonoBehaviour
{
    public TextMeshProUGUI orderListText; // TextMeshPro referans�
    private List<string> itemList = new List<string> { "Cola", "Pizza", "Hamburger" }; // Sipari� olu�turulacak ��eler
    public Dictionary<string, int> currentOrder = new Dictionary<string, int>();
    public TextMeshProUGUI gameOverText; // Oyun sonu mesaj� i�in TextMeshPro referans�
    public CoinManager coinManager; // CoinManager referans�
    public string mainMenuSceneName = "Main Menu"; // Ana men� sahnesinin ad�
    public float gameOverDelay = 3f; // Game over ekran�n�n g�sterilme s�resi

    private int wrongDeliveryCount = 0; // Yanl�� teslimat sayac�

    void Start()
    {
        GenerateNewOrder();
        gameOverText.gameObject.SetActive(false); // Ba�lang��ta oyun sonu mesaj�n� gizle
    }

    public void GenerateNewOrder()
    {
        currentOrder.Clear();
        // Rastgele sipari�ler olu�turun
        foreach (string item in itemList)
        {
            int quantity = Random.Range(1, 3); // 1 ile 2 aras�nda rastgele miktar
            currentOrder[item] = quantity;
        }
        UpdateOrderListText();
    }

    public void UpdateOrderListText()
    {
        if (orderListText != null)
        {
            orderListText.text = "Sipari�ler:\n";
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
            Debug.Log("TESL�MAT DO�RU");
            coinManager.AddCoins(10);
            wrongDeliveryCount = 0; // Yanl�� teslimat sayac�n� s�f�rla
        }
        else
        {
            Debug.Log("TESL�MAT YANLI�");
            coinManager.AddCoins(-10); // Yanl�� teslimatta -10 coin
            wrongDeliveryCount++;
            if (wrongDeliveryCount >= 2)
            {
                GameOver();
            }
        }
    }

    public void GameOver()
    {
        // Oyun sonu i�lemlerini burada yap
        gameOverText.text = "�FLAS ETT�N!";
        gameOverText.gameObject.SetActive(true);
        Debug.Log("Oyun Bitti!");
        // Fare imlecini g�r�n�r ve serbest yap
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // Belirli bir s�re sonra ana men� sahnesine ge�i� yap
        Invoke("LoadMainMenu", gameOverDelay);
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
