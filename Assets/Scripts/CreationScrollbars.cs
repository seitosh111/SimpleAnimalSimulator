using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreationScrollbars : MonoBehaviour
{
    [SerializeField] private GameplayScript gameplayScript;
    [SerializeField] private Scrollbar Nscrollbar;
    [SerializeField] private Scrollbar Mscrollbar;
    [SerializeField] private Scrollbar Vscrollbar;

    private void Start()
    {
        ScrollbarChanged();
    }
    public void ScrollbarChanged()
    {
        gameplayScript.N = Mathf.Clamp(Nscrollbar.value * 1000, 2, 1000);
        Nscrollbar.GetComponentInChildren<Text>().text = "N: " + gameplayScript.N.ToString("0.0");

        gameplayScript.M = (int)Mathf.Clamp(Mscrollbar.value * gameplayScript.N * gameplayScript.N / 2, 1, gameplayScript.N * gameplayScript.N / 2);
        Mscrollbar.GetComponentInChildren<Text>().text = "M: " + gameplayScript.M;

        gameplayScript.V = Mathf.Clamp(Vscrollbar.value * 100, 1, 100);
        Vscrollbar.GetComponentInChildren<Text>().text = "V: " + gameplayScript.V.ToString("0.0");
    }
}
