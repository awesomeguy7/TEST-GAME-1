using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterScript : MonoBehaviour
{

    private Rigidbody2D rb;
    static public Animator animator;
    public float speed = 30;
    bool facingright = false;
    public float maxJumpTime = 0.2f;
    private float jumpButtonPressTime;
    private float rayCastLength = 0.005f;
    private float width;
    private float height;
    bool isJumping = false;
    float horzMove = 0;
    public int hearts = 3;
    bool IsDead = false;
    public float knockback;
    public float knockbackLength;
    public float knockbackCount;
    bool swap = false;
    bool sword = false;
    bool coin = false;
    bool oldman = false;
    public static bool canCallFunction = true;
    public float wallJumpY = 10f;
    public float lostLife = 0;
    public float lostAmount = 2f;
    public int chatnumber = 0;
    


    // Use this for initialization


    // Update is called once per frame
    void Awake()
    {

        rb = GetComponent<Rigidbody2D>();

        //   m_NewForce = new Vector2(-5.0f, 1.0f);

        animator = GetComponent<Animator>();

        width = GetComponent<Collider2D>().bounds.extents.x + 0.1f;

        height = GetComponent<Collider2D>().bounds.extents.y + 0.4f;

        

    }
    void FixedUpdate()
    {
        if(lostLife > 0) { 
        lostLife = lostLife - Time.deltaTime;
        }
        /*  if (IsWallOnLeftOrRight() && !IsOnGround() && horzMove == 1)
           {

               rb.velocity = new Vector2(-GetWallDirection() * speed * -.75f,
               wallJumpY);
           }
   */

        //  Debug.Log(lostLife);

        if (Input.GetKeyDown("e") && sword)
        {
            swap = true;
            animator.SetBool("Swap", true);
        } else if (Input.GetKeyDown("e") && coin)
        {
            Destroy(Slime.thecoin);
            Debug.Log("Coin gotten");
            var scoreComp = GameObject.Find("Text1").GetComponent<Score>();
            scoreComp.update_score();
            coin = false;

        }
        if (Input.GetKeyDown("e") && oldman)
        {
            if (chatnumber == 3 && GameObject.Find("Text1").GetComponent<Score>().score_count > 0)
            {
                chatnumber = 5;
            }
            else if (chatnumber == 4)
            {
                chatnumber = 3;
            } else if(chatnumber <= 3)
            {
                chatnumber += 1;
            }
            oldman = false;
            
        }
        var chatcomp = GameObject.Find("Text2").GetComponent<Chat>();
        chatcomp.FirstLine();

        if (!IsDead && canCallFunction)
        {
            horzMove = Input.GetAxisRaw("Horizontal");
            Vector2 vect = rb.velocity;
            if (knockbackCount <= 0)
            {
                rb.velocity = new Vector2(horzMove * speed, vect.y);
            }
            else if (knockbackCount > 0)
            {
                if (facingright)
                {
                    rb.velocity = new Vector2(-knockback, knockback);
                }
                if (!facingright)
                {
                    rb.velocity = new Vector2(knockback, knockback);
                }
                knockbackCount -= Time.deltaTime;
                
            }
            if (Input.GetKeyDown("x") && swap == true)
            {

                animator.Play("Swing");

                StartCoroutine(Wait(1f));
                Debug.Log(animator.GetCurrentAnimatorStateInfo(0).length);



            }
            else if (horzMove == 0 && IsOnGround() && swap == true && canCallFunction)
            {
                animator.Play("SwordIdle");
            }
            else if (horzMove != 0 && IsOnGround() && swap == true && canCallFunction)
            {
                animator.Play("SwordRunning");
            }
            else if (horzMove == 0 && IsOnGround() && canCallFunction)
            {
                animator.Play("Idle");
            }
            else if (horzMove != 0 && IsOnGround() && canCallFunction)
            {

                animator.Play("Running");
            }



            if (horzMove > 0 && !facingright)
            {

                FlipBob();
            }
            else if (horzMove < 0 && facingright)
            {

                FlipBob();
            }


            float jump = Input.GetAxisRaw("jump");
            Vector2 vect2 = rb.velocity;
            if (IsOnGround() && !isJumping)
            {
                if (jump > 0f)
                {

                    isJumping = true;

                }
            }


            if (jumpButtonPressTime > maxJumpTime)
            {
                if (swap) { jump = 0f; animator.Play("SwordJumping"); }
                else
                {


                    jump = 0f;
                    animator.Play("Jumping");
                }
            }
            if (isJumping && (jumpButtonPressTime < maxJumpTime))
            {
                if (jump > 0)
                {
                    rb.velocity = new Vector2(horzMove * speed, jump * speed);
                }
                if (jump >= 1f)
                {
                    jumpButtonPressTime += Time.deltaTime;
                }
                else
                {
                    isJumping = false;
                }
            }
            else
            {
                isJumping = false;
                jumpButtonPressTime = 0f;
            }
        }

    }

    void FlipBob()
    {
        facingright = !facingright;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

    }
    
    public bool IsOnGround() //copied content
    {

        // Check if contacting the ground straight down
        bool groundCheck1 = Physics2D.Raycast(new Vector2(
        transform.position.x,
        transform.position.y - height),
        -Vector2.up, rayCastLength);

        // Check if contacting ground to the right
        bool groundCheck2 = Physics2D.Raycast(new Vector2(
        transform.position.x + (width - 0.2f),
        transform.position.y - height),
-Vector2.up, rayCastLength);

        // Check if contacting ground to the left
        bool groundCheck3 = Physics2D.Raycast(new Vector2(
        transform.position.x - (width - 0.2f),
        transform.position.y - height),
-Vector2.up, rayCastLength);

        if (groundCheck1 || groundCheck2 || groundCheck3)
            return true;

        return false;

    }
    private void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Enemy" && canCallFunction && lostLife <= 0)
        {

            hearts = hearts - 1;
            knockbackCount = knockbackLength;
            Debug.Log(hearts);
            lostLife = lostAmount;
            
        }

        if (hearts <= 0)
        {

            IsDead = true;
            animator.Play("Dying");
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            
            Application.LoadLevel("MainScene");





        }
        

    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        //   Debug.Log(col);
        if (col.gameObject.tag == "Sword")
        {
            sword = true;
        }
        else sword = false;
        if (col.gameObject.tag == "Coin")
        {
            coin = true;
        }
        if(col.gameObject.tag == "OldMan")
        {
            Debug.Log("Hi");
            oldman = true;
        }
    }

    IEnumerator Wait(float time)
    {
        canCallFunction = false;
    //    Debug.Log("time started");
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(time);
     //   Debug.Log("time ended");
        canCallFunction = true;
    }
    /*   public bool IsWallOnLeft()
       {

           // -Vector2.right checks to the left with a raycast
           // for a wall
           return Physics2D.Raycast(new Vector2(transform.position.x - width,
           transform.position.y),
           -Vector2.right,
           rayCastLength);
       }

       public bool IsWallOnRight()
       {

           // Vector2.right checks to the left with a raycast
           // for a wall
           return Physics2D.Raycast(new Vector2(transform.position.x + width,
           transform.position.y),
           Vector2.right,
           rayCastLength);
       }

       // Verifies if walls are on left or right for wall jumping
       public bool IsWallOnLeftOrRight()
       {

           if (IsWallOnLeft() || IsWallOnRight())
           {
               return true;
           }
           else
           {
               return false;
           }
       }

       // Gets the wall direction if it exists
       // Multiply the results against Manny’s X velocity
       public int GetWallDirection()
       {
           if (IsWallOnLeft())
           {
               return -1;
           }
           else if (IsWallOnRight())
           {
               return 1;
           }
           else
           {
               return 0;
           }
       }
       */
}





