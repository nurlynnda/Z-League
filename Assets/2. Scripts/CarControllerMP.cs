using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CarControllerMP : MonoBehaviour
{
    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentBreakForce;
    private bool isBreaking;

    // Settings
    [SerializeField] private float motorForce, breakForce, maxSteerAngle;
    [SerializeField] private float floatingForce = 2f;
    [SerializeField] private float boostForce = 2f;
    [SerializeField] private KeyCode floatKey = KeyCode.F;
    [SerializeField] private KeyCode boostKey = KeyCode.B;

    // Wheel Colliders
    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    // Wheels
    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

    private bool isFloating;
    private bool isBoosting;

    PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (view.IsMine)
        {
            GetInput();
            HandleMotor();
            HandleSteering();
            UpdateWheels();
        }
    }

    private void GetInput()
    {
        // Steering Input
        horizontalInput = Input.GetAxis("Horizontal");

        // Acceleration Input
        verticalInput = Input.GetAxis("Vertical");

        // Breaking Input
        isBreaking = Input.GetKey(KeyCode.Space);

        // Floating Input
        isFloating = Input.GetKey(floatKey);

        // Boost Input
        isBoosting = Input.GetKey(boostKey);
    }

    private void HandleMotor()
    {
        if (isFloating)
        {
            ApplyFloating();
        }
        else
        {
            frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
            frontRightWheelCollider.motorTorque = verticalInput * motorForce;
            currentBreakForce = isBreaking ? breakForce : 0f;
            ApplyBreaking();
        }

        if (isBoosting)
        {
            ApplyBoost();
        }
    }

    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentBreakForce;
        frontLeftWheelCollider.brakeTorque = currentBreakForce;
        rearLeftWheelCollider.brakeTorque = currentBreakForce;
        rearRightWheelCollider.brakeTorque = currentBreakForce;
    }

    private void ApplyFloating()
    {
        Vector3 floatingForceVector = Vector3.up * floatingForce;
        frontLeftWheelCollider.attachedRigidbody.AddForce(floatingForceVector);
        frontRightWheelCollider.attachedRigidbody.AddForce(floatingForceVector);
        rearLeftWheelCollider.attachedRigidbody.AddForce(floatingForceVector);
        rearRightWheelCollider.attachedRigidbody.AddForce(floatingForceVector);
    }

    private void ApplyBoost()
    {
        frontLeftWheelCollider.attachedRigidbody.AddForce(transform.forward * boostForce);
        frontRightWheelCollider.attachedRigidbody.AddForce(transform.forward * boostForce);
        rearLeftWheelCollider.attachedRigidbody.AddForce(transform.forward * boostForce);
        rearRightWheelCollider.attachedRigidbody.AddForce(transform.forward * boostForce);
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}
