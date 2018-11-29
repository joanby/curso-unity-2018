using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseCharacter {
    [SerializeField]
    public string name;
    [SerializeField]
    public string description;

    [SerializeField]
    public float strength, //daño físico que puede hacer nuestro personaje
    defense, //daño físico que puede recibir 
    dexterity, //mide la 'habilidad' del personaje
    intelligence, //mide la capacidad de razonamiento / interacción
    health, // marca si el personaje está vivo o muerto 
    speed; //Sirve para indicar la velocidad del personaje
    public bool canUseWeapons; //Nos indica si el personaje puede llevar armas o no


}
