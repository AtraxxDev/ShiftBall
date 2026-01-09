using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CoinManager : MonoBehaviour
{

    public static CoinManager Instance;

    public int Coins { get; private set; }
    public int Diamonds { get; private set; }
    public int CoinsCollected { get; private set; }
    public int DiamondsCollected { get; private set; }

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
            Diamonds = PlayerPrefs.GetInt("Diamonds",0);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        CoinsCollected = 0;
        DiamondsCollected = 0;
    }

    [Button]
    public void AddCoin(int amount)
    {
        Coins += amount;
        OnCoinsChanged?.Invoke(Coins);
        PlayerPrefs.SetInt("Coins", Coins);
        PlayerPrefs.Save();
    }
    [Button]

    public void AddDiamonds(int amount)
    {
        Diamonds += amount;
        OnStarsChanged?.Invoke(Diamonds);
        PlayerPrefs.SetInt("Diamonds",Diamonds);
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

    public bool SpendDiamonds(int amount)
    {
        if (Diamonds >= amount)
        {
            Diamonds -= amount;
            OnStarsChanged?.Invoke(Diamonds);
            PlayerPrefs.SetInt("Diamonds", Diamonds);
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
        DiamondsCollected += currentAmount;
        OnStarsCollectChanged?.Invoke(DiamondsCollected);
    }

    public void ResetCoins_StarsCollected()
    {
        CoinsCollected = 0;
        DiamondsCollected = 0;
        OnCoinsCollectChanged?.Invoke(CoinsCollected);
        OnStarsCollectChanged?.Invoke(DiamondsCollected);
    }

    public void MoreStarsCollected(int stars)
    {
      

        // A�ade las monedas al total de monedas
        AddDiamonds(stars);

        // Tambi�n actualiza el total de las monedas recogidas
        AddStarsCollected(stars);

        Debug.Log($"Se han a�adido {stars} estrellas al total.");
    }

}
