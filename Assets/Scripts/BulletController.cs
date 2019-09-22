using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * -20);
        if(transform.position.x - player.transform.position.x >= 20)
        {
            Destroy(gameObject);
        }
    }
}
