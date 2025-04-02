using UnityEngine;
using TMPro;

public class HighscoreUpdater : MonoBehaviour
{
    TMP_Text highscoreText;
    int highscore = 0;
    void Start()
    {
        SetHighscore();
    }

    private void SetHighscore()
    {
        if (PlayerPrefs.HasKey("Highscore"))
            highscore = PlayerPrefs.GetInt("Highscore");
        highscoreText = GetComponent<TMP_Text>();
        highscoreText.SetText("Highscore: Wave " + highscore);
    }

    public void ResetHighscore()
    {
        PlayerPrefs.SetInt("Highscore", 0);
        PlayerPrefs.Save();
        SetHighscore();
    }
}
