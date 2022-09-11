using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField] private float _speed;

    private float _gravity = -9.81f * 2;
    private Vector3 _velocity;
    [Header("Gravity")] public Transform checkSphere;
    public LayerMask groundMask;

    private bool _isGround;
    private float _groundDist = 0.2f;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Проверка на соприкосновение с землей
        _isGround = Physics.CheckSphere(checkSphere.position, _groundDist, groundMask);
        if (_isGround && _velocity.y < 0)
        {
            _velocity.y = -9.81f;
        }

        // Движение персонажа
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        _controller.Move(move * _speed * Time.deltaTime);

        // Симуляция гравитации
        _velocity.y += _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);

    }
}
