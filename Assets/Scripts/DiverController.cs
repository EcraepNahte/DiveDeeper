using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiverController : MonoBehaviour
{
    public float kickForce = 2f;

    private Rigidbody2D rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Kick"))
        {
            Debug.Log("Kick");
            rigidBody.AddForceY(-kickForce, ForceMode2D.Impulse);
        }

        Vector3 worldBottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        if (transform.position.y <= worldBottom.y)
        {
            rigidBody.MovePosition(new Vector2(transform.position.x, worldBottom.y));
        }
    }
}
