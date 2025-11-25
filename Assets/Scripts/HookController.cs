using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HookController : MonoBehaviour
{
    public float horizontalSpeed = 5f;
    public float sinkSpeed = 400f;

    private Rigidbody2D rb;
    private bool triggered = false;

    public Transform fishAnchor;
    public float fishSpacing = 0.5f;
    private List<Transform> caughtFish = new List<Transform>();

    public TextMeshPro fishCounterText;
    public TextMeshPro depthText;
    private int fishCount = 0;

    private AudioSource playerAudio;
    public AudioClip bubbleClip;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -sinkSpeed);

        if (fishCounterText != null)
            fishCounterText.text = "x 0";

        if (depthText != null)
            depthText.text = "0m";

        playerAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        float moveX = 0f;
        float moveY = -sinkSpeed;

        if (Input.GetKey(KeyCode.A))
            moveX = -horizontalSpeed;
        else if (Input.GetKey(KeyCode.D))
            moveX = horizontalSpeed;

        if (Input.GetKey(KeyCode.S))
            moveY = -sinkSpeed * 2f;
        else if (Input.GetKey(KeyCode.W))
            moveY = -sinkSpeed * 0.5f;

        rb.velocity = new Vector2(moveX, moveY);

        if (depthText != null)
        {
            float depth = Mathf.Abs(transform.position.y);
            depthText.text = Mathf.RoundToInt(depth) + "m";
        }
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
            FishSwim3D fish = other.GetComponent<FishSwim3D>();
            if (fish != null)
                fish.enabled = false;

           //plays bubble sound effect
            playerAudio.PlayOneShot(bubbleClip, 1.0f);

            caughtFish.Add(other.transform);

            GameManager.instance.AddPoint(); // award point
            fishCount++;
            if (fishCounterText != null)
                fishCounterText.text = "x " + fishCount;

            other.transform.SetParent(fishAnchor);
            int index = caughtFish.Count - 1;
            other.transform.localPosition = new Vector3(0, -fishSpacing * index, 0);

            if (!triggered)
            {
                triggered = true;
                sinkSpeed *= -1;
            }
        }
    }
}

