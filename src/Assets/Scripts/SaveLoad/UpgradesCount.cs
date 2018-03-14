using UnityEngine;
using System.Collections;

[System.Serializable]
public class UpgradesCount
{
    public int maxBulletNumber;
    public int reloadTime;
    public int bulletDamage;

    public UpgradesCount()
    {
        maxBulletNumber = 0;
        reloadTime = 0;
        bulletDamage = 0;
    }
}

