using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static event Action OnGameOverParticles;

    public static void RaiseGameOverParticles()
    {
        OnGameOverParticles?.Invoke();
    }
    
}
