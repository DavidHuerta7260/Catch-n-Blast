using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int score = 0;

    public int totalFishThisRun = 0;
    public int scoredFish = 0;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI allFishCaughtText;

    public GameObject fishingRod;
    public GameObject pitchfork;

    public float fishHitTimeLimit = 30f;
    private float timer = 0f;
    private bool timerActive = false;

    public TextMeshProUGUI timerText;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        UpdateScoreUI();

        if (allFishCaughtText != null)
            allFishCaughtText.gameObject.SetActive(false);

        if (timerText != null)
            timerText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (timerActive)
        {
            timer -= Time.deltaTime;

            if (timerText != null)
                timerText.text = "Time: " + Mathf.CeilToInt(timer).ToString();

            if (timer <= 0)
                EndFishHitPhase();
        }
    }

    public void StartFishHitTimer()
    {
        if (timerText == null)
        {
            GameObject tObj = GameObject.Find("TimerText");
            if (tObj != null)
                timerText = tObj.GetComponent<TextMeshProUGUI>();
        }

        timer = fishHitTimeLimit;
        timerActive = true;

        if (timerText != null)
        {
            timerText.gameObject.SetActive(true);
            timerText.text = "Time: " + Mathf.CeilToInt(timer).ToString();
        }
    }


    void EndFishHitPhase()
    {
        timerActive = false;

        if (timerText != null)
            timerText.gameObject.SetActive(false);

        RemoveAllRemainingFish();
        GivePlayerBackRod();
    }

    void RemoveAllRemainingFish()
    {
        GameObject[] fishLeft = GameObject.FindGameObjectsWithTag("Fish");
        foreach (GameObject f in fishLeft)
            Destroy(f);
    }

    void GivePlayerBackRod()
    {
        if (fishingRod != null)
            fishingRod.SetActive(true);

        if (pitchfork != null)
        {
            PitchforkThrow pf = pitchfork.GetComponent<PitchforkThrow>();
            if (pf != null)
                pf.enabled = false;

            pitchfork.SetActive(false);
        }
    }

    public void AddPoint()
    {
        score++;
        scoredFish++;

        UpdateScoreUI();
        CheckIfAllFishScored();
    }

    void CheckIfAllFishScored()
    {
        if (scoredFish >= totalFishThisRun)
        {
            timerActive = false;

            if (timerText != null)
                timerText.gameObject.SetActive(false);

            ShowAllFishCaughtMessage();
            GivePlayerBackRod();
        }
    }

    public void ResetFishCounters(int total)
    {
        totalFishThisRun = total;
        scoredFish = 0;

        if (allFishCaughtText != null)
            allFishCaughtText.gameObject.SetActive(false);
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    void ShowAllFishCaughtMessage()
    {
        if (allFishCaughtText != null)
        {
            allFishCaughtText.gameObject.SetActive(true);
            allFishCaughtText.text =
                "🎉 Congratulations!\nYou caught ALL the fish!\nCast your line again to catch more!";
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void setScore(int reduc)
    {
        score = reduc;
    }
}
