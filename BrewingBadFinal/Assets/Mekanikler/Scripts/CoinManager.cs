using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    private int coins = 0;

    void Start()
    {
        UpdateCoinText();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateCoinText();
    }

    void UpdateCoinText()
    {
        coinText.text = "Coins: " + coins.ToString();
    }

    public int GetCoins()
    {
        return coins;
    }
}
