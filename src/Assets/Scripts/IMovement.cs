using UnityEngine;
using System.Collections;

public interface IMovement
{
    void AddSpeedEffect(SpeedEffect effect);

    void RemoveSpeedEffect(string name);
}
