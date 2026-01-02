using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 5f;

    [Header("Horizontal Limits")]
    [SerializeField] private float leftLimit = -2.5f;
    [SerializeField] private float rightLimit = 2.5f;

    private Vector2 direction;
    private bool isMovingLeft = true;
    private Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.position;
    }

    private void Start()
    {
        SetDirectionLeft();
    }
    

    // =============================
    // MOVEMENT
    // =============================
    public void Move()
    {
        transform.position += (Vector3)(direction * speed * Time.fixedDeltaTime);

        // Cambiar dirección por límites en X
        if (transform.position.x <= leftLimit)
        {
            SetDirectionRight();
            AudioManager.Instance.PlaySFX("Pop");
            
        }
        else if (transform.position.x >= rightLimit)
        {
            SetDirectionLeft();
            AudioManager.Instance.PlaySFX("Pop");
        }
        
        
    }

    // =============================
    // INPUT
    // =============================
    public void HandleInput()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) 
        { 
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) 
            { 
                print("Se apreto la UI"); 
            }else{ 
                ToggleDirection(); 
            } 
        } 
    }

    // =============================
    // DIRECTION
    // =============================
    private void ToggleDirection()
    {
        if (isMovingLeft)
            SetDirectionRight();
        else
            SetDirectionLeft();
    }

    private void SetDirectionLeft()
    {
        isMovingLeft = true;
        direction = new Vector2(-1f, 1f).normalized;
    }

    private void SetDirectionRight()
    {
        isMovingLeft = false;
        direction = new Vector2(1f, 1f).normalized;
    }

    // =============================
    // PUBLIC CONTROLS
    // =============================
    public void StopMovement()
    {
        direction = Vector2.zero;
    }

    public void ResumeMovement()
    {
        if (isMovingLeft)
            SetDirectionLeft();
        else
            SetDirectionRight();
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
        SetDirectionLeft();
    }

    // =============================
    // DEBUG
    // =============================
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(new Vector3(leftLimit, -10f, 0f), new Vector3(leftLimit, 10f, 0f));
        Gizmos.DrawLine(new Vector3(rightLimit, -10f, 0f), new Vector3(rightLimit, 10f, 0f));
    }
}
