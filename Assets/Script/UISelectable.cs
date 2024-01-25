using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public struct Selectable
{
    public string label;
    public Sprite sprite;
}
public class UISelectable : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite lockedSprite;
    [SerializeField] private Selectable[] selectables;
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;
    private int selected = 0;
    public bool IsLocked { get; set; } = true;
    void Start()
    {
        if (IsLocked)
        {
            image.sprite = lockedSprite;
            previousButton.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(false);
        }
        else
        {
            image.sprite = selectables[selected].sprite;
        }
    }

    public void GoNext()
    {
        selected = (selected + 1) % selectables.Length;
    }

    public void GoPrevious()
    {
        selected = selected - 1< 0?selectables.Length-1:selected-1;
    }
}
