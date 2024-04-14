using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;       
    public float jumpForce = 5f;       
    public Rigidbody2D rb;              
    private bool isGrounded = true;

    [Header("Death")]
    public GameObject deathScreen;
    public GameObject retryButton, menuButton;
    public bool die;

    [Header("HP")]
    public int hp = 100;
    public Slider healthBar;

    [Header("Animation")]
    public Animator anim;



    private void Start()
    {
        Cursor.visible = false; //MAKE SURE TO SET THIS TO ACTIVE WHEN INTERACTING WITH MENUS
        retryButton.SetActive(false);
        menuButton.SetActive(false);

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        MovePlayer();
        Jump();

        //update health bar
        healthBar.value = hp;
    }

    void MovePlayer()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal"); //right and left movement
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y); // Set the horizontal velocity
    }

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.tag == "Death")
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

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }




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
