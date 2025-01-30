using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownSlider : MonoBehaviour
{
    public event Action<float> OnTimeChanged;
    public event Action OnTimerCompleted;

    public Coroutine countdownCoroutine;


    public float countdownDuration = 5f;

    private float elapsedTime;
    public float remainingTime;

    private void Start()
    {
       // StartCountdown();
    }

    public void StartCountdown()
    {
        if (countdownCoroutine != null)
        {
            Debug.Log($"Parare Corrutina {countdownCoroutine}");
            StopCoroutine(countdownCoroutine);
            Debug.Log($"Pare Corrutina {countdownCoroutine}");

        }

        Debug.Log($"Iniciare Corrutina {countdownCoroutine}");

        countdownCoroutine = StartCoroutine(CountdownRoutine());
    }

    public void RestartCountdown()
    {
        StopCoroutine(CountdownRoutine());
    }


    private IEnumerator CountdownRoutine()
    {
        elapsedTime = 0f;
        Debug.Log($"Puse elapsed time en 0");


        while (elapsedTime <= countdownDuration)
        {
            remainingTime = countdownDuration - elapsedTime;
            OnTimeChanged?.Invoke(remainingTime / countdownDuration); // Notificar el progreso
            elapsedTime += Time.deltaTime;



            yield return null;
        }

        Debug.Log($"Llegue a 0");

        OnTimeChanged?.Invoke(0); // Notificar que llegó a 0
        OnTimerCompleted?.Invoke();
    }
}
