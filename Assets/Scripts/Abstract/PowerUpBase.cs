using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpBase : MonoBehaviour
{

    [SerializeField] private float duration; // Duración del power-up en segundos
    private bool isActive = false;          // Estado del power-up

    public abstract void Activate();        // Lógica específica de cada power-up
    public virtual void Deactivate()        // Lógica al desactivar el power-up
    {
        isActive = false;                   // Marcamos el power-up como inactivo
        Destroy(gameObject);                // Opcional: destruir el objeto al finalizar
    }

    public void StartPowerUpCoroutine()
    {
        if (!isActive)
        {
            isActive = true;
            StartCoroutine(PowerUpDurationCoroutine());
        }
    }

    private IEnumerator PowerUpDurationCoroutine()
    {
        Activate();                         // Llama a la lógica de activación del power-up
        yield return new WaitForSeconds(duration); // Espera el tiempo definido en `duration`
        Deactivate();                       // Llama a la lógica de desactivación
    }



}
