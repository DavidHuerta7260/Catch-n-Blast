using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchforkHit : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // If it hits a fish, destroy both and add a point
        if (collision.gameObject.CompareTag("Fish"))
        {
            Destroy(collision.gameObject); // destroy the fish
            Destroy(gameObject); // destroy the spear

            GameManager.instance.AddPoint(); // award point
        }
    }
}
