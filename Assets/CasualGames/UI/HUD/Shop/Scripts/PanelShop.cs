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
    
    
    public void EnterSetting()
    {
        fadeSystem.FadeInOut(fadeWaitTime,
            onFadeInComplete: () =>
            {
                EventManager.TriggerEvent("OnOpenMenu",false);
                EventManager.TriggerEvent("OnOpenSettings",true);
                print("Settings activated");
            });
    }

    public void ExitSettings()
    {
        fadeSystem.FadeInOut(fadeWaitTime,
            onFadeInComplete: () =>
            {
                EventManager.TriggerEvent("OnOpenMenu",true);
                EventManager.TriggerEvent("OnOpenSettings",false);
                print("Settings deactivated");
            });
    }
    
   
    public void Activate_Page_Settings()
    {
       
    }
}
