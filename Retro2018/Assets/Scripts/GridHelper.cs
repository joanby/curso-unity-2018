using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHelper : MonoBehaviour {

    [SerializeField]
    [Range(0.0f, 1.0f)]
    public float mineWeight = 0.1f;

    public static int w = 15;
    public static int h = 15;
    public static Cell[,] cells = new Cell[w, h];

    public static void UncoverAllTheMines(){
        foreach(Cell c in cells){
            if(c.hasMine){
                c.loadTexture(0);
            }
        }
    }


    public static bool HasMineAt(int x, int y){
        if(x>=0 && y >=0 && x < w && y < h){
            //Estoy dentro de la parrila
            Cell cell = cells[x, y];
            return cell.hasMine;
        }else{
            //Estoy fuera de la parrilla
            return false;
        }
    }

    public static int CountAdjacentMines(int x, int y){
        int count = 0;

        if (HasMineAt(x - 1, y - 1)) count++;//abajo-izquierda
        if (HasMineAt(x - 1, y    )) count++;//abajo-centro
        if (HasMineAt(x - 1, y + 1)) count++;//abajo-derecha
        if (HasMineAt(x    , y - 1)) count++;//medio-izquierda
        if (HasMineAt(x    , y + 1)) count++;//medio-derecha
        if (HasMineAt(x + 1, y - 1)) count++;//arriba-izquierda
        if (HasMineAt(x + 1, y    )) count++;//arriba-centro
        if (HasMineAt(x + 1, y + 1)) count++;//arriba-derecha

        return count;
    }


    public static void FloodFillUncover(int x, int y, bool [,] visited){
        //Solo debemos proceder si el punto (x,y) es válido
        if (x >= 0 && y >= 0 && x < w && y < h)
        {
            //Si ya he pasado por esta celda, el algorimo del FFU no debe hacer nada
            if (visited[x, y])
            {
                return;
            }

            //Si estoy aquí es que la celda no había sido visitada
            //Cuento el número de minas adyacentes a mi posición (x,y)
            int adjacentMines = CountAdjacentMines(x, y);
            //Muestro en la celda, el número de minas adyacentes (desde 0 hasta 8 máximo)
            cells[x, y].loadTexture(adjacentMines);
            //Si tengo minas adyacentes, no puedo seguir destapando
            if(adjacentMines > 0){
                return;
            }

            //Marco la actual como visitada
            visited[x, y] = true;

            //Visito todos los vecinos en C4 de la celda actual
            FloodFillUncover(x - 1, y, visited);//Visitamos izquierda
            FloodFillUncover(x + 1, y, visited);//Visitamos derecha
            FloodFillUncover(x, y - 1, visited);//Visitamos abajo
            FloodFillUncover(x, y + 1, visited);//Visitamos arriba

            FloodFillUncover(x - 1, y - 1, visited);
            FloodFillUncover(x - 1, y + 1, visited);
            FloodFillUncover(x + 1, y - 1, visited);
            FloodFillUncover(x + 1, y + 1, visited);
        }
    }

    public static bool HasTheGameEnded(){
        foreach (Cell cell in cells){
            if(cell.IsCovered() && !cell.hasMine){
                return false; 
            }
        }

        return true;
    }
}
