using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Health : NetworkBehaviour {

    public const int MAX_HEALTH = 100;
    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = MAX_HEALTH;
    public Slider healthBar;
    public bool destroyOnDeath;
    public GameObject[] listOfPlayers;

	// Use this for initialization
	void Start () {
        healthBar.maxValue = MAX_HEALTH;
        healthBar.value = MAX_HEALTH;
	}
	
    public void TakeDamage(int amount){
        currentHealth -= amount;
        if(currentHealth<=0){
            if(destroyOnDeath){
                RpcDied();
                listOfPlayers = GameObject.FindGameObjectsWithTag("Player");
                if(listOfPlayers.Length<=1){
                    Invoke("BackToLobby", 3.0f);
                }
            } else{
                currentHealth = MAX_HEALTH;
                RpcRespawn();
            }
        }
    }

    void OnChangeHealth(int newHealth){
        healthBar.value = newHealth;
    }

    [ClientRpc]
    void RpcDied(){
        gameObject.tag = "Untagged";
        foreach (Transform child in transform)
        {
            if (GetComponent<Renderer>() != null)
            {
                GetComponent<Renderer>().material.color = Color.black;
            }
        }
        //Si es un player, dejo de tenerlo activo
        if(GetComponent<MyPlayerController>()!=null){
            GetComponent<MyPlayerController>().enabled = false;
        }
        //Si es un enemigo, desactivo la IA
        if(GetComponent<EnemyController>()!=null){
            GetComponent<EnemyController>().enabled = false;
        }
    }

    [ClientRpc]
    void RpcRespawn(){
        if(isLocalPlayer){
            transform.position = Vector3.zero;
        }
    }

    void BackToLobby(){
        FindObjectOfType<NetworkLobbyManager>().ServerReturnToLobby();
    }
}
