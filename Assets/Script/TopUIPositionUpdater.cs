using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class TopUIPositionUpdater : MonoBehaviour
{
    private RectTransform rect;
    private bool isPortrait = true;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        if (Screen.height > Screen.width && !isPortrait)
        {
            isPortrait = true;
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x,-250);

        }

        if (Screen.height <= Screen.width && isPortrait)
        {
            isPortrait = false;
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x,-100);
        }
    }

    void Update()
    {
        if (Screen.height > Screen.width && !isPortrait)
        {
            isPortrait = true;
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x,-250);

        }

        if (Screen.height <= Screen.width && isPortrait)
        {
            isPortrait = false;
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x,0);
        }
    }
}
