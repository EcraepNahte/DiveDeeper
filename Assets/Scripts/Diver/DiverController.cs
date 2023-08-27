using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiverController : MonoBehaviour
{
    public int MaxAirLevel = 100;
    public HealthBar AirLevelBar;
    public int AirLostPerKick = 5;
    public AudioClip KickSound;

    public float KickForce = 2f;
    public float RotationalForce = 3.5f;
    public float RotationAngle = 0;

    private int airLevel = 100;
    private bool kickInput = false;
    private float rotationInput = 0;

    private Rigidbody2D rigidBody;
    private AudioSource audioSource;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        rigidBody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pickup"))
        {
            collision.gameObject.GetComponent<Pickup>().TriggerPickup(this);
        }
    }

    // Public methods

    public void SetInputs(bool kickInput, float rotationInput)
    {
        // Has to be or because we want to turn off kick indicator
        this.kickInput = this.kickInput || kickInput;
        this.rotationInput = rotationInput;
    }

    public void AddAir(int airAdded)
    {
        airLevel = Mathf.Clamp(airLevel + airAdded, 0, MaxAirLevel);
    }

    // Private methods

    private void Kick()
    {
        if (kickInput)
        {
            //reset kick input so we don't call again until we get a new true kick result
            kickInput = false;
            Vector2 forceVector = transform.up * KickForce * -1;

            rigidBody.AddForce(forceVector, ForceMode2D.Impulse);
            audioSource.PlayOneShot(KickSound);

            airLevel -= AirLostPerKick;
            if (airLevel <= 0)
            {
                gameManager.GameOver();
            }
            AirLevelBar.SetAirLevel(airLevel);
        }
    }

    private void Rotate()
    {
        RotationAngle -= (gameManager.InvertRotation ? rotationInput : -rotationInput) * RotationalForce;

        rigidBody.MoveRotation(RotationAngle);
    }
}
