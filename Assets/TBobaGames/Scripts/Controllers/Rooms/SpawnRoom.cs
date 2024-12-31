using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
    public Transform cam;
    public GameObject[] prefabRooms;
    public Transform parent;

    public void GenerateRooms()
    {
        if (gameObject.transform.position.y - 12 < cam.position.y)
        {
            GameObject newRoom = Instantiate(prefabRooms[Random.Range(0, prefabRooms.Length)],parent) as GameObject;
            newRoom.transform.position = gameObject.transform.position;
            gameObject.transform.Translate(0, 10.5f, 0);
        }
    }
}
