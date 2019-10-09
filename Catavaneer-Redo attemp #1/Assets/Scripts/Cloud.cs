using UnityEngine;

public class Cloud : MonoBehaviour
{
    float speed = 0;
    BorderInfo borderInfo = new BorderInfo();

    // Start is called before the first frame update
    public void Initiate(float speed, BorderInfo borderInfo)
    {
        this.speed = speed;
        this.borderInfo = borderInfo;
    }
     
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private float RandomFloat(float min, float max)
    {
        return Random.Range(min, max);
    }

    private void Move()
    {
        Vector3 direction = new Vector3(0, 0, speed * Time.deltaTime);
        transform.Translate(direction);

        Warp();
    }

    private void Warp()
    {
        if (transform.position.z > borderInfo.zMax)
        {
            transform.position = new Vector3(RandomFloat(borderInfo.zMin, borderInfo.zMax), transform.position.y, borderInfo.zMin);
        }
    }
}
