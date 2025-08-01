using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource backgroundSound;
    [SerializeField] private AudioClip buttonClick;
    [SerializeField] private AudioClip flySound;
    [SerializeField] private AudioClip earnPointSound;
    [SerializeField] private AudioClip hittedSound;
    [SerializeField] private AudioClip bulletSound;
    [SerializeField] private AudioClip gameOverSound;
    private SoundSystem soundSystem;

    private void Start()
    {
        soundSystem = FindAnyObjectByType<SoundSystem>();
    }
    // Background sound
    public void PlayBackgroundSound()
    {
        backgroundSound.Play();
    }
    public void StopBackgroundSound()
    {
        if (backgroundSound.isPlaying)
        {
            backgroundSound.Pause(); // Pause instead of Stop to allow resuming
        }
    }

    public void ResumeBackgroundSound()
    {
        if (!backgroundSound.isPlaying)
        {
            backgroundSound.UnPause(); // Resumes from where it was paused
        }
    }

    // Button sound
    public void PlayButtonSound()
    {
        audioSource.PlayOneShot(buttonClick);
    }

    // sound effects
    public void PlayFlySound()
    {
        audioSource.PlayOneShot(flySound);
    }

    public void PlayHittedSound()
    {
        audioSource.PlayOneShot(hittedSound);
    }

    public void PlayPointSound()
    {
        audioSource.PlayOneShot(earnPointSound);
    }

    public void PlayBulletSound()
    {
        audioSource.PlayOneShot(bulletSound);
    }

    public void PlayGameOverSound()
    {
        audioSource.PlayOneShot(gameOverSound);
    }

    public void SoundOnAndOff()
    {
        soundSystem.OnPressButton();
        soundSystem.UpdateButtonIcon();
    }
}
