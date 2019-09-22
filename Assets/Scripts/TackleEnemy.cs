using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TackleEnemy : EnemyController
{

    private int health;
    private string path = "EnemySprites/green";
    private Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<SpriteRenderer>();
        sprite = Resources.Load<Sprite>(path);
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(path);
        gameObject.AddComponent<PolygonCollider2D>();
        gameObject.transform.localScale = gameObject.transform.localScale / 2;
        gameObject.transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }

    // Update is called once per frame
    void Update()
    {

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
