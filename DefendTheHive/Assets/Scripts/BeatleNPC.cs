using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatleNPC : MonoBehaviour {

    Animator m_Animator;
	// Use this for initialization
	void Start () {
        m_Animator = GetComponent<Animator>();
	}

	private void OnCollisionEnter(Collision collision)
	{
        if(collision.gameObject.CompareTag("Player")){
            m_Animator.Play("Die_OnGround");
        }
	}
}
