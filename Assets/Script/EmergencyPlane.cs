using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    [Header("Fade Settings")]
    public Image fadeImage; // Image overlay untuk efek fade
    public float fadeDuration = 1f; // Durasi fade-in dan fade-out
    public Button button; // Button untuk trigger
    public GameObject ButtonOxygen; // Button Untuk Masker Oxygen
    public GameObject ParentbtnOxygent; 
    public GameObject Oxygen;
    public GameObject Buttonpelampung; // Button Untuk Pelampung
    public ParticleSystem particleSystem1; // Particle System pertama
    public ParticleSystem particleSystem2; // Particle System kedua
    public AudioSource audioSource1; // Audio pertama
    public AudioSource audioSource2; // Audio kedua
    public AudioSource audioSource3; // Audio ketiga  
    public AudioSource audioSource4; // Audio keempat
    public GameObject Bandara; // 3D Object Untuk bandara
    public GameObject Jetway; // 3D Object Untuk Jetway
    public GameObject Air; // Air

    private bool oxygenButtonPressed = false; // Flag untuk tombol Oxygen

    private void Start()
    {
        button.onClick.AddListener(OnButtonPressed); // Listener untuk tombol utama
        ButtonOxygen.GetComponent<Button>().onClick.AddListener(OnOxygenButtonPressed); // Listener untuk tombol Oxygen
    }

    private void OnButtonPressed()
    {
        button.gameObject.SetActive(false);
        StartCoroutine(FadeAndActivateEffects()); // Menjalankan coroutine saat tombol ditekan
    }

    private void OnOxygenButtonPressed()
    {
        oxygenButtonPressed = true; // Set flag menjadi true saat tombol ditekan
        ButtonOxygen.SetActive(false); // Menyembunyikan tombol setelah ditekan
    }

    private IEnumerator FadeAndActivateEffects()
    {
        // Menunggu 5 detik sebelum fade-in
        yield return new WaitForSeconds(5f);

        // Fade out
        yield return StartCoroutine(FadeOut());

        Bandara.SetActive(false);
        Jetway.SetActive(false);

        yield return StartCoroutine(FadeIn());

        // Mengaktifkan Particle System dan Audio secara berurutan
        particleSystem1.Play();
        particleSystem2.Play();
        audioSource1.Play();

        // Menunggu sampai audio pertama selesai sebelum memulai audio kedua
        yield return new WaitForSeconds(audioSource1.clip.length);

        audioSource2.Play();

        yield return new WaitForSeconds(audioSource2.clip.length);

        audioSource3.Play();
        ParentbtnOxygent.SetActive(true); 
        ButtonOxygen.SetActive(true);

        // Tunggu hingga tombol Oxygen ditekan
        yield return new WaitUntil(() => oxygenButtonPressed);

        // Reset flag
        oxygenButtonPressed = false;

        yield return new WaitForSeconds(5f);

        yield return StartCoroutine(FadeOut());
        Oxygen.SetActive(false);
        ParentbtnOxygent.SetActive(false);
        yield return StartCoroutine(FadeIn());

        Air.SetActive(true);

        yield return new WaitForSeconds(1f);

        audioSource4.Play();
        Buttonpelampung.SetActive(true);
    }

    public IEnumerator FadeIn()
    {
        if (fadeImage == null) yield break;

        Color color = fadeImage.color;
        float startAlpha = color.a;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0f, time / fadeDuration); // Fade to transparent
            color.a = alpha;
            fadeImage.color = color;
            yield return null;
        }

        // Pastikan alpha sesuai dengan 0 di akhir
        color.a = 0f;
        fadeImage.color = color;
    }

    public IEnumerator FadeOut()
    {
        if (fadeImage == null) yield break;

        Color color = fadeImage.color;
        float startAlpha = color.a;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 1f, time / fadeDuration); // Fade to black
            color.a = alpha;
            fadeImage.color = color;
            yield return null;
        }

        // Pastikan alpha sesuai dengan 1 di akhir
        color.a = 1f;
        fadeImage.color = color;
    }
}
