using System.Collections;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 initialPosition;

    public float shakeDuration = 0.8f;
    public float shakeMagnitude = 0.05f;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        initialPosition = cameraTransform.position;
    }

    public void Shake()
    {
        StopAllCoroutines();
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        // Guardar la posici�n inicial en el momento del sacud�n
        Vector3 originalPosition = cameraTransform.position;

        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            cameraTransform.position = originalPosition + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // No restablecer la posici�n original; la c�mara mantiene la posici�n final
    }
}
