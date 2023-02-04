using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    public Animator anim;
    [SerializeField] float distanceToOpen;
    private PlayerMovementHandler player;
    private bool playerExiting;
    public Transform exitPoint;
    [SerializeField] float movePlayerSpeed;
    [SerializeField] bool isGoingToNextScene;
    [SerializeField] bool isGoingToPreviousScene;
    private GameObject thePlayer;
    private PlayerAbilityTracker playerAbility;

    void Awake()
    {
        player = PlayerHealthController.instance.GetComponent<PlayerMovementHandler>();
        thePlayer = PlayerHealthController.instance.gameObject;
        playerAbility = thePlayer.GetComponent<PlayerAbilityTracker>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < distanceToOpen)
        {
            anim.SetBool("doorOpen", true);
        }
        else
        {
            anim.SetBool("doorOpen", false);
        }
        if (playerExiting)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, exitPoint.position, movePlayerSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!playerExiting)
            {
                player.canMove = false;

                StartCoroutine(UseDoor());
            }
        }
    }

    IEnumerator UseDoor()
    {
        playerExiting = true;
        player.anim.enabled = false;

        UIController.instance.StartFadeToBlack();

        yield return new WaitForSeconds(1.5f);
    
        RespawnController.instance.SetSpawnPoint(exitPoint.position);
        player.canMove = true;
        player.anim.enabled = true;

        UIController.instance.StartFadeToNormal();

        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        int previousIndex = SceneManager.GetActiveScene().buildIndex - 1;

        if (isGoingToNextScene)
        {
            if (SceneManager.sceneCountInBuildSettings > nextIndex)
            {
                SceneManager.LoadScene(nextIndex); 
            }
            else
            {
                setToZero();
                SceneManager.LoadScene(0);
               /* 
                
                RespawnController.instance.SetSpawnPoint(Vector3.zero);
                */
                
                
                //Make this for prevent the bug. if there is not a next scene load the first scene but all the progress that the player made is not reset so make it to reset
                
            }
            
        }
        else if (isGoingToPreviousScene)
        {
            SceneManager.LoadScene(previousIndex); 
        }
    }
    void setToZero()
    {
        player.standing.SetActive(true);
        player.ball.SetActive(false);
        thePlayer.transform.position = Vector3.zero;
        PlayerHealthController.instance.FillHeath(); 
        playerAbility.canDash = false;
        playerAbility.canBecomeBall = false;
        playerAbility.canDoubleJump = false;
        playerAbility.canDropBomb = false;
    }
}
