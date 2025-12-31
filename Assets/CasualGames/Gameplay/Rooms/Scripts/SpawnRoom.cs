using Sirenix.OdinInspector;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
    [Title("References")]
    [SerializeField] private Transform cam;
    [SerializeField] private Transform parent;

    [Title("Difficulty Prefabs")]
    [SerializeField] private GameObject[] easyRooms;
    [SerializeField] private GameObject[] mediumRooms;
    [SerializeField] private GameObject[] hardRooms;

    [Title("Settings")]
    [SerializeField] private float roomHeight = 10.5f;
    [SerializeField] private int initialRooms = 5;

    private float nextSpawnY;
    private float startSpawnY;

    private void Awake()
    {
        startSpawnY = transform.position.y;
        nextSpawnY = startSpawnY;
    }

    private void Start()
    {
        GameManager.Instance.OnRestartGame += ResetGenerator;
        ResetGenerator();
    }

    private void OnDisable()
    {
        GameManager.Instance.OnRestartGame -= ResetGenerator;
    }

    public void GenerateRooms()
    {
        while (nextSpawnY - 12f < cam.position.y)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        GameObject prefab = GetRoomBasedOnDifficulty();
        if (prefab == null) return;

        GameObject room = Instantiate(prefab, parent);
        room.transform.position = new Vector3(0, nextSpawnY, 0);

        // Inicializa el auto-destruido
        if (room.TryGetComponent(out RemoveRoomLater remover))
        {
            remover.Init(cam);
        }

        nextSpawnY += roomHeight;
    }

    public void ResetGenerator()
    {
        // Reset dificultad
        //DificultyManager.Instance.ResetDifficulty();

        // Borrar rooms existentes
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }

        // Reset spawn position
        nextSpawnY = startSpawnY;

        // Pre-generar rooms
        for (int i = 0; i < initialRooms; i++)
        {
            Spawn();
        }
    }

    private GameObject GetRoomBasedOnDifficulty()
    {
        float diff = DificultyManager.Instance.GetDifficult();

        float easyWeight = Mathf.Lerp(1f, 0.2f, diff);
        float mediumWeight = Mathf.Sin(diff * Mathf.PI);
        float hardWeight = Mathf.Lerp(0f, 1f, diff);

        float total = easyWeight + mediumWeight + hardWeight;
        float rand = Random.value * total;
        

        if (rand < easyWeight && easyRooms.Length > 0)
            return easyRooms[Random.Range(0, easyRooms.Length)];

        if (rand < easyWeight + mediumWeight && mediumRooms.Length > 0)
            return mediumRooms[Random.Range(0, mediumRooms.Length)];

        if (hardRooms.Length > 0)
            return hardRooms[Random.Range(0, hardRooms.Length)];

        return null;
    }
}
