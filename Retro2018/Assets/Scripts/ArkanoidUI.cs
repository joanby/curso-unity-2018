using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ArkanoidUI : MonoBehaviour {

    public Image live1, live2, live3;
    public Text gameOverText, gameWinText, durationText, highscoreText;
    private bool hasTheGameEnded = false;

    private float gameTime = 0.0f;

    BallArkanoid ball;

	// Use this for initialization
	void Start () {
        gameOverText.enabled = false;
        gameWinText.enabled = false;
        ball = GameObject.Find("Ball").GetComponent<BallArkanoid>();
        highscoreText.text = "Mejor: "+PlayerPrefs.GetFloat("highscore", 9999).ToString("N2");
	}
	
	// Update is called once per frame
	void Update () {
        if(hasTheGameEnded){
            return;
        }

        gameTime += Time.deltaTime;
        durationText.text = gameTime.ToString("N2");

        if(ball.lives < 3){
            live3.enabled = false;
        }
        if(ball.lives < 2){
            live2.enabled = false;
        }
        if(ball.lives < 1){
            live1.enabled = false;
            gameOverText.enabled = true;
            Invoke("GoToMainMenu", 2.0f);
            hasTheGameEnded = true;
        }
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        if(blocks.Length == 0){
            gameWinText.text = "Enhorabuena\nHas ganado en " + gameTime.ToString("N2");
            gameWinText.enabled = true;
            Invoke("GoToMainMenu", 2.0f);
            hasTheGameEnded = true;

            float highscore = PlayerPrefs.GetFloat("highscore", 9999);
            if(gameTime < highscore){
                PlayerPrefs.SetFloat("highscore", gameTime);
            }

        }


	}


    void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

}
