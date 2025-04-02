using UnityEngine;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        SceneManagerGlobal sceneManager = FindFirstObjectByType<SceneManagerGlobal>();

        if (sceneManager != null)
        {
            button.onClick.RemoveAllListeners(); // Verhindert doppelte Events
            button.onClick.AddListener(() => sceneManager.LoadScene("Game Scene")); // Passe den Szenennamen an
        }
        else
        {
            Debug.LogError("SceneManagerGlobal nicht gefunden!");
        }
    }
}
