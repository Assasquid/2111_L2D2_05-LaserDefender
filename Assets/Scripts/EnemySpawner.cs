using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    [SerializeField] float spawnTimeVariance = 0f;
    [SerializeField] float minimumSpawnTime = 0f;
    [SerializeField] bool isLooping;
    WaveConfigSO currentWave;
    float randomTimeBetweenWaves;

    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }
    
    IEnumerator SpawnEnemyWaves()
    {
        do 
        {
            currentWave = waveConfigs[Random.Range(0, waveConfigs.Count)];

                for(int i = 0; i < currentWave.GetEnemyCount(); i++)
                {
                    Instantiate(currentWave.GetEnemyPrefab(i), 
                                currentWave.GetStartingWaypoint().position, 
                                Quaternion.Euler(0, 0, 180),
                                transform);
                
                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }
                RandomTimeBetweenWaves();
                yield return new WaitForSeconds(randomTimeBetweenWaves);
            }
        while (isLooping);
    }

    void RandomTimeBetweenWaves()
    {
        randomTimeBetweenWaves = Random.Range(timeBetweenWaves - spawnTimeVariance,
                                        timeBetweenWaves + spawnTimeVariance);
        Mathf.Clamp(randomTimeBetweenWaves, minimumSpawnTime, float.MaxValue);
    }
}
