using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] List<WaveConfig> waves;

    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;
    
   // bool isRunning = true;
   
    // Use this for initialization
    IEnumerator Start()
    {

        do
        {
            yield return StartCoroutine(spawnAllWaves());
        } while (looping);
    }

    private IEnumerator spawnAllEnemiesInWave(WaveConfig currentWave)
    {
       
        for (int enemyCount = 0; enemyCount < currentWave.getNumberOfEnemies(); enemyCount++)
        {
         GameObject enemy =   Instantiate(currentWave.getEnemyPrefab(), //Enemy prefab for wave
                currentWave.getWaypoints()[0].transform.position,  //position of first wayppoint
                Quaternion.identity); //the objects current rotation
            enemy.GetComponent<EnemyPathing>().setWaveConfig(currentWave);
          
            

            yield return new WaitForSeconds(currentWave.getTimeBetweenSpawns());
        }

       


    }

   

    IEnumerator spawnAllWaves()
    {
       
        for (int waveIndex = startingWave; waveIndex < waves.Count;waveIndex++)
        {
            
            var currentWave = waves[waveIndex];
            //Starts the coroutine and waits until it ends

          
            yield return StartCoroutine(spawnAllEnemiesInWave(currentWave));
           
           
            }
    }


}
