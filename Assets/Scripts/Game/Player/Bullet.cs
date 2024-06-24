using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private new Camera camera;

    private void Awake()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        DestroyWhenOffScreen();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyMovement enemy = collision.GetComponent<EnemyMovement>();
        if (enemy != null)
        {
            enemy.Die();
            Destroy(gameObject);
        }
    }

    private void DestroyWhenOffScreen()
    {
        Vector2 screenPosition = camera.WorldToScreenPoint(transform.position);

        if (screenPosition.x < 0 || screenPosition.x > camera.pixelWidth ||
            screenPosition.y < 0 || screenPosition.y > camera.pixelHeight)
        {
            Destroy(gameObject);
        }
    }
}






