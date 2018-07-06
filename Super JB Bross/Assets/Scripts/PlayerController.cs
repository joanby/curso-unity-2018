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


    private int healthPoints, manaPoints;


    public const int INITIAL_HEALTH = 100, INITIAL_MANA = 15, MAX_HEALTH = 180, MAX_MANA = 30;

    public const int MIN_HEALTH = 10, MIN_MANA = 0;

    public const float MIN_SPEED = 2.5f, HEALTH_TIME_DECREASE = 1.0f;

    public const int SUPERJUMP_COST = 3;

    public const float SUPERJUMP_FORCE = 1.5f;


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

        this.healthPoints = INITIAL_HEALTH;
        this.manaPoints = INITIAL_MANA;

        StartCoroutine("TirePlayer");

	}


    IEnumerator TirePlayer(){
        while(this.healthPoints>MIN_HEALTH){
            this.healthPoints--;
            yield return new WaitForSeconds(HEALTH_TIME_DECREASE);
        }
        yield return null;
    }


	
	// Update is called once per frame
	void Update () {
        //Solo debemos dejar que salte, si el juego está en modo InGame
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if (Input.GetKeyDown(KeyCode.Space)||Input.GetMouseButtonDown(0))
            {
                //Aquí, el usuario acaba de bajar la tecla espacio
                Jump(false);
            }

            if (Input.GetMouseButtonDown(1))
            {
                //Aquí, el usuario acaba de bajar la tecla espacio
                Jump(true);
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
            float currentSpeed = (runningSpeed - MIN_SPEED) * this.healthPoints / 100.0f;

            if (rigidbody.velocity.x < currentSpeed)
            {
                rigidbody.velocity = new Vector2(currentSpeed, //velocidad en el eje de las x
                                                 rigidbody.velocity.y //velocidad en el eje de las y
                                                );
            }
        }

	}



	void Jump(bool isSuperJump){
        //F = m * a ========> a = F/m
        if (IsTouchingTheGround())
        {
            if (isSuperJump && this.manaPoints >= SUPERJUMP_COST)
            {
                manaPoints -= SUPERJUMP_COST;
                rigidbody.AddForce(Vector2.up * jumpForce * SUPERJUMP_FORCE, ForceMode2D.Impulse);
            }else {
                rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
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


        float currentMaxScore = PlayerPrefs.GetFloat("maxscore", 0);

        if (currentMaxScore < this.GetDistance()){
            PlayerPrefs.SetFloat("maxscore", this.GetDistance());
        }

        StopCoroutine("TirePlayer");
    }




    public float GetDistance(){
        float travelledDistance = Vector2.Distance(new Vector2(startPosition.x,0),
                                                   new Vector2(this.transform.position.x,0)
                                                  );
        return travelledDistance; //this.transform.position.x - startPosition.x
    }



    public void CollectHealth(int points){
        this.healthPoints += points;

        if(this.healthPoints >= MAX_HEALTH){
            this.healthPoints = MAX_HEALTH;
        }
    }

    public void CollectMana(int points){
        this.manaPoints += points;

        if(this.manaPoints >= MAX_MANA){
            this.manaPoints = MAX_MANA;
        }
    }


    public int GetHealth(){
        return this.healthPoints;
    }

    public int GetMana(){
        return this.manaPoints;
    }


	private void OnTriggerEnter2D(Collider2D otherCollider)
	{
        if(otherCollider.tag == "Enemy"){
            this.healthPoints -= 35;
        }

        if(otherCollider.tag == "Rock"){
            this.healthPoints -= 5;
        }

        if(GameManager.sharedInstance.currentGameState == GameState.inGame && this.healthPoints <= 0){
            Kill();
        }
	}

}
