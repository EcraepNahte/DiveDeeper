using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public List<Color> WallColors;
    public float MaxWallWidth;
    public float DepthForMaxWallWidth;

    public List<GameObject> EasyObjectSpawnPool;
    public ObjectSpawner ObjectSpawner;

    public UIManager UiManager;

    private int Score { get; set; }
    private int HighScore { get; set; }
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
        UiManager.UpdateScoreText(Score);
        ObjectSpawner.SpawnObject();
    }

    public void GameOver()
    {
        SceneManager.LoadScene(0);
    }
}
