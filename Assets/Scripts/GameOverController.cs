using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{

    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetFloat("Score") == PlayerPrefs.GetFloat("Highscore"))
        {
            scoreText.text = "New Highscore: " + Mathf.RoundToInt(PlayerPrefs.GetFloat("Highscore"));
        }
        else
        {
            scoreText.text = "You Scored: " + Mathf.RoundToInt(PlayerPrefs.GetFloat("Score")) + "\n Highscore: " + Mathf.RoundToInt(PlayerPrefs.GetFloat("Highscore"));
        }

        PlayerPrefs.SetFloat("Score", 0.0f);

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

    public void OnClick()
    {
        GameObject.Find("Space_Reggae").GetComponent<Music>().StopMusic();
        SceneManager.LoadScene(0);
    }
}
