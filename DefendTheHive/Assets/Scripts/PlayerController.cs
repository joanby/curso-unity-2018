using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public string idleState, walkState, runState, jumpState, throwState, dieState;
    bool isWalking, isRunning, isJumping, isIdle, forward, backward, left, right;
    Animator m_Animator;

    public AudioClip jumpClip, throwClip;

	// Use this for initialization
	void Start () {
        m_Animator = GetComponent<Animator>();
        isIdle = true;
	}
	
	// Update is called once per frame
	void Update () {
		// DOWN STATES
        if(Input.GetKeyDown(KeyCode.W)){
            if(!isRunning){
                isWalking = true;
                isIdle = false;
                forward = true;
                m_Animator.SetBool(walkState, true);
                m_Animator.SetBool(idleState, false);
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (!isRunning)
            {
                isWalking = true;
                isIdle = false;
                left = true;
                m_Animator.SetBool(walkState, true);
                m_Animator.SetBool(idleState, false);
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (!isRunning)
            {
                isWalking = true;
                isIdle = false;
                backward = true;
                m_Animator.SetBool(walkState, true);
                m_Animator.SetBool(idleState, false);
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (!isRunning)
            {
                isWalking = true;
                isIdle = false;
                right = true;
                m_Animator.SetBool(walkState, true);
                m_Animator.SetBool(idleState, false);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(isWalking){
                isRunning = true;
                m_Animator.SetBool(runState, true);
                m_Animator.SetBool(walkState, false);
            }
        }


        //ACTIONS
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.E)||Input.GetMouseButtonDown(0))
        {
            Throw();
        }


        //ACTION UP
        if (Input.GetKeyUp(KeyCode.W))
        {
            forward = false;
            if(!left && !backward && !right){
                StopMotion();
            }
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            left = false;
            if (!forward && !backward && !right)
            {
                StopMotion();
            }
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            backward = false;
            if (!left && !forward && !right)
            {
                StopMotion();
            }
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            right = false;
            if (!left && !backward && !forward)
            {
                StopMotion();
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if(isRunning && isWalking){
                isRunning = false;
                m_Animator.SetBool(walkState, true);
                m_Animator.SetBool(runState, false);
            }
        }

        //Comprobamos si nos hemos quedado sin vidas para hacer la animación de la muerte
        if(PlayerManager.livesRemaining == 0){
            m_Animator.Play("CM_Die");
        }
	}

    void StopMotion(){
        isWalking = false;
        isRunning = false;
        isIdle = true;
        m_Animator.SetBool(idleState, true);
        m_Animator.SetBool(walkState, false);
        m_Animator.SetBool(runState, false);
    }

    void Jump(){
        Debug.Log("Animacion de Salto");
        m_Animator.SetBool(jumpState, true);
        m_Animator.SetBool(idleState, false);
        m_Animator.SetBool(walkState, false);
        m_Animator.SetBool(runState, false);
        StartCoroutine(ConsumeJump());
    }

    void Throw(){
        m_Animator.SetBool(throwState, true);
        m_Animator.SetBool(idleState, false);
        m_Animator.SetBool(walkState, false);
        m_Animator.SetBool(runState, false);
        StartCoroutine(ConsumeThrow());
    }

    IEnumerator ConsumeJump(){
        
        yield return new WaitForSeconds(0.66f);

        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(jumpClip);

        m_Animator.SetBool(jumpState, false);
        ReturnToMoveState();
    }

    IEnumerator ConsumeThrow(){
       
        yield return new WaitForSeconds(0.66f);

        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(throwClip);

        m_Animator.SetBool(throwState, false);
        ReturnToMoveState();
    }

    void ReturnToMoveState(){
        if (isRunning)
        {
            m_Animator.SetBool(runState, true);
        }
        else if (isWalking)
        {
            m_Animator.SetBool(walkState, true);
        }
        else if (isIdle)
        {
            m_Animator.SetBool(idleState, true);
        }
    }
}
