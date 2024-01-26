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
    public Action<int> OnModified;
    [SerializeField] private Image image;
    [SerializeField] private Sprite lockedSprite;
    [SerializeField] private Selectable[] selectables;
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;
    private int selected = 0;

    private bool isLock = true;
    public bool IsLocked
    {
        get => isLock;
        set
        {
            isLock = value;
            if (value)
            {
                image.sprite = lockedSprite;
                previousButton.gameObject.SetActive(false);
                nextButton.gameObject.SetActive(false);
            }
            else
            {
                image.sprite = selectables[selected].sprite;
                previousButton.gameObject.SetActive(true);
                nextButton.gameObject.SetActive(true);
            }
        }
    }

    private void Start()
    {
        nextButton.onClick.AddListener(GoNext);
        previousButton.onClick.AddListener(GoPrevious);
    }

    public void SetCurrent(int current = 0)
    {
        selected = current;
        image.sprite = selectables[selected].sprite;
    }
    public void GoNext()
    {
        selected = (selected + 1) % selectables.Length;
        Debug.Log(gameObject.name + "set to "+selected);
        OnModified?.Invoke(selected);
        image.sprite = selectables[selected].sprite;
    }

    public void GoPrevious()
    {
        selected = selected - 1< 0?selectables.Length-1:selected-1;
        Debug.Log(gameObject.name + "set to "+selected);
        OnModified?.Invoke(selected);
        image.sprite = selectables[selected].sprite;
    }
}
