using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
    public Transform cam;
    public GameObject[] easyRooms;  
    public GameObject[] mediumRooms; 
    public GameObject[] hardRooms;  
    public Transform parent;

    public void GenerateRooms()
    {
        if (gameObject.transform.position.y - 12 < cam.position.y)
        {
            GameObject[] currentRooms = GetRoomListBasedOnScore();

            int randomIndex = Random.Range(0, currentRooms.Length);
            GameObject newRoom = Instantiate(currentRooms[randomIndex], parent);
            newRoom.transform.position = gameObject.transform.position;

            gameObject.transform.Translate(0, 10.5f, 0);
        }
    }

    private GameObject[] GetRoomListBasedOnScore()
    {
        int score = ScoreManager.Instance.Score;
        float randomValue = Random.value * 100f; // Genera un número aleatorio entre 0 y 100

        if (score < 60) // Rango fácil: 100% de salas fáciles
        {
            return easyRooms;
        }
        else if (score >= 60 && score < 100) // Rango medio
        {
            // Rango medio: 70% de salas medias, 30% de salas fáciles
            if (randomValue < 70f)
            {
                return mediumRooms; // 70% de probabilidades
            }
            else
            {
                return easyRooms; // 30% de probabilidades
            }
        }
        else if (score >= 100) // Rango difícil
        {
            // Rango difícil: 60% de salas difíciles, 30% de salas medias, 10% de salas fáciles
            if (randomValue < 60f)
            {
                return hardRooms; // 60% de probabilidades
            }
            else if (randomValue >= 60 && randomValue < 90f)
            {
                return mediumRooms; // 30% de probabilidades
            }
            else
            {
                return easyRooms; // 10% de probabilidades
            }
        }

        // Por defecto, devolver salas fáciles (nunca debería llegar aquí)
        return easyRooms;
    }


    private GameObject[] CombineRooms(GameObject[] rooms1, GameObject[] rooms2)
    {
        List<GameObject> combined = new List<GameObject>();
        combined.AddRange(rooms1);
        combined.AddRange(rooms2);
        return combined.ToArray();
    }
}
