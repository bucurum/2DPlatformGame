using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDeath : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealthController.instance.DamagePlayer(100);
        }
    }
}
