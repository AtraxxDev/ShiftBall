using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 initialPosition;

    public float shakeDuration = 0.4f;
    public float shakeMagnitude = 0.08f;

    private Coroutine shakeRoutine;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        initialPosition = cameraTransform.localPosition;
    }

    [Button]
    public void Shake()
    {
        if (shakeRoutine != null)
            StopCoroutine(shakeRoutine);

        shakeRoutine = StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        Vector3 originalPosition = cameraTransform.position;
        
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            cameraTransform.position = originalPosition + new Vector3(0, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        //cameraTransform.localPosition = initialPosition; // regresar
        shakeRoutine = null;
    }
}