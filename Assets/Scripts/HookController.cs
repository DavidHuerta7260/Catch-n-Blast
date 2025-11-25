using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HookController : MonoBehaviour
{
    [Header("Hook Movement")]
    public float horizontalSpeed = 5f;
    public float sinkSpeed = 400f;

    private Rigidbody2D rb;
    private bool triggered = false;

    [Header("Fish Collecting")]
    public Transform fishAnchor;
    public float fishSpacing = 0.5f;
    private List<Transform> caughtFish = new List<Transform>();

    [Header("UI - Fish Counter")]
    public TextMeshPro fishCounterText;   
    private int fishCount = 0;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Hook automatically starts sinking
        rb.velocity = new Vector2(0, -sinkSpeed);

        // Initialize UI
        if (fishCounterText != null)
            fishCounterText.text = "x 0";
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

        // Speed up or slow down sink speed
        if (Input.GetKey(KeyCode.S))
            moveY = -sinkSpeed * 2f;       // Faster sinking
        else if (Input.GetKey(KeyCode.W))
            moveY = -sinkSpeed * 0.5f;     // Slower sinking

        // Apply movement
        rb.velocity = new Vector2(moveX, moveY);
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Bottom") && !triggered)
        {
            triggered = true;
            sinkSpeed *= -1; // reverse
            return;
        }


       
        if (other.CompareTag("Fish"))
        {
            // Stop fish movement
            FishSwim3D fish = other.GetComponent<FishSwim3D>();
            if (fish != null)
                fish.enabled = false;

            // Add to list
            caughtFish.Add(other.transform);

            // Increase count + update UI
            fishCount++;
            if (fishCounterText != null)
                fishCounterText.text = "x " + fishCount;

            // Attach fish to anchor
            other.transform.SetParent(fishAnchor);

            // Stack vertically
            int index = caughtFish.Count - 1;
            other.transform.localPosition = new Vector3(0, -fishSpacing * index, 0);

            // Reverse direction only once
            if (!triggered)
            {
                triggered = true;
                sinkSpeed *= -1;
            }
        }
    }
}

