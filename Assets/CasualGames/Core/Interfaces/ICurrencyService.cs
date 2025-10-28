namespace TB_Tools
{
    public interface ICurrencyService
    {
        bool TrySpend(CurrencyType type, int amount);
        int GetBalance(CurrencyType type);
    }
}