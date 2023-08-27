using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyfishMover : MonoBehaviour
{
    public float jellyfishSpeed = 2f;

    private void FixedUpdate()
    {
        transform.Translate(Vector2.up * jellyfishSpeed * Time.fixedDeltaTime);
    }
}
