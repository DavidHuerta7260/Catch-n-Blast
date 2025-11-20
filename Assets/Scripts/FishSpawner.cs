using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [Header("Fish Spawning Settings")]
    public GameObject fishPrefab;      // Assign your fish prefab here
    public int fishCount = 10;         // Editable number of fish to spawn
    public Vector3 spawnArea = new Vector3(20f, 0f, 20f); // X/Z area size

    [Header("Vertical Movement Settings")]
    public float minY = 2f;
    public float maxY = 10f;

    void Start()
    {
        SpawnFish();
    }

    void SpawnFish()
    {
        for (int i = 0; i < fishCount; i++)
        {
            Vector3 randomPos = GetRandomPosition();

            GameObject fish = Instantiate(fishPrefab, randomPos, Quaternion.identity);

            // Apply vertical movement settings automatically
            FishVerticalMovement movement = fish.GetComponent<FishVerticalMovement>();
            if (movement != null)
            {
                movement.minY = minY;
                movement.maxY = maxY;
            }
        }
    }

    Vector3 GetRandomPosition()
    {
        float x = transform.position.x + Random.Range(-spawnArea.x / 2f, spawnArea.x / 2f);
        float z = transform.position.z + Random.Range(-spawnArea.z / 2f, spawnArea.z / 2f);

        // Start somewhere inside the min/max Y range
        float y = Random.Range(minY, maxY);

        return new Vector3(x, y, z);
    }

    // Draw the spawn area in Scene View
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + new Vector3(0, (minY + maxY) / 2f, 0),
            new Vector3(spawnArea.x, maxY - minY, spawnArea.z));
    }
}
