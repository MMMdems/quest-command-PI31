using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
public class Door : MonoBehaviour
{
    private InteractableObject _interactableObject;

    private float _timer = 0, _kTime = 1f;
    [SerializeField] private float _animTime = 0.6f;
    private Quaternion _closeRot, _openRot;
    [SerializeField] private int _openAngle = 90, _closeAngle = -180;

    public bool Opened { get; private set; } = false;

    private void Start()
    {
        _interactableObject = GetComponent<InteractableObject>();
        _closeRot = Quaternion.Euler(0f, _closeAngle, 0f);
        _openRot = Quaternion.Euler(0f, _closeAngle+_openAngle, 0f);
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
