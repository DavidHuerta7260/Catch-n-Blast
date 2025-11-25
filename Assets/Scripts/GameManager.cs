using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int score = 0;

    public Text scoreText;

    void Awake() {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Subscribe to scene load event
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else { 
            Destroy(gameObject);
        }
    }
    void Start()
    {
        if (scoreText == null)
        {
            scoreText = FindObjectOfType<Text>();
        }

        //    if (playerController == null)
        //   {
        //     playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPlatformerController>();

        // }

        updateScoreUI();
    }

    void Update()
    {
        updateScoreUI();



    }


    public void AddPoint()
    {
        score++;
        Debug.Log("Score: " + score);
        updateScoreUI();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Re‑find the scoreText in the new scene
        scoreText = FindObjectOfType<Text>();
        updateScoreUI();
    }

    private void updateScoreUI() { 
        if (scoreText != null) {
            scoreText.text = "Score: " + GetScore();
        }


    }

    public int GetScore()
    {
        return score;
    }
}
