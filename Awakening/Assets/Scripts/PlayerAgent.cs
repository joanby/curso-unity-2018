using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class PlayerAgent : MonoBehaviour {

    public PlayerCharacter playerCharacterData;
    public HUDElementUI elementUI;

    public void LoadPlayerInfo()
    {
        elementUI = GameObject.Find("CanvasHUD").GetComponent<HUDElementUI>();
        elementUI.healthBar.maxValue = playerCharacterData.health;
        //TODO: haríamos lo mismo con maná y resistencia...
    }
    private void Awake()
    {
        PlayerCharacter tmp = new PlayerCharacter();
        tmp.name = "Juan Gabriel";
        tmp.health = 100;
        tmp.defense = 50;
        tmp.description = "El mejor personaje de la historia";
        tmp.dexterity = 30;
        tmp.intelligence = 80;
        tmp.strength = 40;
        tmp.speed = 10;

        playerCharacterData = tmp;
    }

    private void Update()
    {
        if (elementUI != null)
        {
            elementUI.healthBar.value = playerCharacterData.health;
            //TODO: actualizar el resto de barras si se implementa la funcionalidad...
        }

        if(playerCharacterData.health < 0.0f){
            Debug.Log("EL PERSONAJE DEBE MORIR...");

            playerCharacterData.health = 0;

            //LO matamos a lo dragón
            if (transform.GetComponent<DragonCharacterController>())
            {
                transform.GetComponent<DragonCharacterController>().die = true;
                transform.GetComponent<DragonCharacterController>().dead = true;
                Invoke("DisableAnimator", 3.0f);
            }

            //LO matamos a lo bárbaro
            if(transform.GetComponent<BarbarianCharacterController>()){
                transform.GetComponent<BarbarianCharacterController>().die = true;
                transform.GetComponent<BarbarianCharacterController>().dead = true;
                Invoke("DisableAnimator", 3.0f);
            }
        }
    }

    void DisableAnimator(){
        transform.GetComponent<Animator>().enabled = false;
    }
}
