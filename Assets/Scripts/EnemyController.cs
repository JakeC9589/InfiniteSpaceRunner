using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : GameManager
{
    public Transform spawnHigh;
    public Transform spawnLow;

    GameObject enemy;

    //I am going to use this integer to generate different enemies based on how long the game has gone on. 
    //The modifier will increase after certain time intervals
    public int enemyModifier;
    

    //I will use this time variable to increment the enemy modifier over time
    private float enemyCreateTimer;
    private float enemyTimeInterval;

    // Start is called before the first frame update
    void Start()
    {
        enemyModifier = 0;
        enemySpeed = 100.0f;
        enemyCreateTimer = 0.0f;

        //first enemy is always the same so I create it in start.
        enemy = new GameObject("Enemy");
        float spawnY = Random.Range(spawnLow.position.y, spawnHigh.position.y);
        enemy.transform.position = new Vector3(spawnHigh.position.x, spawnY, 0.0f);
        enemy.AddComponent<TackleEnemy>();
        enemy.tag = "Enemy";
        enemy.AddComponent<Rigidbody2D>();
        enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        enemy.GetComponent<Rigidbody2D>().AddForce(-transform.right * enemySpeed);
        enemyTimeInterval = 10.0f;

    }

    // Update is called once per frame
    void Update()
    {
        enemyCreateTimer += Time.deltaTime;
        //every 30 seconds reset time, create a new enemy, and increment the enemy modifier.
        if (enemyCreateTimer >= enemyTimeInterval)
        {
            if (enemy)
            {
                Destroy(enemy);
            }
            CreateNewEnemy();
            enemyModifier += 1;
            //increase speed as the game goes on
            if (enemySpeed < 1000)
            {
                enemySpeed += enemyModifier * 2;
            }

            //increase the rate at which enemys spawn as the game goes on
            if (enemyModifier < 50)
            {
                enemyTimeInterval = 8.0f;
            }
            else if (enemyModifier < 200)
            {
                enemyTimeInterval = 5.0f;
            }
            else
            {
                enemyTimeInterval = 3.0f;
            }
            enemyCreateTimer = 0.0f;
        }
    }

    private void CreateNewEnemy()
    { 

        if (enemy == null)
        {
            //Add componenets all enemies need
            enemy = new GameObject("Enemy");
            float spawnY = Random.Range(spawnLow.position.y, spawnHigh.position.y);
            enemy.transform.position = new Vector3(spawnHigh.position.x, spawnY, 0.0f);
            enemy.tag = "Enemy";
            enemy.AddComponent<Rigidbody2D>();
            enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
            enemy.GetComponent<Rigidbody2D>().AddForce(-transform.right * enemySpeed);

            //Randomly select an enemy and create it
            int rand = Random.Range(0, 3);

            //add the script based on the random number
            switch (rand)
            {
                case 0:
                    
                    enemy.AddComponent<TackleEnemy>();
                    break;
                case 1:

                    enemy.AddComponent<SlowProjectileEnemy>();
                    break;
                case 2:

                    enemy.AddComponent<SinWaveEnemy>();
                    break;
            }

           

        }
    }
}
