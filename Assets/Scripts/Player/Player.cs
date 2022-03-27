using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    private CharacterController _playerController;
    [Header("Movement")]
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _jumpHeight = 12.0f;

    [Header("Physics")]
    [SerializeField] private float _gravityValue = 9.81f;
    [SerializeField] private float _gravityMultiplier = 4.5f;

    private Camera _playerCamera;
    [Header("Camera Sensitivity")]
    [Range(0.1f, 10.0f)] [SerializeField] private float _mouseXSensitivity;
    [Range(0.1f, 10.0f)] [SerializeField] private float _mouseYSensitivity;
    
    private float _playersYVelocity;

    private int _health = 5;
    public float Health { get; set; }

    private void Awake()
    {
        Health = _health;

        _playerController = GetComponent<CharacterController>();
        _playerCamera = Camera.main;

        if (_playerController == null)
            Debug.LogError("Player Character Controller is Null");

        if (_playerCamera == null)
            Debug.LogError("Main Camera is Null");
    }

    void Update()
    {
        if (Time.timeScale != 0)
        {
            CameraControls();
            CursorLock();
            PlayerMovement();
        }
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

        //transforms the local space velocity to world space
        velocity = transform.TransformDirection(velocity);

        _playerController.Move(velocity * Time.deltaTime);
    }

    private void CameraControls()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.y += mouseX * _mouseXSensitivity;
        transform.localRotation = Quaternion.AngleAxis(currentRotation.y, Vector3.up);

        Vector3 currentCameraRotation = _playerCamera.gameObject.transform.localEulerAngles;
        currentCameraRotation.x -= mouseY * _mouseYSensitivity;
        //currentCameraRotation.x = Mathf.Clamp(currentCameraRotation.x, 0f, 25f);
        //_playerCamera.gameObject.transform.localRotation = Quaternion.AngleAxis(Mathf.Clamp(currentCameraRotation.x, 0f, 25f), Vector3.right);
    }

    private void CursorLock()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if (Input.GetKeyDown(KeyCode.Escape))
            Cursor.lockState = CursorLockMode.None;
    }

    public void Damage()
    {
        Health--;

        Debug.Log("Player Health: " + Health);
        if (Health == 0)
        {
            Time.timeScale = 0;
            Debug.Log("Player is Dead!!! GAME OVER!!!");
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
