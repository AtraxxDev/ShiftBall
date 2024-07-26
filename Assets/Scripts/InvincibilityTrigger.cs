using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Destructible"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
