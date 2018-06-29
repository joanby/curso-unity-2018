using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionScript : MonoBehaviour {


    public bool willItRainToday = false;


	// Use this for initialization
	void Start () {

        /*if(willItRainToday){
            Debug.Log("No te olvides de coger el paraguas");
        } else {
            Debug.Log("No lo cojas hombre, que hace mucho sol!!");
        }*/

        /*if(!willItRainToday){
            Debug.Log("Vamos al cine!");
        }*/

        bool iAmLate = true;
        bool isThereSomeTraffic = false;

        if(iAmLate && !isThereSomeTraffic){ //AND -> ambas deben ser ciertas
            Debug.Log("Dale al gas que llego tarde!!!");
        }else {
            Debug.Log("O bien hay tráfico o bien no llego tarde...");
        }

        bool iAmHungry = true;
        bool kidsAreHungry = false;
        if(iAmHungry || kidsAreHungry){
            Debug.Log("Vamos a hacer la comida");
        }else {
            Debug.Log("Nadie tiene hambre");
        }



        int maxSpeed = -70;

        if(maxSpeed == 120){
            Debug.Log("Podemos ir a fondo!");
            string greeting = "Soy feliz";
            Debug.Log(maxSpeed);
        } else if(maxSpeed<120 && maxSpeed>=60){
            Debug.Log("Podemos ir a velocidad de cruce!");
        } else if(maxSpeed<60 && maxSpeed>=40){
            Debug.Log("Debemos ir a velocidad de ciudad");
        } else if(maxSpeed<40 && maxSpeed>=0) {
            Debug.Log("Mejor vamos dando un paseo...");
        }else{
            Debug.LogError("Veocidad no válida...");
        }


        if(EnterTheParty(17, 25)){
            Debug.Log("Bienvenido a la fiesta!");
        } else {
            Debug.Log("No tienes permisos para entrar");
        }


	}
	
	// Update is called once per frame
	void Update () {
		
	}


    bool EnterTheParty(int age, int money){

        if(age >= 18 && money >=10){
            return true;
        } else {
            return false;
        }


    }

}
