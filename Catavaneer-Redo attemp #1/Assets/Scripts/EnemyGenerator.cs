using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs = null;
    [SerializeField] EnemyTypes generatingType = new EnemyTypes();
    [SerializeField] float generateInterval = 1f;
}
