using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public List<EnemyType> generalEnemyType; // Tüm Düþman Tipleri
    public GameManager gm;  // GM
    public List<EnemyType> dailyEnemies; // O gün gelecek düþmanlarýn listesi
    public float nextSpawnTime; // Spawnlar arasý süreyi kontrol için
    public Transform[] spawnPoints; // Düþmanlarýn Doðacý Spawn yerleri
    Dictionary<int, List<GameObject>> listeler = new Dictionary<int, List<GameObject>>();
    public DeathEnemyController deathEnemyController;
    public List<GameObject> deathEnemies,aliveEnemies;
    float spawnInterval;
    public GameObject enemyPrefab, spawnedEnemy;



    void Start()
    {
        DailyCalculate(); // HER GÜN DEÐÝÞTÝGÝNDE BU METODU ÇALIÞTIR 
        listeler.Add(1, deathEnemyController.deathEnemiesType1);
        listeler.Add(2, deathEnemyController.deathEnemiesType2);
        listeler.Add(3, deathEnemyController.deathEnemiesType3);
        listeler.Add(4, deathEnemyController.deathEnemiesType4);
        listeler.Add(5, deathEnemyController.deathEnemiesType5);
    }

    void Update()
    {
        SpawnWave();
    }
    void SpawnWave()
    {
        int randomNumber;
        if(nextSpawnTime<Time.time)
        {

            for (int i = 0; i < spawnPoints.Length; i++)
            {
                randomNumber = Random.Range(0, dailyEnemies.Count);

                if (isNecessaryInstantiate(dailyEnemies[randomNumber])) // clonlama control
                {

                    enemyPrefab = dailyEnemies[randomNumber].enemyPrefab;
                    spawnedEnemy = Instantiate(enemyPrefab, spawnPoints[i].transform.position, spawnPoints[i].transform.rotation);
                    spawnedEnemy.transform.SetParent(gameObject.transform);
                    aliveEnemies.Add(spawnedEnemy);
                    // spawnedEnemy.GetComponent<EnemyControler>().playerScrpit = playerScrpit;  //  
                    nextSpawnTime = Time.time + spawnInterval;  // Spawninterval gün arttýkça azalýyor düþman dogma hýzýný artýyor bu deðer azalýrsa


                }

                else
                {


                    deathEnemies = listeler[dailyEnemies[randomNumber].type];
                    deathEnemies[0].GetComponent<EnemyControl>().isAlive = true;  // DUSMAN OLDUGUNDE BU DEGERÝ FALSE YAP
                    deathEnemies[0].transform.position = spawnPoints[i].transform.position;
                    deathEnemies[0].transform.rotation = spawnPoints[i].transform.rotation;
                    deathEnemies[0].transform.SetParent(gameObject.transform);
                    aliveEnemies.Add(deathEnemies[0]);
                    deathEnemies.RemoveAt(0);
                    nextSpawnTime = Time.time + spawnInterval;  // Spawninterval gün arttýkça azalýyor düþman dogma hýzýný artýyor bu deðer azalýrsa




                }


            }


          



           
            // nextSpawnTime = Time.time +5 // 5 saniyede bir yeni düþman için
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


    void DailyCalculate()
    {
        int smallestWeight =1;  // Gelecek düþmanlarýn Oranlarýný hesaplamak için 

        for (int i = 0; i < generalEnemyType.Count; i++)
        {
            if(gm.day>= generalEnemyType[i].startDay)
            {
                smallestWeight = generalEnemyType[i].weight;

            }

        }

        for (int i = 0; i < generalEnemyType.Count; i++)
        {
            if (gm.day >= generalEnemyType[i].startDay)
            {

                for (int j = 0; j < (Mathf.CeilToInt(generalEnemyType[i].weight / smallestWeight)); j++) // CeilToInt Yuvarlama yapar
                {
                    dailyEnemies.Add(generalEnemyType[i]);
                  
                }


            }


            }
       spawnInterval = (float)1.25 / ((Mathf.Pow(gm.day, 0.25f) + 2 + (Mathf.Sin(gm.day) / 4))) * 8;


    }

}
