using System;
using System.Collections;
using TB_Tools;
using UnityEngine;

public abstract class PowerUpBase : MonoBehaviour
{
    public static event Action<PowerUpType,Sprite,float> OnPowerUpActivate;

    public PowerUPSO powerUpData;


    public bool isActive;

    public float timerRemaining;
    public virtual void OnActivate()
    {
        isActive = true;
        timerRemaining = powerUpData.duration;

        OnPowerUpActivate?.Invoke(powerUpData.powerUpType, powerUpData.icon, powerUpData.duration);
    } 
    public abstract void OnDeactivate();


}
