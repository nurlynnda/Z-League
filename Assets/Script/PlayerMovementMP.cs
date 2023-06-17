using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerMovementMP : MonoBehaviour
{
    private CharacterController _controller;
    PhotonView view;

    [SerializeField]
    private float _playerSpeed = 5f;

    [SerializeField]
    private float _rotationSpeed = 10f;

    private Vector3 _playerVelocity;
    private bool _groundedPlayer;

    [SerializeField]
    private float _gravityValue = -9.81f;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if(view.IsMine)
        {
            _groundedPlayer = _controller.isGrounded;
            if (_groundedPlayer && _playerVelocity.y < 0)
            {
                _playerVelocity.y = 0f;
            }

            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 movementInput = new Vector3(-verticalInput, 0, horizontalInput);
            Vector3 movementDirection = movementInput.normalized;

            _controller.Move(movementDirection * _playerSpeed * Time.deltaTime);

            if (movementDirection != Vector3.zero)
            {
                Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

                transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, _rotationSpeed * Time.deltaTime);
            }

            _playerVelocity.y += _gravityValue * Time.deltaTime;
            _controller.Move(_playerVelocity * Time.deltaTime);
        }
        
    }

}
