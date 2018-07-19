using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour {


	private void OnTriggerEnter2D(Collider2D otherCollider)
	{
        if(otherCollider.tag == "Player"){

            UIManager.sharedInstance.ScorePoints(500);

            GameManager.sharedInstance.MakeInvincibleFor(15.0f);

            Destroy(this.gameObject);
        }
	}
}
