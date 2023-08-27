using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider Slider;

    public void SetMaxAirLevel(int maxAirLevel)
    {
        Slider.maxValue = maxAirLevel;
    }

    public void SetAirLevel(int airLevel)
    {
        Slider.value = airLevel;
    }
}
