using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Family : MonoBehaviour {

    public Person father;
    public Person mother;
    public Person son;


	// Use this for initialization
	void Start () {
       
        father = new Person("Anakin", "Skywalker");//instanciar
        //después de instanciar, podemos inicializar las variables
        father.age = 35;
        father.isMale = true;

        mother = new Person("Padme", "Amidala", 28, false);
       
        father.spouse = mother;
        mother.spouse = father;

        son = new Person("Luke", "Skywalker");
        son.age = 8;
        son.isMale = true;

        son.spouse = null;


        son.setFirstName("Antonio");
        son.setLastName("Banderas");

        Debug.Log(father.getFirstName() + " y " + mother.getFirstName() + " tienen un hijo llamado " +
                  son.getFirstName());


        if(father.IsMarriedWith(mother)){
            Debug.Log(father.getFirstName() + " y " + mother.getFirstName() + " están casados");
        } else{
            Debug.Log(father.getFirstName() + " y " + mother.getFirstName() + " no están casados");
        }

        Person.SayHello();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
