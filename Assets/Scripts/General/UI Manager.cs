using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI HighScoreText;
    public GameObject PauseMenu;
    public GameObject GameOverMenu;

    private void Start()
    {
        SetHighScoreText(GameManager.HighScore);
        HidePauseMenu();
        HideGameOverMenu();
    }

    public void SetScoreText(int score)
    {
        ScoreText.SetText(score + "m");
    }

    public void SetHighScoreText(int score)
    {
        HighScoreText.SetText("High Score: " + score + "m");
    }

    public void ShowPauseMenu()
    {
        PauseMenu.SetActive(true);
    }

    public void HidePauseMenu()
    {
        PauseMenu.SetActive(false);
    }

    public void ShowGameOverMenu()
    {
        GameOverMenu.SetActive(true);
    }

    public void HideGameOverMenu()
    {
        GameOverMenu.SetActive(false);
    }
}
