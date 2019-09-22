using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinWaveEnemy : EnemyController
{

    private int health;
    private string path = "EnemySprites/darkgray";
    private Sprite sprite;

    //sin wave movement variables
    float amplitude;
    float frequency;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<SpriteRenderer>();
        sprite = Resources.Load<Sprite>(path);
        Debug.Log(sprite);
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(path);
        gameObject.AddComponent<PolygonCollider2D>();
        gameObject.transform.localScale = gameObject.transform.localScale / 2;
        gameObject.transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        transform.position = new Vector2(transform.position.x, -0.05f);
        //increase amplitude as the game goes on
        if(enemyModifier < 50)
        {
            amplitude = 1.0f;
        }
        else if(enemyModifier < 100)
        {
            amplitude = 2.0f;
        }
        else
        {
            amplitude = 3.0f;
        }
        
        frequency = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += amplitude * (Mathf.Sin(2 * Mathf.PI * frequency * Time.time) - Mathf.Sin(2 * Mathf.PI * frequency * (Time.time - Time.deltaTime))) * transform.up;
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
