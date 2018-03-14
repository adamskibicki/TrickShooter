using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Movement : MonoBehaviour, IMovement
{
    protected List<SpeedEffect> speedEffects;

    public float maxSpeed { get; protected set; }

    public Vector2 movementDirection { get; protected set; }

    public abstract void OnStart();

    protected float GetSpeedMultiplier()
    {
        float multiplier = 1f;

        foreach (SpeedEffect item in speedEffects)
        {
            multiplier *= item.value;
        }

        return multiplier;
    }

    public void AddSpeedEffect(SpeedEffect effect)
    {
        for (int i = 0; i < speedEffects.Count; i++)
        {
            if (speedEffects[i].name == effect.name)
            {
                return;
            }
        }
        speedEffects.Add(effect);
    }

    public void RemoveSpeedEffect(string name)
    {
        for (int i = 0; i < speedEffects.Count; i++)
        {
            if (speedEffects[i].name == name)
            {
                speedEffects.RemoveAt(i);
                return;
            }
        }
    }
}
