using UnityEngine;
using System.Collections;

public class BladeHitScript : MonoBehaviour
{
    int damage;

    float destroyTimer = 0f;

    float BLADE_FLIGHT_TIME = 1f;

    void Update()
    {
        destroyTimer += Time.deltaTime;
        if (destroyTimer > BLADE_FLIGHT_TIME)
        {
            Destroy(gameObject);
        }
    }

    public void Throw(float angle, float range, int bladeDamage)
    {
        damage = bladeDamage;
        GetComponent<Transform>().eulerAngles = new Vector3(0, 0, angle - 90);
        GetComponent<Rigidbody2D>().velocity = transform.up * range;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyHealth>().ApplyDamage(damage);
            Destroy(gameObject);
        }
        else if (other.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
