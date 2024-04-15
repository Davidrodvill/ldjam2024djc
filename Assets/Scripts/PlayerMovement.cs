using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;       
    public float jumpForce = 5f;
    public float pushbackForce = 1000f;
    Rigidbody2D rb;              
    //private bool isGrounded = true;
    public bool canMove = true;

    [Header("Death")]
    public GameObject deathScreen;
    public GameObject retryButton, menuButton;
    public bool die;

    [Header("HP")]
    public int hp = 100;
    public Slider healthBar;

    [Header("Animation")]
    public Animator anim;
    public bool facingRight;

    [Header("For Jumping")]
    public GameObject j1, j2; //jump checks
    public bool canJump = true;

    


    public ButtonManager buttonMngr;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Cursor.visible = false; //MAKE SURE TO SET THIS TO ACTIVE WHEN INTERACTING WITH MENUS
        retryButton.SetActive(false);
        menuButton.SetActive(false);
        canMove = true;
        canJump = true;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
        //update health bar
        healthBar.value = hp;
        if (hp < 0)
        {
            Die();


        }

        //check if we're on a platform
        if (Physics2D.OverlapArea(j1.transform.position, j2.transform.position))
        {
            canJump = Physics2D.OverlapArea(j1.transform.position, j2.transform.position).gameObject.tag == "Ground";


        }
        else
        {
            canJump = false;

        }
        //jumping
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.AddForce(new Vector3(0, jumpForce));
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
        
    }

    void MovePlayer()
    {
        if(canMove)
        {

            if (Input.GetAxisRaw("Horizontal") > 0 ) //right
            {
                Debug.Log("YOU ARE PRESSING D");
                facingRight = true;
                //rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y); // Set the horizontal velocity
                transform.position += new Vector3(moveSpeed * Time.deltaTime, 0);

                
                anim.SetBool("isWalking", true);
                //if we were facing left, flip
                if (!facingRight)
                {
                    facingRight = true;
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    Debug.Log("YOU SHOULD BE FACING LEFT NOW");
                    //gameObject.transform.Rotate(0, 0, 0);

                }
                
            }
            
            else
            {
                anim.SetBool("isWalking", false);
            }
            
            if (Input.GetAxisRaw("Horizontal") < 0) //left
            {
                facingRight = false;
                transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0);

                //if we were facing right, flip
                if (facingRight)
                {
                    facingRight = false;
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    Debug.Log("YOU SHOULD BE FACING RIGHT NOW");
                    //gameObject.transform.Rotate(0, 180, 0);
                }



                anim.SetBool("isWalking", true);
            }

            

        }
        


        
    }

    /*
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse); //jump
            isGrounded = false; 
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        // && collision.contacts[0].normal.y > 0.5
        // Check if the player touched ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        
    }
    */

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.tag == "Death")
        {
            Die();
        }

       if(other.tag == "WinGate1")
        {
            buttonMngr.Level2();

        }

       if(other.tag == "WinGate2")
        {

            buttonMngr.Level3();
        }

       if(other.tag == "WinGate3")
        {
            Cursor.visible = true;
            buttonMngr.EndScene();

        }

       if(other.tag == "FirePillar")
        {
            hp = hp - 25;
            //should add a force that pushes the player back
            rb.AddForce(new Vector3(-pushbackForce, 0));
            StartCoroutine(ForceResetKinda());
        }

        if (other.tag == "Fire")
        {
            hp = hp - 10;
            //should add a force that pushes the player back
            rb.AddForce(new Vector3(-pushbackForce, 0));
            StartCoroutine(ForceResetKinda());
        }

    }
    IEnumerator ForceResetKinda()
    {
        yield return new WaitForSeconds(0.5f);
        rb.AddForce(new Vector3(pushbackForce, 0));
    }



    void Die()
    {

            Cursor.visible = true;
            //death splash screen set true
            //deathScreen.SetActive(true);
            StartCoroutine(FadeBlackOutSquare());

            //retry and exit buttons set to true
            retryButton.SetActive(true);
            menuButton.SetActive(true);
            //death sound plays

    }
    
    /*
    void OnCollisionExit2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    */


    //blackout
    public IEnumerator FadeBlackOutSquare(bool fadeToBlack = true, int fadeSpeed = 2)
    {

        Color objectColor = deathScreen.GetComponent<Image>().color;
        float fadeAmount;

        if (fadeToBlack)
        {
            while (deathScreen.GetComponent<Image>().color.a < 1)
            {

                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                deathScreen.GetComponent<Image>().color = objectColor;
                yield return null;
            }

        }
        else
        {

            while (deathScreen.GetComponent<Image>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                deathScreen.GetComponent<Image>().color = objectColor;
                yield return null;

            }

        }
    }





}
