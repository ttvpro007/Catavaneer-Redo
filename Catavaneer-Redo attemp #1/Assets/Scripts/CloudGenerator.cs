using UnityEngine;
using ViTiet.Library.UnityExtension.Gizmos;

public class CloudGenerator : MonoBehaviour
{
    [SerializeField] int cloudNumber = 25;
    [SerializeField] float cloudMinSpeed = 1;
    [SerializeField] float cloudMaxSpeed = 3;
    [SerializeField] float width = 100;
    [SerializeField] float height = 100;
    [SerializeField] float depth = 10;
    [SerializeField] GameObject[] cloudTypes = null;
    BorderInfo borderInfo = new BorderInfo();
    
    private void Start()
    {
        InitiateBorderInfo();
        GenerateCloud();
    }

    private void InitiateBorderInfo()
    {
        borderInfo.xMin = transform.position.x - width / 2;
        borderInfo.xMax = transform.position.x + width / 2;
        borderInfo.yMin = transform.position.y - depth / 2;
        borderInfo.yMax = transform.position.y + depth / 2;
        borderInfo.zMin = transform.position.z - height / 2;
        borderInfo.zMax = transform.position.z + height / 2;
    }

    private void GenerateCloud()
    {
        Vector3 position;
        GameObject cloud;
        float speed;

        for (int i = 0; i < cloudNumber; i++)
        {
            position = RandomPosition();
            speed = RandomFloat(cloudMinSpeed, cloudMaxSpeed);

            cloud = Instantiate(cloudTypes[RandomInt(0, cloudTypes.Length)], position, Quaternion.identity);
            cloud.AddComponent<Cloud>().Initiate(speed, borderInfo);
        }
    }

    private int RandomInt(int min, int max)
    {
        return Random.Range(min, max);
    }

    private float RandomFloat(float min, float max)
    {
        return Random.Range(min, max);
    }

    private Vector3 RandomPosition()
    {
        float x, y, z;

        x = RandomFloat(borderInfo.xMin, borderInfo.xMax);
        y = RandomFloat(borderInfo.yMin, borderInfo.yMax);
        z = RandomFloat(borderInfo.zMin, borderInfo.zMax);

        return new Vector3(x, y, z);
    }

    private void OnDrawGizmosSelected()
    {
        GizmosExtended.DrawWireRectangle3D(transform.position, width, height, depth, Color.red);
    }
}
