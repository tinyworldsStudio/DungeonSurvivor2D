using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public DeathEnemyController enemyControler;
    public EnemyType enemyType;
    public List<float> deneme;
    void Start()
    {
        for (int i = 0; i < 500; i++)
        {
            deneme.Add((float)1.25/((Mathf.Pow(i, 0.25f) +2+ (Mathf.Sin(i)/4)))*8);
        }
    }

 
    void Update()
    {
      

    }
}
