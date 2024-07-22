using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int coinValue = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CoinManager.Instance.AddCoin(coinValue);
            Destroy(gameObject);
        }
        
    }

   
}
