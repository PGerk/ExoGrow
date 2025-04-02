using UnityEngine;
using UnityEngine.UI;

public class QuitGameButton : MonoBehaviour
{
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        SceneManagerGlobal sceneManager = FindFirstObjectByType<SceneManagerGlobal>();

        if (sceneManager != null)
        {
            button.onClick.RemoveAllListeners(); // Verhindert doppelte Events
            button.onClick.AddListener(() => sceneManager.QuitGame()); // Passe den Szenennamen an
        }
        else
        {
            Debug.LogError("SceneManagerGlobal nicht gefunden!");
        }
    }
}
