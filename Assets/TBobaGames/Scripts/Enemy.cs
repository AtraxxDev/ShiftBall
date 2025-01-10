using System.Collections;
using UnityEngine;

public class Enemy : Unit
{
    [SerializeField] private float visionRange;
    [SerializeField] private float chaseDuration = 5f;
    [SerializeField] private DangerZoneSprite ChangeSprite; 

    private GameObject target;
    private bool isChasing = false;
    private bool isDetecting = false; 

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        ChangeSprite.StartIdleAnimation();
    }

    private void Update()
    {
        if (GameManager.Instance.IsPaused()) return;

        if (!isDetecting && !isChasing && Vector3.Distance(transform.position, target.transform.position) <= visionRange)
        {
            StartCoroutine(StartDetectionAndChasing());
        }

        if (isChasing)
        {
            MoveTowardsPlayer();
        }
    }

    private IEnumerator StartDetectionAndChasing()
    {
        isDetecting = true;

        // Iniciar la animación de detección
        ChangeSprite.StartDetectAnimation();

        // Esperar a que termine la animación de detección
        yield return new WaitForSeconds(0.2f);

        isDetecting = false;
        isChasing = true;

        // Iniciar la lógica de persecución
        yield return StartCoroutine(ChasePlayerCoroutine());
    }

    private IEnumerator ChasePlayerCoroutine()
    {
        float chaseEndTime = Time.time + chaseDuration;

        while (Vector3.Distance(transform.position, target.transform.position) <= visionRange && Time.time < chaseEndTime)
        {
            if (GameManager.Instance.IsPaused())
            {
                yield break;
            }

            MoveTowardsPlayer();
            yield return null;
        }

        // Después de que termine la persecución
        isChasing = false;
    }

    private void MoveTowardsPlayer()
    {
        if (GameManager.Instance.IsPaused()) return; // No mover si el juego está en pausa

        Vector3 direction = (target.transform.position - transform.position).normalized;
        Vector3 velocity = direction * speed * Time.deltaTime;

        transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        transform.position += velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(gameObject);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Dibujar una esfera amarilla en la posición del transform para representar el rango de visión
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}
