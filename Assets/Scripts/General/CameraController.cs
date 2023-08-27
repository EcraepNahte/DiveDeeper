using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraStartSpeed = 1;
    public float cameraMaxSpeed = 5;

    private float cameraSpeed;
    private GameManager gameManager;
    private int score;

    private void Start()
    {
        gameManager = GameManager.Instance;
        cameraSpeed = cameraMaxSpeed;
    }

    void Update()
    {
        cameraSpeed = cameraStartSpeed + (Mathf.Clamp(transform.position.y / gameManager.DepthForMaxWallWidth, 0, 1) * (cameraMaxSpeed - cameraStartSpeed));
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
