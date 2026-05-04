using UnityEngine;
using System.Collections;

public class AudioFade : MonoBehaviour
{
    public AudioSource audioSource;
    public float fadeDuration = 2f;

    public void FadeOut()
    {
        StartCoroutine(FadeOutRoutine());
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInRoutine());
    }

    IEnumerator FadeOutRoutine()
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.volume = 0;
        audioSource.Stop();
    }

    IEnumerator FadeInRoutine()
    {
        audioSource.volume = 0;
        audioSource.Play();

        while (audioSource.volume < 0.1)
        {
            audioSource.volume += Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.volume = 0.1f;
    }
}