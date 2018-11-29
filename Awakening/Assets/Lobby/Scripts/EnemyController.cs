using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyController : NetworkBehaviour {

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public GameObject[] listOfPlayers;
    [SyncVar(hook = "OnChangePlayerToAttack")]
    public GameObject playerToAttack;
    float cooldownTime = 0.0f;


	// Update is called once per frame
	void Update () {
        if(!isServer){
            return;
        }

        //Si estoy aquí, estoy seguro que quien ejecuta el Script es el Server...
        listOfPlayers = GameObject.FindGameObjectsWithTag("Player");
        if(listOfPlayers.Length>0){
            //Encuentro al player más cercano
            float currentDistance = 100f;
            foreach(var player in listOfPlayers){
                float d = Vector3.Distance(transform.position, player.transform.position);
                if(d < currentDistance){
                    currentDistance = d;
                    playerToAttack = player;
                }
            }

            if(playerToAttack !=null){
                //En qué dirección se encuentra el jugador que debe atacar la IA
                Vector3 direction = playerToAttack.transform.position - transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                      Quaternion.LookRotation(direction), 0.1f);
                if(currentDistance<15f){
                    if(cooldownTime < Time.time){
                        CmdFireBullet();
                        cooldownTime = Time.time + 1.0f;
                    }
                }
            }
        }
	}


    void OnChangePlayerToAttack(GameObject newPlayer){
        playerToAttack = newPlayer;
    }

    [Command]
    void CmdFireBullet()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation) as GameObject;
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 10f;
        NetworkServer.Spawn(bullet);
        Destroy(bullet, 2.0f);
    }
}
