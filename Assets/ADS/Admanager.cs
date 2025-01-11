using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Admanager : MonoBehaviour
{

    public void RewardDoubleCoins()
    {
        // Mostrar el anuncio recompensado y pasar el callback para duplicar las monedas
        Rewarded.Instance.ShowRewardedAd(OnAdWatched);
    }

    private void OnAdWatched()
    {
        // Este m�todo se ejecutar� solo despu�s de que el anuncio haya sido visto.
        CoinManager.Instance.DoubleCoinsCollected();
    }



}
