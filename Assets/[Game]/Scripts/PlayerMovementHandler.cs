using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{
    [Header("Player")]
    public Rigidbody2D rb;
    [SerializeField] float movementSpeed;
    [SerializeField] float jumpForce;
    public Transform groundPoint;
    private bool isGrounded;
    public LayerMask groundLayer;
    public Animator anim;
    public BulletController shotToFire;
    public Transform shotPoint;
    private bool canDoubleJump;

    [Header("Player_Dash")]
    [SerializeField] float dashSpeed;
    [SerializeField] float dashTime;
    private float dashCounter;
    private float dashRechargeCounter;
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer afterImage;
    [SerializeField] float afterImageLifeTime;
    [SerializeField] float timeBetweenAfterImage;
    private float afterImageCounter;
    [SerializeField] Color afterImageColor;
    [SerializeField] float waitAfterDashing;

    [Header("Player_Ball")]
    public GameObject standing;
    public GameObject ball;
    [SerializeField] float waitToBall;
    private float ballCounter;
    
    [Header("Bomb")]
    public Transform bombPoint;
    public GameObject bomb;
    private float bombRechargeCounter;
    [SerializeField] float bombWaitTime;
    private bool isBombPlaceable;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (dashRechargeCounter > 0)
        {
            dashRechargeCounter -= Time.deltaTime;
        }
        else
        {
            if (Input.GetButtonDown("Fire2") && standing.activeSelf)
            {
                dashCounter = dashTime;
                ShowAfterImage();
            }
        }
        
        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            rb.velocity = new Vector2(dashSpeed * transform.localScale.x, rb.velocity.y);

            afterImageCounter -= Time.deltaTime;
            if (afterImageCounter < 0)
            {
                ShowAfterImage();
            }
            dashRechargeCounter = waitAfterDashing;
        }
        else
        {
            //in this code we set player movement with "ad or arrow keys"
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * movementSpeed, rb.velocity.y); //there is 2 GetAxis method, GetAxis and GetAxisRaw, the diffrence between two is GetAxis is wait a little time and add movement to player movement, GetAxisRaw is when you press the keys the player move instantly
    
            //handle direction change
            if (rb.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (rb.velocity.x > 0)
            {
                transform.localScale = Vector3.one;
            }
        }
        
        isGrounded = Physics2D.OverlapCircle(groundPoint.position, .2f, groundLayer);

        if (isGrounded)
        {
            canDoubleJump = true;
        }

        if (Input.GetButtonDown("Jump") && (isGrounded || canDoubleJump))
        {
            if(!isGrounded)
            {
                canDoubleJump = false;
                anim.SetTrigger("doubleJump");
            }
            
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        
        BombWaitTime();

        if (Input.GetButtonDown("Fire1"))
        {
            if (standing.activeSelf)
            {
                Instantiate(shotToFire, shotPoint.position, shotPoint.rotation).moveDirection = new Vector2(transform.localScale.x, 0);
                anim.SetTrigger("shotFired");
            }
            else if (ball.activeSelf && isBombPlaceable)
            {
                Instantiate(bomb, bombPoint.position, bombPoint.rotation);
                isBombPlaceable = false;
            }   
        }

        //ballMode
        if (!ball.activeSelf)
        {
            if (Input.GetAxisRaw("Vertical") < -.9f)
            {
                ballCounter -= Time.deltaTime;
                if (ballCounter <= 0)
                {
                    ball.SetActive(true);
                    standing.SetActive(false);
                }
            }
            else
            {
                ballCounter = waitToBall;
            }
        }
        else
        {
            if (Input.GetAxisRaw("Vertical") > .9f)
            {
                ballCounter -= Time.deltaTime;
                if (ballCounter <= 0)
                {
                    ball.SetActive(false);
                    standing.SetActive(true);
                }
            }
            else
            {
                ballCounter = waitToBall;
            }
        }

        if (standing.activeSelf)
        {
            anim.SetBool("isGrounded", isGrounded);
            anim.SetBool("isBallActive", false);
            anim.SetFloat("speed",Mathf.Abs(rb.velocity.x)); 
        }
        if (ball.activeSelf)
        {
            anim.SetBool("isBallActive", true);  
            anim.SetFloat("speed",Mathf.Abs(rb.velocity.x)); 
        }
        
        
    }

    public void ShowAfterImage()
    {
        SpriteRenderer image = Instantiate(afterImage, transform.position , Quaternion.identity);
        image.sprite = spriteRenderer.sprite;
        image.transform.localScale = transform.localScale;
        image.color = afterImageColor;

        Destroy(image.gameObject, afterImageLifeTime);

        afterImageCounter = timeBetweenAfterImage;
    }

    public void BombWaitTime()
    {
        if (bombRechargeCounter > 0)
        {
            bombRechargeCounter -= Time.deltaTime;
        }
        else
        {
            bombRechargeCounter = bombWaitTime;
            isBombPlaceable = true;
        } 
    }

}
