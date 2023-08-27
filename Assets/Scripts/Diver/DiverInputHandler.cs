using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiverInputHandler : MonoBehaviour
{
    private DiverController diverController;

    private void Awake()
    {
        diverController = GetComponent<DiverController>();
    }

    // Update is called once per frame
    void Update()
    {
        diverController.SetInputs(Input.GetButtonDown("Kick"), Input.GetAxis("Horizontal"), Input.GetButtonDown("Pause"));
    }
}
