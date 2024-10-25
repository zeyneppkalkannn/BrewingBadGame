using UnityEngine;

public class FinalSceneManager : MonoBehaviour
{
    public float exitDelay = 30f; // Oyunun kapanacaðý süre (saniye cinsinden)

    void Start()
    {
        // Belirtilen süre sonra QuitGame fonksiyonunu çaðýr
        Invoke("QuitGame", exitDelay);
    }

    void QuitGame()
    {
        Debug.Log("Game is exiting after delay");
        Application.Quit();
    }
}
