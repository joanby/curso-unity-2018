using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person {

    private string firstName;
    private string lastName;
    public int age;
    public bool isMale;

    public Person spouse;


    //Constructor por defecto
    public Person(){
        
    }

    public Person(string pFirstName, string pLastName){
        this.firstName = pFirstName;
        this.lastName = pLastName;
    }

    public Person(string firstName, string lastName, int age, bool isMale){
        this.firstName = firstName;
        this.lastName = lastName;
        this.age = age;
        this.isMale = isMale;
     }



    public bool IsMarriedWith(Person otherPerson)
    {
        if(spouse == null){
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


    public static void SayHello(){
        Debug.Log("Hola que tal!");
    }


    /*SETTERS y GETTERS*/
    public void setFirstName(string firstName){
        this.firstName = firstName;
    }

    public void setLastName(string lastName){
        this.lastName = lastName;
    }

    public string getFirstName(){
        return this.firstName;
    }

    public string getLastName(){
        return this.lastName;
    }

}
