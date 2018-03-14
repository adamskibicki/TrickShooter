using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        }
    }
}
