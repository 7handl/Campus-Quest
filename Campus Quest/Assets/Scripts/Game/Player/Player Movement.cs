using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private Vector2 movementInput;
    private Vector2 smoothMovementInput;
    private Vector2 movementInputSmoothVelocity;
    [SerializeField]
    private float speed;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Sprite[] sprites;

    private Camera mainCamera;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        SetPlayerVelocity();
        UpdateSprite();
        ClampPosition();
    }

    private void SetPlayerVelocity()
    {
        smoothMovementInput = Vector2.SmoothDamp(smoothMovementInput,
                    movementInput,
                    ref movementInputSmoothVelocity, 0.1f);

        rigidbody.velocity = smoothMovementInput * speed;
    }

    private void UpdateSprite()
    {
        if (movementInput != Vector2.zero)
        {
            // Bestimmen der Hauptbewegungsrichtung (oben, unten, links, rechts)
            if (Mathf.Abs(movementInput.x) > Mathf.Abs(movementInput.y))
            {
                // Bewegung ist mehr horizontal als vertikal
                if (movementInput.x > 0)
                    SetSprite(3); // rechts
                else
                    SetSprite(2); // links
            }
            else
            {
                // Bewegung ist mehr vertikal als horizontal
                if (movementInput.y > 0)
                    SetSprite(0); // oben
                else
                    SetSprite(1); // unten
            }
        }
    }

    private void SetSprite(int spriteIndex)
    {
        spriteRenderer.sprite = sprites[spriteIndex];
    }

    private void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
    }

    private void ClampPosition()
    {
        Vector3 position = transform.position;
        Vector3 viewPos = mainCamera.WorldToViewportPoint(position);

        // Clamping the position within the screen bounds
        viewPos.x = Mathf.Clamp(viewPos.x, 0.05f, 0.95f); // Adding a margin of 5% on both sides
        viewPos.y = Mathf.Clamp(viewPos.y, 0.05f, 0.95f); // Adding a margin of 5% on top and bottom

        transform.position = mainCamera.ViewportToWorldPoint(viewPos);
    }
}
