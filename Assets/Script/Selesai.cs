using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Selesai : MonoBehaviour
{
    public Image fadeImage; // Image overlay untuk efek fade
    public float fadeDuration = 1f; // Durasi fade-out

    public void Fading()
    {
        // Mulai proses fade-out
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        if (fadeImage == null) yield break;

        Color color = fadeImage.color;
        float startAlpha = color.a;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 1f, time / fadeDuration); // Fade to black (alpha = 1)
            color.a = alpha;
            fadeImage.color = color;
            yield return null;
        }

        // Pastikan alpha sesuai dengan 1 di akhir
        color.a = 1f;
        fadeImage.color = color;

        // Tambahkan log jika ingin memverifikasi bahwa fade-out selesai
        Debug.Log("Fade-out selesai, alpha tetap di 1.");
    }
}
