using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{

  
    public float spawnInterval;
    public GameObject enemyPrefab;
    public List<EnemyType> enemies;
    



}

public class WaveSpawner : MonoBehaviour
{
    public List<EnemyType> GeneralEnemyTypes;
    public List<GameObject> aliveEnemies,deathEnemies;
    public GameManager gameManager;
    public GameObject deathEnemiesParent;
    public Wave wave;
    private GameObject spawnedEnemy;
    private float nextSpawnTime=1;
    private PlayerControl playerScrpit;
    public DeathEnemyController enemyScrpit;
    public Transform[] spawnPoints;
    Dictionary<int, List<GameObject>> listeler = new Dictionary<int, List<GameObject>>();
    int currentEnemyType;

    void Start()
    {
        CalculateTheWave();
        listeler.Add(1, enemyScrpit.deathEnemiesType1);
        listeler.Add(2, enemyScrpit.deathEnemiesType2);
        listeler.Add(3, enemyScrpit.deathEnemiesType3);
        listeler.Add(4, enemyScrpit.deathEnemiesType4);
        listeler.Add(5, enemyScrpit.deathEnemiesType5);


    }

    void Update()
    {
        SpawnWave();
     


    }
    
   

    void SpawnWave()
    {
        int randomNumber;

        if (nextSpawnTime < Time.time)
        {
           
            
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                randomNumber = Random.Range(0, wave.enemies.Count);
                if (isNecessaryInstantiate(wave.enemies[randomNumber]))
                {
                    wave.enemyPrefab = wave.enemies[randomNumber].enemyPrefab;
                    spawnedEnemy = Instantiate(wave.enemyPrefab, spawnPoints[i].transform.position, spawnPoints[i].transform.rotation);
                    spawnedEnemy.transform.SetParent(gameObject.transform);
                    aliveEnemies.Add(spawnedEnemy);
                 // spawnedEnemy.GetComponent<EnemyControler>().playerScrpit = playerScrpit;
                    nextSpawnTime = Time.time + wave.spawnInterval;
                }

                else
                {
                    deathEnemies = listeler[wave.enemies[randomNumber].type];
                    deathEnemies[0].GetComponent<EnemyControl>().isAlive = true;
                    deathEnemies[0].transform.position = spawnPoints[i].transform.position;
                    deathEnemies[0].transform.rotation = spawnPoints[i].transform.rotation;
                    deathEnemies[0].transform.SetParent(gameObject.transform);
                    aliveEnemies.Add(deathEnemies[0]);
                    deathEnemies.RemoveAt(0);
                    nextSpawnTime = Time.time + wave.spawnInterval;


                }










            }


            
            

        }

    }

    bool isNecessaryInstantiate(EnemyType enemyType)
    {
        deathEnemies = listeler[enemyType.type];
        if (deathEnemies.Count > 0)
        { 
            return false;
        }
            
        return true;



    }


    public void CalculateTheWave()
    {

        int smallestWeight=1;


        for (int i = 0; i < GeneralEnemyTypes.Count; i++)
        {

            if (gameManager.day >= GeneralEnemyTypes[i].startWave)
            {
                smallestWeight = GeneralEnemyTypes[i].weight;

            }

        }
        

        for (int i = 0; i < GeneralEnemyTypes.Count; i++)
        {
    

            if (gameManager.day >= GeneralEnemyTypes[i].startWave)
            {
                
                for (int j=0; j < (Mathf.CeilToInt(GeneralEnemyTypes[i].weight / smallestWeight));j++)
                {
                    wave.enemies.Add(GeneralEnemyTypes[i]);
                }
              
                }
        }


        //(Mathf.Pow(gm.day, 1.35f) + 10 + Mathf.Sin(gm.day));
        wave.spawnInterval = (float)1.25 / ((Mathf.Pow(gameManager.day, 0.25f) + 2 + (Mathf.Sin(gameManager.day) / 4))) * 8;


       
    }


}