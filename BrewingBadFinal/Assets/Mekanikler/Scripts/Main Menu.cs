using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Oyun sahnesini y�kler
    public void PlayGame()
    {
        // Sahnelerin index numaras�na g�re y�kleme yapar, 1 numaral� sahneyi y�kler
        SceneManager.LoadSceneAsync(1);
    }

    // Uygulamay� kapat�r
    public void QuitGame()
    {
        // Uygulaman�n kapat�lmas�n� sa�lar. Bu kod edit�rde �al��maz, yaln�zca derlenmi� uygulamada etkilidir.
        Application.Quit();
    }

    // Ana men� sahnesine geri d�ner
    public void ReturnToMainMenu()
    {
        // "Main Menu" adl� sahneyi y�kler
        SceneManager.LoadSceneAsync("Main Menu");
    }
}
