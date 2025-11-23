using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookController : MonoBehaviour
{
    [Header("Hook Movement")]
    public float horizontalSpeed = 5f;
    public float sinkSpeed = 400f;   // your value

    private Rigidbody2D rb;
    private bool triggered = false;

    [Header("Fish Collecting")]
    public Transform fishAnchor;
    public float fishSpacing = 0.5f;
    private List<Transform> caughtFish = new List<Transform>();


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

        // Horizontal movement
        if (Input.GetKey(KeyCode.A))
            moveX = -horizontalSpeed;
        else if (Input.GetKey(KeyCode.D))
            moveX = horizontalSpeed;

        // Speed up/down the sinking speed
        if (Input.GetKey(KeyCode.S))
            moveY = -sinkSpeed * 2f;
        else if (Input.GetKey(KeyCode.W))
            moveY = -sinkSpeed * 0.5f;

        // Apply the final velocity
        rb.velocity = new Vector2(moveX, moveY);
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Bottom") && !triggered)
        {
            triggered = true;
            sinkSpeed *= -1;
            return;
        }


        
        if (other.CompareTag("Fish"))
        {
            // Stop fish movement
            FishSwim3D fish = other.GetComponent<FishSwim3D>();
            if (fish != null)
                fish.enabled = false;

            // Track the fish
            caughtFish.Add(other.transform);

            // Attach fish to anchor
            other.transform.SetParent(fishAnchor);

            // Stack vertically downward
            int index = caughtFish.Count - 1;
            other.transform.localPosition = new Vector3(0, -fishSpacing * index, 0);

            // Trigger upward movement only once
            if (!triggered)
            {
                triggered = true;
                sinkSpeed *= -1;
            }
        }
    }
}

