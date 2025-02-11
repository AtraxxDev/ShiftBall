using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] private int starValue = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.Instance.PlayStarPickupSound();
            CoinManager.Instance.AddStars(starValue);
            CoinManager.Instance.AddStarsCollected(starValue);
            gameObject.SetActive(false);
        }

    }
}
