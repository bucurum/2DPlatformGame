using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    //[HideInInspector]
    public int currentHealth;
    public int maxHealth;
    [SerializeField] float invincibilityLenght;
    private float invincibilityCounter;
    [SerializeField] float flashLenght;
    private float flashCounter;

    public SpriteRenderer[] playerSprites;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); //when we reload or load the next scene don`t destoy the player, so player don`t loose its progress 
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    void Start()
    {
        currentHealth = maxHealth;

        UIController.instance.UpdateHealth(currentHealth, maxHealth);
    }

    void Update()
    {
        if (invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;
        
            flashCounter -=Time.deltaTime;

            if (flashCounter <0)
            {
                foreach (SpriteRenderer spriteRenderer in playerSprites)
                {
                    spriteRenderer.enabled = !spriteRenderer.enabled;
                }
                flashCounter = flashLenght;
            }

            if (invincibilityCounter <= 0)
            {
                foreach (SpriteRenderer spriteRenderer in playerSprites)
                {
                    spriteRenderer.enabled = true;
                }  
                flashCounter = 0;  
            }
        }
    }

    public void DamagePlayer(int damageAmout)
    {
        if (invincibilityCounter <= 0)
        {
            currentHealth -= damageAmout;

            if (currentHealth <= 0)
            {
                currentHealth = 0;

                //gameObject.SetActive(false);
            
                RespawnController.instance.Respawn();
            }
            else
            {
                invincibilityCounter = invincibilityLenght;
            }
            UIController.instance.UpdateHealth(currentHealth, maxHealth);
        }
        
    }
    public void FillHeath()
    {
        currentHealth = maxHealth;
        UIController.instance.UpdateHealth(currentHealth, maxHealth);
    }
    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UIController.instance.UpdateHealth(currentHealth, maxHealth);
    }
}
