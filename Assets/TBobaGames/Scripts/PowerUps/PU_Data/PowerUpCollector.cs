using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TB_Tools;

public class PowerUpCollector : MonoBehaviour
{
    public PowerUpBase[] powerUps;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PowerUp"))
        {
            PowerUPSO powerUp = other.GetComponent<PowerUpItem>().powerUpData;
            Debug.Log($"Agarre {powerUp.powerUpType}");
            switch (powerUp.powerUpType)
            {
                case PowerUpType.Magnet:
                    powerUps[0].OnActivate();
                    break;
                case PowerUpType.Shield:
                    powerUps[1].OnActivate();
                    break;
                case PowerUpType.ExpandigWave:
                    powerUps[2].OnActivate();
                    break;

                // Agrega m�s tipos de power-ups aqu�

                default:
                    Debug.LogWarning("PowerUp no manejado: " + powerUp.powerUpType);
                    break;
            }

            Destroy(other.gameObject); // Destruir el power-up tras ser recogido
        }
    }
}
