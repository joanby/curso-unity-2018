using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour {

    public Transform[] waypoints;
    int currentWaypoint = 0;

    public float speed = 0.3f;

    public bool shouldWaitHome = false;

	private void Update()
	{
        if(GameManager.sharedInstance.invincibleTime>0){
            //GetComponent<SpriteRenderer>().color = Color.blue;
            GetComponent<Animator>().SetBool("PacmanInvincible", true);
        }else{
            //GetComponent<SpriteRenderer>().color = Color.white;
            GetComponent<Animator>().SetBool("PacmanInvincible", false);
        }
	}

	private void FixedUpdate()
	{

        if (GameManager.sharedInstance.gameStarted && !GameManager.sharedInstance.gamePaused)
        {
            GetComponent<AudioSource>().volume = 0.2f;

            if (!shouldWaitHome)
            {
                //Distancia entre el fantasma y el punto de destino
                float distanceToWaypoint = Vector2.Distance((Vector2)this.transform.position,
                                                            (Vector2)waypoints[currentWaypoint].position);

                if (distanceToWaypoint < 0.1f)
                {
                    currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
                    Vector2 newDirection = waypoints[currentWaypoint].position - this.transform.position;
                    GetComponent<Animator>().SetFloat("DirX", newDirection.x);
                    GetComponent<Animator>().SetFloat("DirY", newDirection.y);
                }
                else
                {
                    Vector2 newPos = Vector2.MoveTowards(this.transform.position,
                                                         waypoints[currentWaypoint].position,
                                                         speed * Time.deltaTime);
                    GetComponent<Rigidbody2D>().MovePosition(newPos);
                }
            }
        }else{
            GetComponent<AudioSource>().volume = 0.0f;
        }
	}


	private void OnTriggerEnter2D(Collider2D otherCollider)
	{
        if(otherCollider.tag == "Player"){
            if (GameManager.sharedInstance.invincibleTime <= 0)
            {
                GameManager.sharedInstance.gameStarted = false;
                Destroy(otherCollider.gameObject);

                StartCoroutine("RestartGame");

            } else {
                UIManager.sharedInstance.ScorePoints(1500);

                GameObject home = GameObject.Find("Ghost Home");
                this.transform.position = home.transform.position;
                this.currentWaypoint = 0;
                this.shouldWaitHome = true;
                StartCoroutine("AwakeFromHome");
            }
        }
	}

    IEnumerator RestartGame(){
        yield return new WaitForSecondsRealtime(3.0f);
        GameManager.sharedInstance.RestartGame();
    }

    IEnumerator AwakeFromHome(){
        yield return new WaitForSecondsRealtime(3.0f);
        this.shouldWaitHome = false;
        //Cada vez que el fantasma despierta, es un 20% más rápido
        this.speed *= 1.2f;
    }
}
