using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public GameObject player;
    public float initialLock = 2f;
    public GameObject startTMP;
    public GameObject hSTMP;
    public GameObject controlsTMP;
    public GameObject titleTMP;
    public GameObject menuHighScoresTMP;
    public GameObject byTMP;
    private float tempTimer;
    private bool showingHighScore = false;
    void Update()
    {
        if (GetComponent<GameManagment>().currentGameState == GameManagment.GameState.START)
        {
            tempTimer += Time.deltaTime;
            if (tempTimer > initialLock)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    GetComponent<AudioSource>().Play();

                    gameObject.GetComponent<GameManagment>().currentGameState = GameManagment.GameState.PLAYING;

                    startTMP.SetActive(false);
                    hSTMP.SetActive(false);
                    controlsTMP.SetActive(false);
                    titleTMP.SetActive(false);
                    menuHighScoresTMP.SetActive(false);
                    byTMP.SetActive(false);

                    player.GetComponent<PlayerPlatformerController>().Activate();
                }
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                if(showingHighScore)
                {
                    startTMP.SetActive(true);
                    hSTMP.SetActive(true);
                    controlsTMP.SetActive(true);
                    titleTMP.SetActive(true);
                    byTMP.SetActive(true);

                    menuHighScoresTMP.SetActive(false);
                }
                else
                {
                    startTMP.SetActive(false);
                    hSTMP.SetActive(false);
                    controlsTMP.SetActive(false);
                    titleTMP.SetActive(false);
                    byTMP.SetActive(false);

                    menuHighScoresTMP.SetActive(true);

                    GetComponent<GameManagment>().WriteHighScoreTMP();
                }
                showingHighScore = !showingHighScore;
            }
        }
    }
}
