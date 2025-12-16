using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int baseCoinValue = 1; // Valor base de la moneda

    private void OnEnable()
    {
        ComboManager.Instance.OnComboChanged += UpdateCoinValue;
    }

    private void OnDisable()
    {
        ComboManager.Instance.OnComboChanged -= UpdateCoinValue;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySFX("Coin");
            ComboManager.Instance.AddCombo();
            int comboMultiplier = ComboManager.Instance.GetComboMultiplier();
            int finalCoinValue = baseCoinValue * comboMultiplier;
            CoinManager.Instance.AddCoin(finalCoinValue);
            CoinManager.Instance.AddCoinsCollected(finalCoinValue);
            gameObject.SetActive(false);
        }
    }

    private void UpdateCoinValue(int combo)
    {
        // Este m�todo se llama cada vez que cambia el valor del combo.
        // En este caso, el valor de la moneda se calcula en OnTriggerEnter2D,
        // por lo que esta funci�n no hace nada actualmente.
        // Puedes usarlo si necesitas ajustar algo en funci�n del valor del combo.
    }
}
