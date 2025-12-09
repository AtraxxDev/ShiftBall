using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ParticleData",menuName = "Scriptable Objects/ParticlesDataSystem", order =2)]
public class ParticleData : ScriptableObject
{
    [Header("Particle Effects")]
    public List<ParticleEffectData> particleEffects;


    [System.Serializable]
    public class ParticleEffectData
    {
        public string id;
        public ParticleSystem particleSystemPrefab;
    }

    
    public ParticleSystem GetParticleSystem(string id)
    {
        var effect = particleEffects.Find(e => e.id == id);
        return effect != null ? effect.particleSystemPrefab : null;
    }
}
