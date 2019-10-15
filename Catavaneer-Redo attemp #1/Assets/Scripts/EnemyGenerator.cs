using UnityEngine;
using ViTiet.Library.UnityExtension.Gizmos;
using Plane = ViTiet.Library.UnityExtension.Gizmos.Plane;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs = null;
    [SerializeField] EnemyTypes generatingType = new EnemyTypes();
    [SerializeField] float spawnInterval = 1f;
    [SerializeField] float spawnAreaWidth = 10f;
    [SerializeField] float spawnAreaHeigth = 5f;
    //[SerializeField] float spawnAreaDepth = 5f;
    [SerializeField] int colum = 5;
    [SerializeField] int row = 5;
    //[SerializeField] int layer = 5;
    [SerializeField] bool random = false;

    private void Update()
    {
        Generate(random);
    }

    private void Generate(bool isRandom)
    {
        if (!isRandom)
        {
            Instantiate(enemyPrefabs[GetPrefabIndexByType(generatingType)].gameObject);
        }
        else
        {
            Instantiate(enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Length)].gameObject);
        }
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
        GizmosExtended.DrawWireRectangle2D(transform, spawnAreaWidth, spawnAreaHeigth, Color.white);
        //GizmosExtended.DrawWireRectangle3D(transform, spawnAreaWidth, spawnAreaHeigth, spawnAreaDepth, Color.white);
        GizmosExtended.GenerateGridPoints(transform, new Vector3(spawnAreaWidth, 0, spawnAreaHeigth), new Vector3(row, 1, colum));
        //GizmosExtended.DrawWireEllipse2D(transform, 30, 40, 16, Plane.XZ, Color.white);
        //GizmosExtended.DrawWireEllipse3D(transform, new Vector3(30, 40, 50), 16, Color.white);
    }
}