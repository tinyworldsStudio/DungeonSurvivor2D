using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{

  
    public float spawnInterval = 5;
    public GameObject enemyPrefab;
    public List<EnemyType> enemies;
    



}

public class WaveSpawner : MonoBehaviour
{
    public List<EnemyType> GeneralEnemyTypes;
    public List<GameObject> aliveEnemies;
    public GameManager gameManager;
    public Wave wave;
    private GameObject spawnedEnemy;
    private float nextSpawnTime;
    private PlayerControl playerScrpit;
    public EnemyControler enemyScrpit;
    public Transform spawnPoints;
    int currentEnemyType;

    void Start()
    {
        
       

    }

    void Update()
    {
        SpawnWave();
     


    }



    void SpawnWave()
    {
        if (nextSpawnTime < Time.time)
        {
            CalculateTheWave();
            if (isNecessaryInstantiate(GeneralEnemyTypes[1]))
            {
                //  wave.enemyPrefab = ?
                spawnedEnemy = Instantiate(wave.enemyPrefab, transform.position, transform.rotation);
                spawnedEnemy.transform.SetParent(gameObject.transform);
                aliveEnemies.Add(spawnedEnemy);
                spawnedEnemy.GetComponent<EnemyControler>().playerScrpit = playerScrpit;
               nextSpawnTime = Time.time + wave.spawnInterval;
            }
            

        }

    }
   
    bool isNecessaryInstantiate(EnemyType enemyType)
    {
        //if (enemyScrpit.deathEnemies(enemyType) > 0)
       
        
            return true;
        

      //  else
        //    return false;
       
    }


    public void CalculateTheWave()
    {

        int dailyEnemyCountPerType;
        int totalWeight = 0; 
        float  dailyEnemyCount = (Mathf.Pow(gameManager.day, 1.35f) + 10 + Mathf.Sin(gameManager.day)); 
       
        for (int i = 0; i < GeneralEnemyTypes.Count; i++)
        {

            if (gameManager.day >= GeneralEnemyTypes[i].startWave)
            {
                totalWeight += GeneralEnemyTypes[i].weight;

            }

        }
        
        float WeightPerEnemy = dailyEnemyCount / totalWeight;

        for (int i = 0; i < GeneralEnemyTypes.Count; i++)
        {
            dailyEnemyCountPerType = Mathf.CeilToInt((float)(GeneralEnemyTypes[i].weight * WeightPerEnemy));

            if (gameManager.day >= GeneralEnemyTypes[i].startWave)
            {

                for (int j=0; j < dailyEnemyCountPerType * 4;j++)
                {
                    wave.enemies.Add(GeneralEnemyTypes[i]);
                }
              
                }
        }

       // 
        
        wave.spawnInterval = 1;
      



    }
    

}