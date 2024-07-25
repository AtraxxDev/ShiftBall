using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    private GameObject target;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private float visionRange;
    private bool isChasing = false;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (GameManager.Instance.IsPaused()) return;

        if (Vector3.Distance(transform.position, target.transform.position) <= visionRange && !isChasing)
        {
            isChasing = true;
            StartCoroutine(SeePlayer());
        }

        if (isChasing)
        {
            MoveTowardsPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
        }
    }

    private IEnumerator SeePlayer()
    {
        while (Vector3.Distance(transform.position, target.transform.position) <= visionRange)
        {
            MoveTowardsPlayer();
            yield return null;
        }

        yield return new WaitForSeconds(5);
        speed = 0;
        isChasing = false;
    }

    private void MoveTowardsPlayer()
    {
        direction = (target.transform.position - transform.position).normalized;
        velocity = direction * speed * Time.deltaTime;

        transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        transform.position += velocity;
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position to represent the vision range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}
