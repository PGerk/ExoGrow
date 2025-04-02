using UnityEngine;
using UnityEngine.UI;

public class BackToMenuButton : MonoBehaviour
{
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        SceneManagerGlobal sceneManager = FindFirstObjectByType<SceneManagerGlobal>();

        if (sceneManager != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => sceneManager.LoadScene("Title Screen"));
        }
        else
        {
            Debug.LogError("SceneManagerGlobal nicht gefunden!");
        }
    }
}