using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKHandler : MonoBehaviour {

    private Animator animator;

    public Transform leftFoot, rightFoot;

#region UTILIZADO PARA TESTING MANUAL
    public Transform leftIKTarget, rightIKTarget;
    public Transform hintLeft, hintRight;

    public float ikWeight = 1.0f;
#endregion

    [Header("Valores dinámicos de IK")]
    public Vector3 leftFootPosition, rightFootPosition;
    public Quaternion leftFootRotation, rightFootRotation;
    public float leftFootWeight, rightFootWeight;

    [Header("Más propiedades para IK")]
    public float offsetY;
    public float lookIkWeight = 1.0f;
    public float bodyWeight = 0.7f;
    public float headWeight = 0.9f;
    public float eyesWeight = 1.0f;
    public float clampWeight = 1.0f;

    public Transform lookPosition;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();

        leftFoot = animator.GetBoneTransform(HumanBodyBones.LeftFoot);
        rightFoot = animator.GetBoneTransform(HumanBodyBones.RightFoot);

        leftFootPosition = leftFoot.position;
        leftFootRotation = leftFoot.rotation;

        rightFootPosition = rightFoot.position;
        rightFootRotation = rightFoot.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        //Me sirve para saber donde está mirando el personaje
        Ray ray = new Ray(Camera.main.transform.position,
                          Camera.main.transform.forward);

        Debug.DrawRay(Camera.main.transform.position,
                      Camera.main.transform.forward * 20.0f, Color.cyan);


        RaycastHit leftHit, rightHit;

        Vector3 lPos = leftFoot.TransformPoint(Vector3.zero);
        Vector3 rPos = rightFoot.TransformPoint(Vector3.zero);


        if(Physics.Raycast(lPos, Vector3.down, out leftHit, 1.0f)){
            leftFootPosition = leftHit.point;
            leftFootRotation = Quaternion.FromToRotation(transform.up,
                                                         leftHit.normal) *
                                         transform.rotation;
        }

        Debug.DrawRay(lPos, Vector3.down, Color.red);



        if (Physics.Raycast(rPos, Vector3.down, out rightHit, 1.0f))
        {
            rightFootPosition = rightHit.point;
            rightFootRotation = Quaternion.FromToRotation(transform.up,
                                                          rightHit.normal) *
                                         transform.rotation;
        }

        Debug.DrawRay(rPos, Vector3.down, Color.magenta);


    }


    private void OnAnimatorIK(int layerIndex)
    {
        leftFootWeight = animator.GetFloat("leftFoot");
        rightFootWeight = animator.GetFloat("rightFoot");


        animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot,
                                     leftFootWeight);
        animator.SetIKPositionWeight(AvatarIKGoal.RightFoot,
                                     rightFootWeight);

        animator.SetIKPosition(AvatarIKGoal.LeftFoot,
                               leftFootPosition + new Vector3(0, offsetY, 0));
        animator.SetIKPosition(AvatarIKGoal.RightFoot,
                               rightFootPosition + new Vector3(0, offsetY, 0));



        animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot,
                                     leftFootWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.RightFoot,
                                     rightFootWeight);

        animator.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootRotation);
        animator.SetIKRotation(AvatarIKGoal.RightFoot, rightFootRotation);
    }
}
