using UnityEngine;

public class StaminaHealingItem : MonoBehaviour
{
    protected PlayerController playerController;
    protected AudioManager audioManager;
    protected float healPoint;
    protected int scoreItem;
    protected float fallingSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    protected virtual void Update()
    {
        ItemMove(fallingSpeed);
    }

    public void ItemMove(float speed)
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);

    }

    public void SetStaminaStatus()
    {
        playerController.IncreaseStamina(healPoint);
        GameManager.gameManager.AddScore(scoreItem);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController = collision.GetComponent<PlayerController>();
            if (playerController != null)
            {
                SetStaminaStatus();
                audioManager.PlayPointSound();
                Destroy(gameObject);
            }
        }
    }

    
}
