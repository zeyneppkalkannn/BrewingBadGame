using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinController : MonoBehaviour
{
    public int targetCoinAmount; // Hedef coin miktar�
    public string nextSceneName; // Ge�ilecek sahne ad�
    public CoinManager coinManager; // CoinManager referans�

    void Update()
    {
        // Coin say�s� kontrol�
        if (coinManager.GetCoins() >= targetCoinAmount)
        {
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        // Sahne ge�i�i yap�lmadan �nce fare imlecini g�r�n�r ve serbest yap
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(nextSceneName);
    }
}
