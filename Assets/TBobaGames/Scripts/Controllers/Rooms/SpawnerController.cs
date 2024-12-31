using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] private SpawnRoom spawnRoom;

    // Update is called once per frame
    void Update()
    {
        spawnRoom.GenerateRooms();
    }
}
