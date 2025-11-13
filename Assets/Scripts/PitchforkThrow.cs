using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchforkThrow : MonoBehaviour
{
    [Header("References")]
    public GameObject pitchfork;      // Assign in Inspector
    public Transform throwPoint;      // Where it launches from

    [Header("Throw Settings")]
    public float throwForce = 20f;
    public float destroyAfterSeconds = 5f;

    private bool hasThrown = false;

    void Update()
    {
        // Left mouse click
        if (Input.GetMouseButtonDown(0) && !hasThrown)
        {
            ThrowPitchfork();
        }
    }

    void ThrowPitchfork()
    {
        if (pitchfork == null) return;

        // Detach from player
        pitchfork.transform.parent = null;

        // Enable physics
        Rigidbody rb = pitchfork.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.AddForce(throwPoint.forward * throwForce, ForceMode.Impulse);
        }

        hasThrown = true;
        Destroy(pitchfork, destroyAfterSeconds);
    }
}
