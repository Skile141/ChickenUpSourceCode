using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        if (audioManager != null)
        {
            Debug.Log("Founded!");
        }
        else
        {
            Debug.Log("No audio available");
        }
    }

    public void StartGameButton()
    {
        audioManager.PlayButtonSound();
        gameManager.NewGame();
    }

    public void PauseGame()
    {
        gameManager.GamePause();
        audioManager.PlayButtonSound();
    }

    public void QuitGame()
    {
        audioManager.PlayButtonSound();
        Application.Quit();
    }


    // Leaderboard functions
    public void ShowLeaderboard()
    {
        audioManager.PlayButtonSound();
        gameManager.ShowLeaderboardForm();
    }

    public void HideLeaderboard()
    {
        audioManager.PlayButtonSound();
        gameManager.HideLeaderboardForm();
    }

    // Add and ignore new higher score
    public void SubmitRecord()
    {
        audioManager.PlayButtonSound();
        gameManager.OnHigherScoreSubmit();
        HideRecordForm();
    }

    public void HideRecordForm()
    {
        audioManager.PlayButtonSound();
        gameManager.HideNewRecordForm();
    }

    public void MainMenuButton()
    {
        audioManager.PlayButtonSound();
        gameManager.MainMenu();
    }
}
