using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveZone : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D collision)
	{
        LevelGenerator.sharedInstance.AddLevelBlock();
        LevelGenerator.sharedInstance.RemoveOldestLevelBlock();
	}
}
