using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class HookController : MonoBehaviour
{
    public float horizontalSpeed = 5f;
    public float sinkSpeed = 400f;

    private Rigidbody2D rb;

    private bool triggered = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Hook automatically starts sinking
        rb.velocity = new Vector2(0, -sinkSpeed);

    }

    void Update()
    {
        float moveX = 0f;
        float moveY = -sinkSpeed;

        if (Input.GetKey(KeyCode.A))
            moveX = -horizontalSpeed;
        else if (Input.GetKey(KeyCode.D))
            moveX = horizontalSpeed;

        if (Input.GetKey(KeyCode.S)) {
            moveY = -sinkSpeed * 2f;
        }
        else if (Input.GetKey(KeyCode.W)) {
            moveY = -sinkSpeed * 0.5f;
        }

        rb.velocity = new Vector2(moveX, moveY);

    }



    private void OnTriggerEnter2D(Collider2D other)
    {

        if ((other.gameObject.CompareTag("Fish") || other.gameObject.CompareTag("Bottom")) && !triggered)
        {
            triggered = true;
            sinkSpeed = sinkSpeed * -1;
        }
    }
}
