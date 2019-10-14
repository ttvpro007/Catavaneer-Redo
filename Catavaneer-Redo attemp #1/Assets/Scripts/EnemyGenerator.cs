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
    [SerializeField] int gridWidthSegments = 5;
    [SerializeField] int gridHeigthSegments = 5;
    [SerializeField] bool random = false;
    public int totalVert;

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

        for (int h = 0; h <= gridHeigthSegments; h++)
        {
            for (int w = 0; w <= gridWidthSegments; w++)
            {
                Gizmos.DrawSphere(GenerateGridPoints(w, h), .2f);
            }
        }

        totalVert = gridWidthSegments * gridHeigthSegments;

        //Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaWidth, 0, spawnAreaHeigth));
    }

    private Vector3 GenerateGridPoints(int row, int col)
    {
        Vector3 point = transform.position;

        point.x = transform.position.x - spawnAreaWidth / 2 + (spawnAreaWidth / gridWidthSegments * row);
        point.y = transform.position.y;
        point.z = transform.position.z - spawnAreaHeigth / 2 + (spawnAreaHeigth / gridHeigthSegments * col);

        return point;
    }
}