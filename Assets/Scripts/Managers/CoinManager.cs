using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{

    public static CoinManager Instance;

    public int TotalCoins { get; private set; }
    public int CoinsCollected { get; private set; }

    public Action<int> OnCoinsChanged;

    public Action<int> OnCoinsCollectChanged;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            TotalCoins = PlayerPrefs.GetInt("Coins",0);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        CoinsCollected = 0;
    }

    public void AddTotalCoin(int amount)
    {
        TotalCoins += amount;
        OnCoinsChanged?.Invoke(TotalCoins);
        PlayerPrefs.SetInt("Coins", TotalCoins);
        PlayerPrefs.Save();
    }

    public void AddCoinsCollected(int currentAmount)
    {
        CoinsCollected += currentAmount;
        OnCoinsCollectChanged?.Invoke(CoinsCollected);
    }
}
