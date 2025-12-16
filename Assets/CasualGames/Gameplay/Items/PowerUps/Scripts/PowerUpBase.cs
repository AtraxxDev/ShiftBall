using System;
using System.Collections;
using TB_Tools;
using UnityEngine;

public abstract class PowerUpBase : MonoBehaviour
{
    public static event Action<PowerUpType,Sprite,float> OnPowerUpActivate;
    public static event Action<PowerUpType> OnPowerUpDeactivate;



    public PowerUPSO powerUpData;


    public bool isActive;

    public float timerRemaining;
    public virtual void OnActivate()
    {
        isActive = true;
        timerRemaining = powerUpData.duration;

        OnPowerUpActivate?.Invoke(powerUpData.powerUpType, powerUpData.icon, powerUpData.duration);
        AudioManager.Instance.PlaySFX("PowerUp");
        Debug.Log($"Power-Up Activado: {powerUpData.powerUpType}");
    }
    public virtual void OnDeactivate()
    {
        isActive = false;
        OnPowerUpDeactivate?.Invoke(powerUpData.powerUpType);
        Debug.Log($"Power-Up Desactivado: {powerUpData.powerUpType}");
    }


}
