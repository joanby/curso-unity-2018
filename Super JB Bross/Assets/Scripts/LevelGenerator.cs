using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public static LevelGenerator sharedInstance;

    public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>();

    public Transform levelStartPoint;

    public List<LevelBlock> currentBlocks = new List<LevelBlock>();

	private void Awake()
	{
        sharedInstance = this;
	}

	private void Start()
	{
        AddLevelBlock();
        AddLevelBlock();
	}


	public void AddLevelBlock()
    {
        int randomIndex = Random.Range(0, allTheLevelBlocks.Count);

        LevelBlock currentBlock = (LevelBlock)Instantiate(allTheLevelBlocks[randomIndex]);
        currentBlock.transform.SetParent(this.transform, false);

        Vector3 spawnPosition = Vector3.zero;

        if(currentBlocks.Count == 0)
        {
            spawnPosition = levelStartPoint.position;
        } else {
            spawnPosition = currentBlocks[currentBlocks.Count - 1].exitPoint.position;
        }

        currentBlock.transform.position = spawnPosition;
        currentBlocks.Add(currentBlock);

    }

    public void RemoveOldestLevelBlock()
    {
        
    }


    public void RemoveAllTheBlocks()
    {
        
    }

    public void GenerateInitialBlocks()
    {
        
    }
}
