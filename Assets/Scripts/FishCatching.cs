using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCatching : MonoBehaviour
{

    private HookController hookCon;

    private bool triggered = false; 

    // Start is called before the first frame update
    void Start()
    {
        hookCon = GameObject.FindObjectOfType<HookController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player") && !triggered)
        {

            triggered = true;
            //  Debug.Log("triggered!");


            // Disable collider so it doesn't trigger again
            GetComponent<Collider2D>().enabled = false;

            // Optional: disable fish Rigidbody so it doesn't interfere
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null) rb.isKinematic = true;

            // Parent fish to the hook so it moves with it
            transform.SetParent(other.transform);

            // Optional: reposition fish to sit nicely on the hook
           // transform.localPosition = new Vector3(0, -0.5f, 0);

            // Destroy fish later (e.g., after 3 seconds)
            Destroy(gameObject, 3f);

            //  hookCon.gameOver = true;
        }

    }
}
