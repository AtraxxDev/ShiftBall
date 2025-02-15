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
        float randomValue = Random.value * 100f; // Genera un n�mero aleatorio entre 0 y 100

        print(randomValue);

        if (score < 120) // Rango f�cil: 100% de salas f�ciles
        {
            return easyRooms;
        }
        else if (score >= 120 && score < 300) // Rango medio
        {
            // Rango medio: 70% de salas medias, 30% de salas f�ciles
            if (randomValue < 50f)
            {
                return mediumRooms; // 70% de probabilidades
            }
            else
            {
                return easyRooms; // 30% de probabilidades
            }
        }
        else if (score >= 300) // Rango dif�cil
        {
            // Rango dif�cil: 30% de salas dif�ciles, 40% de salas medias, 30% de salas f�ciles
            if (randomValue < 30f)
            {
                return hardRooms; // 30% de probabilidades
            }
            else if (randomValue >= 30f && randomValue < 70f)
            {
                return mediumRooms; // 40% de probabilidades
            }
            else
            {
                return easyRooms; // 30% de probabilidades
            }
        }


        // Por defecto, devolver salas f�ciles (nunca deber�a llegar aqu�)
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
