using UnityEngine;

public class FinalSceneManager : MonoBehaviour
{
    public float exitDelay = 30f; // Oyunun kapanaca�� s�re (saniye cinsinden)

    void Start()
    {
        // Belirtilen s�re sonra QuitGame fonksiyonunu �a��r
        Invoke("QuitGame", exitDelay);
    }

    void QuitGame()
    {
        Debug.Log("Game is exiting after delay");
        Application.Quit();
    }
}
