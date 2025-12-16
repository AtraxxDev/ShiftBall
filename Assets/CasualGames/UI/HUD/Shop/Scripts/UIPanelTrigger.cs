using UnityEngine;

public class UIPanelTrigger : MonoBehaviour
{
    public FadeSystem fadeSystem;
    public float fadeWaitTime = 0.2f;

    [Header("Events to Trigger")]
    public string eventToDisable;   // Ej: "OnOpenMenu"
    public string eventToEnable;    // Ej: "OnOpenShop"

    public void Trigger()
    {
        fadeSystem.FadeInOut(fadeWaitTime,
            onFadeInComplete: () =>
            {
                if (!string.IsNullOrEmpty(eventToDisable))
                    EventManager.TriggerEvent(eventToDisable, false);

                if (!string.IsNullOrEmpty(eventToEnable))
                    EventManager.TriggerEvent(eventToEnable, true);
            });
    }
}