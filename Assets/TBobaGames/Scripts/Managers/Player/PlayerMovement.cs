using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerMovement : Unit
{
    [SerializeField] private bool isMovingLeft = true;

    private void Start()
    {
        direction = new Vector3(isMovingLeft ? -1 : 1, 1, 0).normalized;


    }

    public void HandleInput()
    {

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            print("Cambie de direccion 2");

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

    public void SetZero()
    {
        direction = Vector3.zero; // Movimiento solo vertical
    }

    public void RestoreDiagonalMovement()
    {
        direction = new Vector3(isMovingLeft ? -1 : 1, 1, 0).normalized; // Restaurar movimiento diagonal
    }



}
