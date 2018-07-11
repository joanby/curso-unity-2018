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


    private float turnAmount, forwardAmount;
    [SerializeField] float m_MovingTurnSpeed = 360;
    [SerializeField] float m_StationaryTurnSpeed = 180;
    [SerializeField] float m_JumpPower = 12f;

    [Range(1f, 20f)] [SerializeField] float m_GravityMultiplier = 2f;


    public Transform m_Cam;                  // A reference to the main camera in the scenes transform
    private Vector3 m_CamForward;             // The current forward direction of the camera
    private Vector3 m_Move;
    private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.


    [SerializeField] float m_GroundCheckDistance = 0.1f;
    private bool isGrounded;
    private Vector3 groundNormal;
    float m_OrigGroundCheckDistance;


    private Animator m_Animator;
    [SerializeField] float m_MoveSpeedMultiplier = 1f;
    [SerializeField] float m_AnimSpeedMultiplier = 1f;

	private void Start()
	{
        m_Rigidbody = GetComponent<Rigidbody>();
        currentMoveSpeed = moveSpeed;
        m_Animator = GetComponent<Animator>();
        m_OrigGroundCheckDistance = m_GroundCheckDistance;
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



	private void FixedUpdate()
	{
        if(m_Cam!=null){
            //Calculamos la dirección de movimiento relativa a donde mira la cámara 
            m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
            m_Move = vertical * m_CamForward + horizontal * m_Cam.right;
        }else{
            //En caso de no tener cámara de movimiento, calculamos las coordenadas absolutas del mundo
            m_Move = vertical * Vector3.forward + horizontal * Vector3.right;
        }

        if (m_Move.magnitude > 0){
            Move(m_Move);
        }

        if (isGrounded && m_Move.magnitude > 0)
        {
            m_Animator.speed = m_AnimSpeedMultiplier;
        }
        else
        {
            // don't use that while airborne
            m_Animator.speed = 1;
        }

	}


    //Comprueba si el personaje está en el suelo
    void CheckGroundStatus(){

        RaycastHit hitInfo;
        #if UNITY_EDITOR
        // helper to visualise the ground check ray in the scene view
            Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
        #endif
        // 0.1f is a small offset to start the ray from inside the character
        // it is also good to note that the transform position in the sample assets is at the base of the character
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
        {
            groundNormal = hitInfo.normal;
            isGrounded = true;
            m_Animator.applyRootMotion = true;
        }
        else
        {
            isGrounded = false;
            groundNormal = Vector3.up;
            m_Animator.applyRootMotion = false;
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
        turnAmount = Mathf.Atan2(movement.x, movement.z);
        forwardAmount = movement.z;

        m_Rigidbody.velocity = transform.forward * currentMoveSpeed;
        ApplyExtraRotation();


        if (!isGrounded)
        {
            HandleAirborneMovement();
        }



	}

    void ApplyExtraRotation(){
        float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, forwardAmount);
        transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0);
 
    }



	private void OnAnimatorMove()
	{
        if (isGrounded && Time.deltaTime > 0)
        {
            Vector3 v = (m_Animator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;

            // we preserve the existing y part of the current velocity.
            v.y = m_Rigidbody.velocity.y;
            m_Rigidbody.velocity = v;
        }
	}

    void HandleAirborneMovement()
    {
        // apply extra gravity from multiplier:
        Vector3 extraGravityForce = (Physics.gravity * m_GravityMultiplier) - Physics.gravity;
        m_Rigidbody.AddForce(extraGravityForce);

        m_GroundCheckDistance = m_Rigidbody.velocity.y < 0 ? m_OrigGroundCheckDistance : 0.01f;
    }
}
