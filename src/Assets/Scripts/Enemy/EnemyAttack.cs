using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    EnemyStatistics ES;

    int damage;
    float attackCooldown;

    float attackTimer;
    bool canAttack;
    public void OnStart()
    {
        ES = GetComponent<EnemyStatistics>();
        damage = ES.strength;
        attackCooldown = ES.attackSpeed;
        attackTimer = 0f;
        canAttack = false;
    }

    void Update()
    {
        if (ES.playerDead == false && canAttack == false)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackCooldown)
            {
                attackTimer = 0f;
                canAttack = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (canAttack == true && collision2D.collider.tag == "Player")
        {
            collision2D.collider.gameObject.GetComponent<PlayerHealth>().ApplyDamage(damage);
            canAttack = false;
        }
    }

    void OnCollisionStay2D(Collision2D collision2D)
    {
        if (canAttack == true && collision2D.collider.tag == "Player")
        {
            collision2D.collider.gameObject.GetComponent<PlayerHealth>().ApplyDamage(damage);
            canAttack = false;
        }
    }
}
