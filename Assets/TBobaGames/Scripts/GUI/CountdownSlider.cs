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

    private int maxCounter = 3;

    private int counter;

    public int GetCounter() => counter;
    public int GetMaxCounter() => maxCounter;

    public void ResetCounter()
    {
        counter = 0;
        Debug.Log("Reinicié el contador");
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



    private IEnumerator CountdownRoutine()
    {
        counter++;
        Debug.Log($"Contador : {counter}");
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
