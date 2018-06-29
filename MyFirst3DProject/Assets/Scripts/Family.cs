using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Family : MonoBehaviour {

    public Person father;
    public Person mother;
    public Person son;


	// Use this for initialization
	void Start () {
       
        father = new Person();//instanciar
        //después de instanciar, podemos inicializar las variables
        father.firstName = "Anakin";
        father.lastName = "Skywalker";
        father.age = 35;
        father.isMale = true;

        this.mother = new Person();
        mother.firstName = "Padme";
        mother.lastName = "Amidala";
        mother.age = 28;
        mother.isMale = false;

        father.spouse = mother;
        mother.spouse = father;

        son = new Person();
        son.firstName = "Luke";
        son.lastName = "Skywalker";
        son.age = 8;
        son.isMale = true;


        Debug.Log(father.firstName + " y " + mother.firstName + " tienen un hijo llamado " +
                  son.firstName);


        if(mother.IsMarriedWith(father)){
            Debug.Log(father.firstName + " y " + mother.firstName + " están casados");
        } else{
            Debug.Log(father.firstName + " y " + mother.firstName + " no están casados");
        }


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
