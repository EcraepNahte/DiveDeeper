using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public bool IsRotationInverted = false;

    public bool IsGamePaused;
    public bool IsGameOver;

    public List<Color> WallColors;
    public float MaxWallWidth;
    public float DepthForMaxWallWidth;

    public List<GameObject> EasyObjectSpawnPool;
    public ObjectSpawner ObjectSpawner;

    public UIManager UiManager;
    public GameObject MusicManager;

    private int Score;
    public static int HighScore;

    private void Awake()
    {
        HighScore = PlayerPrefs.GetInt("HighScore");
        IsRotationInverted = PlayerPrefs.GetInt("IsRotationInverted") == 1;
    }

    // Public Methods

    public void PlayGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }

    public void PauseGame()
    {
        UiManager.ShowPauseMenu();
        IsGamePaused = true;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        UiManager.HidePauseMenu();
        UiManager.HideGameOverMenu();
        IsGamePaused = false;
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        IsGameOver = false;
        ResumeGame();
        DontDestroyOnLoad(MusicManager);
        SceneManager.LoadScene("SampleScene");
    }

    public List<GameObject> GetSpawnPool()
    {
        //TODO if there are harder sections, return that spawn pool
        return EasyObjectSpawnPool;
    }

    public void SetScore(int score)
    {
        Score = score;
        if (UiManager != null)
        {
            UiManager.SetScoreText(Score);
        }
        ObjectSpawner.SpawnObject();
    }

    public void GameOver()
    {
        if (Score < HighScore)
        {
            Debug.Log("New High Score");
            HighScore = Score;

            PlayerPrefs.SetInt("HighScore", HighScore);
            PlayerPrefs.Save();
        }

        IsGameOver = true;
        Time.timeScale = 0;
        UiManager.ShowGameOverMenu();
    }

    public void NavigateToMainMenu()
    {
        ResumeGame();
        SceneManager.LoadScene("MainMenu");
    }

    public void InvertRotation()
    {
        IsRotationInverted = !IsRotationInverted;

        PlayerPrefs.SetInt("IsRotationInverted", IsRotationInverted ? 1 : 0);
        PlayerPrefs.Save();
    }
}
