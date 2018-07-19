using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour {

    public static UIManager sharedInstance;

    public Text titleLabel;
    public Text scoreLabel;

    private int totalScore;

	// Use this for initialization
	void Awake () {
        if(sharedInstance == null){
            sharedInstance = this;
        }
        totalScore = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if(GameManager.sharedInstance.gamePaused||!GameManager.sharedInstance.gameStarted){
            titleLabel.enabled = true;
        } else {
            titleLabel.enabled = false;
        }
	}


    public void ScorePoints(int points){
        totalScore += points;
        scoreLabel.text = "Score: " + totalScore;
    }
}
