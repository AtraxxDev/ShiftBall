
    using TB_Tools;using UnityEngine;
    
    public class CurrencyService:MonoBehaviour, ICurrencyService
    {
        public bool TrySpend(CurrencyType type, int amount)
        {
            switch (type)
            {
                case CurrencyType.Coins:
                    return CoinManager.Instance.SpendCoins(amount);
                case CurrencyType.Diamonds:
                    return CoinManager.Instance.SpendDiamonds(amount);
                default:
                    return false;  
            }
        }

        public int GetBalance(CurrencyType type)
        {
            return type switch
            {
                CurrencyType.Coins => CoinManager.Instance.Coins,
                CurrencyType.Diamonds => CoinManager.Instance.Diamonds,
                _ => 0
            };
        }
    }