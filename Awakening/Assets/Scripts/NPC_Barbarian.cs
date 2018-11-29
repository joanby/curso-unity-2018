using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_Barbarian : MonoBehaviour {

    private Animator animator;

    private NavMeshAgent nav;

    private SphereCollider col;

    public GameObject player;


    public float speed = 0.0f;
    public float h = 0.0f, v = 0.0f;
    public bool attack = false, //Está atacando?
                jump = false, //Está saltando?
                die = false; //Está muerto?

    public bool DEBUG = false, DEBUG_DRAW = false;

    public Vector3 direction; //Donde está player en relación a NPC

    public float distance = 0.0f; //Distancia entre jugador y NPC

    public float angle = 0.0f; //Ángulo entre jugador y NPC

    public bool playerInSight = false; //Está el jugador en el FOV del NPC?

    public float fieldOfViewAngle = 120;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        col = GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

        if(die){
            //TODO: animar la muerte del NPC
            //- poner a true un parámetro die como el del personaje
            //- desactivar los scripts de movimiento del NPC
            //- Preprogramar la destrucción del NPC dentro de 3 segundos.
            Destroy(gameObject);
        }

        if(playerInSight){
            this.transform.rotation = //Girar al NPC 
                Quaternion.Slerp(this.transform.rotation, //desde donde mira ahora
                                 Quaternion.LookRotation(direction), //hacia la dirección del player
                                 0.1f //en un tiempo de 0.1f
                                );
        }

        if (player.transform.GetComponent<DragonCharacterController>() &&
            player.transform.GetComponent<DragonCharacterController>().dead){
            animator.SetBool("attack", false);
            animator.SetFloat("speed", 0);
            animator.SetFloat("angularSpeed", 0);
        }


	}

    private void FixedUpdate()
    {
        h = angle;
        v = distance;
        speed = distance / Time.deltaTime;
        if(DEBUG){
            Debug.Log(string.Format("H:{0} - V:{1}, S:{2}", h, v, speed));
        }
        animator.SetFloat("speed", speed * GetComponent<NPCAgent>().npcData.speed);
        animator.SetFloat("angularSpeed", h);
        animator.SetBool("attack", attack);
       

        if(playerInSight){
            if(animator.GetFloat("attack2")>0.5f || animator.GetFloat("attack3") > 0.5f){
                if (player.GetComponent<PlayerAgent>() != null)
                {
                    float baseDamage = 10.0f;
                    float enemyAttack = GetComponent<NPCAgent>().npcData.strength;
                    float playerDefense = player.GetComponent<PlayerAgent>().playerCharacterData.defense;
                    //TODO: si hay armas equipadas o armaduras lo suyo es hacer un bucle e incrementar 
                    //el ataque/defensa según los calificadores de las armas o armaduras en cuestión
                    player.GetComponent<PlayerAgent>().playerCharacterData.health -= 
                        baseDamage * enemyAttack / playerDefense * Time.deltaTime;
                }
            }
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if(player.GetComponent<DragonCharacterController>() && 
           player.GetComponent<DragonCharacterController>().dead)
        {
            return;
        }

        if(other.transform.tag.Equals("Player")){
            Debug.Log("ENTRANDO EN EL TRIGGER");
            //vector = destino - origen
            direction = player.transform.position - this.transform.position;
            distance = Vector3.Magnitude(direction);
            angle = Vector3.Dot(this.transform.forward, player.transform.position);

            if(DEBUG_DRAW){
                Debug.DrawLine(this.transform.position + Vector3.up,
                               direction * 50, Color.red);
                Debug.DrawLine(player.transform.position, 
                               this.transform.position, 
                               Color.blue);
            }


            playerInSight = false;
            float calculateAngle = Vector3.Angle(direction, transform.forward);
            //Si el player está en el campo de visión
            if(calculateAngle < 0.5f*fieldOfViewAngle){
                RaycastHit hit; 
                if(DEBUG_DRAW){
                    Debug.DrawRay(this.transform.position + transform.up,
                                  direction.normalized, Color.green);
                }
                //Trazo un rayo desde NPC hasta player
                if(Physics.Raycast(transform.position + transform.up, direction.normalized, 
                                   out hit, col.radius)){
                    //Si lo primero que localiza el rayo es el jugador
                    if(hit.collider.gameObject == player){
                        playerInSight = true;
                        if(DEBUG){
                            Debug.Log("Jugador en el campo de visión!!!!");
                        }
                    }
                }
            }

            //Si después de toda la comprobación anterior, el player está en FoV del NPC
            if(playerInSight){
                nav.SetDestination(player.transform.position);
                CalculatePathLength(player.transform.position);
                //Si estoy muy cerca, puedo atacar...
                if(distance < 1.1f){
                    attack = true; 
                }else{
                    attack = false;
                }
            }


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.tag == "Player"){
            distance = 0;
            angle = 0;
            attack = false;
            playerInSight = false;
        }
    }

    float CalculatePathLength(Vector3 targetPosition){

        NavMeshPath path = new NavMeshPath();
        if (nav.enabled)
        {
            nav.CalculatePath(targetPosition, path);
            Vector3[] allTheWaypoints = new Vector3[path.corners.Length + 2];
            allTheWaypoints[0] = this.transform.position;
            allTheWaypoints[allTheWaypoints.Length - 1] = targetPosition;

            for (int i = 0; i < path.corners.Length; i++)
            {
                allTheWaypoints[i + 1] = path.corners[i];
            }

            float pathLength = 0;
            for (int i = 0; i < allTheWaypoints.Length - 1; i++)
            {
                pathLength += Vector3.Distance(allTheWaypoints[i], allTheWaypoints[i + 1]);
                if (DEBUG_DRAW)
                {
                    Debug.DrawLine(allTheWaypoints[i], allTheWaypoints[i + 1], Color.gray);
                }
            }

            return pathLength;
        }
        else
        {
            return 0;
        }

    }

}
