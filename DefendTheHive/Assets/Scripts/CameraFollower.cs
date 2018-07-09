using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {

    public static CameraFollower sharedInstance;

    public GameObject followTarget;

    public float movementSmoothness = 1.0f;
    public float rotationSmoothness = 1.0f;

    public bool canFollow = true;

	private void Awake()
	{
        sharedInstance = this;
	}

	private void LateUpdate()
	{
        if(followTarget == null || canFollow == false){
            return;
        }


        /*
        s_x rxy rxz p_x
        rxy s_y ryz p_y
        rxz ryz s_z p_z 
         0   0   0   1
        */
        transform.position = Vector3.Lerp(transform.position, followTarget.transform.position,
                                          Time.deltaTime * movementSmoothness);

        transform.rotation = Quaternion.Slerp(transform.rotation, followTarget.transform.rotation,
                                           Time.deltaTime * rotationSmoothness);
	}

}
