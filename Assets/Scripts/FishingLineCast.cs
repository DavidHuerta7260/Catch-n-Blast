using UnityEngine;
using UnityEngine.SceneManagement;

public class FishingLineCast : MonoBehaviour
{
    // Name of your 2D scene
    public string underwaterSceneName = "UnderWater Game";

    public void OnCastLine()
    {
        // Call this when the line hits the water or when the cast animation finishes
        SceneManager.LoadScene(underwaterSceneName);
    }
}

