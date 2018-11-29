using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour {

    public GameObject enemyPrefab;

    public int numberOfEnemies = 12;


    public override void OnStartServer()
    {
        for (int i = 0; i < numberOfEnemies; i++){
            var spawnPosition = new Vector3(
                Random.Range(-50f, 50f),
                0,
                Random.Range(-50f, 50f)
            );

            var spawnRotation = Quaternion.Euler(0, Random.Range(0f, 180f), 0);

            var enemy = (GameObject)Instantiate(enemyPrefab, spawnPosition, spawnRotation);
            NetworkServer.Spawn(enemy);
        }
    }

}
