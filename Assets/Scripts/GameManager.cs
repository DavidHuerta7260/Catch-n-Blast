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
    }

    public void AddPoint()
    {
        score++;
        score *= score;
        scoredFish++;

        UpdateScoreUI();
        CheckIfAllFishScored();
    }

    void CheckIfAllFishScored()
    {
        if (scoredFish >= totalFishThisRun)
        {
            ShowAllFishCaughtMessage();

            if (fishingRod != null)
                fishingRod.SetActive(true);

            if (pitchfork != null)
                pitchfork.SetActive(false);
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
            allFishCaughtText.text = "🎉 Congratulations!\nYou caught ALL the fish!\nCast your line again to catch more!";
        }
    }
    public int GetScore()
    {
        return score;
    }

    public void setScore(int reduc) {
        score = reduc;
    }
}
