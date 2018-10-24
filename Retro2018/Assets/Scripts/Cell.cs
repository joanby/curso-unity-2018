using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cell : MonoBehaviour {

    public bool hasMine;

    public Sprite[] emptyTextures;
    public Sprite mineTexture;

	// Use this for initialization
	void Start () {
        GridHelper helper = GameObject.Find("Panel").GetComponent<GridHelper>();
        hasMine = (Random.value < helper.mineWeight);

        int x = (int)this.transform.position.x;
        int y = (int)this.transform.parent.transform.position.y;

        GridHelper.cells[x, y] = this;
	}
	

    public void loadTexture(int adjacentCount){
        if(hasMine){
            GetComponent<SpriteRenderer>().sprite = mineTexture;
        } else {
            GetComponent<SpriteRenderer>().sprite = emptyTextures[adjacentCount];
        }
    }

    //Método de ayuda para saber si la celda está o no tapada 
    //Para ello comprobamos si la imagen es la de 'panel' o bien otra.
    public bool IsCovered(){
        return GetComponent<SpriteRenderer>().sprite.texture.name == "panel";
    }
    //Método que se llama al hacer click en una celda
	private void OnMouseUpAsButton()
	{
        if(hasMine){
            //TODO: mostrar mensaje de Game Over...
            GridHelper.UncoverAllTheMines();
            Debug.Log("Pringaooo, que había una mina!!!!");
            Invoke("ReturnToMainMenu", 3.0f);
        }else{
            //TODO: cambiar la textura de la celda
            int x = (int)this.transform.position.x;
            int y = (int)this.transform.parent.transform.position.y;
            loadTexture(GridHelper.CountAdjacentMines(x, y));
            //TODO: descubrir toda el área sin minas alrededor de la celda abierta
            GridHelper.FloodFillUncover(x, y, new bool[GridHelper.w, GridHelper.h]);
            //TODO: comprobar si el juego ha terminado o no
            if(GridHelper.HasTheGameEnded()){
                Debug.Log("Has ganado. Fin de la partida...");
                Invoke("ReturnToMainMenu", 3.0f);
            }
        }
	}

    private void ReturnToMainMenu(){
        SceneManager.LoadScene("Main Menu");
    }


}
