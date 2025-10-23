using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class PanelShop : MonoBehaviour
{
    [BoxGroup("Panel Objects")]
    public GameObject[] panelShopObjects;

    [BoxGroup("Reference")]
    public FadeSystem fadeSystem;
    public float fadeTime = 0.2f;

    public void ActivateShop()
    {
        fadeSystem.FadeInOut(fadeTime, () =>
        {
            panelShopObjects[0].SetActive(true);
            Debug.Log("Shop Activated");
        });
    }

    public void ExitShop()
    {
        fadeSystem.FadeInOut(fadeTime, () =>
        {
            panelShopObjects[0].SetActive(false);
            Debug.Log("Shop Exited");
        });
    }

    public void ExitInfo()
    {
        fadeSystem.FadeInOut(fadeTime, () =>
        {
            panelShopObjects[4].SetActive(false);
            Debug.Log("Info Exited");
        });
    }

    public void ExitSettings()
    {
        fadeSystem.FadeInOut(fadeTime, () =>
        {
            panelShopObjects[5].SetActive(false);
            Debug.Log("Settings Exited");
        });
    }

    public void Activate_Page_1()
    {
        fadeSystem.FadeInOut(fadeTime, () =>
        {
            panelShopObjects[1].SetActive(true);
            panelShopObjects[2].SetActive(false);
            panelShopObjects[3].SetActive(false);
            Debug.Log("Page 1 Activated");
        });
    }

    public void Activate_Page_2()
    {
        fadeSystem.FadeInOut(fadeTime, () =>
        {
            panelShopObjects[1].SetActive(false);
            panelShopObjects[2].SetActive(true);
            panelShopObjects[3].SetActive(false);
            Debug.Log("Page 2 Activated");
        });
    }

    public void Activate_Page_3()
    {
        fadeSystem.FadeInOut(fadeTime, () =>
        {
            panelShopObjects[1].SetActive(false);
            panelShopObjects[2].SetActive(false);
            panelShopObjects[3].SetActive(true);
            Debug.Log("Page 3 Activated");
        });
    }

    public void Activate_Page_Info()
    {
        fadeSystem.FadeInOut(fadeTime, () =>
        {
            panelShopObjects[4].SetActive(true);
            Debug.Log("Page Info Activated");
        });
    }

    public void Activate_Page_Settings()
    {
        fadeSystem.FadeInOut(fadeTime, () =>
        {
            panelShopObjects[5].SetActive(true);
            Debug.Log("Page Settings Activated");
        });
    }
}
