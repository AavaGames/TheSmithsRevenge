using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TextMeshProUGUI
using TMPro;
//SceneLoading
using UnityEngine.SceneManagement;
//Array Sort
using System;
public class GameManagment : MonoBehaviour
{
    static public int totalScore;
    public GameObject player;
    public GameObject scoreTMP;
    public GameObject healthTMP;
    public GameObject gameOverTMP;
    public GameObject menuHighScoresTMP;
    public int pointsByTime = 10;
    static public bool paused = false;
    private float tempTimer = 0f;
    public enum GameState {START, PLAYING, DEAD, RESTART};
    public GameState currentGameState = GameState.START;
    private bool initialLevelBuilt = false;
    static public int[] highScoreList = {112, 1000, 2050, 3010, 3289, 4000, 4111, 6112, 8231, 15000};
    private bool isHighScore = false;
    private bool highScoreSaved = false;

    private void Start() 
    {
        isHighScore = false;
        SortHighScores();
    }
    private void Update() {
        if (currentGameState == GameState.START)
        {
            Time.timeScale = 1f;
            totalScore = 0;
            highScoreSaved = false;

            if (initialLevelBuilt == false)
            {
                GetComponent<ChunkBuilder>().BuildChunks();
                initialLevelBuilt = true;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
        if (currentGameState == GameState.PLAYING)
        {
            AddTimePoints();

            ShowScore();
            ShowHealth();

            if (player.GetComponent<PlayerPlatformerController>().isDead == true)
            {
                currentGameState = GameState.DEAD;
            }
        }
        if (currentGameState == GameState.DEAD)
        {
            ClearUI();

            gameOverTMP.SetActive(true);

            if (!highScoreSaved)
            {
                SaveHighScore();
                SortHighScores();
            }
            ShowGameOver();

            //SAVE HIGH SCORE

            if (Input.GetButtonDown("Jump"))
            {
                currentGameState = GameState.RESTART;
            }
        }
        if (currentGameState == GameState.RESTART)
        {
            SceneManager.LoadScene("Main");
        }
    }
    //Completing a "level" nets points 
    private void AddTimePoints()
    {
        tempTimer += Time.deltaTime;
        if (tempTimer >= 1f)
        {
            totalScore += pointsByTime;
            tempTimer = 0f;
        }
    }
    private void ShowScore()
    {
        scoreTMP.GetComponent<TextMeshProUGUI>().SetText("Score: " + totalScore);
    }
    
    private void ShowHealth()
    {
        healthTMP.GetComponent<TextMeshProUGUI>().SetText("Health: " + player.GetComponent<PlayerPlatformerController>().health);
    }

    private void ShowGameOver()
    {
        if (isHighScore)
        {
            gameOverTMP.GetComponent<TextMeshProUGUI>().SetText("Game Over!\n\nYou got a High Score!\nScore: " + totalScore + "\n\nSPACE to Restart!");
        }
        else if (!isHighScore)
        {
            gameOverTMP.GetComponent<TextMeshProUGUI>().SetText("Game Over!\n\nScore: " + totalScore + "\n\nSPACE to Restart!");
        }

    }
    private void ClearUI()
    {
        scoreTMP.SetActive(false);
        healthTMP.SetActive(false);
    }
    private void SaveHighScore()
    {
        for(int i = 0; i < highScoreList.Length; i++)
        {
            if(totalScore > highScoreList[i])
            {
                highScoreList[i] = totalScore;
                isHighScore = true;
                break;
            }
            isHighScore = false;
        }
        highScoreSaved = true;
    }

    private void SortHighScores()
    {
        Array.Sort(highScoreList);
        Array.Reverse(highScoreList);
    }

    public void WriteHighScoreTMP()
    {
        string scoreString = "";
        for(int i = 0; i < highScoreList.Length; i++)
        {
            int temp = i + 1;
            scoreString = scoreString + "\n" + temp + ": " + highScoreList[i];
        }
        menuHighScoresTMP.GetComponent<TextMeshProUGUI>().SetText("HIGH SCORES\n" + scoreString + "\n\nPress SPACE to Start Game\nPress H to go Back");

    }
}
