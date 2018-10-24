using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Librería para cargar la Interfaz gráfica de usuario

public class GameManager : MonoBehaviour {

    public static GameManager sharedInstance = null;
    public bool gameStarted = false;


    public Text title;
    public Button start;

   

	private void Awake()
	{
        if(sharedInstance == null){
            sharedInstance = this;
        }
	}

    public void StartGame(){
        gameStarted = true;
        title.gameObject.SetActive(false);
        start.gameObject.SetActive(false);
        ball = GameObject.FindGameObjectWithTag("Ball");
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    GameObject ball;
    Vector2 nextDirection;

    public void GoalScored(){
        ball.GetComponent<TrailRenderer>().time = 0;
        ball.transform.position = Vector2.zero;
        nextDirection = new Vector2(-ball.GetComponent<Rigidbody2D>().velocity.x, 0);
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        Invoke("LaunchBall", 2.0f);
    }

    void LaunchBall(){
        ball.GetComponent<TrailRenderer>().time = 1;
        Ball b = ball.GetComponent<Ball>();
        ball.GetComponent<Rigidbody2D>().velocity = nextDirection.normalized * b.speed;
    }
}
