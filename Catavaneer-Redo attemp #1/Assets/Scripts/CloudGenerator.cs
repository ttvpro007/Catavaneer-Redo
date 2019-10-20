using UnityEngine;
using ViTiet.Library.ProceduralGenerator.Helper;
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
    
    private void Start()
    {
        GenerateCloud();
    }

    private void GenerateCloud()
    {
        BorderInfo borderInfo = new BorderInfo(transform.position, width, height, depth);
        Vector3 position;
        GameObject cloud;
        float speed;

        for (int i = 0; i < cloudNumber; i++)
        {
            position = RandomPosition(borderInfo);
            speed = RandomFloat(cloudMinSpeed, cloudMaxSpeed);

            cloud = Instantiate(cloudTypes[RandomInt(0, cloudTypes.Length)], position, transform.rotation);
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

    private Vector3 RandomPosition(BorderInfo borderInfo)
    {
        float x, y, z;

        x = RandomFloat(borderInfo.xMin, borderInfo.xMax);
        y = RandomFloat(borderInfo.yMin, borderInfo.yMax);
        z = RandomFloat(borderInfo.zMin, borderInfo.zMax);

        return new Vector3(x, y, z);
    }

    private void OnDrawGizmosSelected()
    {
        GizmosExtended.DrawWireRectangle3D(transform, width, height, depth, Color.red);
    }
}
