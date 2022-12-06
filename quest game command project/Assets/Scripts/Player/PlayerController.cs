using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")] 
    
    [SerializeField] private float walkSpeed;
    [SerializeField] private float gravity = -9.81f;

    private float _horizontalMove;
    private float _verticalMove;
    private Vector3 _moveDirection;
    private Vector3 _velocity;
    private CharacterController _controller;
    
    [Header("Mouse")] 
    [SerializeField] private Camera cam;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float mouseClamp;

    private float _verticalRotation;
    private float _yAxis;
    private float _xAxis;

    [Header("Menu parameters")]
    [SerializeField] private MenuSystem menuManager;


    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!menuManager.isPause)
        {
            Movement();
            MouseLook();
        }
    }

    private void Movement()
    {
        if (_controller.isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
        
        _horizontalMove = Input.GetAxis("Horizontal");
        _verticalMove = Input.GetAxis("Vertical");

        _moveDirection = transform.forward * _verticalMove + transform.right * _horizontalMove;

        _controller.Move(_moveDirection * walkSpeed * Time.deltaTime);

        _velocity.y += gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }

    private void MouseLook()
    {
        _xAxis = Input.GetAxis("Mouse X");
        _yAxis = Input.GetAxis("Mouse Y");

        _verticalRotation += -_yAxis * mouseSensitivity;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -mouseClamp, mouseClamp);
        cam.transform.localRotation = Quaternion.Euler(_verticalRotation,0,0);
        transform.rotation *= Quaternion.Euler(0, _xAxis * mouseSensitivity, 0);
    }
    
}
