using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadUnderwaterOnBait : MonoBehaviour
{
    public string underwaterSceneName = "UnderWater Game";

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.ToLower().Contains("bait"))
        {
            SceneManager.LoadScene(underwaterSceneName);
        }
    }
}

