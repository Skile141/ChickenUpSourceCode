using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI gameScoreText;
    [SerializeField] private GameObject newRecordForm;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameObject highScoreNotification;
    private List<PlayerData> topPlayers;

    private void Start()
    {
        // Wait for DataManager to initialize
        StartCoroutine(InitializeWhenReady());
        newRecordForm.SetActive(false);
        highScoreNotification.SetActive(false);
    }

    private IEnumerator InitializeWhenReady()
    {
        while (DataManager.instance == null)
        {
            yield return null;
        }
        topPlayers = DataManager.instance.GetTopPlayers();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.GameOver();
        }  
    }

    public void PlayGameOver()
    {
        audioManager.StopBackgroundSound();
        audioManager.PlayGameOverSound();
        StartCoroutine(CountToFinalScore());
    }

    private IEnumerator CountToFinalScore()
    {
        yield return new WaitForSecondsRealtime(0.1f);

        int finalScore = gameManager.GetScore();
        int displayScore = 0;

        while (displayScore < finalScore)
        {
            displayScore += Mathf.CeilToInt(finalScore / 60f);
            displayScore = Mathf.Min(displayScore, finalScore);
            gameScoreText.text = displayScore.ToString("D7");
            yield return new WaitForSecondsRealtime(0.05f);
        }

        bool qualifies = false;
        if (topPlayers != null)
        {
            qualifies = topPlayers.Count < 3 || (topPlayers.Count > 0 && finalScore > topPlayers[topPlayers.Count - 1].playerScore);
        }
        else
        {
            qualifies = true; // If no data exists, player qualifies
        }

        if (qualifies)
        {
            highScoreNotification.SetActive(true);
            yield return new WaitForSecondsRealtime(1f);
            newRecordForm.SetActive(true);
            
        }
        else
        {
            newRecordForm.SetActive(false);
            highScoreNotification.SetActive(false);
        }
    }

    public void ResetScoreDisplay()
    {
        gameScoreText.text = "0000000"; 
    }
}

