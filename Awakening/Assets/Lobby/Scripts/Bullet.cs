using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour {

    [SyncVar]
    public Color myColor;

	// Use this for initialization
	void Start () {
        GetComponent<MeshRenderer>().material.color = myColor;
	}

    private void OnCollisionEnter(Collision collision)
    {
        var hit = collision.gameObject; //COn quien he chocado?
        var health = hit.GetComponent<Health>();
        if(health!=null){
            health.TakeDamage(10);
        }
        Destroy(gameObject);
    }

}
