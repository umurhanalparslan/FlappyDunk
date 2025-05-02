using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_MapGenerator : MonoBehaviour
{
    public GameObject player;
    public FP_ObjectPool objectPool; // Havuzdan obstacle cekilecek

    public GameObject obstacle1;
    public GameObject obstacle2;
    public GameObject obstacle3;
    public GameObject obstacle4;

    public float minObstacleY;
    public float maxObstacleY;

    public float minObstacleRotation;
    public float maxObstacleRotation;

    public float obstacleSpacing = 8f; // Obstacle'lar arasi mesafe

    private void Start()
    {
        obstacle1 = GenerateObstacle(player.transform.position.x + 5);
        obstacle2 = GenerateObstacle(obstacle1.transform.position.x);
        obstacle3 = GenerateObstacle(obstacle2.transform.position.x);
        obstacle4 = GenerateObstacle(obstacle3.transform.position.x);
    }

    GameObject GenerateObstacle(float x)
    {
        GameObject obstacle = objectPool.GetPooledObject();
        obstacle.SetActive(true);
        SetTransform(obstacle, x);
        return obstacle;
    }

    void SetTransform(GameObject obstacle, float x)
    {
        obstacle.transform.position = new Vector3(x + obstacleSpacing, Random.Range(minObstacleY, maxObstacleY), obstacle.transform.position.z);
        obstacle.transform.localRotation = Quaternion.Euler(
            obstacle.transform.localRotation.eulerAngles.x,
            Random.Range(minObstacleRotation, maxObstacleRotation),
            obstacle.transform.localRotation.eulerAngles.z
        );
    }

    private void Update()
    {

        if (player.transform.position.x > obstacle2.transform.position.x)
        {
            var tempObstacle = obstacle1;
            obstacle1 = obstacle2;
            obstacle2 = obstacle3;
            obstacle3 = obstacle4;

            SetTransform(tempObstacle, obstacle3.transform.position.x);
            obstacle4 = tempObstacle;
        }
    }
}
