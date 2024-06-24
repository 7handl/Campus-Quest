using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab;
    [SerializeField]
    private float _bulletSpeed;
    [SerializeField]
    private Transform _gunOffset;
    [SerializeField]
    private float timeBetweenShots = 0.5f; // Zeit zwischen Schüssen, um Dauerfeuer zu verhindern
    private bool isFiring;
    private float lastFireTime;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (!enabled) return; // Überprüfen, ob die Komponente aktiviert ist

        if (isFiring)
        {
            float timeSinceLastFire = Time.time - lastFireTime;

            if (timeSinceLastFire >= timeBetweenShots)
            {
                FireBullet();
                lastFireTime = Time.time;
            }
        }
    }

    private void FireBullet()
    {
        if (!enabled) return; // Überprüfen, ob die Komponente aktiviert ist

        // Holen der Mausposition
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        // Umwandeln der Mausposition in Weltkoordinaten
        Vector3 worldMousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
        worldMousePosition.z = 0; // Z-Komponente auf 0 setzen, da wir uns in 2D befinden

        // Berechnen der Richtung vom Gewehr zur Maus
        Vector2 direction = (worldMousePosition - _gunOffset.position).normalized;
        // Berechnen des Winkels in Grad
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Erstellen der Quaternion-Rotation
        Quaternion bulletRotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Instanziieren des Geschosses
        GameObject bullet = Instantiate(_bulletPrefab, _gunOffset.position, bulletRotation);
        // Holen des Rigidbody2D des Geschosses
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();

        // Setzen der Geschwindigkeit des Geschosses
        rigidbody.velocity = direction * _bulletSpeed;
    }

    private void OnFire(InputValue inputValue)
    {
        if (!enabled) return; // Überprüfen, ob die Komponente aktiviert ist

        isFiring = inputValue.isPressed;
        if (isFiring)
        {
            // Schieße sofort und setze den letzten Schusszeitpunkt
            FireBullet();
            lastFireTime = Time.time;
        }
    }
}










