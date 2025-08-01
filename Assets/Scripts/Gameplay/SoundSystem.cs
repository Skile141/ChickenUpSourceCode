using UnityEngine;
using UnityEngine.UI;

public class SoundSystem : MonoBehaviour
{
    [SerializeField] private Image muteButton;
    [SerializeField] private Image unmuteButton;
    private bool isMuted = false;

    private void Start()
    {
        UpdateButtonIcon();
        AudioListener.pause = isMuted;
    }
    public void OnPressButton()
    {
        if (isMuted == false)
        {
            isMuted = true;
            AudioListener.pause = true;
        }
        else
        {
            isMuted = false;
            AudioListener.pause = false;
        }
        UpdateButtonIcon();
    }

    public void UpdateButtonIcon()
    {
        if (isMuted == false)
        {
            muteButton.enabled = true;
            unmuteButton.enabled = false;

        }
        else
        {
            muteButton.enabled = false;
            unmuteButton.enabled = true;
        }
    }    
}
