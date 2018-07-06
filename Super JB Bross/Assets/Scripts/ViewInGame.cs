using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewInGame : MonoBehaviour {

    public Text collectableLabel;
    public Text scoreLabel;
    public Text maxscoreLabel;


	// Update is called once per frame
	void Update () {
        if(GameManager.sharedInstance.currentGameState == GameState.inGame ||
           GameManager.sharedInstance.currentGameState == GameState.gameOver){
            int currentObjects = GameManager.sharedInstance.collectedObjects;
            this.collectableLabel.text = currentObjects.ToString();
        }

        if(GameManager.sharedInstance.currentGameState == GameState.inGame){
            float travelledDistance = PlayerController.sharedInstance.GetDistance();
            this.scoreLabel.text = "Score\n" + travelledDistance.ToString("f1");

            float maxscore = PlayerPrefs.GetFloat("maxscore", 0);
            this.maxscoreLabel.text = "MaxScore\n" + maxscore.ToString("f1");
        }

	}
}
