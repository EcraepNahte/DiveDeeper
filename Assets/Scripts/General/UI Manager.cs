using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;

    public void UpdateScoreText(int score)
    {
        ScoreText.SetText(score + "m");
    }
}
