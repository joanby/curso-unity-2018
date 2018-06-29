using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionsScript : MonoBehaviour {

    public string student1 = "Christian";
    public string student2 = "Kate";
    public string student3 = "Mia";
    public string student4 = "Anastasia";

    //Todas las estructuras de datos, empiezan en la posición número 0
    //El último elemento de un array, es el de su dimensión -1

    /*
     * ARRAY
     *  - Es homogéneo (solo un tipo de dato)
     *  - Es de tamaño fijo (una vez creado, no puede contener más elementos)
     *  - Tiene un orden (se accede por posición)
    */

    public string[] studentsArray = new string[]{"Christian", "Kate", "Mia", "Anastasia"};

    public string[] familyNames = new string[4];//{ , , , }

    private int[] numberOfDoorsInMyStreet = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 11 };

    private bool[] hasPassedTheExam = new bool[] { true, false, false, true };


    /*
     * LISTA
     * using System.Collections.Generic;
     *  - Es homogéneo (solo un tipo de dato)
     *  - Es de tamaño ajustable o variable (podemos añadir más elementos en tiempo real y eliminarlos)
     *  - Tiene un orden (se acceede por posición)
     */

    public List<string> studentsNames = new List<string>();


    /*
     * ARRAYLIST
     *  - Es heterogéneo (puede guardar diferentes tipos de datos en la misma estructura)
     *  - Es de tamaño ajustable o variable (podemos añadir más elementos en tiempo real y eliminarlos)
     *  - Tiene un orden (se acceede por posición)
    */

    public ArrayList inventory = new ArrayList();

    /*
     * DICCIONARIO <-> HASHTABLE
     *  - Se puede redimensionar dinámicamente (igual que una lista)
     *  - Puede contener información heterogénea (igual que una array list)
     *  - Se accede por clave, no por posición
     */

    public Hashtable personalDetails = new Hashtable();



    // Use this for initialization
    void Start()
    {
        //Add -> añade elementos al final de la lista 
        //aquí, la lista está vacía
        studentsNames.Add("Christian");

        //aquí, la lista tiene un elemento, Christian
        studentsNames.Add("Kate");

        //aquí, la lista tiene dos elementos, Christian y Kate
        studentsNames.Add("Mia");

        //aquí, la lista tiene tres elementos, Christian, Kate y Mia
        studentsNames.Add("Anastasia");

        //aquí, la lista tiene cuatro elementos, Christian, Kate, Mia y Anastasia
        studentsNames.Add("Jack");

        //aquí, la lista tiene cinco elementos, Christian, Kate, Mia, Anastasia y Jack

        //Contains -> nos dice si la lista contiene o no un objeto: true o false
        if (studentsNames.Contains("Jack")) { 
        //Remove -> elimina elementos de la lista
            studentsNames.Remove("Jack");
        }


        studentsNames.Insert(2, "Paul");
        //ahora el orden es Christian, Kate, Paul, Mia, Anastasia

        //ToArray() -> Convierte una lista en un array
        string[] studentsNamesToArray = studentsNames.ToArray();

        //Clear -> Eliminar definitivamente todos los elementos de la lista
        //studentsNames.Clear();
        //ahora la lista está vacía [];   


        //Acceso a arrays y tamaño del mismo
        Debug.Log("El tamaño del array de estudiantes es: "+studentsArray.Length);

        int pos = 0;

        if (pos >= 0 && pos < studentsArray.Length)
        {
            Debug.Log("El primer estudiante del array : " + studentsArray[pos]);//El primer estudiante del array
        }


        //Acceso a listas y tamaño de las mismas
        Debug.Log("El tamaño de la lista de estudiantes es: " + studentsNames.Count);

        pos = 2;

        if (pos >= 0 && pos < studentsNames.Count)
        {
            string thirdStudent = studentsNames[pos];//El tercer estudiante de la lista
            Debug.Log("El tercer estudiante de la lista: " + thirdStudent);
        }


        inventory.Add(30);
        inventory.Add(3.14159265);
        inventory.Add("Juan Gabriel");
        inventory.Add(false);
        inventory.Add(GameObject.FindGameObjectsWithTag("Fireworks"));

        //Pedimos el tipo de dato que va a salir de la ArrayList
        Debug.Log(inventory[2].GetType());
        Debug.Log(inventory[4].GetType());


        personalDetails.Add("firstName",  "Juan Gabriel");
        personalDetails.Add("lastName",   "Gomila");
        personalDetails.Add("age",         30);
        personalDetails.Add("gender",     "male");
        personalDetails.Add("isMarried",   false);
        personalDetails.Add("hasChildren", false);

        if (personalDetails.Contains("firstName") && personalDetails.Contains("age"))
        {
            string username = (string)personalDetails["firstName"];
            int age = (int)personalDetails["age"];

            Debug.Log(username);
        } else {
            Debug.Log("El diccionario no contiene las claves que se han pedido");
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}