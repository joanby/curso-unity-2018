using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public static PlayerController sharedInstance;

    public float jumpForce = 5f;

    public Animator animator;

    public float runningSpeed = 1.5f;

    private Rigidbody2D rigidbody;

    private Vector3 startPosition;


    //Esta variable sirve para detectar la capa del suelo...
    public LayerMask groundLayer;


	void Awake()
	{
        sharedInstance = this;
        rigidbody = GetComponent<Rigidbody2D>();
        //La variable start position toma el valor de donde empieza la primera
        //vez nuestro personaje
        startPosition = this.transform.position;
	}

	public void StartGame () {
        animator.SetBool("isAlive", true);
        animator.SetBool("isGrounded", true);
        //Cada vez que reiniciamos, ponemos al personaje en la start position
        this.transform.position = startPosition;
	}
	
	// Update is called once per frame
	void Update () {
        //Solo debemos dejar que salte, si el juego está en modo InGame
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if (Input.GetKeyDown(KeyCode.Space)||Input.GetMouseButtonDown(0))
            {
                //Aquí, el usuario acaba de bajar la tecla espacio
                Jump();
            }

            //Asignamos a la animación el mismo valor que el método IsTouchingTheGround
            //que es true si tocamos el suelo y false en caso contrario
            animator.SetBool("isGrounded", IsTouchingTheGround());
        }
	}

	void FixedUpdate()
	{
        //Solo nos movemos si estamos en el modo InGame
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if (rigidbody.velocity.x < runningSpeed)
            {
                rigidbody.velocity = new Vector2(runningSpeed, //velocidad en el eje de las x
                                                 rigidbody.velocity.y //velocidad en el eje de las y
                                                );
            }
        }

	}



	void Jump(){
        //F = m * a ========> a = F/m
        if (IsTouchingTheGround())
        {
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    bool IsTouchingTheGround(){//devuelve true si estamos tocando el suelo y false en otro caso

        if(Physics2D.Raycast(this.transform.position,//trazamos el rayo desde la posición del juegador
                             Vector2.down,           //en dirección hacia abajo
                             0.2f,                   //hasta un máximo de 20 cm
                             groundLayer             //y nos encontramos con la capa del suelo...
                            )){
            return true; 
        } else {
            return false;
        }

    }



    public void Kill()
    {
        GameManager.sharedInstance.GameOver();
        this.animator.SetBool("isAlive", false);
    }


}
