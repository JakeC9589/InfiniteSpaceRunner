using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : GameManager
{

    float coinTimer;
    int coinTimeInterval;

    public Transform spawnHigh;
    public Transform spawnLow;
    private Transform target;

    private 

    // Start is called before the first frame update
    void Start()
    {
        coinTimer = 0.0f;
        coinTimeInterval = 0;
        target = spawnHigh;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(spawnHigh.position, spawnLow.position, (Mathf.Sin(3 * Time.time) + 1.0f) / 2.0f);

        coinTimer += Time.deltaTime;

        //spawn coins for a randomamount of time every random amount of time
        if(coinTimer >= coinTimeInterval)
        {
            StartCoroutine("CreateCoins");
        }
    }

    IEnumerator CreateCoins()
    {
        coinTimeInterval = Random.Range(3, 9);

        for (int i = 0; i <= coinTimeInterval; i++)
        {
            coinTimer = 0;

            GameObject coin = (GameObject)Instantiate(Resources.Load("Coin"));
            coin.transform.position = transform.position;
            coin.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1.0f, 0.0f) * enemySpeed);

            yield return new WaitForSeconds(0.5f);
        }
    }
}
