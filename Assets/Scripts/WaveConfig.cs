using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Enemy wave config")]
public class WaveConfig : ScriptableObject {
    [SerializeField] GameObject pathPrefab;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = .3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] bool BossWave = false;

    public List<Transform> getWaypoints (){

        var waveWaypoints = new List<Transform>();

        foreach (Transform child in pathPrefab.transform)
            {
            waveWaypoints.Add(child);
        }

        return waveWaypoints;
    }

    public GameObject getEnemyPrefab (){ return enemyPrefab; }

    public float getTimeBetweenSpawns (){ return timeBetweenSpawns; }

    public float getSpawnRandomFactor () { return spawnRandomFactor; }

    public float getmoveSpeed() { return moveSpeed; }

    public int getNumberOfEnemies() { return numberOfEnemies; }
    // Use this for initialization
    public bool getIsBossWave()
    {
        return BossWave;
    }
    
   
}
