using UnityEngine;
using System.Collections;

public class EnemySpawnScript : MonoBehaviour
{
    float spawnCooldown;
    float spawnTimer;

    public GameObject enemy;
    public GameObject player;

    void Start()
    {
        spawnCooldown = 10f;
        spawnTimer = 0f;
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnCooldown && player != null)
        {
            GameObject newEnemy = Instantiate(enemy, transform.position, new Quaternion(0, 0, 0, 0)) as GameObject;
            newEnemy.GetComponent<EnemyStatistics>().player = player;
            spawnTimer = 0f;
        }
    }
}
