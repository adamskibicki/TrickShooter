using UnityEngine;
using System.Collections;

public class PlayerAnimationController : MonoBehaviour
{
    PlayerAxisMovement PMV;
    PlayerShootingController PSC;

    public RuntimeAnimatorController handgunAnimator;
    public RuntimeAnimatorController rifleAnimator;
    public RuntimeAnimatorController shotgunAnimator;

    Animator topAnimator;
    Animator footAnimator;

    public void OnStart()
    {
        PMV = GetComponent<PlayerAxisMovement>();
        footAnimator = transform.GetChild(0).GetComponent<Animator>();
        topAnimator = transform.GetChild(1).GetComponent<Animator>();
        PSC = GetComponent<PlayerShootingController>();
    }

    void LateUpdate()
    {
        #region strafing left change foot and move animation
        float angleBetweenPlayerFaceAndMovementDirection = Vector2.Angle(PMV.transform.right, new Vector2(PMV.joystickMovement.Horizontal(), PMV.joystickMovement.Vertical()));

        if ((PMV.joystickMovement.Horizontal() != 0f || PMV.joystickMovement.Vertical() != 0f)
            && angleBetweenPlayerFaceAndMovementDirection > 45 && angleBetweenPlayerFaceAndMovementDirection < 135)
        {
            footAnimator.SetBool("MoveLeftOrRight", true);
            footAnimator.SetBool("IsRun", false);
            footAnimator.SetBool("IsWalk", false);
            topAnimator.SetBool("IsMoving", true);
        }
        else
        {
            footAnimator.SetBool("MoveLeftOrRight", false);

            #region movevement speed animation
            if (PMV.movementDirection.magnitude > 0.5)
            {
                topAnimator.SetBool("IsMoving", true);
                footAnimator.SetBool("IsWalk", true);
                footAnimator.SetBool("IsRun", true);
            }
            else if (PMV.movementDirection.magnitude > 0.1)
            {
                topAnimator.SetBool("IsMoving", true);
                footAnimator.SetBool("IsWalk", true);
            }
            else
            {
                footAnimator.SetBool("IsRun", false);
                footAnimator.SetBool("IsWalk", false);
                topAnimator.SetBool("IsMoving", false);
            }
            #endregion
        }
        #endregion



        #region action animation
        if (PSC.canMakeActionFlag == true)
        {
            switch (PSC.playerGunState)
            {
                case PlayerShootingController.PlayerGunState.nonAction:
                    topAnimator.SetBool("IsShooting", false);
                    topAnimator.SetBool("IsReloading", false);
                    topAnimator.SetBool("IsMeleeAttacking", false);
                    break;
                case PlayerShootingController.PlayerGunState.shoot:
                    topAnimator.SetBool("IsReloading", false);
                    topAnimator.SetBool("IsMeleeAttacking", false);
                    topAnimator.SetBool("IsShooting", true);
                    break;
                case PlayerShootingController.PlayerGunState.reload:
                    topAnimator.SetBool("IsShooting", false);
                    topAnimator.SetBool("IsMeleeAttacking", false);
                    topAnimator.SetBool("IsReloading", true);
                    if (PSC.selectedWeapon == PlayerShootingController.SelectedWeapon.shotgun)
                    {
                        topAnimator.SetFloat("ReloadMultiplier", (PSC.gunInHands.reloadAnimationTime / PSC.gunInHands.reloadTime) * PSC.gunInHands.maxBulletNumber);
                    }
                    else
                    {
                        topAnimator.SetFloat("ReloadMultiplier", PSC.gunInHands.reloadAnimationTime / PSC.gunInHands.reloadTime);
                    }
                    break;
                case PlayerShootingController.PlayerGunState.meleeAttack:
                    topAnimator.SetBool("IsShooting", false);
                    topAnimator.SetBool("IsReloading", false);
                    topAnimator.SetBool("IsMeleeAttacking", true);
                    break;
            }
        }
        else
        {
            topAnimator.SetBool("IsShooting", false);
            topAnimator.SetBool("IsReloading", false);
            topAnimator.SetBool("IsMeleeAttacking", false);
        }
        #endregion
    }

    public void SwitchWeaponAnimatorController(string weapon)
    {
        if (weapon == "handgun")
        {
            topAnimator.runtimeAnimatorController = handgunAnimator;
        }
        else if (weapon == "rifle")
        {
            topAnimator.runtimeAnimatorController = rifleAnimator;
        }
        else if (weapon == "shotgun")
        {
            topAnimator.runtimeAnimatorController = shotgunAnimator;
        }
    }
}
