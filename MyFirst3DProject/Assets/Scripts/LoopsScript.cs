using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopsScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
        /*
         * FOREACH -> iterar sobre los elementos de una colección
            foreach(Type elementName in collection){
                //Código del bucle
            }

        */
        List<string> studentsNames = new List<string>();
        studentsNames.Add("Christian");
        studentsNames.Add("Kate");
        studentsNames.Add("Mia");
        studentsNames.Add("Anastasia");

        Debug.Log("IMPRIMIENDO CON FOREACH");
        foreach(string person in studentsNames){
            Debug.Log(person);
        }

        int[] someInts = new int[] { 4, 8, 3, 0, 9, 6, 8, 7 };
        int sum = 0;
        int n = someInts.Length;
        foreach(int i in someInts){
            sum = sum + i;
            Debug.Log("La suma vale ahora: " + sum);
        }

        Debug.Log("La media de los números vale: " + sum / n);


        /*
         * FOR -> acceder a posiciones
         * for(inicialización; condición de continuación; iterador){
         *      //código del bucle
         * }
         */

        Debug.Log("IMPRIMIENDO CON EL FOR");
        for (int i = 1; i <= 10; i++){
            Debug.Log(i);
        }
        
        Debug.Log("CUENTA ATRÁS CON EL FOR");

        for (int j = 10; j >= 0; j--){
            Debug.Log(j);
        }


        for (int pos = 0; pos < studentsNames.Count; pos++){
            string name = studentsNames[pos];
            Debug.Log("El elemento número " + pos + " de la lista es " + name);
        }


        /*
         * WHILE
         * Inicialización 
         * while(condicion de continuación){
         *  //código a ejecutar
         *  iterador
         * }
         * La variable del bucle sigue existiendo después...
         */

        Debug.Log("CONTAR DE 1 a 10 CON BUCLE WHILE");
        int counter = 1;
        while(counter<=10){
            Debug.Log(counter);
            counter++;
        }


        bool isRaining = false;
        while(!isRaining){
            //ir a la playa
            //comprobar si llueve...
            isRaining = true;
        }


        for (int i = 0; i < 100; i++){
            if (i == 0)
            {
                Debug.Log("El número cero es especial...");
            }
            else if (IsNumberEven(i))
            {
                Debug.Log("El número " + i + " es par.");
            }
            else
            {
                Debug.Log("El número " + i + " es impar.");
            }
        }


        Debug.Log("Números primos");

        for (int number = 2; number <= 100; number++)
        {
            bool isPrime = true;
            for (int i = 2; i < number; i++)
            {
                int remainder = number % i;
                if (remainder == 0)
                {
                    isPrime = false;
                }
            }

            if (isPrime)
            {
                Debug.Log("El número " + number + " es primo.");
            }
            /*else
            {
                Debug.Log("El número " + number + " es compuesto.");
            }*/
        }

        Debug.Log("Algoritmos de búsqueda");

        int objectPos = -1;
        for (int i = 0; i < studentsNames.Count; i++){
            Debug.Log("Vamos por la iteración número " + i);
            if(studentsNames[i] == "Christian"){
                objectPos = i;
                break;
            }
        }
        
        if(objectPos == -1){
            Debug.Log("No hemos encontrado el objeto que buscabas...");
        } else{
            Debug.Log("el objeto buscado se encuentra en la posición " + objectPos);
        }


	}
	
	
    public bool IsNumberEven(int number){//even = par, odd = impar
        //int quotient = number / 2;
        int remainder = number % 2;//resto de dividir number entre 2

        if(remainder == 0){
            return true;
        }else{ //el resto en este caso será 1
            return false;
        }
    }




}
