using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public bool InvertRotation = false;

    public List<Color> WallColors;
    public float MaxWallWidth;
    public float DepthForMaxWallWidth;

    public List<GameObject> EasyObjectSpawnPool;
    public ObjectSpawner ObjectSpawner;

    public UIManager UiManager;
    public GameObject MusicManager;

    private int Score;
    public static int HighScore;
    private int GoldForRun { get; set; }
    private int GoldInWallet { get; set; }

    public List<GameObject> GetSpawnPool()
    {
        //TODO if there are harder sections, return that spawn pool
        return EasyObjectSpawnPool;
    }

    public void SetScore(int score)
    {
        Score = score;
        UiManager.SetScoreText(Score);
        ObjectSpawner.SpawnObject();
    }

    public void GameOver()
    {
        if (Score < HighScore)
        {
            Debug.Log("New High Score");
            HighScore = Score;
        }

        DontDestroyOnLoad(MusicManager);
        SceneManager.LoadScene(0);
        UiManager.SetHighScoreText(HighScore);
    }
}
