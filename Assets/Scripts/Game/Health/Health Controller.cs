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
    private SpriteRenderer spriteRenderer; // Der Sprite Renderer des Charakters
    [SerializeField]
    private Sprite deathSprite; // Das Sprite, das beim Tod angezeigt wird
    [SerializeField]
    private HealthbarUI healthbarUI; // Referenz zur Healthbar UI

    private Rigidbody2D rb2d; // Referenz zum Rigidbody2D des Spielers
    private Collider2D col2d; // Referenz zum Collider2D des Spielers

    public float RemainingHealthPercentage
    {
        get
        {
            return currentHealth / maximumHealth;
        }
    }

    public bool IsInvincible { get; set; }

    public UnityEvent OnDied;

    public UnityEvent OnDamaged;

    public UnityEvent OnHealthChanged;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        col2d = GetComponent<Collider2D>();
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

        // Set Rigidbody2D to Kinematic to prevent movement
        if (rb2d != null)
        {
            rb2d.bodyType = RigidbodyType2D.Kinematic;
        }

        // Disable Collider2D to prevent further collisions
        if (col2d != null)
        {
            col2d.enabled = false;
        }

        OnDied.Invoke();
    }

    private void UpdateHealthUI()
    {
        if (healthbarUI != null)
        {
            healthbarUI.UpdateHealthBar(this);
        }
    }
}
