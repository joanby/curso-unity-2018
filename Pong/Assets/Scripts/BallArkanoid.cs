using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallArkanoid : MonoBehaviour {

    private float speed;
    public float initialSpeed = 5;
    public int lives = 3;

    [SerializeField]
    [Range(1.0f, 2.0f)]
    public float difficultyFactor = 1.005f;

	// Use this for initialization
	void Start () {
        speed = initialSpeed;
        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
        StartCoroutine(UpgradeDifficulty());
	}


    IEnumerator UpgradeDifficulty(){
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            speed *= difficultyFactor;
        }
    }

    //Esta función se llama automáticamente cuando hay una colisión
	private void OnCollisionEnter2D(Collision2D collision)
	{
        GetComponent<AudioSource>().Play();

        //Comprobamos que el objeto con el que he colisionado es la pala
        if(collision.gameObject.name == "Paddle"){
            float x = hitFactor(this.transform.position,
                                collision.transform.position,
                                collision.collider.bounds.size.x);
            Vector2 direction = new Vector2(x, 1).normalized;
            GetComponent<Rigidbody2D>().velocity = direction * speed;
        }
	}
    /*

    -0.5    -0.25       0       0.25     0.5
    ======================================== <- esto es la pala

    */
    float hitFactor(Vector2 ball, Vector2 paddle, float paddleWidth){
        return (ball.x - paddle.x) / paddleWidth;
    }

    public GameObject ballStartPosition;

    public void ResetBall(){
        lives--;
        speed = initialSpeed;
        transform.position = ballStartPosition.transform.position;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if (lives > 0)
        {
            Invoke("RestartBallMovement", 2.0f);
        }
    }

    void RestartBallMovement(){
        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
    }

   
}
