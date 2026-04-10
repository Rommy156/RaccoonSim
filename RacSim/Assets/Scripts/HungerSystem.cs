//Allen Adepoju
//000948096
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HungerSystem : MonoBehaviour
{
    public Slider hungerSlider;
    public float maxHunger = 100f;
    public float currentHunger;
    public float hungerDrainRate = 2f;
    public GameObject starvedText;

    [Header("Respawn")]
    public Transform respawnPoint;

    [Header("Death Fade UI")]
    public Image deathFade;
    public TextMeshProUGUI deathQuote;

    public float fadeToBlackTime = 1f;
    public float quoteFadeInTime = 0.6f;
    public float quoteHoldTime = 1.2f;
    public float quoteFadeOutTime = 0.6f;
    public float blackHoldTime = 0.3f;

    private bool isDead = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        starvedText.SetActive(false);

        if (deathFade != null)
        {
            Color fadeColor = deathFade.color;
            fadeColor.a = 0f;
            deathFade.color = fadeColor;
        }

        if (deathQuote != null)
        {
            Color quoteColor = deathQuote.color;
            quoteColor.a = 0f;
            deathQuote.color = quoteColor;
        }

        currentHunger = maxHunger;
        hungerSlider.maxValue = maxHunger;
        hungerSlider.value = currentHunger;
    }

    void Update()
    {
        if (isDead) return;

        DrainHunger();
        hungerSlider.value = currentHunger;

        if (currentHunger <= 0)
        {
            isDead = true;
            starvedText.SetActive(true);
            StartCoroutine(DeathSequence());
        }
    }

    void DrainHunger()
    {
        currentHunger -= hungerDrainRate * Time.deltaTime;
        currentHunger = Mathf.Clamp(currentHunger, 0f, maxHunger);

        if (currentHunger < 30)
        {
            hungerSlider.fillRect.GetComponent<Image>().color = Color.red;
        }
    }

    public void AddHunger(float amount)
    {
        currentHunger += amount;
        currentHunger = Mathf.Clamp(currentHunger, 0f, maxHunger);
    }

    void RespawnPlayer()
    {
        if (respawnPoint != null)
        {
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.position = respawnPoint.position;
                rb.rotation = respawnPoint.rotation;
            }
            else
            {
                transform.position = respawnPoint.position;
                transform.rotation = respawnPoint.rotation;
            }
        }

        currentHunger = maxHunger;
        hungerSlider.value = currentHunger;
        starvedText.SetActive(false);
        isDead = false;
    }

    IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(0.5f);

        if (deathFade != null)
            yield return StartCoroutine(FadeImage(deathFade, 0f, 1f, fadeToBlackTime));

        if (deathQuote != null)
            yield return StartCoroutine(FadeText(deathQuote, 0f, 1f, quoteFadeInTime));

        yield return new WaitForSeconds(quoteHoldTime);

        if (deathQuote != null)
            yield return StartCoroutine(FadeText(deathQuote, 1f, 0f, quoteFadeOutTime));

        yield return new WaitForSeconds(blackHoldTime);

        RespawnPlayer();

        if (deathFade != null)
            yield return StartCoroutine(FadeImage(deathFade, 1f, 0f, fadeToBlackTime));

        starvedText.SetActive(false);
    }

    IEnumerator FadeImage(Image image, float startAlpha, float endAlpha, float duration)
    {
        float time = 0f;
        Color color = image.color;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / duration);
            color.a = Mathf.Lerp(startAlpha, endAlpha, t);
            image.color = color;
            yield return null;
        }

        color.a = endAlpha;
        image.color = color;
    }

    IEnumerator FadeText(TextMeshProUGUI text, float startAlpha, float endAlpha, float duration)
    {
        float time = 0f;
        Color color = text.color;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / duration);
            color.a = Mathf.Lerp(startAlpha, endAlpha, t);
            text.color = color;
            yield return null;
        }

        color.a = endAlpha;
        text.color = color;
    }
}