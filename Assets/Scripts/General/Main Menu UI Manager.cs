using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    public Toggle InvertRotationToggle;
    public GameObject Credits;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;

        InvertRotationToggle.isOn = gameManager.IsRotationInverted;
    }

    public void SetInvertRotationToggle()
    {
        InvertRotationToggle.isOn = gameManager.IsRotationInverted;
    }

    public void ToggleCredits()
    {
        Credits.SetActive(!Credits.activeInHierarchy);
    }
}
