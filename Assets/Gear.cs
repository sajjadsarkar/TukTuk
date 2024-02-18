using UnityEngine;
using UnityEngine.UI;

public class Gear : MonoBehaviour
{
    public Scrollbar scrollbar;
    public Image image;
    public Sprite R;
    public Sprite D;
    public ButtonController buttonController;

    void Start()
    {
        
        if (scrollbar != null)
        {
            scrollbar.onValueChanged.AddListener(OnScrollbarValueChanged);
        }
        else
        {
            Debug.LogError("Scrollbar reference is not set!");
        }
    }

    void OnScrollbarValueChanged(float value)
    {
        //if the value is  0 or 1
        if (value != 0.0f && value != 1.0f)
        {
            //set it to the nearest valid value (0 or 1)
            scrollbar.value = Mathf.Round(value);
        }

        if(value == 1.0f)
        {
            image.sprite = R;
            buttonController.isForward = false;
        }
        if (value == 0f)
        {
            image.sprite = D;
            buttonController.isForward = true;
        }
    }
}
