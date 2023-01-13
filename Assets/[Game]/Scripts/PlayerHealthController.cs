using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    //[HideInInspector]
    public int currentHealth;
    public int maxHealth;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;

        UIController.instance.UpdateHealth(currentHealth, maxHealth);
    }


    public void DamagePlayer(int damageAmout)
    {
        currentHealth -= damageAmout;

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            gameObject.SetActive(false);
        }
        UIController.instance.UpdateHealth(currentHealth, maxHealth);
    }
}
