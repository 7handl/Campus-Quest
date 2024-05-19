using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxProgress(int progress)
    {
        slider.maxValue = progress;
        slider.value = 0;
    }

    public void SetProgress(int progress)
    {
        slider.value = progress;
    }
}
