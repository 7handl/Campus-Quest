using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float wanderChangeInterval = 2.0f;
    private new Rigidbody2D rigidbody;
    private PlayerAwarenessController playerAwarenessController;
    private Vector2 targetDirection;
    private Vector2 wanderDirection;
    private float wanderTimer;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite upSprite;
    [SerializeField] private Sprite downSprite;
    [SerializeField] private Sprite leftSprite;
    [SerializeField] private Sprite rightSprite;
    [SerializeField] private Sprite deathSprite;

    private Camera mainCamera;
    private bool isDead = false;

    public UnityEvent OnEnemyDeath;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        playerAwarenessController = GetComponent<PlayerAwarenessController>();
        mainCamera = Camera.main;
        wanderTimer = wanderChangeInterval;
        SetRandomWanderDirection();
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        wanderTimer -= Time.deltaTime;

        if (wanderTimer <= 0)
        {
            SetRandomWanderDirection();
            wanderTimer = wanderChangeInterval;
        }

        UpdateTargetDirection();
        SetVelocity();
        UpdateSprite();
        CheckOffScreen();
    }

    private void UpdateTargetDirection()
    {
        if (playerAwarenessController.AwareOfPlayer)
        {
            targetDirection = playerAwarenessController.DirectionToPlayer;
        }
        else
        {
            targetDirection = wanderDirection;
        }
    }

    private void SetVelocity()
    {
        rigidbody.velocity = targetDirection.normalized * speed;
    }

    private void UpdateSprite()
    {
        if (targetDirection != Vector2.zero)
        {
            Vector2 direction = targetDirection.normalized;
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                spriteRenderer.sprite = direction.x > 0 ? rightSprite : leftSprite;
            }
            else
            {
                spriteRenderer.sprite = direction.y > 0 ? upSprite : downSprite;
            }
        }
    }

    private void SetRandomWanderDirection()
    {
        wanderDirection = Random.insideUnitCircle.normalized;
    }

    private void CheckOffScreen()
    {
        Vector3 position = transform.position;
        Vector3 viewPos = mainCamera.WorldToViewportPoint(position);

        if (viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1)
        {
            Vector2 screenCenter = mainCamera.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
            wanderDirection = (screenCenter - (Vector2)transform.position).normalized;
        }
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        if (spriteRenderer != null && deathSprite != null)
        {
            spriteRenderer.sprite = deathSprite;
        }

        if (rigidbody != null)
        {
            rigidbody.velocity = Vector2.zero;
            rigidbody.bodyType = RigidbodyType2D.Kinematic;
        }

        Collider2D col2d = GetComponent<Collider2D>();
        if (col2d != null)
        {
            col2d.enabled = false;
        }

        OnEnemyDeath.Invoke();

        Destroy(gameObject, 2f);
    }
}



















