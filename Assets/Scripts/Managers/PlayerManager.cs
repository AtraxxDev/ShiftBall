using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TB_Tools;

public class PlayerManager : Unit
{
    [SerializeField] private bool isMovingLeft;
    [SerializeField] private bool isInvincible = false;
    public bool IsSpeedBoostActive { get; private set; }

    [Header("Invincibility Settings")]
    [SerializeField] private Collider2D invincibilityCollider;
    [SerializeField] private SpriteRenderer sp;

    // Start is called before the first frame update
    void Start()
    {
        isMovingLeft = true;
        direction = new Vector3(isMovingLeft ? -1 : 1, 1, 0).normalized;

        
    }

    // FixedUpdate is called at a fixed interval and is used for physics updates
    void FixedUpdate()
    {
        if (GameManager.Instance.IsPaused()) return;

        float deltaSpeed = speed * Time.fixedDeltaTime;
        transform.Translate(direction * deltaSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsPaused()) return;

        if (IsSpeedBoostActive)
        {
            // Movimiento solo vertical
            direction = Vector3.up;
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ToggleDirection();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsSpeedBoostActive && collision.gameObject.CompareTag("Wall"))
        {
            ToggleDirection();
        }
    }

    public void ToggleDirection()
    {
        isMovingLeft = !isMovingLeft;
        direction = new Vector3(isMovingLeft ? -1 : 1, 1, 0).normalized;
        AudioManager.Instance.PlayWallHitSound();
    }

    public void ActivatePowerUp(PowerUpType powerUpType, float duration)
    {
        StartCoroutine(PowerUpRoutine(powerUpType, duration));
    }

    private IEnumerator PowerUpRoutine(PowerUpType powerUpType, float duration)
    {
        switch (powerUpType)
        {
            case PowerUpType.Invincibility:
                isInvincible = true;
                // Asegúrate de que el collider de invencibilidad esté activo
                invincibilityCollider.enabled = true;
                sp.enabled = true;
                break;

            case PowerUpType.SpeedBoost:
                IsSpeedBoostActive = true;
                speed *= 2; // Aumenta la velocidad
                transform.position = new Vector3(0, transform.position.y, transform.position.z); // Lleva al jugador a x=0
                direction = Vector3.up; // Movimiento solo vertical

                // También activa la invencibilidad
                isInvincible = true;
                invincibilityCollider.enabled = true;
                break;
        }

        yield return new WaitForSeconds(duration);

        // Revertir el efecto del power-up
        switch (powerUpType)
        {
            case PowerUpType.Invincibility:
                isInvincible = false;
                sp.enabled = false;
                invincibilityCollider.enabled = false;
                break;

            case PowerUpType.SpeedBoost:
                IsSpeedBoostActive = false;
                speed /= 2; // Restablecer la velocidad
                direction = new Vector3(isMovingLeft ? -1 : 1, 1, 0).normalized; // Restaurar el movimiento diagonal

                // Desactiva la invencibilidad cuando el Speed Boost termina
                isInvincible = false;
                invincibilityCollider.enabled = false;
                break;
        }
    }
}
