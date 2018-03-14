using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerShootingController : MonoBehaviour
{
    #region declarations
    //transforms for directoning shot line
    public Transform raycastStartPoint;
    public Transform raycastEndPoint;

    public Transform viewFinder;

    public enum SelectedWeapon
    {
        handgun,
        rifle,
        shotgun,
        noWeapon
    }

    public SelectedWeapon selectedWeapon { get; private set; }

    public enum PlayerGunState
    {
        nonAction,
        shoot,
        reload,
        meleeAttack,
    }

    public PlayerGunState playerGunState { get; private set; }


    public Gun gunInHands { get; private set; }

    public bool canMakeActionFlag { get; private set; }
    public Text infoUI;

    //audis sources for storing sounds of selected weapon
    public AudioSource gunShotAudioSource;
    public AudioSource reloadAudioSource;

    //timers used for changing playerGunState according to selected weapon's time stats
    public float meleeAttackTimer { get; private set; }
    public float shootTimer { get; private set; }
    public float reloadTimer { get; private set; }

    //flag used for playing gun sound and raycasting shot according to to selected weapon's shoot time
    bool gunShotFlag;

    //flag used for playing reload sound according to to selected weapon's reload time
    bool reloadFlag;

    //reference to movement player script used for checking is rotation joystick is dragged to start shooting
    PlayerAxisMovement PAM;

    PlayerAnimationController PAC;

    WeaponInfo WI;

    //objects used for raycasting and rendering line during shooting
    RaycastHit2D shootHit;
    int shootableMask;
    public LineRenderer[] shotLine;

    #endregion

    //initializing in Start()
    public void OnStart()
    {
        //set: shooting objects
        shootableMask = LayerMask.GetMask("Shootable");

        //set: flags
        reloadFlag = true;
        gunShotFlag = true;
        canMakeActionFlag = true;

        //set: for actions of player
        playerGunState = PlayerGunState.nonAction;
        selectedWeapon = SelectedWeapon.noWeapon;
        PAM = GetComponent<PlayerAxisMovement>();
        PAC = GetComponent<PlayerAnimationController>();

        WI = GetComponent<WeaponInfo>();

        //set: timers
        meleeAttackTimer = 0f;
        shootTimer = 0f;
        reloadTimer = 0f;
    }

    void Update()
    {
        if (selectedWeapon == SelectedWeapon.noWeapon)
        {
            changeWeapon("rifle");
        }
        updateInfoUI();
        #region setting actions
        if (playerGunState == PlayerGunState.nonAction)
        {
            if (PAM.isRotationDragged == true)
            {
                playerGunState = PlayerGunState.shoot;
            }
            else
            {
                playerGunState = PlayerGunState.nonAction;
            }
            if (gunInHands.curBulletNumber <= 0)
            {
                playerGunState = PlayerGunState.reload;
            }
        }
        #endregion

        //performing acions of player
        switch (playerGunState)
        {
            case PlayerGunState.shoot:
                #region shoot
                if (gunShotFlag == true)
                {
                    gunShotEffects();

                    gunShotFlag = false;
                }
                shootTimer += Time.deltaTime;
                if (shootTimer >= 0.05f)
                {
                    shotLine[0].enabled = false;
                    shotLine[1].enabled = false;
                    shotLine[2].enabled = false;
                    shotLine[3].enabled = false;
                    shotLine[4].enabled = false;
                }
                if (shootTimer >= gunInHands.shootTime)
                {
                    gunShotFlag = true;
                    shootTimer = 0f;
                    playerGunState = PlayerGunState.nonAction;
                }
                #endregion
                break;
            case PlayerGunState.meleeAttack:
                #region melee attack
                meleeAttackTimer += Time.deltaTime;
                if (meleeAttackTimer >= gunInHands.meleeAttackTime)
                {
                    meleeAttackTimer = 0f;
                    playerGunState = PlayerGunState.nonAction;
                }
                #endregion
                break;
            case PlayerGunState.reload:
                #region reload
                if (reloadFlag == true)
                {
                    reloadAudioSource.Play();
                    reloadFlag = false;
                }
                reloadTimer += Time.deltaTime;
                if (reloadTimer >= gunInHands.reloadTime)
                {
                    reloadFlag = true;
                    reloadTimer = 0f;
                    gunInHands.Reload();
                    playerGunState = PlayerGunState.nonAction;
                }
                #endregion
                break;
        }
    }

    public void updateInfoUI()
    {
        infoUI.text = string.Format("Bullets: {0}/{1}",
            gunInHands.curBulletNumber, 
            gunInHands.maxBulletNumber);
    }

    public void changeWeapon(string weapon)
    {
        if (compareWeaponStringAndSelectedWeapon(weapon))
        {
            return;
        }

        if (playerGunState == PlayerGunState.reload)
        {
            abortReloadAction();
        }

        if (weapon == "handgun")
        {
            selectedWeapon = SelectedWeapon.handgun;
            gunInHands = WI.handgun;
            PAC.SwitchWeaponAnimatorController("handgun");
        }
        else if (weapon == "rifle")
        {
            selectedWeapon = SelectedWeapon.rifle;
            gunInHands = WI.rifle;
            PAC.SwitchWeaponAnimatorController("rifle");
        }
        else if (weapon == "shotgun")
        {
            selectedWeapon = SelectedWeapon.shotgun;
            gunInHands = WI.shotgun;
            PAC.SwitchWeaponAnimatorController("shotgun");
        }

        viewFinder.localPosition = gunInHands.raycastStartPoint.localPosition + new Vector3(gunInHands.bulletRange, 0f, 0f);

        gunShotAudioSource.clip = gunInHands.shotSound;

        reloadAudioSource.clip = gunInHands.reloadSound;
    }

    public bool compareWeaponStringAndSelectedWeapon(string weapon)
    {
        if ((weapon == "handgun" && selectedWeapon == SelectedWeapon.handgun)
                || (weapon == "rifle" && selectedWeapon == SelectedWeapon.rifle)
                || (weapon == "shotgun" && selectedWeapon == SelectedWeapon.shotgun))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void abortReloadAction()
    {
        playerGunState = PlayerGunState.nonAction;
        reloadFlag = true;
        reloadTimer = 0f;
    }

    public void gunShotEffects()
    {
        Vector3 direction;
        if (selectedWeapon == SelectedWeapon.shotgun)
        {
            foreach (LineRenderer item in shotLine)
            {
                item.SetPosition(0, gunInHands.raycastStartPoint.position);

                direction = (transform.right.normalized + new Vector3(Random.Range(-gunInHands.bulletRange * gunInHands.dispersion, gunInHands.bulletRange * gunInHands.dispersion), Random.Range(-gunInHands.bulletRange * gunInHands.dispersion, gunInHands.bulletRange * gunInHands.dispersion))).normalized;

                shootHit = Physics2D.Raycast(gunInHands.raycastStartPoint.position, direction, gunInHands.bulletRange, shootableMask);

                if (shootHit)
                {
                    item.SetPosition(1, shootHit.point);
                    EnemyHealth EH = shootHit.collider.gameObject.GetComponent<EnemyHealth>();
                    if (EH != null)
                        EH.ApplyDamage(gunInHands.bulletDamage);
                }
                else
                {
                    raycastEndPoint.position = gunInHands.raycastStartPoint.position + direction * gunInHands.bulletRange;
                    item.SetPosition(1, raycastEndPoint.position);
                }

                item.enabled = true;
            }

        }
        else
        {
            shotLine[0].SetPosition(0, gunInHands.raycastStartPoint.position);

            direction = (transform.right.normalized + new Vector3(Random.Range(-gunInHands.bulletRange * gunInHands.dispersion, gunInHands.bulletRange * gunInHands.dispersion), Random.Range(-gunInHands.bulletRange * gunInHands.dispersion, gunInHands.bulletRange * gunInHands.dispersion))).normalized;

            shootHit = Physics2D.Raycast(gunInHands.raycastStartPoint.position, direction, gunInHands.bulletRange, shootableMask);

            if (shootHit)
            {
                shotLine[0].SetPosition(1, shootHit.point);
                EnemyHealth EH = shootHit.collider.gameObject.GetComponent<EnemyHealth>();
                if (EH != null)
                    EH.ApplyDamage(gunInHands.bulletDamage);
            }
            else
            {
                raycastEndPoint.position = gunInHands.raycastStartPoint.position + direction * gunInHands.bulletRange;
                shotLine[0].SetPosition(1, raycastEndPoint.position);
            }
            shotLine[0].enabled = true;
        }

        gunInHands.curBulletNumber--;
        gunShotAudioSource.Play();
    }
}
