using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class CountdownSlider : MonoBehaviour
{
    public event Action<float> OnTimeChanged;
    public event Action OnTimerCompleted;

    [Header("Settings")]
    [SerializeField] private float countdownDuration = 5f;
    [SerializeField] private int maxCounter = 3;

    private Coroutine countdownCoroutine;
    private float elapsedTime;
    private int counter;

    // -----------------------------
    // Public API
    // -----------------------------
    [ShowInInspector,ReadOnly]
    public int Counter => counter;
    public int MaxCounter => maxCounter;
    public float Duration => countdownDuration;
    public float RemainingTime => Mathf.Max(0, countdownDuration - elapsedTime);

    public void StartCountdown()
    {
        StopCountdown();
        counter++;
        countdownCoroutine = StartCoroutine(CountdownRoutine());
    }

    public void StopCountdown()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
            countdownCoroutine = null;
        }
    }

    public void ResetCounter()
    {
        counter = 0;
    }

    // -----------------------------
    // Internal logic
    // -----------------------------
    private IEnumerator CountdownRoutine()
    {
        elapsedTime = 0f;

        while (elapsedTime < countdownDuration)
        {
            elapsedTime += Time.deltaTime;
            OnTimeChanged?.Invoke(elapsedTime / countdownDuration);
            yield return null;
        }

        OnTimeChanged?.Invoke(1f);
        OnTimerCompleted?.Invoke();

        countdownCoroutine = null;
    }
}