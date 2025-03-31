using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerGlobal : MonoBehaviour
{
    public static SceneManagerGlobal Instance { get; private set; }

    [SerializeField] private CanvasGroup fadeCanvas; // Optionaler Fade-Canvas für Übergänge
    [SerializeField] private float fadeDuration = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Überlebt Szenenwechsel
        }
        else
        {
            Destroy(gameObject); // Verhindert doppelte Instanzen
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    public void ReloadCurrentScene()
    {
        StartCoroutine(LoadSceneAsync(SceneManager.GetActiveScene().name));
    }

    public void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            StartCoroutine(LoadSceneAsync(SceneManager.GetSceneByBuildIndex(nextSceneIndex).name));
        }
        else
        {
            Debug.Log("Keine weitere Szene verfügbar.");
        }
    }

    public void QuitGame()
    {
        Debug.Log("Spiel wird beendet.");
        Application.Quit();
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        if (fadeCanvas != null) yield return StartCoroutine(FadeToBlack());

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        if (fadeCanvas != null) yield return StartCoroutine(FadeFromBlack());
    }

    private IEnumerator FadeToBlack()
    {
        fadeCanvas.blocksRaycasts = true;
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.unscaledDeltaTime; // Fix für Editor-Ruckler
            fadeCanvas.alpha = Mathf.SmoothStep(0, 1, timer / fadeDuration);
            yield return null;
        }
        fadeCanvas.alpha = 1;
    }

    private IEnumerator FadeFromBlack()
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.unscaledDeltaTime;
            fadeCanvas.alpha = Mathf.SmoothStep(1, 0, timer / fadeDuration);
            yield return null;
        }
        fadeCanvas.alpha = 0;
        fadeCanvas.blocksRaycasts = false;
    }
}
