using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    protected PlayerController playerController;
    protected AudioManager audioManager;
    protected float speed;
    protected float speedIncreaseRate = 0.2f;
    protected float timeToIncrease = 15f;
    private float timer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        IncreaseSpeedOverTime();
        Move(speed);
        Destroy(gameObject, 5f);
    }

    protected virtual void Move(float speed)
    {
        if (gameObject.transform.localScale.x <= 0)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

    }

    private void IncreaseSpeedOverTime()
    {
        timer += Time.deltaTime;
        if (timer >= timeToIncrease)
        {
            speed += speedIncreaseRate;
            timer = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (playerController != null)
            {
                audioManager.PlayHittedSound();
                playerController.Die();
            }
            else
            {
                Debug.Log("Cannot find Player");
            }
        }
    }

}
