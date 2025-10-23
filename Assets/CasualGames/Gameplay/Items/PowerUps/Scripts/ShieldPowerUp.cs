using System;
using System.Collections;
using System.Collections.Generic;
using TB_Tools;
using UnityEngine;

public class ShieldPowerUp : PowerUpBase
{
    void Update()
    {
        if (isActive)
        {
            timerRemaining -= Time.deltaTime;

            if (timerRemaining <= 0)
            {
                OnDeactivate();
            }
        }
    }

    public override void OnActivate()
    {
        base.OnActivate();
        Debug.Log("Shield Activated");

        // Aquí puedes activar efectos visuales, como un aura de escudo
        EnableShieldVisual(true);
    }

    public override void OnDeactivate()
    {
        base.OnDeactivate();
        // Desactivar efectos visuales del escudo
        Debug.Log("ShieldPowerUp desactivado.");
        EnableShieldVisual(false);
    }

    public bool AbsorbHit()
    {
        if (isActive)
        {
            Debug.Log("El escudo está activo y absorbió el golpe.");
            OnDeactivate(); 
            return true; // Golpe absorbido
        }

        return false; // El golpe no fue absorbido
    }

    private void EnableShieldVisual(bool isEnabled)
    {
        Transform shieldVisual = transform.root.Find("ShieldVisual");
        if (shieldVisual != null)
        {
            SpriteRenderer sprite = shieldVisual.gameObject.GetComponent<SpriteRenderer>();
            if (sprite != null)
            {
                sprite.enabled = isEnabled; // Activa o desactiva el sprite

            }
        }
        else
        {
            Debug.LogWarning("ShieldVisual no encontrado en el jugador.");
        }
    }
}
