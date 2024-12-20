using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TB_Tools;

public class PowerUpController : MonoBehaviour
{
    [SerializeField] private bool isInvincible = false;
    public bool IsSpeedBoostActive { get; private set; }
    public bool IsMagnetActive { get; private set; }

    [Header("Invincibility Settings")]
    [SerializeField] private Collider2D invincibilityCollider;
    [SerializeField] private SpriteRenderer invincibilitySprite;

    [Header("Magnet Settings")]
    [SerializeField] private float magnetRange = 2.6f;
    [SerializeField] private float magnetStrength = 10f;

    [SerializeField] private PlayerMovement playerMovement;

    private Coroutine currentPowerUpCoroutine;

    public void ActivatePowerUp(PowerUpType powerUpType, float duration)
    {
        // Si ya hay un power-up activo, detén la coroutine actual
        if (currentPowerUpCoroutine != null)
        {
            StopCoroutine(currentPowerUpCoroutine);
            DeactivatePowerUp(powerUpType); // Revertir efecto actual
        }
        currentPowerUpCoroutine = StartCoroutine(PowerUpRoutine(powerUpType, duration));
    }

    private IEnumerator PowerUpRoutine(PowerUpType powerUpType, float duration)
    {
        ApplyPowerUpEffect(powerUpType);

        yield return new WaitForSeconds(duration);

        DeactivatePowerUp(powerUpType);
        currentPowerUpCoroutine = null;
    }

    private void ApplyPowerUpEffect(PowerUpType powerUpType)
    {
        switch (powerUpType)
        {
            case PowerUpType.Invincibility:
                isInvincible = true;
                invincibilityCollider.enabled = true;
                invincibilitySprite.enabled = true;
                break;

            case PowerUpType.SpeedBoost:
                IsSpeedBoostActive = true;
                playerMovement.speed *= 2;
                transform.parent.position = new Vector3(0, transform.parent.position.y, transform.parent.position.z); // Lleva al jugador a x=0
                playerMovement.SetVerticalDirectionOnly();
                ActivateInvincibility(); // Aumenta protección en SpeedBoost
                break;

            case PowerUpType.Magnet:
                IsMagnetActive = true;
                break;
        }
    }

    private void DeactivatePowerUp(PowerUpType powerUpType)
    {
        switch (powerUpType)
        {
            case PowerUpType.Invincibility:
                isInvincible = false;
                invincibilityCollider.enabled = false;
                invincibilitySprite.enabled = false;
                break;

            case PowerUpType.SpeedBoost:
                IsSpeedBoostActive = false;
                playerMovement.speed /= 2;
                playerMovement.RestoreDiagonalMovement();
                DeactivateInvincibility(); // Revertir protección extra
                break;

            case PowerUpType.Magnet:
                IsMagnetActive = false;
                break;
        }
    }

    private void ActivateInvincibility()
    {
        isInvincible = true;
        invincibilityCollider.enabled = true;
    }

    private void DeactivateInvincibility()
    {
        isInvincible = false;
        invincibilityCollider.enabled = false;
    }

    private void FixedUpdate()
    {
        if (IsMagnetActive)
        {
            AttractCoins();
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
}
