using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Librería para cargar la Interfaz gráfica de usuario

public class GameManager : MonoBehaviour {

    public static GameManager sharedInstance = null;
    public bool gameStarted = false;


    public Text title;
    public Button start;

	private void Awake()
	{
        if(sharedInstance == null){
            sharedInstance = this;
        }
	}

    public void StartGame(){
        gameStarted = true;
        title.gameObject.SetActive(false);
        start.gameObject.SetActive(false);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
