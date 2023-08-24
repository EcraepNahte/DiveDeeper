using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiverController : MonoBehaviour
{
    public float KickForce = 2f;
    public float RotationalForce = 3.5f;
    public float RotationAngle = 0;

    private bool kickInput = false;
    private float rotationInput = 0;

    private Rigidbody2D rigidBody;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Kick();

        Rotate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DamageObject"))
        {
            gameManager.GameOver();
        }
    }

    public void SetInputs(bool kickInput, float rotationInput)
    {
        // Has to be or because we want to turn off kick indicator
        this.kickInput = this.kickInput || kickInput;
        this.rotationInput = rotationInput;
    }

    private void Kick()
    {
        if (kickInput)
        {
            //reset kick input so we don't call again until we get a new true kick result
            kickInput = false;
            Vector2 forceVector = transform.up * KickForce * -1;

            rigidBody.AddForce(forceVector, ForceMode2D.Impulse);
        }
    }

    private void Rotate()
    {
        RotationAngle -= rotationInput * RotationalForce;

        rigidBody.MoveRotation(RotationAngle);
    }
}
