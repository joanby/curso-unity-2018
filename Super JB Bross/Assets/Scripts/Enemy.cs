using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float runningSpeed = 1.5f;

    private Rigidbody2D rigidbody;

    public static bool turnAround;

    private Vector3 startPosition;


	private void Awake()
	{
        rigidbody = GetComponent<Rigidbody2D>();
        startPosition = this.transform.position;

	}

	private void Start()
	{
        this.transform.position = startPosition;
	}


	private void FixedUpdate()
	{

        float currentRunningSpeed = runningSpeed;

        if (turnAround == true)
        {
            //aquí la velocidad es positiva...
            currentRunningSpeed = runningSpeed;
            this.transform.eulerAngles = new Vector3(0, 180.0f, 0);
        }
        else
        {
            //aquí la velocidad es negativa
            currentRunningSpeed = -runningSpeed;
            this.transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if(GameManager.sharedInstance.currentGameState == GameState.inGame){
            rigidbody.velocity = new Vector2(currentRunningSpeed, rigidbody.velocity.y);
        }
	}


}
