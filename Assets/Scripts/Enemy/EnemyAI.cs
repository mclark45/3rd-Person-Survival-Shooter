using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IDamageable
{
    private CharacterController _enemyAI;
    private Player _playerScript;

    public float Health { get; set; }

    [Header("Enemy Stats")]
    [SerializeField] private int _health = 5;
    [SerializeField] private float _speed = 5f;

    [Header("Physics")]
    [SerializeField] private float _gravityValue = 9.81f;
    [SerializeField] private float _gravityMultiplier = 4.5f;

    private float _enemiesYVelocity;

    void Start()
    {
        Health = _health;
        _enemyAI = GetComponent<CharacterController>();
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (_enemyAI == null)
            Debug.LogError("Enemy Script is Null");

        if (_playerScript == null)
            Debug.LogError("Player Script is Null");
    }

    private void Update()
    {
        EnemyMovement();
    }


    private void EnemyMovement()
    {
        Vector3 direction = _playerScript.transform.position - transform.position;
        Vector3 velocity = direction * _speed;
        float _gravity = _gravityValue * _gravityMultiplier * Time.deltaTime;

        if (_enemyAI.isGrounded)
        {

        }
        else
        {
            _enemiesYVelocity -= _gravity;
        }

        velocity.y = _enemiesYVelocity;

        velocity.Normalize();

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(velocity), _speed * Time.deltaTime);

        _enemyAI.Move(velocity * Time.deltaTime);
    }

    public void Damage()
    {
        Debug.Log("Enemy Hit");
    }
}
