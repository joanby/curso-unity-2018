using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public float speed = 25;
    private bool hasTheBallMoved = false;

	// Use this for initialization
	void Start () {
	
    }

	private void Update()
	{
        if(GameManager.sharedInstance.gameStarted  && !hasTheBallMoved ){
            GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
            hasTheBallMoved = true;
        }

        if (GameManager.sharedInstance.gameStarted)
        {
            //TERNARIO
            //(condicion ? valor si verdad : valor si falso)
            string racketName = (GetComponent<Rigidbody2D>().velocity.x > 0 ? "Racket Left" : "Racket Right");
            GameObject racket = GameObject.Find(racketName);
            GetComponent<SpriteRenderer>().color = racket.GetComponent<SpriteRenderer>().color;

        }

	}

	/*
     * El objeto collision del paréntesis contiene la información del choque. 
     * En particular, me interesa saber cuando choca con una raqueta.
     * - collision.gameObject : tiene información del objeto contra el cual he colisionado (raqueta)
     * - collision.transform.position: tiene información de la posición de la raqueta
     * - collision.collider: tiene información del collider de la raqueta
     */

	private void OnCollisionEnter2D(Collision2D collision)
	{
        if(collision.gameObject.name == "Racket Left"){
            float y = hitFactor(transform.position,
                                collision.transform.position,
                                collision.collider.bounds.size.y);
            Vector2 direction = new Vector2(1, y);//convierte el vector a módulo 1
            GetComponent<Rigidbody2D>().velocity = direction*speed;//ahora tiene módulo speed
        }
        if (collision.gameObject.name == "Racket Right")
        {
            float y = hitFactor(transform.position,
                                collision.transform.position,
                                collision.collider.bounds.size.y);
            Vector2 direction = new Vector2(-1, y);
            GetComponent<Rigidbody2D>().velocity = direction * speed;
        }
	}

    /*
    *  1/2 - La bola choca contra la parte superior de la raqueta
    *  0   - La bola choca contra el centro de la raqueta
    * -1/2 - La bola choca contra la parte inferior de la raqueta
    * Método que devuelve el factor antes citado
    */
    float hitFactor(Vector2 ballPosition, Vector2 racketPosition, float raquetHeight){
        return (ballPosition.y - racketPosition.y) / raquetHeight;
    }

}
