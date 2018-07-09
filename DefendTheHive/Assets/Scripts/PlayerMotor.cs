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

    private Animator m_Animator;

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

        Debug.Log("H:" + horizontal + ", V:" + vertical);

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

    private float turnAround, forwardAmount;
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


	private bool isGrounded;
    //Comprueba si el personaje está en el suelo
    void CheckGroundStatus(){
       
    }

	private void Move(Vector3 move)
	{
       
	}

    void ApplyExtraRotation(){
        
    }

}
