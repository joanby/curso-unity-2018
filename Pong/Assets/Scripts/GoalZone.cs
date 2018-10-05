using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalZone : MonoBehaviour {

    /*
     * Este método se llama automáticamente cuando algo entra dentro del trigger...
    */
	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.gameObject.tag == "Ball")
        {
            Debug.Log("Alguien ha marcado gol");
        }
	}

}
