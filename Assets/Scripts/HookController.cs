using UnityEngine;

public class HookController : MonoBehaviour
{
    public float horizontalSpeed = 5f;
    public float sinkSpeed = 3f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Hook automatically starts sinking
        rb.velocity = new Vector2(0, -sinkSpeed);
    }

    void Update()
    {
        float moveX = 0f;

        if (Input.GetKey(KeyCode.A))
            moveX = -horizontalSpeed;
        else if (Input.GetKey(KeyCode.D))
            moveX = horizontalSpeed;

        // Keep sinking while moving sideways
        rb.velocity = new Vector2(moveX, -sinkSpeed);
    }
}
