using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TMP_Text scoreText;      // "Score : 0"
    public TMP_Text bestText;       // "Best : Name : Score"

    private int score;
    private int bestScore;
    private string bestName;

    void Awake()
    {
        if (Instance != null)
        { 
            Destroy(gameObject); 
            return;
        }

        Instance = this;
    }

    void Start()
    {
        // Load saved best
        bestScore = PlayerPrefs.GetInt("HighScore", 0);
        bestName = PlayerPrefs.GetString("HighScoreName", "None");
        if (bestText) bestText.text = $"Best : {bestName} : {bestScore}";
        if (scoreText) scoreText.text = $"Score : {score}";
    }

    public void AddScore(int points)
    {
        score += points;
        if (scoreText) scoreText.text = $"Score : {score}";
    }

    public void GameOver()
    {
        // Update/save high score if beaten
        if (score > bestScore)
        {
            bestScore = score;
            bestName = (Menu.Instance != null && !string.IsNullOrWhiteSpace(Menu.Instance.nameEntered))
                      ? Menu.Instance.nameEntered
                      : "Player";

            PlayerPrefs.SetInt("HighScore", bestScore);
            PlayerPrefs.SetString("HighScoreName", bestName);
            PlayerPrefs.Save();
        }

        // Update label so restarting the scene also shows the new best immediately
        if (bestText)
        {
            bestText.text = $"Best : {bestName} : {bestScore}";
        }
    }
}
