using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private float currentHealth;
    [SerializeField]
    private float maximumHealth;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Sprite deathSprite;
    [SerializeField]
    private HealthbarUI healthbarUI;

    private Rigidbody2D rb2d;
    private Collider2D col2d;

    private GameOverManager gameOverManager;

    public float RemainingHealthPercentage => currentHealth / maximumHealth;

    public bool IsInvincible { get; set; }

    public UnityEvent OnDied;
    public UnityEvent OnDamaged;
    public UnityEvent OnHealthChanged;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        col2d = GetComponent<Collider2D>();

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer is not assigned!");
        }

        if (healthbarUI == null)
        {
            Debug.LogError("HealthbarUI is not assigned!");
        }

        gameOverManager = GameOverManager.instance;
        if (gameOverManager == null)
        {
            Debug.LogError("GameOverManager is not assigned!");
        }
    }

    private void Start()
    {
        UpdateHealthUI();
    }

    public void TakeDamage(float damageAmount)
    {
        if (currentHealth == 0)
        {
            return;
        }

        if (IsInvincible)
        {
            return;
        }

        currentHealth -= damageAmount;
        Debug.Log("Player took damage, current health: " + currentHealth);

        OnHealthChanged.Invoke();
        UpdateHealthUI();

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        if (currentHealth == 0)
        {
            Die();
        }
        else
        {
            OnDamaged.Invoke();
        }
    }

    public void AddHealth(float amountToAdd)
    {
        if (currentHealth == maximumHealth)
        {
            return;
        }

        currentHealth += amountToAdd;
        Debug.Log("Player healed, current health: " + currentHealth);

        OnHealthChanged.Invoke();
        UpdateHealthUI();

        if (currentHealth > maximumHealth)
        {
            currentHealth = maximumHealth;
        }
    }

    private void Die()
    {
        if (spriteRenderer != null && deathSprite != null)
        {
            spriteRenderer.sprite = deathSprite;
        }

        if (rb2d != null)
        {
            rb2d.bodyType = RigidbodyType2D.Kinematic;
        }

        if (col2d != null)
        {
            col2d.enabled = false;
        }

        OnDied.Invoke();
        Debug.Log("Player died");

        if (gameOverManager != null)
        {
            gameOverManager.OnPlayerDeath();
        }
        else
        {
            Debug.LogError("GameOverManager is not assigned or OnPlayerDeath method is missing!");
        }
    }

    private void UpdateHealthUI()
    {
        if (healthbarUI != null)
        {
            healthbarUI.UpdateHealthBar(this);
        }
        else
        {
            Debug.LogError("HealthbarUI is not assigned!");
        }
    }
}
















