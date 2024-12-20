using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public float speed;
    [SerializeField] private bool isMovingLeft = true;
    private Vector3 direction;

    private void Start()
    {
        direction = new Vector3(isMovingLeft ? -1 : 1, 1, 0).normalized;

    }

    public void HandleInput()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ToggleDirection();
        }
    }

    public void MovePlayer()
    {
        float deltaSpeed = speed * Time.fixedDeltaTime;
        transform.Translate(direction * deltaSpeed);
    }

    public void HandleCollision(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            ToggleDirection();
            AudioManager.Instance.PlayWallHitSound();
        }
    }

    private void ToggleDirection()
    {
        isMovingLeft = !isMovingLeft;
        direction = new Vector3(isMovingLeft ? -1 : 1, 1, 0).normalized;
    }

    public void SetVerticalDirectionOnly()
    {
        direction = Vector3.up; // Movimiento solo vertical
    }

    public void RestoreDiagonalMovement()
    {
        direction = new Vector3(isMovingLeft ? -1 : 1, 1, 0).normalized; // Restaurar movimiento diagonal
    }

}
