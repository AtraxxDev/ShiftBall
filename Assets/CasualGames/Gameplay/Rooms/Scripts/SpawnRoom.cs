using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
    public Transform cam;
    public Transform parent;
    
    [Title("Difficulty Prefabs")]
    public GameObject[] easyRooms;  
    public GameObject[] mediumRooms; 
    public GameObject[] hardRooms;  

    private float _roomHeight = 10.5f;

    public void GenerateRooms()
    {
        if (transform.position.y - 12 < cam.position.y)
        {
            GameObject nextRoom = GetRoomBasedOnDifficulty();

            GameObject newRoom = Instantiate(nextRoom, parent);
            newRoom.transform.position = transform.position;

            transform.Translate(0, _roomHeight, 0);
        }
    }

    private GameObject GetRoomBasedOnDifficulty()
    {
        float diff = DificultyManager.Instance.GetDifficult();
        
        // Pesos que cambian progresivamente
        float easyWeight = Mathf.Lerp(1f, 0.2f, diff); // cada vez menos
        float mediumWeight = Mathf.Sin(diff * Mathf.PI); // sube al medio
        float hardWeight = Mathf.Lerp(0f, 1f, diff); // cada vez mas
        
        float total = easyWeight + mediumWeight + hardWeight;
        float rand = Random.value * total;  
        
        if (rand < easyWeight)
            return easyRooms[Random.Range(0, easyRooms.Length)];
        else if  (rand < easyWeight + mediumWeight)
            return mediumRooms[Random.Range(0, mediumRooms.Length)];
        else
            return hardRooms[Random.Range(0, hardRooms.Length)];

    }
    private GameObject[] CombineRooms(GameObject[] rooms1, GameObject[] rooms2)
    {
        List<GameObject> combined = new List<GameObject>();
        combined.AddRange(rooms1);
        combined.AddRange(rooms2);
        return combined.ToArray();
    }
}
