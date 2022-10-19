using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
public class Rotatable : MonoBehaviour
{
    [Header("Door parameters")]
    
    [SerializeField] private Vector3 openAngle, closeAngle;
    [SerializeField] private float animTime = 0.6f;
    
    private float _timer = 0, _kTime = 1f;
    private Quaternion _closeRot, _openRot;
    
    private InteractableObject _interactableObject;
    
    public bool Opened { get; private set; } = false;

    private void Start()
    {
        _interactableObject = GetComponent<InteractableObject>();
        _closeRot = Quaternion.Euler(closeAngle.x, closeAngle.y, closeAngle.z);
        _openRot = Quaternion.Euler(closeAngle.x + openAngle.x, closeAngle.y+openAngle.y, closeAngle.z + openAngle.z);
    }

    private void LateUpdate()
    {
        if (_interactableObject.hasInteract && !Opened)
        {
            _timer += Time.deltaTime;
            if (_timer > animTime) { _timer = animTime; }
            _kTime = _timer / animTime;
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
            if (_timer > animTime) { _timer = animTime; }
            _kTime = _timer / animTime;
            transform.rotation = Quaternion.Slerp(_openRot,_closeRot,_kTime);
            if (_kTime == 1f)
            {
                Opened = false;
                _timer = 0;
            }
        }
    }
}
