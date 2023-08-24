using UnityEngine;

public class DiverSpriteController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    private DiverController diverController;

    private bool isSpriteFlipped;

    private void Awake()
    {
        diverController = GetComponent<DiverController>();
    }

    private void Update()
    {
        if (diverController.RotationAngle > 0)
        {
            isSpriteFlipped = ((int)diverController.RotationAngle / 180) % 2 == 0;
        }
        else
        {
            isSpriteFlipped = ((int)Mathf.Abs(diverController.RotationAngle) / 180) % 2 == 1;
        }

        spriteRenderer.flipY = isSpriteFlipped;
    }
}
