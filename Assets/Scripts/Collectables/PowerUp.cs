using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TB_Tools;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private PowerUpType powerUpType;
    [SerializeField] private float duration = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Busca el componente PowerUpController en los hijos del objeto "Player"
            PowerUpController powerUpController = other.GetComponentInChildren<PowerUpController>();
            if (powerUpController != null)
            {
                // Activa el power-up
                powerUpController.ActivatePowerUp(powerUpType, duration);
                gameObject.SetActive(false); // Desactiva este objeto
            }
            else
            {
                Debug.LogWarning("PowerUpController no encontrado en los hijos del jugador.");
            }
        }
    }
}
