using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnentPowerUp : PowerUpBase
{
    public float attractionRadius = 2.5f;  
    public float magnetSpeed = 7f;

    void Update()
    {
        if (isActive)
        {
            timerRemaining -= Time.deltaTime;

            // Atraer monedas si el power-up est� activo
            AttractCoins();

            // Desactivar el im�n cuando se acabe el tiempo
            if (timerRemaining <= 0)
            {
                OnDeactivate();
            }
        }
    }

    public override void OnActivate()
    {
        base.OnActivate();
        Debug.Log("Magnet Activated");
        // Aqu� puedes agregar efectos visuales si es necesario
    }

    public override void OnDeactivate()
    {
        isActive = false;
        // Desactivar efectos visuales si es necesario
    }

    private void AttractCoins()
    {
        Collider2D[] coins = Physics2D.OverlapCircleAll(transform.position, attractionRadius, LayerMask.GetMask("Coin"));
        foreach (Collider2D coin in coins)
        {
            Vector3 directionToPlayer = (transform.position - coin.transform.position).normalized;
            coin.transform.position += directionToPlayer * magnetSpeed * Time.fixedDeltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attractionRadius);
    }
}
