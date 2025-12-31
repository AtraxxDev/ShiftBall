using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    public float speed = 2f; // Velocidad del movimiento
    private Vector2 initialPosition; // Posici�n inicial del obst�culo
    private Vector2 targetPosition; // Posici�n objetivo

    [SerializeField] private bool isLeft;

    void Start()
    {
        // Establecer la posici�n inicial como la posici�n actual del obst�culo
        initialPosition = transform.position;

        // Determina la primera posici�n objetivo aleatoriamente
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
        if (GameManager.Instance == null) return;
        if (GameManager.Instance.IsPaused) return;


        // Mueve el obst�culo hacia la posici�n objetivo
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Si el obst�culo ha llegado a la posici�n objetivo, selecciona la siguiente posici�n
        if ((Vector2)transform.position == targetPosition)
        {
            // Alterna la posici�n objetivo entre izquierda y derecha
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
