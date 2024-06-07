using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Counter : MonoBehaviour
{
    public TextMeshProUGUI timeText, highscoreText, currentScoreText;  // Reference to the Text object in the UI
    private float elapsedTime = 0f;
    private bool isCounting = true;
    private bool saveOnce = true;

    public GameObject gameOver;

    private void Start()
    {
        saveOnce = true;

        // Initialize the highscoreText with the current high score at the start
        float currentHighScore = PlayerPrefs.GetFloat("HighScore", 0f);
        highscoreText.text = FormatTime(currentHighScore);
    }

    void Update()
    {
        if (PlayerMovementAdvanced.gamOver && saveOnce)
        {
            StopCounter();
        }

        if (isCounting)
        {
            // Increment elapsedTime by the time passed since the last frame
            elapsedTime += Time.deltaTime;

            // Update the time text with the current elapsed time
            timeText.text = FormatTime(elapsedTime);
        }
    }

    public void StopCounter()
    {
        isCounting = false;
        saveOnce = false;

        gameOver.SetActive(true);
        timeText.gameObject.SetActive(false);

        // Save current time as high score if it's higher than the current high score
        float currentHighScore = PlayerPrefs.GetFloat("HighScore", 0f);
        if (elapsedTime > currentHighScore)
        {
            PlayerPrefs.SetFloat("HighScore", elapsedTime);
            PlayerPrefs.Save();
            highscoreText.text = "Highscore: " + FormatTime(elapsedTime);
            currentScoreText.text = "current score: " + FormatTime(elapsedTime);
        }
        else
        {
            highscoreText.text = "Highscore: " + FormatTime(currentHighScore);
            currentScoreText.text = "current score: " + FormatTime(elapsedTime);
        }
    }

    // Function to reset the counter (optional)
    public void ResetCounter()
    {
        elapsedTime = 0f;
        isCounting = true;
    }

    // Helper function to format time as 00:00:000 (minutes:seconds:milliseconds)
    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);
        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}
