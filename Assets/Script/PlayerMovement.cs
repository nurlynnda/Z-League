using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _controller;

    [SerializeField]
    private float _playerSpeed = 5f;

    [SerializeField]
    private float _rotationSpeed = 10f;

    [SerializeField]
    private float _jumpForce = 5f;

    [SerializeField]
    private float _floatForce = 2f;

    [SerializeField]
    private Camera _followCamera;

    private Vector3 _playerVelocity;
    private bool _groundedPlayer;
    private bool _isBoosting;

    [SerializeField]
    private float _gravityValue = -9.81f;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Movement();
    }

    void Movement()
    {
        _groundedPlayer = _controller.isGrounded;
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        bool jumpInput = Input.GetKeyDown(KeyCode.Z);
        bool boostInput = Input.GetKeyDown(KeyCode.LeftShift);

        Vector3 movementInput = Quaternion.Euler(0, _followCamera.transform.eulerAngles.y, 0) * new Vector3(horizontalInput, 0, verticalInput);
        Vector3 movementDirection = movementInput.normalized;

        if (!_isBoosting)
        {
            _controller.Move(movementDirection * _playerSpeed * Time.deltaTime);
        }
        else
        {
            _controller.Move(movementDirection * _playerSpeed * 2f * Time.deltaTime);
        }

        if (movementDirection != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, _rotationSpeed * Time.deltaTime);
        }

        if (jumpInput && _groundedPlayer)
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpForce * -3.0f * _gravityValue);
        }

        if (boostInput && !_isBoosting)
        {
            StartCoroutine(Boost());
        }

        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }

    IEnumerator Boost()
    {
        _isBoosting = true;
        _playerSpeed *= 2f;
        yield return new WaitForSeconds(3f);
        _playerSpeed /= 2f;
        _isBoosting = false;
    }
}
