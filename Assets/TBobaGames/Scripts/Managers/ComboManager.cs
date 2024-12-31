using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    public static ComboManager Instance;

    public int comboCount;
    public float comboDuration = 2f; // Duración del combo en segundos
    private Coroutine comboCoroutine;

    public delegate void ComboChanged(int newCombo);
    public event ComboChanged OnComboChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCombo()
    {
        comboCount++;
        OnComboChanged?.Invoke(comboCount);

        if (comboCoroutine != null)
        {
            StopCoroutine(comboCoroutine);
        }
        comboCoroutine = StartCoroutine(ComboTimeout());
    }

    private IEnumerator ComboTimeout()
    {
        yield return new WaitForSeconds(comboDuration);
        ResetCombo();
    }

    public void ResetCombo()
    {
        comboCount = 0;
        OnComboChanged?.Invoke(comboCount);
    }

    public int GetComboMultiplier()
    {
        int multiplier = comboCount / 10; // Por cada 15 combos, aumenta el multiplicador
        return Mathf.Max(multiplier, 1); // Asegúrate de que el multiplicador sea al menos 1
    }
}
