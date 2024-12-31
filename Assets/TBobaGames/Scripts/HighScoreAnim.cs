using UnityEngine;
using TMPro;
using System.Collections;

public class HighScoreAnim : MonoBehaviour
{
    [SerializeField] private TMP_Text newHighScoreText;
    [SerializeField] private float maxScale = 1.0f;
    [SerializeField] private float rainbowDuration = 4f;

    private bool hasPlay = false;

    private void Start()
    {
        // Inicialmente, ocultar el texto o establecer su escala a 0
        
        newHighScoreText.gameObject.SetActive(false);

        // Suscribirse al evento de High Score Changed
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnHighScoreChanged += AnimateNewHighScore;
        }
    }

    private void OnDestroy()
    {
        // Desuscribirse del evento para evitar errores al destruir el objeto
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnHighScoreChanged -= AnimateNewHighScore;
        }
    }

    public void AnimateNewHighScore(int newHighScore)
    {
        newHighScoreText.gameObject.SetActive(true);
        if (!hasPlay)
        {
            AudioManager.Instance.PlayCelebrationSFX();
            hasPlay = true;
            StartCoroutine(StartAnim());
            StartCoroutine(RainbowText());
        }

    }

    public IEnumerator StartAnim()
    {
        GameObject newText = newHighScoreText.gameObject;
        LeanTween.scale(newText, new Vector3(maxScale, maxScale, maxScale), 0.5f)
            .setEase(LeanTweenType.easeInOutBack);
        yield return new WaitForSeconds(0.5f);

        LeanTween.scale(newText, new Vector3(0.5f, 0.5f, 0.5f), 0.5f)
            .setEase(LeanTweenType.easeInOutBack);
        yield return new WaitForSeconds(0.5f);

        LeanTween.scale(newText, new Vector3(maxScale, maxScale, maxScale), 0.5f)
            .setEase(LeanTweenType.easeInOutBack);
        yield return new WaitForSeconds(2f);

        LeanTween.scale(newText, Vector3.zero, 0.5f);
    }

    private IEnumerator RainbowText()
    {
        Color[] rainbowColors = {
            Color.red,
            Color.yellow,
            Color.green,
            Color.cyan,
            Color.blue,
            Color.magenta
        };

        float cycleDuration = rainbowDuration; // Duración total del ciclo de arcoíris
        float elapsedTime = 0f;

        while (elapsedTime < cycleDuration)
        {
            float lerpT = Mathf.PingPong(elapsedTime / (cycleDuration / rainbowColors.Length), rainbowColors.Length - 1);
            int colorIndex = Mathf.FloorToInt(lerpT);
            Color color1 = rainbowColors[colorIndex];
            Color color2 = rainbowColors[Mathf.Clamp(colorIndex + 1, 0, rainbowColors.Length - 1)];
            Color rainbowColor = Color.Lerp(color1, color2, lerpT - colorIndex);
            newHighScoreText.color = rainbowColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

       
    }

}
