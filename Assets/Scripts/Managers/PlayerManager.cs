using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Unit
{
    [SerializeField] private bool isMovingLeft;

    [SerializeField] private bool isInvecible = false;

    

    // Start is called before the first frame update
    void Start()
    {
        
        isMovingLeft = true;
        direction = new Vector3(isMovingLeft ? -1 : 1, 1, 0).normalized;
    }

   


    // FixedUpdate is called at a fixed interval and is used for physics updates
    void FixedUpdate()
    {
        if (GameManager.Instance.IsPaused()) return;

        float deltaspeed = speed * Time.fixedDeltaTime;
        transform.Translate(direction * deltaspeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsPaused()) return;
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ToggleDirection();
        }
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            ToggleDirection();
        }
    }

    public void ToggleDirection()
    {
        isMovingLeft = !isMovingLeft;
        direction = new Vector3(isMovingLeft ? -1 : 1, 1, 0).normalized;
        AudioManager.Instance.PlayWallHitSound();
    }

    
   
}
