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
            PlayerManager playerManager = other.GetComponent<PlayerManager>();
            
                playerManager.ActivatePowerUp(powerUpType, duration);
                gameObject.SetActive(false);
            
        }
    }
}