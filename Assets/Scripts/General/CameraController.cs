using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed = 5;

    private GameManager gameManager;
    private int score;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        // Move the camera down
        transform.Translate(Vector2.down * cameraSpeed * Time.deltaTime);

        int newScore = (int)transform.position.y;
        
        if (score != newScore)
        {
            score = newScore;
            gameManager.SetScore(score);
        }
    }
}
