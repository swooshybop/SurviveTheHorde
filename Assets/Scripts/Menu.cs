using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Menu : MonoBehaviour
{
    public static Menu Instance;

    public TMP_InputField playerName;
    public TMP_Text highScoreText;

    public string nameEntered = "Player";

    private void Start()
    {
        int bestScore = PlayerPrefs.GetInt("HighScore", 0);
        string bestName = PlayerPrefs.GetString("HighScoreName", "None");

        if (highScoreText != null)
        {
            highScoreText.text = $"High Score : {bestName} : {bestScore}";
        }

    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void StartNew()
    {
        var nameTyped = playerName.text;
        nameEntered = nameTyped;

        SceneManager.LoadScene(1);
    }

    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
        PlayerPrefs.DeleteKey("HighScoreName");
        PlayerPrefs.Save();

        int bestScore = PlayerPrefs.GetInt("HighScore", 0);
        string bestName = PlayerPrefs.GetString("HighScoreName", "None");

        if (highScoreText != null)
        {
            highScoreText.text = $"High Score : {bestName} : {bestScore}";
        }

    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
