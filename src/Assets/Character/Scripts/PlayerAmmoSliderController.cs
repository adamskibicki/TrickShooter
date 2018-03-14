using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerAmmoSliderController : MonoBehaviour
{
    public Slider ammoSlider;
    PlayerShootingController PSC;

    public void OnStart()
    {
        PSC = GetComponent<PlayerShootingController>();
        ammoSlider.value = 0f;
    }

    void Update()
    {

        try
        {
            if (PSC.playerGunState == PlayerShootingController.PlayerGunState.reload)
            {
                ammoSlider.value = PSC.reloadTimer / PSC.gunInHands.reloadTime * ammoSlider.maxValue;
            }
            else
            {
                ammoSlider.value = (float)((float)PSC.gunInHands.curBulletNumber / (float)PSC.gunInHands.maxBulletNumber) * ammoSlider.maxValue;
            }
        }
        catch (System.NullReferenceException)//PSC.gunInHands - using it above throw this exception at the first Update() invoking
        {
            return;
        }
    }
}
