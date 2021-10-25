using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private EnemyAI _enemy;

    void Start()
    {
        _enemy = GetComponentInParent<EnemyAI>();

        if (_enemy == null)
            Debug.LogError("Enemy AI Script is Null");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            _enemy.StartAttack();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            _enemy.StopAttack();
    }
}
