using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private float smoothTime = 0;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset; // Desplazamiento para posicionar la c�mara

    private float velocityY = 0.0f; // Variable para almacenar la velocidad de interpolaci�n en Y

    private void FixedUpdate()
    {
        // Calcular la posici�n objetivo de la c�mara en el eje Y
        float targetY = target.position.y + offset.y;

        // Interpolaci�n suave de la posici�n en el eje Y usando SmoothDamp
        float smoothY = Mathf.SmoothDamp(transform.position.y, targetY, ref velocityY, smoothTime);

        // Aplicar la nueva posici�n a la c�mara
        transform.position = new Vector3(transform.position.x, smoothY, transform.position.z);
    }
}
