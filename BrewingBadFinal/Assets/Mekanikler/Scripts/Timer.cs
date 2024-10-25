using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] public float remainingTime = 60f; // Ba�lang��ta 60 saniye olarak ayarland�
    private float initialTime = 60f; // S�reyi s�f�rlamak i�in ba�lang�� zaman�

    private bool isCounting = true; // Geri say�m�n devam edip etmedi�ini izlemek i�in bir de�i�ken

    void Start()
    {
        // Timer'� oyun ba�lad���nda �al��t�r
        UpdateTimerText();
    }

    void Update()
    {
        // Geri say�m devam ediyorsa zaman� g�ncelle
        if (isCounting && remainingTime > 0)
        {
            remainingTime -= Time.deltaTime; // Zaman� azalt 
            UpdateTimerText(); // Zaman de�i�ti�inde metni g�ncelle
        }

        // Zaman doldu�unda veya s�f�ra indi�inde i�lem yap
        if (remainingTime <= 0)
        {
            remainingTime = 0; // Zaman� s�f�rla
            isCounting = false; // Geri say�m� durdur
            // Timer'�n ba�l� oldu�u OrderManager'a kaybetme mesaj� g�nder
            OrderManager orderManager = FindObjectOfType<OrderManager>();
            if (orderManager != null)
            {
                orderManager.GameOver();
            }
            else
            {
                Debug.LogError("OrderManager component is missing or not attached to any object in the scene.");
            }
        }
    }

    void UpdateTimerText()
    {
        // Metni g�ncelle
        int minutes = Mathf.FloorToInt(remainingTime / 60); // Dakikalar� hesapla
        int seconds = Mathf.FloorToInt(remainingTime % 60); // Saniyeleri hesapla
        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds); // Metni formatla

        // Zaman s�f�r ise, metni "00:00" olarak g�ncelle
        if (remainingTime <= 0)
        {
            timerString = "00:00";
        }

        timerText.text = timerString; // Metni g�ncelle

        // Metnin konumunu ve boyutunu ayarla
        RectTransform rectTransform = timerText.rectTransform;
        rectTransform.anchorMin = new Vector2(0f, 1f); // Sol �st k��e
        rectTransform.anchorMax = new Vector2(0f, 1f); // Sol �st k��e
        rectTransform.pivot = new Vector2(0f, 1f); // Sol �st k��e
        rectTransform.anchoredPosition = new Vector2(20, -20); // Daha fazla sola ve a�a��ya

        // Metnin boyutunu b�y�t
        timerText.fontSize = 70; // Boyutunu 70 olarak ayarla

        // Metnin rengini k�rm�z� yap
        timerText.color = Color.red; // Rengi k�rm�z� yap
    }

    public void ResetTimer()
    {
        remainingTime = initialTime; // S�reyi s�f�rla
        isCounting = true; // Geri say�m� yeniden ba�lat
        UpdateTimerText(); // Metni g�ncelle
    }
}
