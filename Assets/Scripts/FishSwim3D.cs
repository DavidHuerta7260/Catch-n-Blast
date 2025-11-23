using UnityEngine;

public class FishSwim3D : MonoBehaviour
{
    [Header("Movement Settings")]
    public float swimSpeed = 2f;          // How fast the fish moves
    public float swimDuration = 2f;       // How long it swims before turning

    private float timer = 0f;
    private bool movingRight = true;

    void Update()
    {
        // Move the fish forward (Z axis) relative to its rotation
        transform.Translate(Vector3.forward * swimSpeed * Time.deltaTime);

        timer += Time.deltaTime;

        // After duration, flip direction
        if (timer >= swimDuration)
        {
            timer = 0f;
            FlipFish();
        }
    }

    void FlipFish()
    {
        movingRight = !movingRight;

        // Rotate exactly 180° around Y (since fish is already rotated 90°)
        Vector3 rot = transform.eulerAngles;
        rot.y += 180f;
        transform.eulerAngles = rot;
    }
}

