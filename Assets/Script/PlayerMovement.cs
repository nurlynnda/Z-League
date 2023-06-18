using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _controller;
    private Vector3 _playerVelocity;
    private bool _groundedPlayer;

    [SerializeField]
    private float _playerSpeed = 5f;

    [SerializeField]
    private float _rotationSpeed = 10f;

    [SerializeField]
    private Camera _followCamera;

    [SerializeField]
    private float _jumpHeight = 1f;

    [SerializeField]
    private float _boostMultiplier = 2f;

    private float _gravityValue = -20f;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Movement();

        if (Input.GetButtonDown("Jump") && _groundedPlayer)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Boost();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            EndBoost();
        }
    }

    private void Movement()
    {
        _groundedPlayer = _controller.isGrounded;
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementInput = Quaternion.Euler(0, _followCamera.transform.eulerAngles.y, 0) * new Vector3(horizontalInput, 0, verticalInput);
        Vector3 movementDirection = movementInput.normalized;

        float currentSpeed = _playerSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed *= _boostMultiplier;
        }

        _controller.Move(movementDirection * currentSpeed * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, _rotationSpeed * Time.deltaTime);
        }

        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }

    private void Jump()
    {
        _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -2f * _gravityValue);
    }

    private void Boost()
    {
        _playerSpeed *= _boostMultiplier;
    }

    private void EndBoost()
    {
        _playerSpeed /= _boostMultiplier;
    }
}
