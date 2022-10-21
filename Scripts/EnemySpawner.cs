using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemySpawner : MonoBehaviourPun
{
    public string enemyPrefabPath;
    public float maxEnemies;
    public float spawnRadius;
    public float spawnCheckTime;

    public float lastSpawnCheckTime;
    private List<GameObject> curEnemies = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!PhotonNetwork.IsMasterClient)
        {
            return;
        }

        if(Time.time - lastSpawnCheckTime > spawnCheckTime)
        {
            lastSpawnCheckTime = Time.time;
            TrySpawn();
        }
    }

    void TrySpawn()
    {
        Debug.Log("Try Spawn");

        //remove any dead enemies from curEnemies list
        for (int x = 0; x < curEnemies.Count; ++x)
        {

            if (!curEnemies[x])
            {
                curEnemies.RemoveAt(x);
            }
        }
            //if we have maxed out our enemies, return
            if(curEnemies.Count >= maxEnemies)
            {
                Debug.Log("Max Enemies");
                return;
            }

            //otherwise spawn an enemy
            Vector3 randomInCircle = Random.insideUnitCircle * spawnRadius;
            GameObject enemy = PhotonNetwork.Instantiate(enemyPrefabPath, transform.position + randomInCircle, Quaternion.identity);
            curEnemies.Add(enemy);
            Debug.Log("Enemy Spawned");
    }
}
