using UnityEngine;
using System.Collections;

public class WeaponInfo : MonoBehaviour
{
    //sounds of handgun
    public AudioClip handgunShotSound;
    public AudioClip handgunReloadSound;

    //sounds of rifle
    public AudioClip rifleShotSound;
    public AudioClip rifleReloadSound;

    //sounds of handgun
    public AudioClip shotgunShotSound;
    public AudioClip shotgunReloadSound;

    //weapons shotlines(raycasts) start points
    public Transform handgunRaycastStartPoint;
    public Transform rifleRaycastStartPoint;
    public Transform shotgunRaycastStartPoint;

    //gun statistic's classes
    public Gun handgun;
    public Gun rifle;
    public Gun shotgun;

    //public BaseAttributtes baseAttributtes;

    public void OnStart()
    {
        #region weapons stats
        handgun = new Gun
        {
            curBulletNumber = 15,
            maxBulletNumber = 15,
            bulletDamage = 10,
            bulletRange = 10,
            dispersion = 0.003f,
            meleeAttackTime = 1.250f,
            shootTime = 0.250f,
            reloadTime = 1.500f,
            meleeAttackAnimationTime = 1.250f,
            shootAnimationTime = 0.250f,
            reloadAnimationTime = 1.250f,
            raycastStartPoint = handgunRaycastStartPoint,
            reloadSound = handgunReloadSound,
            shotSound = handgunShotSound
        };

        rifle = new Gun
        {
            curBulletNumber = 30,
            maxBulletNumber = 30,
            bulletDamage = 8,
            bulletRange = 12,
            dispersion = 0.005f,
            meleeAttackTime = 1.250f,
            shootTime = 0.1f,
            reloadTime = 3.000f,
            meleeAttackAnimationTime = 1.250f,
            shootAnimationTime = 0.250f,
            reloadAnimationTime = 1.667f,
            raycastStartPoint = rifleRaycastStartPoint,
            reloadSound = rifleReloadSound,
            shotSound = rifleShotSound
        };

        shotgun = new Gun
        {
            curBulletNumber = 7,
            maxBulletNumber = 7,
            bulletDamage = 7,
            bulletRange = 5,
            dispersion = 0.03f,
            meleeAttackTime = 1.250f,
            shootTime = 0.5f,
            reloadTime = 5f,
            meleeAttackAnimationTime = 1.250f,
            shootAnimationTime = 0.250f,
            reloadAnimationTime = 1.667f,
            raycastStartPoint = shotgunRaycastStartPoint,
            reloadSound = shotgunReloadSound,
            shotSound = shotgunShotSound
        };
        #endregion
    }
}
