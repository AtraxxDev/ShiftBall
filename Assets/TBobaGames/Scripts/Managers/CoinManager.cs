using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{

    public static CoinManager Instance;

    public int Coins { get; private set; }
    public int Stars { get; private set; }
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
            Coins = PlayerPrefs.GetInt("Coins",0);
            Stars = PlayerPrefs.GetInt("Stars",0);
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

    public void AddCoin(int amount)
    {
        Coins += amount;
        OnCoinsChanged?.Invoke(Coins);
        PlayerPrefs.SetInt("Coins", Coins);
        PlayerPrefs.Save();
    }

    public void AddStars(int amount)
    {
        Stars += amount;
        OnStarsChanged?.Invoke(Stars);
        PlayerPrefs.SetInt("Stars",Stars);
        PlayerPrefs.Save();
    }

    public bool SpendCoins(int amount)
    {
        if (Coins >= amount)
        {
            Coins -= amount;
            OnCoinsChanged?.Invoke(Coins);
            PlayerPrefs.SetInt("Coins",Coins);
            PlayerPrefs.Save();
            return true;
        }
        return false;
    }

    public bool SpendStars(int amount)
    {
        if (Stars >= amount)
        {
            Stars -= amount;
            OnStarsChanged?.Invoke(Stars);
            PlayerPrefs.SetInt("Stars", Stars);
            PlayerPrefs.Save();
            return true;
        }
        return false;
    }

    // If use Coins Collected in UI

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

    public void DoubleCoinsCollected()
    {
        // Calcula el doble de las monedas recogidas en la partida
        int doubledCoins = CoinsCollected;

        // A�ade las monedas duplicadas al total de monedas
        AddCoin(doubledCoins);

        // Tambi�n actualiza el total de las monedas recogidas
        AddCoinsCollected(doubledCoins);

        Debug.Log($"Se han a�adido {doubledCoins} monedas al total.");
    }

}
