using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameResume : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private GameObject pauseUI;

    private bool isCounting = false;
    public void ResumingPerformance()
    {
        if (!isCounting)
        {
            StartCoroutine(Countdown());
        }
    }

    private IEnumerator Countdown()
    {
        isCounting = true;
        pauseUI.SetActive(false);
        countdownText.gameObject.SetActive(true);

        int count = 3;
        while (count >= 0)
        {
            countdownText.text = count.ToString();
            yield return new WaitForSecondsRealtime(1f);
            count--;
        }

        pauseUI.SetActive(true);
        countdownText.gameObject.SetActive(false);
        gameObject.SetActive(false);
        isCounting = false;
        Time.timeScale = 1;
    }
}
