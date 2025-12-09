using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchforkHit : MonoBehaviour
{
    public GameObject fishHitEffectPrefab;  // drag your particle prefab here

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fish"))
        {
            // Spawn particle effect at fish position
            if (fishHitEffectPrefab != null)
            {
                Instantiate(
                    fishHitEffectPrefab,
                    collision.transform.position,
                    Quaternion.identity
                );
            }

            // Award point
            GameManager.instance.AddPoint();

            // Destroy the fish
            Destroy(collision.gameObject);

            // Destroy the pitchfork projectile
            Destroy(gameObject);
        }
    }
}

