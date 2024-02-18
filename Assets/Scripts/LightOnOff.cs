using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightOnOff : MonoBehaviour
{
    public Sprite onSprite;
    public Sprite offSprite;

    private bool isOn = false;
    private Image image;
    private void Start()
    {
        image = GetComponent<Image>();
        UpdateSprite();
    }
    public void Toggle()
    {
        isOn = !isOn;
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        image.sprite = isOn ? onSprite : offSprite;
    }
}
