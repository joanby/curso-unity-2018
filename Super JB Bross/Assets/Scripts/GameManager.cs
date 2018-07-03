using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Posibles estados del videojuego
public enum GameState{
    menu,
    inGame, 
    gameOver
}

public class GameManager : MonoBehaviour {

    //Variable que referencia al propio Game Manager
    public static GameManager sharedInstance;

    //Variable para saber en qué estado del juego nos encontramos ahora mismo
    //Al inicio, queremos que empiece en el menú principal
    public GameState currentGameState = GameState.menu;

    public Canvas menuCanvas, gameCanvas, gameOverCanvas;


	private void Awake()
	{
        sharedInstance = this;
	}

	private void Start()
	{
        BackToMenu();
	}

	private void Update()
	{
        if(Input.GetButtonDown("Start") && this.currentGameState != GameState.inGame){
            StartGame();              
        }

        if(Input.GetButtonDown("Pause")){
            BackToMenu();
        }
	}

	//Método encargado de iniciar el juego
	public void StartGame(){
        SetGameState(GameState.inGame);


        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        CameraFollow cameraFollow = camera.GetComponent<CameraFollow>();
        cameraFollow.ResetCameraPosition();

        if (PlayerController.sharedInstance.transform.position.x > 10)
        {
            LevelGenerator.sharedInstance.RemoveAllTheBlocks();
            LevelGenerator.sharedInstance.GenerateInitialBlocks();
        }

        PlayerController.sharedInstance.StartGame();
    }

    //Método que se llamará cuando el jugador muera
    public void GameOver(){
        SetGameState(GameState.gameOver);
    }

    //Método para volver al menú principal cuando el usuario lo quiera hacer
    public void BackToMenu(){
        SetGameState(GameState.menu);
    }

   
    //Método encargado de cambiar el estado del juego
    void SetGameState(GameState newGameState){

        if (newGameState == GameState.menu)
        {
            //Hay que preparar la escena de Unity para mostrar el menú
            menuCanvas.enabled = true;
            gameCanvas.enabled = false;
            gameOverCanvas.enabled = false;

        }
        else if (newGameState == GameState.inGame)
        {
            //Hay que preparar la escena de Unity para jugar
            menuCanvas.enabled = false;
            gameCanvas.enabled = true;
            gameOverCanvas.enabled = false;

        }
        else if (newGameState == GameState.gameOver)
        {
            //Hay que preparar la escena de Unity para el Game Over
            menuCanvas.enabled = false;
            gameCanvas.enabled = false;
            gameOverCanvas.enabled = true;

        }

        //Asignamos el estado de juego actual al que nos ha llegado por parámetro
        this.currentGameState = newGameState;
    }

}
