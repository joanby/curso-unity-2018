using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {

    float lastFall = 0.0f;
	// Use this for initialization
	void Start () {
        StartCoroutine("CheckGameOver");
	}

    IEnumerator CheckGameOver(){
        yield return new WaitForSecondsRealtime(0.11f);
        if (!IsValidPiecePosition())
        {
            Debug.Log("GAME OVER");
            AdsManager.ShowAds();
            Destroy(this.gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.LeftArrow)) //Movimiento de la ficha a la izquierda
        {
            MovePieceHorizontally(-1);
        }else if(Input.GetKeyDown(KeyCode.RightArrow)) //Movimiento de la ficha a la derecha
        {
            MovePieceHorizontally(1);
        }else if(Input.GetKeyDown(KeyCode.UpArrow)) //Rotar la ficha
        {
            this.transform.Rotate(0, 0, -90);
            if(IsValidPiecePosition()){
                UpdateGrid();
            }else{
                this.transform.Rotate(0, 0, 90);
            }
        }else if(Input.GetKeyDown(KeyCode.DownArrow)||(Time.time-lastFall)>1.0f){ //Mover la ficha hacia abajo
            this.transform.position += new Vector3(0, -1, 0);

            if(IsValidPiecePosition()){
                UpdateGrid();
            }else{
                this.transform.position += new Vector3(0, 1, 0);
                //Como la pieza no puede bajar más, a lo mejor ha llegado el momento de eliminar filas
                GridHelper.DeleteAllFullRows();
                //Hacemos que se spawnee una nueva ficha
                FindObjectOfType<PieceSpawner>().SpawnNextPiece();
                //Deshabilitamos el script para que esta pieza no vuelva a moverse
                this.enabled = false;
            }

            lastFall = Time.time;
        }


	}


    void MovePieceHorizontally(int direction){
        //Muevo la pieza a la izquierda
        this.transform.position += new Vector3(direction, 0, 0);

        //Comprobamos si la nueva posición es válida
        if (IsValidPiecePosition())
        {
            //Persisto la información del movimiento en la parrilla del helper
            UpdateGrid();
        }
        else
        {
            //Si la posición no es válida, revierto el movimiento
            this.transform.position += new Vector3(-direction, 0, 0);
        }
    }


    bool IsValidPiecePosition(){
        foreach(Transform block in this.transform){
            //POsición de cada uno de los hijos de la pieza
            Vector2 pos = GridHelper.RoundVector(block.position);

            //Si la posición está fuera de los límites, entonces no es una posición válida
            if(!GridHelper.IsInsideBorders(pos)){
                return false;
            }

            //Si ya hay otro bloque en esa misma posición, tampoco es válida
            Transform possibleObject = GridHelper.grid[(int)pos.x, (int)pos.y];
            if(possibleObject != null && possibleObject.parent != this.transform){
                return false;
            }

        }

        return true;
    }


    void UpdateGrid(){
        for (int y = 0; y < GridHelper.height; y++){
            for (int x = 0; x < GridHelper.width; x++){
                if(GridHelper.grid[x,y]!=null){
                    //El padre del bloque es la pieza del propio script
                    if(GridHelper.grid[x,y].parent == this.transform){
                        GridHelper.grid[x, y] = null;
                    }
                }
            }
        }

        foreach(Transform block in this.transform){
            Vector2 pos = GridHelper.RoundVector(block.position);

            GridHelper.grid[(int)pos.x, (int)pos.y] = block;
        }

    }


}
