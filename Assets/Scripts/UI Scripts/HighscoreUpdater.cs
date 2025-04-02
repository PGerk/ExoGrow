using UnityEngine;
using TMPro;

public class HighscoreUpdater : MonoBehaviour
{
    TMP_Text highscoreText;
    int highscore = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerPrefs.HasKey("Highscore"))
        {
            highscore = PlayerPrefs.GetInt("Highscore");
            highscoreText = GetComponent<TMP_Text>();
            highscoreText.SetText("Highscore: Wave " + highscore);
        }        
    }
}
