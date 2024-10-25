using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] public float remainingTime = 60f; // Baþlangýçta 60 saniye olarak ayarlandý
    private float initialTime = 60f; // Süreyi sýfýrlamak için baþlangýç zamaný

    private bool isCounting = true; // Geri sayýmýn devam edip etmediðini izlemek için bir deðiþken

    void Start()
    {
        // Timer'ý oyun baþladýðýnda çalýþtýr
        UpdateTimerText();
    }

    void Update()
    {
        // Geri sayým devam ediyorsa zamaný güncelle
        if (isCounting && remainingTime > 0)
        {
            remainingTime -= Time.deltaTime; // Zamaný azalt 
            UpdateTimerText(); // Zaman deðiþtiðinde metni güncelle
        }

        // Zaman dolduðunda veya sýfýra indiðinde iþlem yap
        if (remainingTime <= 0)
        {
            remainingTime = 0; // Zamaný sýfýrla
            isCounting = false; // Geri sayýmý durdur
            // Timer'ýn baðlý olduðu OrderManager'a kaybetme mesajý gönder
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
        // Metni güncelle
        int minutes = Mathf.FloorToInt(remainingTime / 60); // Dakikalarý hesapla
        int seconds = Mathf.FloorToInt(remainingTime % 60); // Saniyeleri hesapla
        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds); // Metni formatla

        // Zaman sýfýr ise, metni "00:00" olarak güncelle
        if (remainingTime <= 0)
        {
            timerString = "00:00";
        }

        timerText.text = timerString; // Metni güncelle

        // Metnin konumunu ve boyutunu ayarla
        RectTransform rectTransform = timerText.rectTransform;
        rectTransform.anchorMin = new Vector2(0f, 1f); // Sol üst köþe
        rectTransform.anchorMax = new Vector2(0f, 1f); // Sol üst köþe
        rectTransform.pivot = new Vector2(0f, 1f); // Sol üst köþe
        rectTransform.anchoredPosition = new Vector2(20, -20); // Daha fazla sola ve aþaðýya

        // Metnin boyutunu büyüt
        timerText.fontSize = 70; // Boyutunu 70 olarak ayarla

        // Metnin rengini kýrmýzý yap
        timerText.color = Color.red; // Rengi kýrmýzý yap
    }

    public void ResetTimer()
    {
        remainingTime = initialTime; // Süreyi sýfýrla
        isCounting = true; // Geri sayýmý yeniden baþlat
        UpdateTimerText(); // Metni güncelle
    }
}
