using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public float speed = 5f; // Adjust this to change the speed
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("gameEnd");
        Time.timeScale = 0f;
    }
   

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
