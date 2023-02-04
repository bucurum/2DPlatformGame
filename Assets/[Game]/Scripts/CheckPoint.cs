using System.Collections;
using TMPro;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [Header("TextmeshPro")]
    public TMP_Text unlockText;


    //TODO: whenever the player picks up the checkpoint and die, destroy the pickuped abilities
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            RespawnController.instance.SetSpawnPoint(transform.position);
            unlockText.gameObject.SetActive(true);
            Destroy(unlockText, 3f);
        }   
    
    }

   /* public IEnumerator TextCoroutine()
    {
        unlockText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        unlockText.gameObject.SetActive(false);
    }*/
}
