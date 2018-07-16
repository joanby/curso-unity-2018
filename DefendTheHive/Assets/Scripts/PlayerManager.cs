using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static int livesRemaining;
    public static int currentCherryCount;
    public int tempCurrentCherryCount;
    public bool isCollectingCherries;

    public static bool hasDead;

    public Transform[] spawningZones;

	private void Awake()
    {
        livesRemaining = 3;
        currentCherryCount = 100;
        tempCurrentCherryCount = 0;
        isCollectingCherries = false;
        hasDead = false;
	}

	private void Update()
	{
        if(isCollectingCherries){
            //Cada 60 frames
            if(tempCurrentCherryCount>=60){
                currentCherryCount += 1;

                PointsManager.AddPoints(5);

                tempCurrentCherryCount = 0;
            }else{
                //aún no hemos llegado al frame número 60
                tempCurrentCherryCount += 1;
            }
        }

        if (HealthManager.currentHealth <= 0 && !hasDead)
        {
            hasDead = true;
            livesRemaining--;

            if (livesRemaining == 2)
            {
                Destroy(GameObject.Find("Life3"));
                GetComponent<Animator>().Play("CM_Die");
                StartCoroutine(RespawnPlayer());
            }

            if (livesRemaining == 1)
            {
                Destroy(GameObject.Find("Life2"));
                GetComponent<Animator>().Play("CM_Die");
                StartCoroutine(RespawnPlayer());
            }

            if (livesRemaining == 0)
            {
                Destroy(GameObject.Find("Life1"));

            }
        }
	}

    IEnumerator RespawnPlayer(){
        //Caclulamos aleatoriamente en qué posición debemos aparecer
        int randomPos = Random.Range(0, spawningZones.Length);
        //Esperamos 4 segundos que dura la muerte
        yield return new WaitForSecondsRealtime(4f);
        //Movemos al jugador a la zona de spawning
        this.transform.position = spawningZones[randomPos].transform.position;
        //Volvemos a poner al jugador con su animación de Idle
        GetComponent<Animator>().Play("CM_Idle");
        //Volvemos a poner la vida del personaje a 100 puntos
        HealthManager.currentHealth = 100;
        //INdicamos que no ha muerto 
        hasDead = false;
    }

	private void OnTriggerEnter(Collider other)
	{
        if(other.gameObject.CompareTag("CherryTree")){
            //Aquí el personaje está en la zona debajo de un cerezo
            isCollectingCherries = true;
            currentCherryCount += 1;
            PointsManager.AddPoints(5);
        }
	}

	private void OnTriggerExit(Collider other)
	{
        if(other.gameObject.CompareTag("CherryTree")){
            isCollectingCherries = false;
        }
	}

}
