using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Admanager : MonoBehaviour
{
    public void RewardDoubleCoins()
    {
        // Mostrar el anuncio recompensado y pasar el callback para duplicar las monedas
        Rewarded.Instance.ShowRewardedAd(OnAdWatchedDoubleCoins);
    }

    public void ShowReviveAd()
    {
        Rewarded.Instance.ShowRewardedAd(OnAdWatcheRevive);
    }

    private void OnAdWatchedDoubleCoins()
    {
        // Este m�todo se ejecutar� solo despu�s de que el anuncio haya sido visto.
        CoinManager.Instance.DoubleCoinsCollected();

        
        
    }

    private void OnAdWatcheRevive()
    {
        Debug.Log("Anuncio completado. Reviviendo jugador.");
        GameManager.Instance.RevivePlayer();

    }



}
