using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{

    public static CoinManager Instance;

    public int Coins { get; private set; }
    public int CoinsCollected { get; private set; }

    public Action<int> OnCoinsChanged;

    public Action<int> OnCoinsCollectChanged;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Coins = PlayerPrefs.GetInt("Coins",0);
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

    public void AddCoin(int amount)
    {
        Coins += amount;
        OnCoinsChanged?.Invoke(Coins);
        PlayerPrefs.SetInt("Coins",Coins);
        PlayerPrefs.Save();
    }

    public void CoinsNumber(int currentAmount)
    {
        CoinsCollected += currentAmount;
        OnCoinsCollectChanged?.Invoke(CoinsCollected);
    }
}
