using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewScene : MonoBehaviour
{
    public string sceneName;
    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player") || other.CompareTag("Hook"))
        {
            if (!string.IsNullOrEmpty(sceneName))
            {
                if (sceneName == "Nature Scene 1")
                    SceneLoadState.equipPitchforkOnLoad = true;
                    SceneLoadState.enableFishSpawnerOnLoad = true;

                SceneManager.LoadScene(sceneName);
                triggered = true;
            }
        }
    }
}

