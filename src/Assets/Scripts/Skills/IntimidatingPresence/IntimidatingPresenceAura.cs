using UnityEngine;
using System.Collections.Generic;

public class IntimidatingPresenceAura : MonoBehaviour
{
    List<GameObject> enemies;

    private float damageIncrease;
    private float slowEffect;

    public void Start()
    {
        enemies = new List<GameObject>();
    }

    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward, 5 * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            enemies.Add(collision.gameObject);
            Movement mv = collision.gameObject.GetComponent<EnemyMovement>();
            if (mv != null)
            {
                mv.AddSpeedEffect(new SpeedEffect("IntimidatingPresenceAura",1f - slowEffect));
            }

            Damage dg = collision.gameObject.GetComponent<EnemyHealth>();
            if (dg != null)
            {
                dg.AddDamageEffect(new DamageEffect("IntimidatingPresenceAura", damageIncrease));
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            enemies.Remove(collision.gameObject);

            IMovement imv = collision.gameObject.GetComponent<EnemyMovement>();
            if (imv != null)
            {
                imv.RemoveSpeedEffect("IntimidatingPresenceAura");
            }

            IDamageable idg = collision.gameObject.GetComponent<EnemyHealth>();
            if (idg != null)
            {
                idg.RemoveDamageEffect("IntimidatingPresenceAura");
            }
        }
    }

    public void OnDestroy()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null)
            {
                continue;
            }
            Movement mv = enemies[i].GetComponent<EnemyMovement>();
            if (mv != null)
            {
                mv.RemoveSpeedEffect("IntimidatingPresenceAura");
            }

            Damage dg = enemies[i].GetComponent<EnemyHealth>();
            if (dg != null)
            {
                dg.RemoveDamageEffect("IntimidatingPresenceAura");
            }
        }
    }

    public void SetSkillData(float damageIncrease,float slowEffect)
    {
        this.damageIncrease = damageIncrease;
        this.slowEffect = slowEffect;
    }
}
