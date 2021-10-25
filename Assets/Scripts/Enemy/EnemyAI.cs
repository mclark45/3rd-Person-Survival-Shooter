using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IDamageable
{
    public enum EnemyState
    {
        Idle,
        Chase,
        Attack
    }

    [SerializeField] private EnemyState _currentState;
    private CharacterController _enemyAI;
    private Player _playerScript;
    private IDamageable _hit;

    public float Health { get; set; }

    [Header("Enemy Stats")]
    [SerializeField] private int _health = 5;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _attackDelay = 2.0f;
    private float _nextAttack = -1;

    [Header("Physics")]
    [SerializeField] private float _gravityValue = 9.81f;
    [SerializeField] private float _gravityMultiplier = 4.5f;

    private float _enemiesYVelocity;

    void Start()
    {
        Health = _health;
        _currentState = EnemyState.Chase;
        _enemyAI = GetComponent<CharacterController>();
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _hit = GameObject.FindGameObjectWithTag("Player").GetComponent<IDamageable>();

        if (_enemyAI == null)
            Debug.LogError("Enemy Script is Null");

        if (_playerScript == null)
            Debug.LogError("Player Script is Null");

        if (_hit == null)
            Debug.LogError("Player IDamagable Interface is Null");
    }

    private void Update()
    {
        Attack();
    }

    private void EnemyMovement()
    {
        Vector3 direction = _playerScript.transform.position - transform.position;
        direction.y = 0f;
        direction.Normalize();
        Vector3 velocity = direction * _speed;
        float _gravity = _gravityValue * _gravityMultiplier * Time.deltaTime;

        if (_enemyAI.isGrounded)
        {
            
        }
        else if (!_enemyAI.isGrounded)
        {
            _enemiesYVelocity -= _gravity;
        }

        velocity.y = _enemiesYVelocity;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), _speed * Time.deltaTime);

        _enemyAI.Move(velocity * Time.deltaTime);
    }

    public void Attack()
    {
        switch (_currentState)
        {
            case EnemyState.Idle:
                break;

            case EnemyState.Chase:
                EnemyMovement();
                break;

            case EnemyState.Attack:
                if (Time.time > _nextAttack)
                {
                    _hit.Damage();
                    _nextAttack = Time.time + _attackDelay;
                }
                break;

            default:
                break;
        }
    }

    public void StartAttack()
    {
        _currentState = EnemyState.Attack;
    }

    public void StopAttack()
    {
        _currentState = EnemyState.Chase;
    }

    public void Damage()
    {
        Health--;

        if (Health == 0)
            Destroy(this.gameObject);
    }
}
