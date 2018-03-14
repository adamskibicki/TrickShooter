using UnityEngine;
using System.Collections;

public class Gun
{
    public int curBulletNumber { get; set; }
    public int maxBulletNumber { get; set; }
    public int bulletDamage { get; set; }
    public int bulletRange { get; set; }
    public float dispersion { get; set; }
    public float meleeAttackTime { get; set; }
    public float shootTime { get; set; }
    public float reloadTime { get; set; }
    public float meleeAttackAnimationTime { get; set; }
    public float shootAnimationTime { get; set; }
    public float reloadAnimationTime { get; set; }
    public AudioClip shotSound { get; set; }
    public AudioClip reloadSound { get; set; }
    public Transform raycastStartPoint { get; set; }

    public void Reload()
    {
        curBulletNumber = maxBulletNumber;
    }
}