/*
 * NAMESPACE
 * 
 */
using System.Collections;
using UnityEngine;


/*
    CLASS
*/
public class Coin : MonoBehaviour 
{


    //PROPIEDADES Y MÉTODOS
    //Variable global y estática (una para todas las monedas)
    public static int coinsCount = 0;

	// Use this for initialization
	void Start () 
    {
        Coin.coinsCount++;
        Debug.Log("El juego ha comenzado y ahora hay " + Coin.coinsCount + " monedas");
	}
	
	// Update is called once per frame
	void Update () 
    {
        //Debug.Log("Estamos en el Update");
	}

    /*
     * Método que se llama automáticamente cuando
     * otro collider entra en contacto con el 
     * que tiene este script (en particular la moneda)
     */
    private void OnTriggerEnter(Collider otherCollider)
	{

        if (otherCollider.CompareTag("Player") == true)
        {
            //Como el jugador choca contra la moneda, ahora hay una menos
            Coin.coinsCount--;

            Debug.Log("Hemos recogido la moneda y ahora hay " + Coin.coinsCount + " monedas");

            //En el caso de que el contador de monedas llegue a cero, hemos recogido todo!
            if (Coin.coinsCount == 0){
                Debug.Log("El juego ha terminado");

                GameObject gameManager = GameObject.Find("GameManager");

                Destroy(gameManager);

                GameObject[] fireworksSystem = GameObject.FindGameObjectsWithTag("Fireworks");

                foreach(GameObject fireworks in fireworksSystem){
                    fireworks.GetComponent<ParticleSystem>().Play();
                }


            }

            Destroy(gameObject);
        }
	}

}
