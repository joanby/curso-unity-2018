using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonCharacterController : MonoBehaviour {

    private const string ANIM_SPEED = "speed",
                        ANIM_HORI = "horizontal",
                        ANIM_VERT = "vertical",
                        ANIM_DIE = "die",
                        ANIM_HIT = "hit",
                        ANIM_ATT = "attack";

    private Animator animator;
    private Rigidbody rigidBody;

    public float speed = 0.0f;
    public float horizontal = 0.0f;
    public float vertical = 0.0f;

    public bool die, dead, attack, hit;

    private Transform dragonMouth;
    public GameObject fireballPrefab;
    private GameObject currentFireball;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();

        dragonMouth = GameObject.Find("DragonMouth").transform;

        currentFireball = Instantiate(fireballPrefab, dragonMouth);
	}
	
	// Update is called once per frame
	void Update () {
        if (dead)
        {
            if (die)
            {
                animator.SetBool(ANIM_DIE, true);
                die = !die;
            }
            return;
        }


        if(Input.GetMouseButtonDown(0)){
            attack = true;
        }
        if (Input.GetMouseButtonUp(0)){
            attack = false;
        }
        animator.SetBool(ANIM_ATT, attack);

        if(Input.GetMouseButtonDown(1)){
            currentFireball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            currentFireball.GetComponent<Rigidbody>().AddForce(transform.forward*fireSpeed,
                                                               ForceMode.Impulse);
            currentFireball.gameObject.transform.parent = null;

            currentFireball.GetComponent<AudioSource>().volume = GameMaster.sharedInstance.sfxVolume;
            currentFireball.GetComponent<AudioSource>().Play();

            Invoke("LoadNewFireball", 1.0f);
        }


        if (Input.GetKeyDown(KeyCode.H))
        {
            hit = true;
        } 
        if (Input.GetKeyUp(KeyCode.H)){
            hit = false;
        }
        animator.SetBool(ANIM_HIT, hit);




        if (Input.GetKeyDown(KeyCode.I))
        {
            die = true;
            dead = true;
        }

    }

    float maxSpeed = 5.0f;
    float rotationSpeed = 90;
    float fireSpeed = 200.0f;

    private void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        speed = new Vector2(horizontal, vertical).sqrMagnitude;

        rigidBody.velocity = this.transform.forward * vertical * maxSpeed + 
            new Vector3(0, rigidBody.velocity.y, 0);

        //Debug.Log(rigidBody.velocity);
        //S = V*t
        this.transform.Rotate(0, horizontal * rotationSpeed * Time.deltaTime, 0);

        animator.SetFloat(ANIM_SPEED, speed);
        animator.SetFloat(ANIM_HORI, horizontal);
        animator.SetFloat(ANIM_VERT, vertical);
    }


    private void LoadNewFireball(){
        currentFireball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        currentFireball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        currentFireball.transform.position = dragonMouth.position;
        currentFireball.gameObject.transform.parent = dragonMouth;
    }


}
