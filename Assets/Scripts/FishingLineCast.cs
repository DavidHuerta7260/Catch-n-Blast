using UnityEngine;
using UnityEngine.SceneManagement;

public class FishingLineCast : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D hookRb;
    public Transform hookTransform;

    [Header("Settings")]
    public float castForce = 12f;
    public string underwaterSceneName = "Underwater Game";

    private bool hasCast = false;

    void Update()
    {
        // Cast on left-click
        if (Input.GetMouseButtonDown(0) && !hasCast)
        {
            CastLine();
        }
    }

    void CastLine()
    {
        hasCast = true;

        // Reset velocity
        hookRb.velocity = Vector2.zero;

        // Apply downward force
        hookRb.bodyType = RigidbodyType2D.Dynamic;
        hookRb.AddForce(Vector2.down * castForce, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If hook hits the "Water" plane
        if (other.CompareTag("Water"))
        {
            LoadUnderwaterScene();
        }
    }

    void LoadUnderwaterScene()
    {
        SceneManager.LoadScene(underwaterSceneName);
    }
}



