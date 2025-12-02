using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject fishPrefab1;
    public GameObject fishPrefab2;
    public GameObject fishPrefab3;

    public int fishCount = 10;
    public Vector3 spawnArea = new Vector3(20f, 0f, 20f);

    public float minY = 2f;
    public float maxY = 10f;

    void Start()
    {
        if (!SceneLoadState.enableFishSpawnerOnLoad)
            return;

        if (SceneLoadState.fishCaughtLastRun > 0)
            fishCount = SceneLoadState.fishCaughtLastRun;

        SpawnFish();

        // NEW: Tell GameManager how many fish exist this run
        GameManager.instance.ResetFishCounters(fishCount);

        SceneLoadState.enableFishSpawnerOnLoad = false;
    }


    void SpawnFish()
    {
        for (int i = 0; i < fishCount; i++)
        {
            Vector3 randomPos = GetRandomPosition();
            GameObject chosenFish = ChooseFish();

            GameObject fish = Instantiate(chosenFish, randomPos, Quaternion.identity);

            FishVerticalMovement movement = fish.GetComponent<FishVerticalMovement>();
            if (movement != null)
            {
                movement.minY = minY;
                movement.maxY = maxY;
            }
        }
    }

    GameObject ChooseFish()
    {
        int r = Random.Range(0, 3);
        if (r == 0) return fishPrefab1;
        if (r == 1) return fishPrefab2;
        return fishPrefab3;
    }

    Vector3 GetRandomPosition()
    {
        float x = transform.position.x + Random.Range(-spawnArea.x / 2f, spawnArea.x / 2f);
        float z = transform.position.z + Random.Range(-spawnArea.z / 2f, spawnArea.z / 2f);
        float y = Random.Range(minY, maxY);
        return new Vector3(x, y, z);
    }
}


