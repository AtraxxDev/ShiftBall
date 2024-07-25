using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{

    public static CoinManager Instance;

    public int TotalCoins { get; private set; }
    public int TotalStars { get; private set; }
    public int CoinsCollected { get; private set; }
    public int StarsCollected { get; private set; }

    public Action<int> OnCoinsChanged;
    public Action<int> OnStarsChanged;

    public Action<int> OnCoinsCollectChanged;
    public Action<int> OnStarsCollectChanged;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            TotalCoins = PlayerPrefs.GetInt("Coins",0);
            TotalStars = PlayerPrefs.GetInt("Stars",0);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        CoinsCollected = 0;
        StarsCollected = 0;
    }

    public void AddTotalCoin(int amount)
    {
        TotalCoins += amount;
        OnCoinsChanged?.Invoke(TotalCoins);
        PlayerPrefs.SetInt("Coins", TotalCoins);
        PlayerPrefs.Save();
    }

    public void AddTotalStars(int amount)
    {
        TotalStars += amount;
        OnStarsChanged?.Invoke(TotalStars);
        PlayerPrefs.SetInt("Stars", TotalStars);
        PlayerPrefs.Save();
    }

    public void AddCoinsCollected(int currentAmount)
    {
        CoinsCollected += currentAmount;
        OnCoinsCollectChanged?.Invoke(CoinsCollected);
    }

    public void AddStarsCollected(int currentAmount)
    {
        StarsCollected += currentAmount;
        OnStarsCollectChanged?.Invoke(StarsCollected);
    }

    public void ResetCoins_StarsCollected()
    {
        CoinsCollected = 0;
        StarsCollected = 0;
        OnCoinsCollectChanged?.Invoke(CoinsCollected);
        OnStarsCollectChanged?.Invoke(StarsCollected);
    }
}
