using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetlePatrol : MonoBehaviour {

    public static bool isDead = false;
    public static bool isEating = false;
    public static bool isAttacking = false;

    public float speed = 5f;
    public float directionChangeInterval = 1f;
    public float maxHeadingChange = 30f;

    Animator beetleAnimator;

    CharacterController controller;
    float heading;//ángulo entre 0 y 360º
    Vector3 targetRotation;

	private void Start()
	{
        beetleAnimator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

        heading = Random.Range(0, 360);
        transform.eulerAngles = new Vector3(0, heading, 0);

        StartCoroutine(NewHeading());
	}

	private void Update()
	{
        if(!isDead && !isEating && !isAttacking){
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles,
                                                  targetRotation, Time.deltaTime * directionChangeInterval);
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            controller.SimpleMove(forward*speed);
        }
	}

	IEnumerator NewHeading(){
        while(true){
            NewHeadingRoutine();
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    void NewHeadingRoutine(){
        float floor = transform.eulerAngles.y - maxHeadingChange;
        float ceil = transform.eulerAngles.y + maxHeadingChange;
        heading = Random.Range(floor, ceil);
        targetRotation = new Vector3(0, heading, 0);
    }

}
