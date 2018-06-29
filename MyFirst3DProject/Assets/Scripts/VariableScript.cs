using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableScript : MonoBehaviour {


    /*

        SECCIÓN DE VARIABLES

    */
    public int number1 = 5;
    public int number2 = 8;


	private void Awake()
	{
        Debug.Log("El objeto ha despertado");
	}


	// Use this for initialization
	void Start () {
        Debug.Log("El objeto ha arrancado");

        int result = AddTwoNumbers(5, 8);

        Debug.Log(result); //13

        AddTwoNumbers(6, -9);

        SayHello("Antonio");

        string myMessage = CreateWelcome("Juan Gabriel"); //Bienvenido al curso Juan Gabriel

	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("El objeto se está actualizando");


        if (Input.GetKeyDown(KeyCode.Return)){
            AddTwoGlobalNumbers();
        }
	}

	private void FixedUpdate()
	{
        Debug.Log(Time.time);
	}


	void AddTwoGlobalNumbers(){
        Debug.Log(number1 + number2);
    }


    void AddTwoNumbersAndShowResultInConsole(int firstNumber, int secondNumber){
        Debug.Log(firstNumber + secondNumber);
    }

    int AddTwoNumbers(int firstNumber, int secondNumber){
        int result = firstNumber + secondNumber;
        return result;
    }

    void SayHello(string username){
        Debug.Log("Hola "+username);
    }


    string CreateWelcome(string username){
        string message = "Bienvenido al Curso " + username;
        return message;
    }

    void DoubleNumber(float number){
        Debug.Log(2.0f * number);
    }

}
