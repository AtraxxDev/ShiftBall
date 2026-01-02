using System.Collections;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    [Header("Shake Settings")]
    [SerializeField] private float duration = 0.15f;
    [SerializeField] private float magnitude = 0.12f;
    [SerializeField] private AnimationCurve decayCurve = new AnimationCurve(
        new Keyframe(0f, 1f),
        new Keyframe(0.15f, 0.9f),
        new Keyframe(1f, 0f)
    );


    private Vector3 initialLocalPos;
    private Coroutine shakeRoutine;

    private void Awake()
    {
        initialLocalPos = transform.localPosition;
    }

    public void Shake()
    {
        if (shakeRoutine != null)
            StopCoroutine(shakeRoutine);

        shakeRoutine = StartCoroutine(ShakeRoutine());
        
    }

    private IEnumerator ShakeRoutine()
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float strength = decayCurve.Evaluate(elapsed / duration);
            Vector2 offset = Random.insideUnitCircle * magnitude * strength;

            transform.localPosition = initialLocalPos +
                                      new Vector3(offset.x, offset.y, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = initialLocalPos;
        shakeRoutine = null;
    }
}