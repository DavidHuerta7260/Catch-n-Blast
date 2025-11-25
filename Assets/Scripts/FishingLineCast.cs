using UnityEngine;
using UnityEngine.SceneManagement;

public class FishingLineCast : MonoBehaviour
{
    [Header("Settings")]
    public float castForce = 12f;
    public string underwaterSceneName = "Underwater Game";

    private bool hasCast = false;
    private Rigidbody2D rb;

    void Start()
    {
        // Get the Rigidbody2D on the same GameObject
        rb = GetComponent<Rigidbody2D>();
    }

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
        rb.velocity = Vector2.zero;

        // Apply downward force
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce(Vector2.down * castForce, ForceMode2D.Impulse);
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






