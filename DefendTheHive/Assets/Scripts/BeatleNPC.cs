using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatleNPC : MonoBehaviour {

    Animator m_Animator;
    public GameObject nextCucumberToDestroy;

    //Variables para responder al ataque de las cerezas
    public bool cherryHit = false;
    public bool hasReachedThePlayer = false;
    public float smoothTime = 3.0f;
    public Vector3 smoothVelocity = Vector3.zero;

    public HealthManager healthManager;


    private static BeetleManager manager;
	// Use this for initialization
	void Start () {
        m_Animator = GetComponent<Animator>();
        healthManager = GameObject.Find("Health Slider").GetComponent<HealthManager>();
        if (manager == null)
        {
            manager = GameObject.Find("Beetles Value").GetComponent<BeetleManager>();
        }
        manager.RecalculateBeetles();
	}

	private void OnCollisionEnter(Collision collision)
	{
        if(collision.gameObject.CompareTag("Player")){
            hasReachedThePlayer = true;

            healthManager.ReduceHealth();
           

            if(!cherryHit){
                BeetlePatrol.isAttacking = true;
                GameObject thePlayer = collision.gameObject;
                Transform trans = thePlayer.transform;
                this.gameObject.transform.LookAt(trans);

                m_Animator.Play("Attack_OnGround");
                StartCoroutine(DestroyBeetle());
            } else {
                Debug.Log("Animación de ataque de pie");
                m_Animator.Play("Attack_Standing");
                StartCoroutine(DestroyBeetleStanding());
            }
           

 
        }

	}

	private void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.CompareTag("Cucumber"))
        {
            nextCucumberToDestroy = other.gameObject;
            BeetlePatrol.isEating = true;
            m_Animator.Play("Eat_OnGround");
            StartCoroutine(DestroyCucumber());
        }

        if(other.gameObject.CompareTag("Cherry")){

            PointsManager.AddPoints(25);

            BeetlePatrol.isAttacking = true;
            cherryHit = true;
            m_Animator.Play("Stand");
        }
	}


	IEnumerator DestroyCucumber(){
        yield return new WaitForSeconds(3.0f);
        Destroy(nextCucumberToDestroy.gameObject);
        BeetlePatrol.isEating = false;
    }

    IEnumerator DestroyBeetle(){
        yield return new WaitForSecondsRealtime(4.0f);
        m_Animator.Play("Die_OnGround");
        Destroy(this.gameObject, 2.0f);
        hasReachedThePlayer = false;
        manager.RecalculateBeetles();

    }

    IEnumerator DestroyBeetleStanding(){
        yield return new WaitForSecondsRealtime(4.0f);
        m_Animator.Play("Die_Standing");
        Destroy(this.gameObject, 2.0f);
        cherryHit = false;
        hasReachedThePlayer = false;
        manager.RecalculateBeetles();

    }


	private void Update()
	{
        if(cherryHit){
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Transform transPlayer = player.transform;
            this.gameObject.transform.LookAt(transPlayer);

            if (!hasReachedThePlayer)
            {
                m_Animator.Play("Run_Standing");
            }

            transform.position = Vector3.SmoothDamp(transform.position,
                                                    transPlayer.position,
                                                    ref smoothVelocity, smoothTime);
            
        }
	}
}
