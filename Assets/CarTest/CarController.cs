using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Car Settings")]
    public float driftFactor = .95f;
    public float accellerationFactor = 30.0f;
    public float turnFactor = 3.5f;

    private float accelerationInput = 0.0f;
    private float steeringInput = 0.0f;

    private float rotationAngle = 0.0f;

    private Rigidbody2D rb;

    // unity functions
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ApplyEngineForce();

        ApplySteering();
    }

    // public
    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }

    // private
    private void ApplyEngineForce()
    {
        // Create force for the engine
        Vector2 engineForceVector = transform.up * accelerationInput * accellerationFactor;

        rb.AddForce(engineForceVector, ForceMode2D.Force);
    }

    private void ApplySteering()
    {
        rotationAngle -= steeringInput * turnFactor;

        rb.MoveRotation(rotationAngle);
    }
}
