using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
   [SerializeField] int totalHealth;
   public GameObject deathEffect;

   public void DamageEnemy(int damageAmount)
   {
        totalHealth -= damageAmount;

        if (totalHealth <= 0)
        {
            if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
   }
}
