using UnityEngine;

public class AutoEquipWeaponOnLoad : MonoBehaviour
{
    public GameObject fishingRod;
    public GameObject pitchforkWeapon;
    public MonoBehaviour pitchforkScript;

    void Start()
    {
        if (SceneLoadState.equipPitchforkOnLoad)
        {
            fishingRod.SetActive(false);
            pitchforkWeapon.SetActive(true);
            pitchforkScript.enabled = true;
            SceneLoadState.equipPitchforkOnLoad = false;
        }
        else
        {
            fishingRod.SetActive(true);
            pitchforkWeapon.SetActive(false);
            pitchforkScript.enabled = false;
        }
    }
}


