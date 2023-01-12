using UnityEngine;
using TMPro;

public class AbilityUnlock : MonoBehaviour
{
    public bool unlockDoubleJump;
    public bool unlockDash;
    public bool unlockBecomeBall;
    public bool unlockDropBomb;
    private PlayerAbilityTracker player;
    public GameObject effect;

    [Header("TextmeshPro")]
    public TMP_Text unlockText;
    [SerializeField] string unlockMessage;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponentInParent<PlayerAbilityTracker>();   

            if (unlockDoubleJump)
            {
                player.canDoubleJump = true;
            }
            if (unlockDash)
            {
                player.canDash = true;
            } 
            if (unlockBecomeBall)
            {
                player.canBecomeBall = true;
            } 
            if (unlockDropBomb)
            {
                player.canDropBomb = true;
            } 
            Instantiate(effect, transform.position, Quaternion.identity);
            
            unlockText.transform.parent.SetParent(null); //make the canvas unparented so when we destroy gameobject, text will not be destroyed instantly
            unlockText.transform.parent.position = transform.position;

            unlockText.text = unlockMessage;
            unlockText.gameObject.SetActive(true);

            Destroy(unlockText, 3f);
            Destroy(gameObject);
        }
    }
}
