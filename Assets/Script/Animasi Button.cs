using UnityEngine;

public class ButtonScaleAnimation : MonoBehaviour
{
    public float scaleUpDuration = 0.5f; // Durasi animasi scale up
    public float scaleDownDuration = 0.3f; // Durasi animasi scale down

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;

        // Animasi muncul dengan scale up
        transform.localScale = Vector3.zero; // Mulai dari skala 0
        LeanTween.scale(gameObject, originalScale, scaleUpDuration).setEaseOutBack();
    }

    public void OnButtonPressed()
    {
        // Animasi menghilang dengan scale down
        LeanTween.scale(gameObject, Vector3.zero, scaleDownDuration)
            .setEaseInBack()
            .setOnComplete(() =>
            {
                // Setelah animasi selesai, nonaktifkan tombol
                gameObject.SetActive(false);
            });
    }
}
