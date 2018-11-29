using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarbarianCharacterController : MonoBehaviour {

    private const string ANIM_SPEED = "speed",
                        ANIM_HORI = "horizontal",
                        ANIM_VERT = "vertical",
                        ANIM_JUMP = "jump",
                        ANIM_S_AT = "superattack",
                        ANIM_DIE = "die",
                        ANIM_ATT = "attack",
                        ANIM_RUN = "run";

    private Animator animator;

    public float speed = 5.0f;
    public float horizontal = 0.0f;
    public float vertical = 0.0f;

    public bool attack = false;
    public bool jump = false;
    public bool die = false;
    public bool superattack = false;

    public bool run = false;
    public bool dead = false;

    public PlayerCharacter characterData;

    public List<GameObject> weapons;

 
    //public Vector3 moveDirection = Vector3.zero;


    // Use this for initialization
    void Start () {
        InventoryItemAgent[] tempData = GetComponentsInChildren<InventoryItemAgent>();
        foreach(InventoryItemAgent agent in tempData){
            weapons.Add(agent.gameObject);
            weapons[weapons.Count - 1].SetActive(false);
        }

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {


        if (dead){
            if(die){
                animator.SetBool(ANIM_DIE, true);
                die = !die;
            }
            return;
        }

        //AQUÍ EL PERSONAJE SEGURO QUE ESTÁ VIVO

        if(Input.GetKeyDown(KeyCode.C)&&!attack){
            attack = true;
            animator.SetBool(ANIM_ATT, attack);
        }
        if(Input.GetKeyUp(KeyCode.C)){
            attack = false;
            animator.SetBool(ANIM_ATT, attack);
        }




        if (Input.GetKeyDown(KeyCode.P))
        {
            superattack = true;
        }
        if (Input.GetKeyUp(KeyCode.P))
        {
            superattack = false;
        }
        animator.SetBool(ANIM_S_AT, superattack);


        if (Input.GetKeyDown(KeyCode.LeftShift)||speed>=0.5)
        {
            run = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)||speed<0.5)
        {
            run = false;
        }
        animator.SetBool(ANIM_RUN, run);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            jump = false;
        }
        animator.SetBool(ANIM_JUMP, jump);


        /*if (Input.GetKeyDown(KeyCode.I))
        {
            die = true;
            dead = true;
        }*/

    }


    private void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        speed = new Vector2(horizontal, vertical).sqrMagnitude * GetComponent<PlayerAgent>().playerCharacterData.speed;

        animator.SetBool(ANIM_RUN, speed >= 0.5);
        animator.SetFloat(ANIM_SPEED, speed);
        animator.SetFloat(ANIM_HORI, horizontal);
        animator.SetFloat(ANIM_VERT, vertical);
    }


    private void OnTriggerStay(Collider other)
    {
        //TODO: Cambiar el daño a un script DamageWeapon asignado a la arma
        //que haga daño cuando un enemigo entra en el collider del arma
        //y el pesonaje esté en modo de ataque...
        Debug.Log(other.tag);

        if(superattack && other.tag == "Enemy"){

            float baseDamage = 10.0f;
            float playerDamage = GetComponent<PlayerAgent>().playerCharacterData.strength;
            float enemyDefense = other.GetComponent<NPCAgent>().npcData.defense;
            //TODO: añadir modificadores según armas del player / defensas del enemy...

            foreach(GameObject weapon in weapons){
                if(weapon.activeInHierarchy){
                    playerDamage += weapon.GetComponent<InventoryItemAgent>().item.Strength;
                }
            }

            NPCAgent agent = other.GetComponent<NPCAgent>();
            agent.npcData.health -= baseDamage*playerDamage/enemyDefense*Time.deltaTime;

            Debug.Log("Mi player tiene que hacer: " + baseDamage * playerDamage / enemyDefense * Time.deltaTime);
            if(agent.npcData.health<=0){
                other.GetComponent<NPC_Barbarian>().die = true;
            }
        }
    }


}
