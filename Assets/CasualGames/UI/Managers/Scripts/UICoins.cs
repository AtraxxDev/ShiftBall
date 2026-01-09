using TMPro;
using UnityEngine;

public class UICoins : MonoBehaviour
{
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text diamondsText;

    private void Start()
    {
        

        CoinManager.Instance.OnCoinsChanged += UpdateCoins;
        CoinManager.Instance.OnStarsChanged += UpdateDiamonds;

        // Refresco inmediato
        UpdateCoins(CoinManager.Instance.Coins);
        UpdateDiamonds(CoinManager.Instance.Diamonds);
    }

    private void OnDisable()
    {
       

        CoinManager.Instance.OnCoinsChanged -= UpdateCoins;
        CoinManager.Instance.OnStarsChanged -= UpdateDiamonds;
    }

    private void UpdateCoins(int value)
    {
        coinsText.text = value.ToString();
    }

    private void UpdateDiamonds(int value)
    {
        diamondsText.text = value.ToString();
    }
}