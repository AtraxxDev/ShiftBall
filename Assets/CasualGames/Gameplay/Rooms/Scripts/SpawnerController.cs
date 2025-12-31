using TB_Tools;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] private SpawnRoom spawnRoom;

    private void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.Playing)
            return;

        spawnRoom.GenerateRooms();
    }
}