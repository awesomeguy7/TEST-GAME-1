using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour {

    private Rigidbody2D rb;
    public Animator animator;
    public float speed;
    Vector2 direction = Vector2.right;
    public float horzMove = 1;
    bool facingright = false;
    public float knockback;
    public float knockbackLength;
    public float knockbackCount;
    public int hearts = 3;
    bool chasing = false;
    public float maxJumpTime = 0.3f;
    public float jumpCount = 0f;
    private float width;
    private float height;
    private float rayCastLength = 0.005f;
    public float jump;
    Vector2 Targetpos;
    bool wallOnLeft = false;
    bool wallOnRight = false;
    bool isJumping = false;
    bool noWallOnLeft = true;
    bool noWallOnRight = true;
    int vspeed = 0;
    bool groundCheck1;
    bool groundCheck2;
    bool groundCheck3;
    bool groundCheck4;
    float x = 0;
    bool change;
    bool WallOnRight;
    bool WallOnLeft;
    bool isFalling;
    public float lostLife = 0;
    public float lostAmount = 2f;
    public GameObject coin;
    public static GameObject thecoin;
    bool allowjump = true;

    // Use this for initialization
    void Awake () {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        width = GetComponent<Collider2D>().bounds.extents.x +0.2f;

        height = GetComponent<Collider2D>().bounds.extents.y + 0.16f;

        facingright = !facingright;
        transform.localScale = new Vector2(-1 * transform.localScale.x,
                   transform.localScale.y);

        


    }
	
    
    
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 playerpos = GameObject.Find("Tommy").transform.position;
      //  Debug.Log(playerpos.x);
      
        Debug.Log(playerpos.y - transform.position.y);
        if ((playerpos.x - transform.position.x >= 0) && (playerpos.x - transform.position.x <= 3) && (playerpos.y - transform.position.y <= 0.3) && (playerpos.y - transform.position.y >= 0))
        {
            allowjump = false;
            Debug.Log("DONT JUMP");
        }
        else if ((playerpos.x - transform.position.x <= 0) && (playerpos.x - transform.position.x >= -3) && (playerpos.y - transform.position.y <= 0.3) && (playerpos.y - transform.position.y >= 0))
        {
            allowjump = false;
            Debug.Log("DONT JUMP");
        }
        else allowjump = true;

            if (lostLife > 0)
        {
            lostLife -= Time.deltaTime;
        }
        //Debug.Log(isJumping);
        //  Debug.Log(IsNoWallOnLeftOrRight());
        //  Debug.Log(IsWallToLeftOrRight());
        IsOnGround();
   //   Debug.Log(WallOnLeft);
        if (knockbackCount<= 0) {
            //  Debug.Log(IsOnGround());
            IsOnGround();
            IsWallToLeftOrRight();
            IsWallUpOnLeftOrRight();
            
            if (!noWallOnRight && wallOnRight && direction.x > 0 && !isJumping && groundCheck1 && allowjump)
        {
            Jump();
                
            } else if (!noWallOnLeft && wallOnLeft && direction.x < 0 && !isJumping && groundCheck1 && allowjump)
        {  
            Jump();
                
            }
            else if (noWallOnLeft && WallOnLeft && direction.x < 0 && change == false)
        {
            Change();
            change = true;
        }
            else if (noWallOnRight && WallOnRight && direction.x > 0 && change == false)
            {
                Change();
                change = true;
            }
            else if (!isJumping)
        {
           
            rb.velocity = direction * speed;
        }

       

        if(isJumping)
        {
            
            rb.velocity = new Vector2(direction.x * speed * 1.5f, jump);
                jumpCount -= Time.deltaTime;



            }
            if (!isJumping && jumpCount != 0)
            {

                jumpCount -= Time.deltaTime;
            }
            if (jumpCount <= 0)
        {
                jumpCount = 0;
            isJumping = false;

               
            }


            

            if (!isJumping && !IsOnGround())
            {
                rb.velocity = new Vector2(direction.x *speed * 0.15f, -4f);
                change = false;
                Debug.Log("Gravity");
                isFalling = true;
                
            }

            /* if(!isJumping)
             {
             rb.velocity = direction * speed;
             }

             if(isJumping && (jumpCount >= maxJumpTime))
             {
                 isJumping = false;
             }
             if (IsWallToLeftOrRight())
             {
                 if (IsOnGround() && !isJumping)
                 {
                     isJumping = true;
                 }
                 if (isJumping && (jumpCount < maxJumpTime))
                 {
                     if (wallOnRight == true)
                     {
                         rb.velocity = new Vector2(jump, 2 * jump);
                     }
                     else if (wallOnLeft == true)
                     {
                         rb.velocity = new Vector2(-jump, 2 * jump);
                     }
                     jumpCount += Time.deltaTime;
                 }


               */
        }
        if(isFalling && IsOnGround())
        {
            isFalling = false;
        }
    
        // Vector2 vect = rb.velocity;

        if (knockbackCount > 0)
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
        /*
        if(knockbackCount<=0) { 
         if (chasing)
         {
             Targetpos.x = GameObject.Find("Tommy").transform.position.x;
            Targetpos.y = transform.position.y;

             transform.position = Vector2.MoveTowards(transform.position, Targetpos, speed * Time.deltaTime);

             //   if tommy is within jump range jump towards him or if Tommy jumps and gets higher then find where he jumped from and do the same jump
         }

        }

        */



    }
    private void OnTriggerEnter2D(Collider2D collision)
     {
         facingright = !facingright;
         transform.localScale = new Vector2(-1 * transform.localScale.x,
                 transform.localScale.y);

         // Change direction
         direction = new Vector2(-1 * direction.x, direction.y);
         
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Tommy" && !CharacterScript.canCallFunction && lostLife <= 0)
        {
            chasing = true;
            lostLife = lostAmount;
            hearts = hearts - 1;
            
               knockbackCount = knockbackLength;
            
            if(hearts == 2) { 
          Animator heartanim = GameObject.Find("Slimeheart3").GetComponent<Animator>();
            heartanim.Play("Hearts 0");
            } else if (hearts == 1)
            {
                Animator heartanim = GameObject.Find("Slimeheart2").GetComponent<Animator>();
                heartanim.Play("Hearts 0");
            }
            if (hearts < 1)
            {
                Animator heartanim = GameObject.Find("Slimeheart1").GetComponent<Animator>();
                heartanim.Play("Hearts 0");
               
               thecoin = Instantiate(coin, transform.position, Quaternion.identity);

                Destroy(gameObject);
                Destroy(GameObject.Find("Slimehearts"));
               


            }
        }
        else if(collision.gameObject.tag == "Tommy" && !chasing)
        {
            transform.localScale = new Vector2(-1 * transform.localScale.x,
                transform.localScale.y);

            // Change direction
            direction = new Vector2(-1 * direction.x, direction.y);
        } 
    }
    public bool IsOnGround() //copied content
    {

        // Check if contacting the ground straight down
        groundCheck1 = Physics2D.Raycast(new Vector2(
        transform.position.x,
        transform.position.y -0.45f ),
        -Vector2.up, rayCastLength);

        // Check if contacting ground to the right
        groundCheck2 = Physics2D.Raycast(new Vector2(
        transform.position.x + (width),
        transform.position.y - 0.45f ),
-Vector2.up, rayCastLength);
        
        // Check if contacting ground to the left
        groundCheck3 = Physics2D.Raycast(new Vector2(
        transform.position.x - (width),
        transform.position.y - 0.45f ),
-Vector2.up, rayCastLength);

        bool groundCheck4 = Physics2D.Raycast(new Vector2(
        transform.position.x,
        transform.position.y - height * 2),
        -Vector2.up, rayCastLength);

        if (groundCheck1 ||groundCheck2 ||  groundCheck3)
        {
            return true;
        } else
            

        return false;

    }
    public bool IsWallToLeftOrRight()
    {
        // 1
        wallOnLeft = Physics2D.Raycast(new Vector2(
        transform.position.x - 3f*width, transform.position.y),
        -Vector2.right, rayCastLength);
        wallOnRight = Physics2D.Raycast(new Vector2(
        transform.position.x + 3f*width, transform.position.y),
        Vector2.right, rayCastLength);
        // 2
        

        if (wallOnLeft || wallOnRight)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool IsWallUpOnLeftOrRight()
    {
        // 1
        noWallOnLeft = Physics2D.Raycast(new Vector2(
        transform.position.x - 3*width, transform.position.y + 2f* height),
        -Vector2.right, rayCastLength);
        noWallOnRight = Physics2D.Raycast(new Vector2(
        transform.position.x + 3*width, transform.position.y + 2* height),
        Vector2.right, rayCastLength);


        WallOnLeft = Physics2D.Raycast(new Vector2(
        transform.position.x - width, transform.position.y +  height),
        -Vector2.right, rayCastLength);
        WallOnRight = Physics2D.Raycast(new Vector2(
        transform.position.x + width, transform.position.y +  height),
        Vector2.right, rayCastLength);
        // 2
        if (noWallOnLeft || noWallOnRight)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void Jump ()
    {
        /*  while (jumpCount < maxJumpTime)
          {

              rb.velocity = new Vector2(knockback, 3 * knockback);

          if (wallOnLeft == true)
          {
              rb.velocity = new Vector2(-knockback, 3 * knockback);
          }
          jumpCount += Time.deltaTime;
          }
          */
        isJumping = true;
        jumpCount = maxJumpTime;

        
    }
    void Change()
    {
        facingright = !facingright;
        transform.localScale = new Vector2(-1 * transform.localScale.x,
                   transform.localScale.y);

        // Change direction
        direction = new Vector2(-1 * direction.x, direction.y);
        change = !change;
    }

}
