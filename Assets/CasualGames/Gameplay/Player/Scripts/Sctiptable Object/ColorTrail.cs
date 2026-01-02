using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ColorTrail : MonoBehaviour
{
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private TrailData trailData;
    

    private void Start()
    {
        GameManager.Instance.OnStartGame += ApplyRandomTrail;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnStartGame -= ApplyRandomTrail;

    }

    private void ApplyRandomTrail()
    {
        if (trailRenderer == null || trailData == null || trailData.trailGradients.Count == 0)
            return;

        trailRenderer.Clear();            // 👈 CLAVE
        trailRenderer.enabled = false;    // reset visual
        trailRenderer.enabled = true;
        
        int randomIndex = Random.Range(0, trailData.trailGradients.Count);
        var data = trailData.trailGradients[randomIndex];

        trailRenderer.colorGradient = data.trailGradient;
        
        Debug.Log($"[ColorTrail] Trail seleccionado → Index: {randomIndex} | ID: {data.id}");

    }
}