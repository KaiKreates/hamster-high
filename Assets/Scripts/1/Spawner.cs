using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] obstacles;
    public GameObject coins;
    private GameObject spawnedObject;
    private GameObject spawnedCoin;
    private int index;
    public float cooldownTime = 4f;
    private float obstacleCd = 0f;
    private float coinCd = 0.5f;
    private int obstacleCount = 0;
    private Light pointLight;
    private GameObject[] lights;
    private float red = 1f, green = 0f, blue = 0f;
    void Awake()
    {

    }

    void Update()
    {
        
        if (obstacleCount > 30)
        {
            cooldownTime = 3f;
            spawnedObject.GetComponent<ObstacleController>().angularAcc = 10f;
        }
        else if(obstacleCount > 50)
        {
            cooldownTime = 2.5f;
            spawnedObject.GetComponent<ObstacleController>().angularAcc = 20f;
        }
        else if (obstacleCount > 80)
        {
            cooldownTime = 2f;
            spawnedObject.GetComponent<ObstacleController>().angularAcc = 40f;
        }
        else if (obstacleCount > 100)
        {
            cooldownTime = 2f;
            spawnedObject.GetComponent<ObstacleController>().angularAcc = 60f;
        }
        if(Time.time > obstacleCd)
        {
            SpawnObstacles();
            obstacleCd = Time.time + cooldownTime;
            Debug.Log(spawnedObject.GetComponent<Rigidbody>().angularVelocity);
        }
        if(Time.time > coinCd)
        {
            SpawnCoins();
            coinCd = Time.time + cooldownTime;
        }

    }

    void SpawnObstacles()
    {
        index = Random.Range(0, obstacles.Length);
        spawnedObject = Instantiate(obstacles[index], transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        spawnedObject.GetComponent<Rigidbody>().centerOfMass = new Vector3(0, 0, 0);                
        spawnedObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -2.5f);
        spawnedObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, Random.Range(-2f, 2f));
        pointLight = spawnedObject.transform.GetChild(0).gameObject.GetComponent<Light>();
        pointLight.color = RandomColor();
        obstacleCount++;
    }

    void SpawnCoins()
    {
        float theta = Random.Range(0, 2 * Mathf.PI);
        for(int i = 0; i < 5; i++)
        {
            Vector3 coinPos = new Vector3(0.75f * Mathf.Cos(theta), 0.75f * Mathf.Sin(theta), transform.position.z + (i * 2));
            spawnedCoin = Instantiate(coins, coinPos, Quaternion.identity);
            spawnedCoin.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -2.5f);
        }
    }

    Color ClockwiseColor()
    {
        if (red >= 1f)
        {
            red = 1f;
            blue += Random.Range(0.1f, 0.3f);
            green -= Random.Range(0.1f, 0.3f);
        }
        if (blue >= 1f)
        {
            blue = 1f;
            red -= Random.Range(0.1f, 0.3f);
        }
        if (red <= 0f)
        {
            red = 0f;
            green += Random.Range(0.1f, 0.3f);
        }
        if (green >= 1f)
        {
            green = 1f;
            blue -= Random.Range(0.1f, 0.3f);
        }
        if (blue <= 0f)
        {
            blue = 0f;
            red += Random.Range(0.1f, 0.3f);
        }

        return new Color(red, green, blue);
    }

    Color RandomColor()
    {
        return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    void AllColor()
    {
        lights = GameObject.FindGameObjectsWithTag("Light");
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].GetComponent<Light>().color = Color.Lerp(Color.red, Color.Lerp(Color.green, Color.blue, Mathf.PingPong(Time.time, 1f)), Mathf.PingPong(Time.time, 2f));
        }
    }
}
