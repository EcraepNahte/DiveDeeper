using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI HighScoreText;

    private void Start()
    {
        SetHighScoreText(GameManager.HighScore);
    }

    public void SetScoreText(int score)
    {
        ScoreText.SetText(score + "m");
    }

    public void SetHighScoreText(int score)
    {
        HighScoreText.SetText("High Score: " + score + "m");
    }
}
