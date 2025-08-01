using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private DataManager dataManager;
    [SerializeField] private AudioManager audioManager;
    private GameOver gameOverScript;
    private PlayerNameInputManager playerNameInputManager;

    [SerializeField] private SkyDayNight skyDayNight;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    private float timeToChange = 5f;
    private float time = 0f;

    private bool isOver = false;
    private int score = 0;

    [SerializeField] private GameObject mainmenu;
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject gamePause;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject leaderboardForm;
    [SerializeField] private GameObject newRecordForm;

    [SerializeField] private GameResume gameResume;
    [SerializeField] private Animator animator;
    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }

        if (gameOver != null)
        {
            gameOverScript = gameOver.GetComponent<GameOver>();
        }
    }

    private void Start()
    {
        MainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOver)
        {
            UpdateScoreUI();
            ChangeDayAndNight();
        }
    }

    private void UpdateScoreUI()
    {
        if (gameOverScript != null)
        {
            gameOverScript.ResetScoreDisplay();
        }

        if (textMeshProUGUI != null)
        {
            textMeshProUGUI.text = score.ToString("D7");
        }
    }

    private void ChangeDayAndNight()
    {
        time += Time.deltaTime;
        if (time >= timeToChange)
        {
            skyDayNight.ToggleDayNight();
            time = 0;
        }
    }
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    public void MainMenu()
    {
        mainmenu.SetActive(true);
        audioManager.PlayBackgroundSound();
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        gamePause.SetActive(false);
        gameOver.SetActive(false);
        inGameUI.SetActive(false);
        Time.timeScale = 0;
    }

    public void GamePause()
    {
        mainmenu.SetActive(false);
        gamePause.SetActive(true);
        gameOver.SetActive(false);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        gameResume.ResumingPerformance();
    }

    public void GameOver()
    {
        isOver = true;
        mainmenu.SetActive(false);
        gamePause.SetActive(false);
        inGameUI.SetActive(false);
        gameOver.SetActive(true);
        gameOverScript.PlayGameOver();
        Time.timeScale = 0;
    }

    public void NewGame()
    {
        score = 0;
        isOver = false;
        time = 0f;
        skyDayNight.IsDay(true);
        audioManager.ResumeBackgroundSound();

        Camera.main.transform.position = new Vector3(0, 0, -10);
        playerController.ResetStamina();
        
        
        UpdateScoreUI();
        mainmenu.SetActive(false);
        gamePause.SetActive(false);
        gameOver.SetActive(false);
        inGameUI.SetActive(true);

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = new Vector3(0, 0, 0);
        }

        foreach (GameObject obj1 in GameObject.FindGameObjectsWithTag("obstacle"))
        {
            Destroy(obj1);
        }

        foreach (GameObject obj2 in GameObject.FindGameObjectsWithTag("stamina"))
        {
            Destroy(obj2);
        }

        foreach (GameObject obj3 in GameObject.FindGameObjectsWithTag("foreground"))
        {
            Destroy(obj3);
        }

        Time.timeScale = 1;
    }

    public void ShowLeaderboardForm()
    {
        leaderboardForm.SetActive(true);
        DataManager.instance.UpdateLeaderboardUI();
    }

    public void HideLeaderboardForm()
    {
        leaderboardForm.SetActive(false);
    }

    public void HideNewRecordForm()
    {
        newRecordForm.SetActive(false);
    }

    public void OnHigherScoreSubmit()
    {
        if (dataManager == null)
        {
            Debug.LogError("DataManager instance not found!");
            return;
        }

        // Get the PlayerNameInputManager component from the scene
        if (playerNameInputManager == null)
        {
            playerNameInputManager = FindAnyObjectByType<PlayerNameInputManager>();
        }

        if (playerNameInputManager == null)
        {
            Debug.LogError("PlayerNameInputManager not found!");
            return;
        }
        string name = playerNameInputManager.GetInput();
        int higherScore = GetScore();
        dataManager.AddToTheLeaderboard(name, higherScore);
        Debug.Log($"Added to leaderboard: {name} with score {higherScore}");
        newRecordForm.SetActive(false);
    }

    public int GetScore()
    {
        return score;
    }
}
