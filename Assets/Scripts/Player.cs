using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _jumpHeight = 12.0f;

    [Header("Physics")]
    [SerializeField] private float _gravityValue = 9.81f;
    [SerializeField] private float _gravityMultiplier = 4.5f;

    private CharacterController _playerController;
    private float _playersYVelocity;


    void Start()
    {
        _playerController = GetComponent<CharacterController>();

        if (_playerController == null)
            Debug.LogError("Player Character Controller is Null");
    }

    void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");
        float _gravity = _gravityValue * _gravityMultiplier * Time.deltaTime;

        Vector3 direction = new Vector3(horizontalMovement, 0, verticalMovement);
        Vector3 velocity = direction * _speed;

        if (_playerController.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _playersYVelocity = _jumpHeight;
        }
        else
        {
            _playersYVelocity -= _gravity;
        }


        velocity.y = _playersYVelocity;
        _playerController.Move(velocity * Time.deltaTime);
    }
}
