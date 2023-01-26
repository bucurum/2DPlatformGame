using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damageAmount;
    public bool explode;
    public GameObject explosionEffect;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DealDamage();
            if (explode)
            {
                PlayerHealthController.instance.invincibilityCounter = 0; 
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DealDamage();
            if (explode)
            {
            PlayerHealthController.instance.invincibilityCounter = 0; 
            }
        }
    }
    void OnTriggerExit2D(Collision2D other)
    {
        if (explode)
        {
            PlayerHealthController.instance.invincibilityCounter = 0; 
        }
    }
    void DealDamage()
    {
        PlayerHealthController.instance.DamagePlayer(damageAmount);

        if (explode)
        {
            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}
