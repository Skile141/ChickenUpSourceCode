using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour
{
    [Header("Chicken Info")]
    [SerializeField] private StaminaController staminaController;
    [SerializeField] private AudioManager flyingSound;
    [SerializeField] private float maxStamina;
    [SerializeField] private float currentStamina;
    [SerializeField] private int consumedStamina;
    [SerializeField] private float chickenSize;

    [Header("Jump Handler")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float delayAfterClicking;

    [Header("Sprite Animation")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite tiredSprite;

    private Animator animator;
    private Rigidbody2D rb2D;
    private bool isDead = false;
    private bool canFly = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        currentStamina = maxStamina;
        staminaController.SetMaxHealth(maxStamina);
        if (currentStamina > 0 && !animator.enabled)
        {
            animator.enabled = true;
        }
    }

    void Update()
    {
        if (!isDead)
        {
            HandleTouchInput();          
        }     
    }

    private void HandleTouchInput()
    {
        if (Time.timeScale == 0f) return;

        Vector2 inputPosition = Vector2.zero;
        bool isPressed = false;
        bool isTap = false;

        if (Input.GetMouseButton(0))
        {
            inputPosition = Input.mousePosition;
            isPressed = true;
            isTap = Input.GetMouseButtonDown(0);
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            inputPosition = touch.position;
            isPressed = true;
            isTap = (touch.phase == TouchPhase.Began);
        }

        if (isPressed) { 

            float screenWidth = Screen.width;

            if (inputPosition.x < screenWidth / 2)
            {
                // Left side touched - move left
                Move(Vector2.left);
                transform.localScale = new Vector3(chickenSize, chickenSize, 1);
            }
            else if (inputPosition.x > screenWidth / 2)
            {
                // Right side touched - move right
                Move(Vector2.right);
                transform.localScale = new Vector3(-chickenSize, chickenSize, 1);
            }
            else
            {
                int randomPos = UnityEngine.Random.Range(1,3);
                if (randomPos == 1)
                {
                    Move(Vector2.left);
                    transform.localScale = new Vector3(chickenSize, chickenSize, 1);
                }
                else
                {
                    Move(Vector2.right);
                    transform.localScale = new Vector3(-chickenSize, chickenSize, 1);
                }
            }

            // If touch just began, apply upward force (flying)
            if (canFly && isTap)
            {
                // Apply upward force for flying
                rb2D.linearVelocity = Vector2.up * jumpForce;
                StartCoroutine(FlyCooldown());
                DecreaseStamina();
                flyingSound.PlayFlySound();
            }
        }
    }

    private IEnumerator FlyCooldown()
    {
        canFly = false;
        yield return new WaitForSeconds(delayAfterClicking);
        canFly = true;
    }

    private void Move(Vector2 direction)
    {
        rb2D.linearVelocity = new Vector2(direction.x * moveSpeed, rb2D.linearVelocity.y);
    }

    public void Die()
    {
        if (!isDead)
        {
            isDead = true;
            currentStamina = 0;
            ForceDead();
            staminaController.SetStamina(0);
        }
    }
    public void ForceDead()
    {
        if (currentStamina <= 0)
        {
            rb2D.linearVelocity = Vector2.zero;
            animator.enabled = false;
            spriteRenderer.sprite = tiredSprite;
            return;  
        }
    }

    private void DecreaseStamina()
    {
        currentStamina -= consumedStamina;
        staminaController.SetStamina(currentStamina);
        if (currentStamina <= 0)
        {
            Die();
        }
    }

    public void IncreaseStamina(float healPoint)
    {
        if (isDead) return;

        currentStamina += healPoint;
        if (currentStamina > maxStamina)
        {
            currentStamina = maxStamina;
        }
        staminaController.SetStamina(currentStamina);
    }

    public void ResetStamina()
    {
        currentStamina = maxStamina;
        staminaController.SetStamina(currentStamina);
        isDead = false;
        animator.enabled = true;
    }
}