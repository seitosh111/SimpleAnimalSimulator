using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimescaleRegulate : MonoBehaviour
{
    [SerializeField] private Scrollbar timeScaleScrollbar;
    public void ScrollBarValueChanged()
    {
        Time.timeScale = timeScaleScrollbar.value * 1000;
        timeScaleScrollbar.GetComponentInChildren<Text>().text = "TIME SCALE: " + (timeScaleScrollbar.value * 1000).ToString("0.0");
    }
}
