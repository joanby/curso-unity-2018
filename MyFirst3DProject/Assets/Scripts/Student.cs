using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Student : MonoBehaviour {

    public string firstName = "Luke";
    public string lastName = "Skywalker";
    public string email = "luke@starwars.com";
    public int age = 32;
    public float height = 1.78f;
    public float weight = 82.5f;

    bool booleanVariable; //true o false
    int integerVariable; // -3, -2, -1, 0, 1, 2, 3,... 
    float realVariable; // 3.14159265, 6.28, -4.73
    char characterVariable = 'a'; // 'a', 'b', '7', '@', '#', '$', ' '
    string stringVariable = "Hola, me llamo Juan Gabriel";


	// Use this for initialization
	void Start () {
        float playerHeight = this.transform.position.y;

        playerHeight += 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
