using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class FirstPersonMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    [Header("Mouse Settings")]
    public float mouseSensitivity = 2f;
    public Transform playerCamera;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Rigidbody rb;
    private float xRotation = 0f;
    private bool isGrounded;

    public GameObject ShopPanel;
    private bool nearShop = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //ShopPanel = GetComponent<Panel>();
        if (ShopPanel != null) { 
            ShopPanel.SetActive(false );
        }
    }

    void Update()
    {
        HandleLook();
        HandleMove();
        HandleJump();

        if (nearShop && Input.GetKeyDown(KeyCode.E))
        {
            if (ShopPanel.activeSelf)
            {
                ShopPanel.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked; // unlock cursor for UI
                Cursor.visible = false;
            }
            else
            {
                ShopPanel.SetActive(true);
                Cursor.lockState = CursorLockMode.None; // re-lock cursor for gameplay
                Cursor.visible = true;
            }
        }
    }

    void HandleLook()
    {
        if (playerCamera == null) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Horizontal look: rotate the player body
        transform.Rotate(Vector3.up * mouseX);

        // Vertical look: rotate only the camera
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void HandleMove()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        Vector3 velocity = new Vector3(move.x * moveSpeed, rb.velocity.y, move.z * moveSpeed);
        rb.velocity = velocity;
    }

    void HandleJump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Shop"))
        {
            nearShop = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Shop"))
        {
            nearShop = false; 
            ShopPanel.SetActive(false);
        }
    }
}
