using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public static LevelGenerator sharedInstance;

    public LevelBlock firstBlock;

    //[nivel_1, nivel_2, nivel_3, nivel_4,..., nivel_n]
    public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>();

    public Transform levelStartPoint;

    public List<LevelBlock> currentBlocks = new List<LevelBlock>();

	private void Awake()
	{
        sharedInstance = this;
	}

	private void Start()
	{
        GenerateInitialBlocks();
	}


	public void AddLevelBlock()
    {
        //Random.Range(a,b) genera un número aleatorio entero x entre a<=x<b
        int randomIndex = Random.Range(0, allTheLevelBlocks.Count);

        //Creamos una copia del bloque de nivel desde la carpeta assets hasta la escena
        LevelBlock currentBlock;

        Vector3 spawnPosition = Vector3.zero;

        if(currentBlocks.Count == 0)
        {
            currentBlock = (LevelBlock)Instantiate(firstBlock);
            currentBlock.transform.SetParent(this.transform, false);
            spawnPosition = levelStartPoint.position;
        } else {
            currentBlock = (LevelBlock)Instantiate(allTheLevelBlocks[randomIndex]);
            //Pone el nuevo bloque de nivel como hijo del Level Generator
            currentBlock.transform.SetParent(this.transform, false);
            spawnPosition = currentBlocks[currentBlocks.Count - 1].exitPoint.position;
        }

        Vector3 correction = new Vector3(spawnPosition.x-currentBlock.startPoint.position.x,
                                         spawnPosition.y-currentBlock.startPoint.position.y,
                                         0);

        Debug.Log(correction);

        currentBlock.transform.position = correction;
        currentBlocks.Add(currentBlock);

    }

    public void RemoveOldestLevelBlock()
    {
        Debug.Log("Vamos a destruir un bloque. De momento hay "+currentBlocks.Count);
        LevelBlock oldestBlock = currentBlocks[0];
        currentBlocks.Remove(oldestBlock);
        Destroy(oldestBlock.gameObject);
        Debug.Log("Hemos destruido un bloque. Ahora quedan " + currentBlocks.Count);
    }


    public void RemoveAllTheBlocks()
    {
        while(currentBlocks.Count>0){
            RemoveOldestLevelBlock();
        }
    }

    public void GenerateInitialBlocks()
    {
        for (int i = 0; i < 2; i++){
            AddLevelBlock();
        }
    }
}
