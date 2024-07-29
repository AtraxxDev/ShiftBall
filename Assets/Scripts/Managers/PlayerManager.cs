using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TB_Tools;

public class PlayerManager : Unit
{
    [SerializeField] private bool isMovingLeft;
    [SerializeField] private bool isInvincible = false;
    public bool IsSpeedBoostActive { get; private set; }
    public bool IsMagnetActive { get; private set; } // Indica si el imán está activo

    [Header("Invincibility Settings")]
    [SerializeField] private Collider2D invincibilityCollider;
    [SerializeField] private SpriteRenderer sp;

    [Header("Magnet Settings")]
    [SerializeField] private float magnetRange = 2.6f; // Rango del imán
    [SerializeField] private float magnetStrength = 10f; // Fuerza del imán

    [Header("Particle System")]
    [SerializeField] private int particleEffectID;

    [Header("Camera Shake Settings")]
    [SerializeField] private ShakeCamera shakeCamera;


    private void OnEnable()
    {
        if (ParticleManager.Instance != null)
        {
            ParticleManager.Instance.OnParticleEffectChanged += UpdateParticleEffectID;
        }
    }

    private void OnDisable()
    {
        if (ParticleManager.Instance != null)
        {
            ParticleManager.Instance.OnParticleEffectChanged -= UpdateParticleEffectID;
        }
    }

    private void Start()
    {
        isMovingLeft = true;
        direction = new Vector3(isMovingLeft ? -1 : 1, 1, 0).normalized;

       
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.IsPaused()) return;

        float deltaSpeed = speed * Time.fixedDeltaTime;
        transform.Translate(direction * deltaSpeed);

        if (IsMagnetActive)
        {
            AttractCoins();
        }
    }

    private void Update()
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
                invincibilityCollider.enabled = true;
                sp.enabled = true;
                break;

            case PowerUpType.SpeedBoost:
                IsSpeedBoostActive = true;
                speed *= 2; // Aumenta la velocidad
                transform.position = new Vector3(0, transform.position.y, transform.position.z); // Lleva al jugador a x=0
                direction = Vector3.up; // Movimiento solo vertical

                isInvincible = true;
                invincibilityCollider.enabled = true;
                break;

            case PowerUpType.Magnet:
                IsMagnetActive = true;
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

                isInvincible = false;
                invincibilityCollider.enabled = false;
                break;

            case PowerUpType.Magnet:
                IsMagnetActive = false;
                break;
        }
    }

    private void AttractCoins()
    {
        Collider2D[] coins = Physics2D.OverlapCircleAll(transform.position, magnetRange, LayerMask.GetMask("Coin"));

        foreach (Collider2D coin in coins)
        {
            Vector3 directionToPlayer = (transform.position - coin.transform.position).normalized;
            coin.transform.position += directionToPlayer * magnetStrength * Time.fixedDeltaTime;
        }
    }

    private void UpdateParticleEffectID(int newParticleEffectID)
    {
        particleEffectID = newParticleEffectID;
    }

    public void PlayGameOverParticles()
    {
        // Reproduce el efecto de partículas cuando el juego termine
        if (ParticleManager.Instance != null)
        {
            gameObject.SetActive(false);
            ParticleManager.Instance.PlayParticleEffect(particleEffectID, transform.position);
        }
        if (shakeCamera != null)
        {
            shakeCamera.Shake();
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Dibuja una esfera amarilla para representar el rango del imán
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, magnetRange);
    }
}
