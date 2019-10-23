using UnityEngine;
using ViTiet.UnityExtension.Gizmos;
using ViTiet.ProceduralGenerator.Helper;
using Plane = ViTiet.UnityExtension.Math.Plane;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs = null;
    [SerializeField] EnemyTypes generatingType = new EnemyTypes();
    [SerializeField] float spawnInterval = 1f;
    [SerializeField] float spawnAreaWidth = 10f;
    [SerializeField] float spawnAreaHeight = 5f;
    [SerializeField] int colum = 5;
    [SerializeField] int row = 5;
    [SerializeField] bool random = false;
    Vector3[] spawnPoints;
    GameObject[] enemies;

    private void Start()
    {
        spawnPoints = GeneratorHelper.GenerateGridPoints3D(transform, spawnAreaWidth, spawnAreaHeight, 0, row, colum, 1);
        enemies = Generate(random);
    }

    private void Update()
    {
        //Generate(random);
    }

    private GameObject[] Generate(bool isRandom)
    {
        GameObject[] gameObjects = new GameObject[spawnPoints.Length];

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (!isRandom)
            {
                gameObjects[i] = Instantiate(enemyPrefabs[GetPrefabIndexByType(generatingType)].gameObject, spawnPoints[i], transform.rotation);
            }
            else
            {
                gameObjects[i] = Instantiate(enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Length)].gameObject, spawnPoints[i], transform.rotation);
            }
        }

        return gameObjects;
    }

    private int GetPrefabIndexByType(EnemyTypes type)
    {
        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            if (type == enemyPrefabs[i].GetComponent<Enemy>().type)
            {
                return i;
            }
        }

        // index out of bound error if can't find type in collecion
        return -1;
    }

    private void OnDrawGizmosSelected()
    {
        GizmosExtended.DrawWireRectangle2D(transform, spawnAreaWidth, spawnAreaHeight, Color.white);
        //GizmosExtended.DrawWireRectangle3D(transform, spawnAreaWidth, spawnAreaHeight, 10, Color.white);
        //GizmosExtended.DrawWireEllipse2D(transform, 30, 40, 16, Plane.XZ, Color.white);
        //GizmosExtended.DrawWireEllipse3D(transform, new Vector3(30, 40, 50), 16, Color.white);

        spawnPoints = GeneratorHelper.GenerateGridPoints3D(transform, spawnAreaWidth, spawnAreaHeight, 0, row, colum, 1);

        foreach (Vector3 point in spawnPoints)
        {
            Gizmos.DrawSphere(point, .2f);
        }
    }

    private void OnValidate()
    {
        if (row < 1) row = 1;
        if (row > 15) row = 15;
        if (colum < 1) colum = 1;
        if (colum > 15) colum = 15;
    }
}