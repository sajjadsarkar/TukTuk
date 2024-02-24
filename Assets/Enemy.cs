using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f; // Adjust this to change the speed
    public float minXLimit = -5f; // Minimum x-axis limit
    public float maxXLimit = 5f; // Maximum x-axis limit

    private bool movingRight = true;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("gameEnd");
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the enemy forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Move the enemy left and right within the specified x-axis limits
        if (transform.position.x <= minXLimit)
        {
            movingRight = true;
        }
        else if (transform.position.x >= maxXLimit)
        {
            movingRight = false;
        }

        if (movingRight)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
}
