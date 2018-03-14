using UnityEngine;
using System.Collections;

public class EnemyStatistics : MonoBehaviour
{
    public int level { get; private set; }
    public int toughness { get; private set; }
    public int strength { get; private set; }
    public float speed { get; private set; }
    public float attackSpeed { get; private set; }

    public bool playerDead { get; private set; }
    public GameObject player { get; set; }

    public void OnStart()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        level = (int)(Random.value * 10 + 1);
        toughness = 10 + level;
        strength = 10 + level;
        speed = 1.5f + (float)level / 10;
        attackSpeed = 2f - (float)level / 50;
        playerDead = false;
    }

    void FixedUpdate()
    {
        if (player == null)
        {
            playerDead = true;
        }
    }
}
