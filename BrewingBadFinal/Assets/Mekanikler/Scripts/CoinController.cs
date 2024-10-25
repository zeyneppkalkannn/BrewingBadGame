using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinController : MonoBehaviour
{
    public int targetCoinAmount; // Hedef coin miktarý
    public string nextSceneName; // Geçilecek sahne adý
    public CoinManager coinManager; // CoinManager referansý

    void Update()
    {
        // Coin sayýsý kontrolü
        if (coinManager.GetCoins() >= targetCoinAmount)
        {
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        // Sahne geçiþi yapýlmadan önce fare imlecini görünür ve serbest yap
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(nextSceneName);
    }
}
