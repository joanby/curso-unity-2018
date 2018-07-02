using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float jumpForce = 5f;

    public Animator animator;

    public float runningSpeed = 1.5f;

    private Rigidbody2D rigidbody;

	void Awake()
	{
        rigidbody = GetComponent<Rigidbody2D>();
	}

	// Use this for initialization
	void Start () {
        animator.SetBool("isAlive", true);
        animator.SetBool("isGrounded", true);
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

    //Esta variable sirve para detectar la capa del suelo...
    public LayerMask groundLayer;

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
}
