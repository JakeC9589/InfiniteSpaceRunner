using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowProjectileEnemy : EnemyController
{
    private int health;
    private string path = "EnemySprites/blue";
    private Sprite sprite;
    private float time;
    private GameObject player;
    private int shotTimer;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<SpriteRenderer>();
        sprite = Resources.Load<Sprite>(path);
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(path);
        gameObject.AddComponent<PolygonCollider2D>();
        gameObject.transform.localScale = gameObject.transform.localScale / 2;
        gameObject.transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        time = 0;
        player = GameObject.Find("Player");
        //using theenemy modifier to determine how fast the enemy shoots
        if (enemyModifier <= 4 && enemyModifier >= 0)
        {
            shotTimer = 5 - enemyModifier;
        }
        else
        {
            shotTimer = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
       
        if(time >= shotTimer)
        {
            time = 0;
            //shoot a projectile towards the player every 5 seconds
            GameObject projectile = (GameObject)Instantiate(Resources.Load("Bullet"));
            projectile.tag = "Projectile";
            projectile.transform.position = transform.position;
            projectile.GetComponent<SpriteRenderer>().color = Color.red;
            projectile.GetComponent<Rigidbody2D>().gravityScale = 0;
            projectile.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            Vector3 dir = player.transform.position - transform.position;
            projectile.GetComponent<Rigidbody2D>().AddForce(dir.normalized * 200);
            AudioSource a = GameObject.Find("shootSound").GetComponent<AudioSource>();
            a.Play();

        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Bullet")
        {
            SetScore(GetScore() + 10);
            Destroy(gameObject);
        }
    }
}
