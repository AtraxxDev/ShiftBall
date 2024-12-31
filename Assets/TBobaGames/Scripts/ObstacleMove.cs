using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    public float speed = 2f; // Velocidad del movimiento
    private Vector2 initialPosition; // Posición inicial del obstáculo
    private Vector2 targetPosition; // Posición objetivo

    void Start()
    {
        // Establecer la posición inicial como la posición actual del obstáculo
        initialPosition = transform.position;

        // Determina la primera posición objetivo aleatoriamente
        if (Random.value < 0.5f)
        {
            targetPosition = new Vector2(-1.7f, initialPosition.y); // Mover a la izquierda
        }
        else
        {
            targetPosition = new Vector2(1.7f, initialPosition.y); // Mover a la derecha
        }
    }

    void Update()
    {
        // Mueve el obstáculo hacia la posición objetivo
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Si el obstáculo ha llegado a la posición objetivo, selecciona la siguiente posición
        if ((Vector2)transform.position == targetPosition)
        {
            // Alterna la posición objetivo entre izquierda y derecha
            if (targetPosition.x == -1.7f)
            {
                targetPosition = new Vector2(1.7f, initialPosition.y);
            }
            else
            {
                targetPosition = new Vector2(-1.7f, initialPosition.y);
            }
        }
    }
}
