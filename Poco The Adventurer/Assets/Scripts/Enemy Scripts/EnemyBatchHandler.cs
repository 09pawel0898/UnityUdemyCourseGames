using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBatchHandler : MonoBehaviour
{
    [SerializeField]
    private List<Enemy> enemies;

    private void Start()
    {
        foreach(Transform trans in GetComponentInChildren<Transform>())
        {
            if (trans != this)
                enemies.Add(trans.GetComponent<Enemy>());
        }
    }

    public void EnablePlayerTarget()
    {
        foreach(Enemy characterMovement in enemies)
            characterMovement.HasPlayerTarget = true;
    }

    public void DisablePlayerTarget()
    {
        foreach (Enemy characterMovement in enemies)
            characterMovement.HasPlayerTarget = false;
    }
}
