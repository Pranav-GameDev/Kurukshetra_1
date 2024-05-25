using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsScript : MonoBehaviour
{

    public Transform Box;
    public CanvasGroup background;
    public void OnEnable()
    {
        background.alpha = 0;
        background.LeanAlpha(1, 0.5f);

        Box.localPosition = new Vector3(0, -900);
        Box.LeanMoveY(490, 0.5f).setEaseOutExpo().delay = 0.1f;
    }

    public void CloseDialog()
    {
        background.LeanAlpha(1, 0.5f);
        Box.LeanMoveY(-Screen.height, 0.5f).setEaseOutExpo();
    }
}
