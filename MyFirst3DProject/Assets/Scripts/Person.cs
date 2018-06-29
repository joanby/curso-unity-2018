using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour {

    public string firstName;
    public string lastName;
    public int age;
    public bool isMale;

    public Person spouse;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public bool IsMarriedWith(Person otherPerson)
    {
        if(this.spouse.firstName == null){
            Debug.Log("No está casado");
            return false;
            //aquí no está casado
        } else {
            Debug.Log("Está casado");
            if(otherPerson.firstName == this.spouse.firstName &&
               otherPerson.lastName == this.spouse.lastName){
                Debug.Log("Están casado con la otra persona");
                return true;
                //aquí está casado con otherPerson
            } else {
                Debug.Log("Están casado pero no con la otra persona");
                return false;
                //aquí está casado pero no con otherPerson
            }
        }
    }
}
