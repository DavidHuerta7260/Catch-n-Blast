using System.Collections;
using UnityEngine;

public class PitchforkThrow : MonoBehaviour
{
    [Header("References")]
    public GameObject pitchforkPrefab;   // Prefab of the pitchfork
    public Transform spawnPoint;         // Where the pitchfork appears (on player hand)
    public Camera playerCamera;          // For forward direction

    [Header("Throw Settings")]
    public float throwForce = 20f;
    public float destroyAfterSeconds = 5f;

    private GameObject currentPitchfork;

    void Start()
    {
        SpawnNewPitchfork();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ThrowPitchfork();
        }
    }

    void SpawnNewPitchfork()
    {
        currentPitchfork = Instantiate(pitchforkPrefab, spawnPoint.position, spawnPoint.rotation, spawnPoint);
        Rigidbody rb = currentPitchfork.GetComponent<Rigidbody>();
        rb.isKinematic = true;  // So it stays in player hand
    }

    void ThrowPitchfork()
    {
        if (currentPitchfork == null) return;

        // Detach
        currentPitchfork.transform.parent = null;

        // Enable physics
        Rigidbody rb = currentPitchfork.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;

        // Throw in camera forward direction
        rb.AddForce(playerCamera.transform.forward * throwForce, ForceMode.Impulse);

        // Destroy after seconds
        Destroy(currentPitchfork, destroyAfterSeconds);

        // Spawn next pitchfork
        StartCoroutine(RespawnAfterDelay(0.3f));
    }

    IEnumerator RespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnNewPitchfork();
    }
}
