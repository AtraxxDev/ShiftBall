using Sirenix.OdinInspector;
using UnityEngine;

public class PanelShop : MonoBehaviour
{
    [BoxGroup("Reference")]
    public FadeSystem fadeSystem;
    public float fadeWaitTime = 0.2f;

    public void ActivateShop()
    {
        fadeSystem.FadeInOut(fadeWaitTime,
            onFadeInComplete: () =>
            {
                EventManager.TriggerEvent("OnOpenMenu",false);
                EventManager.TriggerEvent("OnOpenShop",true);
                print("Shop activated");
            });
    }

    public void ExitShop()
    {
        fadeSystem.FadeInOut(fadeWaitTime,
            onFadeInComplete: () =>
            {
                EventManager.TriggerEvent("OnOpenMenu",true);
                EventManager.TriggerEvent("OnOpenShop",false);
                print("Shop deactivated");
            });
    }


    public void ExitSettings()
    {
        fadeSystem.FadeInOut(fadeWaitTime, () =>
        {
            Debug.Log("Settings Exited");
        });
    }

   
    public void Activate_Page_Settings()
    {
       
    }
}
