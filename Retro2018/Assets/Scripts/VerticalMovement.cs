using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovement : MonoBehaviour {

    public float speed = 25;
    public string axis = "Vertical";

    /*
    * Se llama automáticamente
    * a intervalos de tiempo fijo
     * (muchos menos que el número de FPS)
    * Los cálculos de la física iran siempre aquí.
 */
    private void FixedUpdate()
    {
        if (GameManager.sharedInstance.gameStarted == true)
        {
            float v = Input.GetAxisRaw(axis);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed * v);
        }

    }


    /*
     * El método Awake
     * se llama antes de empezar el juego
     * y se suele usar para configuraciones previas
    */
	private void Awake()
	{
		
	}

	/*
	 * El método Start
	 * se llama automáticamente por Unity
	 * cuando arranca el videojuego
	 */
	void Start () {
		
	}
	
	/*
	 * El método Update
	 * se llama automáticamente por Unity
	 * a cada frame (si vamos a 60FPS, el método
	 * se ejecutará 60 veces por segundo).
	 * */
	void Update () {
		
	}



}
