using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class VerticalLayoutUpdate : MonoBehaviour
{
    private VerticalLayoutGroup layoutGroup;

    private bool isPortrait = false;
    private void Start()
    {
        layoutGroup = GetComponent<VerticalLayoutGroup>();
    }

    void Update()
    {
        if (Screen.height > Screen.width && !isPortrait)
        {
            isPortrait = true;
            layoutGroup.spacing = -50;
        }

        if (Screen.height <= Screen.width && isPortrait)
        {
            isPortrait = false;
            layoutGroup.spacing = -50;
        }
    }
}
