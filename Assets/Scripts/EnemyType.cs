using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class EnemyType : ScriptableObject
{
    public float damage, armor, fireRate, speed, heal;
    public int award, startWave,weight;
    public string enemyName;
    public GameObject enemyPrefab;

}