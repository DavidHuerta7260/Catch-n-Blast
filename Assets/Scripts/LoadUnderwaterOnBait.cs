using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadUnderwaterOnBait : MonoBehaviour
{
  //  public string underwaterSceneName = "UnderWater Game";

    public int randStage;

    void Awake()
    {
        randStage = Random.Range(0, 3);
    }

    private void OnTriggerEnter(Collider other)
    {

        switch (randStage)
        {
            default:
                break;
            case 0:
                SceneManager.LoadScene("Underwater Game");
                break;
            case 1:
                SceneManager.LoadScene("UnderWater2");
                break;
            case 2:
                SceneManager.LoadScene("UnderWater3");
                break;
        }
        // if (other.name.ToLower().Contains("bait"))
        //{
        //    SceneManager.LoadScene(underwaterSceneName);
        //}
    }
}

