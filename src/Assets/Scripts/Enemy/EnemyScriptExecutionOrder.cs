using UnityEngine;
using System.Collections;

public class EnemyScriptExecutionOrder : MonoBehaviour
{
    void Awake()
    {
        GetComponent<EnemyStatistics>().OnStart();
        GetComponent<EnemyAttack>().OnStart();
        GetComponent<EnemyHealth>().OnStart();
        GetComponent<EnemyMovement>().OnStart();
    }
}
