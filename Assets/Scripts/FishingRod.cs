using System.Collections;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    public bool isEquipped;
    public bool isFishingAvailable;

    public bool isCasted;
    public bool isPulling;
    public bool isBaitInWater;

    private Animator animator;
    public GameObject baitPrefab;
    public GameObject endof_of_rope;
    public GameObject start_of_rope;
    public GameObject start_of_rod;

    private Transform baitPosition;
    private GameObject currentBaitInstance;

    private void Start()
    {
        animator = GetComponent<Animator>();
        isEquipped = true;
    }

    void Update()
    {
        if (!isEquipped) return;

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        // Check for valid fishing spot
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("FishingArea"))
            {
                isFishingAvailable = true;

                // 🎣 Left Click to Cast
                if (Input.GetMouseButtonDown(0) && !isCasted && !isPulling)
                {
                    StartCoroutine(CastRod(hit.point));
                }

                // 🎣 Left Click Again to Retrieve (if bait already in water)
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

        // 🎯 Update Rope Positions
        if (isCasted || isPulling)
        {
            if (start_of_rope && start_of_rod && endof_of_rope)
            {
                start_of_rope.transform.position = start_of_rod.transform.position;

                if (baitPosition)
                    endof_of_rope.transform.position = baitPosition.position;
            }
        }

        // 🪝 Right Click to Pull Animation
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

        // Wait for casting animation
        yield return new WaitForSeconds(1f);

        // Spawn bait at target
        currentBaitInstance = Instantiate(baitPrefab);
        currentBaitInstance.transform.position = targetPosition;
        baitPosition = currentBaitInstance.transform;

        // Delay to simulate splash/settling
        yield return new WaitForSeconds(0.5f);

        isBaitInWater = true;
    }

    IEnumerator RetrieveLine()
    {
        // Trigger a reel-back animation
        animator.SetTrigger("Retrieve");

        // Optional: Wait a moment for animation
        yield return new WaitForSeconds(0.8f);

        // Destroy bait and reset state
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
}
