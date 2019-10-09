using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] EnemyPrefab[] enemyPrefabs = null;
    [SerializeField] EnemyTypes generatingType = new EnemyTypes();
    [SerializeField] float spawnInterval = 1f;
    [SerializeField] bool random = false;

    private void Update()
    {
        // generate
        // if random generate randomly
        // if not random generate default
        Generate(GetPrefabIndexByType(generatingType), random);

    }

    private void Generate(int index, bool isRandom)
    {
        if ()
        Instantiate(enemyPrefabs[index].gameObject);
    }

    private int GetPrefabIndexByType(EnemyTypes type)
    {
        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            if (type == enemyPrefabs[i].type)
            {
                return i;
            }
        }

        // index out of bound error if can't find type in collecion
        return -1;
    }


}

[Serializable]
public class EnemyPrefab
{
    public GameObject gameObject;
    public EnemyTypes type;
}