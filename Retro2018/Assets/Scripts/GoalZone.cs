using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalZone : MonoBehaviour {

    public Text scoreText;
    int score;

    public GameObject racket;

    private void Awake()
	{
        score = 0;
        scoreText.text = score.ToString();

        scoreText.color = racket.GetComponent<SpriteRenderer>().color;
	}

	/*
     * Este método se llama automáticamente cuando algo entra dentro del trigger...
    */
	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.gameObject.tag == "Ball")
        {
            Debug.Log("Alguien ha marcado gol");
            //score = score +1;
            //score += 1;
            score++;//incrementar variable en una unidad
            scoreText.text = score.ToString();

            GameManager.sharedInstance.GoalScored();
        }
	}

}
