using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject fishingRod;
    public GameObject pitchfork;

    void Start()
    {
        // Fishing rod is default at scene start
        fishingRod.SetActive(true);
        pitchfork.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            fishingRod.SetActive(true);
            pitchfork.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            fishingRod.SetActive(false);
            pitchfork.SetActive(true);
        }
    }
}
