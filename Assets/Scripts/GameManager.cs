using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //varibles I'll need to access in other places
    private static float score;
    private float highScore;
    public static float enemySpeed;

    private Text scoreText;
    private Text gravityText;
    private Rigidbody2D playerRB;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        if (PlayerPrefs.HasKey("Highscore")) {
            highScore = PlayerPrefs.GetFloat("Highscore");
        }
        else
        {
            highScore = 0;
        }
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        gravityText = GameObject.Find("Gravity Scale").GetComponent<Text>();
        playerRB = GameObject.Find("Player").GetComponent<Rigidbody2D>();

        //https://forum.unity.com/threads/aspect-ratio-issue-on-build.543722/
        // set the desired aspect ratio (the values in this example are
        // hard-coded for 16:9, but you could make them into public
        // variables instead so you can set them at design time)
        float targetaspect = 16.0f / 9.0f;

        // determine the game window's current aspect ratio
        float windowaspect = (float)Screen.width / (float)Screen.height;

        // current viewport height should be scaled by this amount
        float scaleheight = windowaspect / targetaspect;

        // obtain camera component so we can modify its viewport
        Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();

        // if scaled height is less than current height, add letterbox
        if (scaleheight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            camera.rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }

    // Update is called once per frame
    void Update()
    {
        score += Time.deltaTime;
        PlayerPrefs.SetFloat("Score", score);
        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("Highscore", highScore);
        }
        scoreText.text = "Score: " + Mathf.RoundToInt(score).ToString();
        gravityText.text = "Gravity: " + Math.Round(playerRB.gravityScale, 2).ToString();
    }

    public float GetScore()
    {
        return score;
    }

    public void SetScore(float newScore)
    {
        score = newScore;
    }
}
