using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveRoomLater : MonoBehaviour
{
    private Transform cam;

    private void Start()
    {
        cam = GameObject.Find("Main Camera").transform;
    }

    private void Update()
    {
        DestroyRoom();
    }

    private void DestroyRoom()
    {
        if (gameObject.transform.position.y + 16 < cam.position.y)
        {
            Destroy(gameObject);
        }
    }
}
