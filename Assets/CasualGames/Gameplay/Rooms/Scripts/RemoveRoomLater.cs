using UnityEngine;

public class RemoveRoomLater : MonoBehaviour
{
    private Transform cam;
    private bool active;

    public void Init(Transform camera)
    {
        cam = camera;
        active = true;
    }

    private void Update()
    {
        if (!active || cam == null) return;

        if (transform.position.y + 16f < cam.position.y)
        {
            Destroy(gameObject);
        }
    }
}