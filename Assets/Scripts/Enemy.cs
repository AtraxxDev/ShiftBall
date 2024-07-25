using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    private GameObject target;
    [SerializeField] private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
       
    }

    

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsPaused()) return;
        StartCoroutine(SeePlayer());

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.GameOver();
    }

    public IEnumerator SeePlayer()
    {
        direction = (target.transform.position - transform.position).normalized;
        velocity = direction * speed * Time.deltaTime;

        transform.rotation = Quaternion.FromToRotation(Vector3.up,direction);

        transform.position += velocity;

        
        yield return new WaitForSeconds(5);
        speed = 0;
    }

    


}
