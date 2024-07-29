using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance { get; private set; }

    [SerializeField] private ParticleData particleData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayParticleEffect(int id, Vector3 position)
    {
        ParticleSystem effect = particleData.GetParticleSystem(id);
        if (effect != null)
        {
            ParticleSystem instance = Instantiate(effect, position, Quaternion.identity);
            instance.Play();
            Destroy(instance.gameObject, instance.main.duration);
        }
        else
        {
            Debug.LogWarning($"ParticleEffect with ID {id} not found.");
        }
    }
}
