using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerMovement : Unit
{
    [SerializeField] private bool isMovingLeft = true;
    private Vector3 _startPosition;
    private Vector3 _currentPosition;

    private void Start()
    {
        _startPosition = transform.position;
        direction = new Vector3(isMovingLeft ? -1 : 1, 1, 0).normalized;

    }

    public void HandleInput()
    {
        // Verifica si hay un toque y no est� sobre la UI
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                print("Se apreto la UI");
            }
            else
            {
                ToggleDirection();
            }

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
            AudioManager.Instance.PlaySFX("Pop");
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

    [Button]
    public void ResetPosition()
    {
        transform.position = _startPosition;
        isMovingLeft = true;
        direction = new Vector3(-1, 1, 0).normalized;
    }



}
