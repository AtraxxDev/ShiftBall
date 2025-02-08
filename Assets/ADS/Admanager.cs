using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Admanager : MonoBehaviour
{
    public void RewardMoreStars()
    {
        // Mostrar el anuncio recompensado y pasar el callback para duplicar las monedas
        Rewarded.Instance.ShowRewardedAd(OnAdWatchedMoreStars);
    }

    public void ShowReviveAd()
    {
        Rewarded.Instance.ShowRewardedAd(OnAdWatcheRevive);
    }

    private void OnAdWatchedMoreStars()
    {
        // Este método se ejecutará solo después de que el anuncio haya sido visto.
        CoinManager.Instance.MoreStarsCollected(5);

        
        
    }

    private void OnAdWatcheRevive()
    {
        Debug.Log("Anuncio completado. Reviviendo jugador.");
        GameManager.Instance.RevivePlayer();

    }



}
