using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public EnemyControler enemyControler;
    public EnemyType enemyType;
    void Start()
    {
        enemyControler.deathEnemies.Add(enemyType, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
