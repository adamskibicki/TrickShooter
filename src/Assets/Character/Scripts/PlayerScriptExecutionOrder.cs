using UnityEngine;
using System.Collections;

public class PlayerScriptExecutionOrder : MonoBehaviour
{
    void Awake()
    {
        gameObject.GetComponent<WeaponInfo>().OnStart();
        gameObject.GetComponent<PlayerAxisMovement>().OnStart();
        gameObject.GetComponent<PlayerShootingController>().OnStart();
        gameObject.GetComponent<PlayerAnimationController>().OnStart();
        gameObject.GetComponent<PlayerHealth>().OnStart();
        gameObject.GetComponent<PlayerAmmoSliderController>().OnStart();
        gameObject.GetComponent<SkillsUsingController>().OnStart();
    }
}
