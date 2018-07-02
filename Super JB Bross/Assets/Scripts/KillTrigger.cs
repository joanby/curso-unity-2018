using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTrigger : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player")
        {
            PlayerController.sharedInstance.Kill();
        }
    }
}
