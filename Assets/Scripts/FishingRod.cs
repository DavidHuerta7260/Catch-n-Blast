using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishingRod : MonoBehaviour
{
    [Header("Fishing States")]
    public bool isEquipped = true;
    public bool isFishingAvailable;
    public bool isCasted;
    public bool isPulling;
    public bool isBaitInWater;

    [Header("References")]
    private Animator animator;
    public GameObject baitPrefab;
    public GameObject endof_of_rope;
    public GameObject start_of_rope;
    public GameObject start_of_rod;

    private Transform baitPosition;
    private GameObject currentBaitInstance;

    [Header("Scene Settings")]
    public string underwaterSceneName = "UnderWater Game";
    public float timeBeforeSceneChange = 2f;  // bait stays visible before switching scenes

    void Start()
    {
        animator = GetComponent<Animator>();
        isEquipped = true; // rod is active by default
    }

    void Update()
    {
        if (!isEquipped) return; // prevent running when switching weapons

        // Center-screen ray
        Ray ray = Camera.main.ScreenPointToRay(
            new Vector3(Screen.width / 2, Screen.height / 2, 0)
        );
        RaycastHit hit;

        // Detect fishing area
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("FishingArea"))
            {
                isFishingAvailable = true;

                // CAST LINE
                if (Input.GetMouseButtonDown(0) && !isCasted && !isPulling)
                {
                    StartCoroutine(CastRod(hit.point));
                }

                // RETRIEVE LINE
                else if (Input.GetMouseButtonDown(0) && isCasted && isBaitInWater && !isPulling)
                {
                    StartCoroutine(RetrieveLine());
                }
            }
            else
            {
                isFishingAvailable = false;
            }
        }
        else
        {
            isFishingAvailable = false;
        }

        // Update rope endpoints
        if (isCasted || isPulling)
        {
            if (start_of_rope && start_of_rod && endof_of_rope)
            {
                start_of_rope.transform.position = start_of_rod.transform.position;

                if (baitPosition)
                    endof_of_rope.transform.position = baitPosition.position;
            }
        }

        // PULL ANIMATION
        if (isCasted && Input.GetMouseButtonDown(1))
        {
            PullRod();
        }
    }

    IEnumerator CastRod(Vector3 targetPosition)
    {
        isCasted = true;
        isBaitInWater = false;

        animator.SetTrigger("Cast");

        yield return new WaitForSeconds(1f); // casting animation

        // Spawn bait at water position
        currentBaitInstance = Instantiate(baitPrefab, targetPosition, Quaternion.identity);
        baitPosition = currentBaitInstance.transform;

        // Small splash delay
        yield return new WaitForSeconds(0.5f);

        isBaitInWater = true;

        // 🎣 WAIT a bit before switching to the underwater scene
        yield return new WaitForSeconds(timeBeforeSceneChange);

        SceneManager.LoadScene(underwaterSceneName);
    }

    IEnumerator RetrieveLine()
    {
        animator.SetTrigger("Retrieve");

        yield return new WaitForSeconds(0.8f);

        if (currentBaitInstance)
            Destroy(currentBaitInstance);

        baitPosition = null;
        isCasted = false;
        isBaitInWater = false;
        isPulling = false;
    }

    private void PullRod()
    {
        animator.SetTrigger("Pull");
        isPulling = true;
        isBaitInWater = false;

        if (currentBaitInstance)
            Destroy(currentBaitInstance);

        isCasted = false;
    }

    // Call this from WeaponSwitcher when switching
    public void SetEquipped(bool equipped)
    {
        isEquipped = equipped;

        if (!equipped)
        {
            // Prevent leftover states if rod is swapped away mid-cast
            isCasted = false;
            isBaitInWater = false;

            if (currentBaitInstance)
                Destroy(currentBaitInstance);
        }
    }
}
