using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if(collision.tag == "Ball"){
            collision.gameObject.GetComponent<BallArkanoid>().ResetBall();
            GetComponent<AudioSource>().Play();
        }
	}
}
