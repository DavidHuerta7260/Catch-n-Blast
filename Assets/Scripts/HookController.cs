using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookController : MonoBehaviour
{
    public float horizontalSpeed = 5f;
    public float sinkSpeed = 3f;

    private Rigidbody2D rb;

    private bool triggered = false;

    private List<Transform> caughtFish = new List<Transform>();
    public float fishSpacing = 0.5f;

    public Transform fishAnchor;
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


        if ((other.gameObject.CompareTag("Fish") || other.gameObject.CompareTag("Bottom")) && !triggered)

        {
            // Freeze the fish movement
            FishSwim3D fishScript = other.GetComponent<FishSwim3D>();
            if (fishScript != null)
                fishScript.enabled = false;

            // Add fish to the list
            caughtFish.Add(other.transform);

            // Parent fish to the FishAnchor instead of the hook
            other.transform.SetParent(fishAnchor);

            // Stack the fish neatly relative to the anchor
            int index = caughtFish.Count - 1;
            other.transform.localPosition = new Vector3(0, -fishSpacing * index, 0);

            // Reverse hook gravity only once
            if (!triggered)
            {
                triggered = true;
                rb.gravityScale *= -1;
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }
    }






    //  void changeGrav() {
    //    Physics.gravity *= -1;

    // }
}

