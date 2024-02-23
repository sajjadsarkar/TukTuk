using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class ylimit : MonoBehaviour
{
    public RCC_UIController left;
    public RCC_UIController right;
    public float Miny = -90f;
    public float Maxy = 90f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerEluerAngel = gameObject.transform.rotation.eulerAngles;
        playerEluerAngel.y = (playerEluerAngel.y > 180) ? playerEluerAngel.y - 360 : playerEluerAngel.y;
        playerEluerAngel.y = Mathf.Clamp(playerEluerAngel.y, Miny, Maxy);

        /*gameObject.transform.rotation = Quaternion.Euler(playerEluerAngel);*/
        if (playerEluerAngel.y == Miny) {
            {
                left.pressing = false;
            }
        }
        if (playerEluerAngel.y == Maxy)
        {
            {
                right.pressing = false;
            }
        }
    }
}