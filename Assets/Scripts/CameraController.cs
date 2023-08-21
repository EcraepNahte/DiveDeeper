using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed = 5;

    // Update is called once per frame
    void Update()
    {

        // Move the killer towards the player
        transform.Translate(Vector2.down * cameraSpeed * Time.deltaTime);
    }
}
