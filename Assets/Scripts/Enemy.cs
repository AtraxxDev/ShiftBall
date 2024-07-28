using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    [SerializeField] private float visionRange;
    [SerializeField] private float chaseDuration = 5f; // Duración del tiempo que el enemigo persigue al jugador

    private GameObject target;
    private bool isChasing = false;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (GameManager.Instance.IsPaused()) return;

        if (Vector3.Distance(transform.position, target.transform.position) <= visionRange && !isChasing)
        {
            StartChasing();
        }

        if (isChasing)
        {
            MoveTowardsPlayer();
        }
    }

    private void StartChasing()
    {
        isChasing = true;
        StartCoroutine(ChasePlayerCoroutine());
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
            GameManager.Instance.GameOver();
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Dibujar una esfera amarilla en la posición del transform para representar el rango de visión
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }


   
}
