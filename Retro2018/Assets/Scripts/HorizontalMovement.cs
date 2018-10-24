using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : MonoBehaviour {

    //Velocidad de Movimiento de la raqueta
    public float speed = 100;

	private void FixedUpdate()
	{
        //Obtenemos la cantidad de movimiento en dirección horizontal
        float h = Input.GetAxisRaw("Horizontal");

        GetComponent<Rigidbody2D>().velocity = Vector2.right * h * speed;
	}
}
