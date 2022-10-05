using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
public class Rotatable : MonoBehaviour
{
    [Header("Door parameters")]
    
    [SerializeField] private Vector3 _openAngle, _closeAngle;
    [SerializeField] private float _animTime = 0.6f;
    
    private float _timer = 0, _kTime = 1f;
    private Quaternion _closeRot, _openRot;
    
    private InteractableObject _interactableObject;
    
    
    public bool Opened { get; private set; } = false;

    private void Start()
    {
        _interactableObject = GetComponent<InteractableObject>();
        _closeRot = Quaternion.Euler(_closeAngle.x, _closeAngle.y, _closeAngle.z);
        _openRot = Quaternion.Euler(_closeAngle.x + _openAngle.x, _closeAngle.y+_openAngle.y, _closeAngle.z + _openAngle.z);
    }

    private void LateUpdate()
    {
        if (_interactableObject.hasInteract && !Opened)
        {
            _timer += Time.deltaTime;
            if (_timer > _animTime) { _timer = _animTime; }
            _kTime = _timer / _animTime;
            transform.rotation = Quaternion.Slerp(_closeRot,_openRot,_kTime);
            if (_kTime == 1f)
            {
                Opened = true;
                _timer = 0;
            }
        }
        else if (!_interactableObject.hasInteract && Opened)
        {
            _timer += Time.deltaTime;
            if (_timer > _animTime) { _timer = _animTime; }
            _kTime = _timer / _animTime;
            transform.rotation = Quaternion.Slerp(_openRot,_closeRot,_kTime);
            if (_kTime == 1f)
            {
                Opened = false;
                _timer = 0;
            }
        }
    }
}
