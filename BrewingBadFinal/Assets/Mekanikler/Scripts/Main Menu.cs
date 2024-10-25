using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Oyun sahnesini yükler
    public void PlayGame()
    {
        // Sahnelerin index numarasýna göre yükleme yapar, 1 numaralý sahneyi yükler
        SceneManager.LoadSceneAsync(1);
    }

    // Uygulamayý kapatýr
    public void QuitGame()
    {
        // Uygulamanýn kapatýlmasýný saðlar. Bu kod editörde çalýþmaz, yalnýzca derlenmiþ uygulamada etkilidir.
        Application.Quit();
    }

    // Ana menü sahnesine geri döner
    public void ReturnToMainMenu()
    {
        // "Main Menu" adlý sahneyi yükler
        SceneManager.LoadSceneAsync("Main Menu");
    }
}
