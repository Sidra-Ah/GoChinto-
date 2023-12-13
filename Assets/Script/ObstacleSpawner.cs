using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameManager gameManager;
    public List<GameObject> ObstaclePrefabs;
    public float centerX, leftX, rightX;
    public float spawnInterval;
    float timeSinceLastSpawn;
    //Queue<GameObject> SpawnedObstacles = new Queue<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.IsGameStarted)
        {
            timeSinceLastSpawn += Time.deltaTime;
            if(timeSinceLastSpawn >= spawnInterval)
            {
                SpawnObstacles();
                timeSinceLastSpawn = 0;
            }
        }
    }
    void SpawnObstacles()
    {
        //if(SpawnedObstacles.Count > 20)
        //{
        //    GameObject.Destroy(SpawnedObstacles.Dequeue());
        //}
        int random = Random.Range(0, ObstaclePrefabs.Count);
        GameObject obstacle = Instantiate(ObstaclePrefabs[random]);
        obstacle.transform.parent = transform;
        Vector3 playerPos = gameManager.player.transform.position;
        obstacle.transform.position = new Vector3(getRandomX(), 0.1f, playerPos.z + 50);
        //SpawnedObstacles.Enqueue(obstacle);
    }
    float getRandomX()
    {
        float[] f = new float[3] { centerX, leftX, rightX };
        return f[Random.Range(0, f.Length)];
    }
}
