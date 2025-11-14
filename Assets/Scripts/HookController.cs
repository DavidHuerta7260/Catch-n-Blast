using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookController : MonoBehaviour
{
    public float horizontalSpeed = 5f;
    public float sinkSpeed = 3f;

    private Rigidbody2D rb;

    private bool triggered = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Hook automatically starts sinking
        rb.velocity = new Vector2(0, -sinkSpeed);

      // if (triggered) {
      //     Physics2D.gravity *= -1;
      //  }
    }

    void Update()
    {
        float moveX = 0f;

        if (Input.GetKey(KeyCode.A))
            moveX = -horizontalSpeed;
        else if (Input.GetKey(KeyCode.D))
            moveX = horizontalSpeed;

        // Keep sinking while moving sideways
       // rb.velocity = new Vector2(moveX, -sinkSpeed);
        rb.velocity = new Vector2(moveX, rb.velocity.y);


       // if (triggered)
       // {
       //     Physics2D.gravity *= -1;
       // }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Fish") && !triggered)
        {
            triggered = true;

            rb.gravityScale *= -1; // Multiplies the current gravity vector by -1, reversing its direction

            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }


  //  void changeGrav() {
    //    Physics.gravity *= -1;

   // }
}
