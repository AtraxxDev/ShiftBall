using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private float smoothTime = 0;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset; // Desplazamiento para posicionar la cámara

    private float velocityY = 0.0f; // Variable para almacenar la velocidad de interpolación en Y

    private void FixedUpdate()
    {
        // Calcular la posición objetivo de la cámara en el eje Y
        float targetY = target.position.y + offset.y;

        // Interpolación suave de la posición en el eje Y usando SmoothDamp
        float smoothY = Mathf.SmoothDamp(transform.position.y, targetY, ref velocityY, smoothTime);

        // Aplicar la nueva posición a la cámara
        transform.position = new Vector3(transform.position.x, smoothY, transform.position.z);
    }
}
