using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Player only relevant variables
    private Rigidbody2D rb;
    private bool isGrounded;
    private Animator animator;
    private GameObject gun;
    private bool isFlipped;

    //Player variables that need to be accessed elsewhere, or can be changed in the editor
    public float jumpForce;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        gun = GameObject.Find("Gun");
        isFlipped = false;
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        ChangeGravity();
        Shoot();
    }

    //spacebar = jump code
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            if (isFlipped)
            {
                rb.AddForce(-transform.up * jumpForce, ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    //use w and s to change the gravity scale
    void ChangeGravity()
    {
        if (Input.GetKey(KeyCode.S))
        {
            rb.gravityScale += 0.1f;
        }

        if (Input.GetKey(KeyCode.W))
        {
            rb.gravityScale -= 0.1f;
        }

        //fliiping the player if gravity scale is negative
        if(rb.gravityScale < 0 && isFlipped == false)
        {
            StartCoroutine(Flip(0.2f, -1.0f));
            isFlipped = true;

        }
        else if(rb.gravityScale >= 0 && isFlipped == true)
        {
            StartCoroutine(Flip(0.2f, 1.0f));
            isFlipped = false;
        }
    }

    //take in the amount of time to flip the player, and what the new why of the player should be after the flip, than lerp to the new scale over the amount of time
    IEnumerator Flip(float time, float desiredY)
    {

        Vector3 originalScale = transform.localScale;
        Vector3 destinationScale = new Vector3(transform.localScale.x, desiredY, transform.localScale.z);

        float currentTime = 0.0f;

        do
        {
            transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);

    }

    //create a bullet and give it a force
    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            GameObject bullet = (GameObject)Instantiate(Resources.Load("Bullet"));
            bullet.transform.position = gun.transform.position;
            bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(1.0f, 0.0f) * 1000);
            AudioSource a = GameObject.Find("shootSound").GetComponent<AudioSource>();
            a.Play();
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            onDeath();
        }
    }

    //logic so the player can only jump off of the ground, and jump animation logic
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ground")
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }

        if (collision.tag == "Projectile")
        {
            onDeath();
        }
    }

    void onDeath()
    {
        SceneManager.LoadScene(2);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            isGrounded = false;
            animator.SetBool("isJumping", true);
        }
    }

}
