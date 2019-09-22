using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < -10.0f)
        {
            Destroy(gameObject);
        }
    }

    //logic so the player can only jump off of the ground, and jump animation logic
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameObject gm = GameObject.Find("GameManager");
            GameManager gmScript = gm.GetComponent<GameManager>();
            gmScript.SetScore(gmScript.GetScore() + 10.0f);
            Destroy(gameObject);
        }
    }
}
