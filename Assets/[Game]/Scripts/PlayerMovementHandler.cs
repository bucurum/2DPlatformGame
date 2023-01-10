using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{
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
    [SerializeField] float dashSpeed;
    [SerializeField] float dashTime;
    private float dashCounter;
    
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer afterImage;
    [SerializeField] float afterImageLifeTime;
    [SerializeField] float timeBetweenAfterImage;
    private float afterImageCounter;
    [SerializeField] Color afterImageColor;
    [SerializeField] float waitAfterDashing;
    private float dashRechargeCounter;


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
            if (Input.GetButtonDown("Fire2"))
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

        if (Input.GetButtonDown("Jump") && (isGrounded || canDoubleJump))
        {
            if (isGrounded)
            {
               canDoubleJump = true; 
            }
            else
            {
                canDoubleJump = false;
                anim.SetTrigger("doubleJump");
            }
            
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(shotToFire, shotPoint.position, shotPoint.rotation).moveDirection = new Vector2(transform.localScale.x, 0);
            anim.SetTrigger("shotFired");
        }


        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("speed",Mathf.Abs(rb.velocity.x));

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

}
