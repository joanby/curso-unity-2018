using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawner : MonoBehaviour {

    public GameObject[] levelPieces;

	private void Start()
	{
        SpawnNextPiece();
	}

	public void SpawnNextPiece(){
        //aleatoriamente decido qué pieza del array debe spawnearse
        int i = Random.Range(0, levelPieces.Length);

        Instantiate(levelPieces[i], this.transform.position, Quaternion.identity);
    }
}
