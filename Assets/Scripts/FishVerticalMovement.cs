using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishVerticalMovement : MonoBehaviour
{
    [Header("Movement Bounds")]
    public float minY = 2f;
    public float maxY = 10f;

    [Header("Movement Settings")]
    public float speed = 2f;

    private bool movingUp = true;

    void Update()
    {
        MoveVertically();
    }

    void MoveVertically()
    {
        float newY;

        if (movingUp)
        {
            newY = transform.position.y + speed * Time.deltaTime;

            if (newY >= maxY)
            {
                newY = maxY;
                movingUp = false;
            }
        }
        else
        {
            newY = transform.position.y - speed * Time.deltaTime;

            if (newY <= minY)
            {
                newY = minY;
                movingUp = true;
            }
        }

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
