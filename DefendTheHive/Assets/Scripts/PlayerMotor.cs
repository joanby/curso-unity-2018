using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    float horizontal, vertical;

    Rigidbody m_Rigidbody;

    public float jumpForce, moveSpeed, runSpeed;

    private float currentJumpForce = 0, currentMoveSpeed = 0;

	private void Start()
	{
        m_Rigidbody = GetComponent<Rigidbody>();
        currentMoveSpeed = moveSpeed;
        m_Animator = GetComponent<Animator>();
	}


	private void Update()
	{
        //Comprobamos si estamos o no en el suelo
        CheckGroundStatus();

        //Comprobamos la cantidad de movimiento V/H
        horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        vertical = CrossPlatformInputManager.GetAxis("Vertical");

        //Debug.Log("H:" + horizontal + ", V:" + vertical);

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
            m_Rigidbody.AddForce(0, jumpForce, 0);
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && isGrounded){
            currentMoveSpeed = runSpeed;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift)){
            currentMoveSpeed = moveSpeed;
        }

	}

    private float turnAmount, forwardAmount;
    [SerializeField] float stationaryTurnAround = 180;
    [SerializeField] float movingTurnSpeed = 360;

    public Transform m_camera;
    private Vector3 cameraForward;
    private Vector3 move;
    private bool jump;

	private void FixedUpdate()
	{
        if(m_camera!=null){
            //Calculamos la dirección de movimiento relativa a donde mira la cámara 
            cameraForward = Vector3.Scale(m_camera.forward, new Vector3(1, 0, 1)).normalized;
            move = vertical * cameraForward + horizontal * m_camera.right;
        }else{
            //En caso de no tener cámara de movimiento, calculamos las coordenadas absolutas del mundo
            move = vertical * Vector3.forward + horizontal * Vector3.right;
        }

        if (move.magnitude > 0){
            Move(move);
        }

	}


    [SerializeField] float groundCheckDistance = 0.1f;
	private bool isGrounded;
    private Vector3 groundNormal;
    //Comprueba si el personaje está en el suelo
    void CheckGroundStatus(){

        #if UNITY_EDITOR
            Debug.DrawLine(transform.position + Vector3.up * 1.0f,
                       transform.position + Vector3.down * 1.0f,
                       Color.red);  
        #endif


        RaycastHit hitInfo;

        if(Physics.Raycast(transform.position+Vector3.up * 0.1f, //trazamos el rayo unos 10cm más arriba de la suela del jugador...
                           Vector3.down, out hitInfo, groundCheckDistance)){
            isGrounded = true;
            groundNormal = hitInfo.normal;
        } else {
            isGrounded = false;
            groundNormal = Vector3.up;
        }

        //Debug.Log(groundNormal);

    }

	private void Move(Vector3 movement)
	{
        if(movement.magnitude>1.0f){
            movement.Normalize();//aquí ahora si que tiene longitud 1
        }
        movement = transform.InverseTransformDirection(movement);
        CheckGroundStatus();
        //modificamos el movimiento según el vector normal a la superfície sobre la que camina...
        movement = Vector3.ProjectOnPlane(movement, groundNormal);
        turnAmount = Mathf.Atan2(move.x, move.z);
        forwardAmount = move.z;
        m_Rigidbody.velocity = transform.forward * currentMoveSpeed;
        ApplyExtraRotation();
	}

    void ApplyExtraRotation(){
        float turnSpeed = Mathf.Lerp(stationaryTurnAround, movingTurnSpeed, forwardAmount);
        //s = v * t
        transform.Rotate(0, turnSpeed * turnAmount * Time.deltaTime, 0);
    }

    private Animator m_Animator;
    [SerializeField] float moveSpeedMultiplier = 1.0f;

	private void OnAnimatorMove()
	{
        if(isGrounded && Time.deltaTime > 0){
            Vector3 vel = m_Animator.deltaPosition * moveSpeedMultiplier / Time.deltaTime;
            vel.y = m_Rigidbody.velocity.y;//para que el personaje siga con la misma velocidad de salto
            m_Rigidbody.velocity = vel;
        }
	}
}
