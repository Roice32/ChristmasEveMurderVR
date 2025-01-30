using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public float delay = 48f; // Timpul total până la încărcarea scenei
    public string nextSceneName; // Numele scenei următoare
    public Image fadeImage; // Imaginea pentru efectul de fade (din Canvas)
    public float fadeDuration = 2f; // Durata efectului de fade (la început și la sfârșit)
    [SerializeField] AudioSource audioSource; // Sursa audio pentru efectul de fade

    private void Start()
    {
        // Asigură-te că imaginea începe complet transparentă
        if (fadeImage != null)
        {
            fadeImage.color = new Color(1, 1, 1, 0); // Alb complet transparent
        }

        // Fade-in la începutul scenei
        StartCoroutine(FadeIn());
        StartCoroutine(PlayAudio());

        // Fade-out care începe la secunda 43.5
        Invoke("StartFadeOut", 50.5f);

        // Încarcă scena după delay (45 secunde)
        Invoke("LoadNextScene", delay);
    }
    
    private IEnumerator PlayAudio()
    {
        yield return new WaitForSeconds(7f);
        audioSource.Play();
    }


    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);

            if (fadeImage != null)
            {
                fadeImage.color = new Color(1, 1, 1, alpha); // Crește Alpha de la 0 la 1
            }

            yield return null;
        }

        // Asigură-te că Alpha este complet la final
        if (fadeImage != null)
        {
            fadeImage.color = new Color(1, 1, 1, 1); // Alb complet opac
        }
    }

    private void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);

            if (fadeImage != null)
            {
                fadeImage.color = new Color(1, 1, 1, alpha); 
            }

            yield return null;
        }

        if (fadeImage != null)
        {
            fadeImage.color = new Color(1, 1, 1, 0);
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName); 
    }
}
