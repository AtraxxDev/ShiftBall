using UnityEngine;
using System.Collections;

public class ExpandingWavePowerUp : PowerUpBase
{
    [Header("Settings")]
    [SerializeField] private float waveRadius = 2.5f;
    [SerializeField] private float waveSpeed = 1f;
    [SerializeField] private float waveInterval = 1f;
    [SerializeField] private LayerMask destructibleLayer;

    private float currentRadius;
    private bool isWaveInProgress = false;

    void Update()
    {
        if (isActive)
        {
            timerRemaining -= Time.deltaTime;

            if (timerRemaining <= 0)
            {
                OnDeactivate();
            }
        }
    }

    public override void OnActivate()
    {
        base.OnActivate();
        Debug.Log("Expanding Wave Activated");
        StartCoroutine(LaunchWavesRepeatedly());
    }

    public override void OnDeactivate()
    {
        base.OnDeactivate();
        Debug.Log("Expanding Wave Deactivated");
        StopAllCoroutines();
        currentRadius = 0f;
        isWaveInProgress = false;
    }

    private IEnumerator LaunchWave()
    {
        isWaveInProgress = true;
        currentRadius = 0f;

        while (currentRadius < waveRadius)
        {
            currentRadius += waveSpeed * Time.deltaTime;

            // Detectar objetos dentro del rango de la onda
            Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, currentRadius, destructibleLayer);

            foreach (Collider2D hitObject in hitObjects)
            {
                if (hitObject != null) // Verificar que el objeto no sea nulo
                {
                    Destroy(hitObject.gameObject);
                }
            }

            yield return null; // Esperar hasta el siguiente frame
        }

        isWaveInProgress = false; // Marcar que la onda ha terminado
    }

    private IEnumerator LaunchWavesRepeatedly()
    {
        while (timerRemaining > 0f && isActive)
        {
            // Solo lanzar una nueva onda si no hay una en progreso
            if (!isWaveInProgress)
            {
                yield return StartCoroutine(LaunchWave());
            }

            // Esperar al intervalo entre ondas
            yield return new WaitForSeconds(waveInterval);
        }

        OnDeactivate(); 
    }

    private void OnDrawGizmos()
    {
        // Visualización del radio de la onda en el editor (solo para referencia en el editor)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, waveRadius);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, currentRadius);
    }
}
