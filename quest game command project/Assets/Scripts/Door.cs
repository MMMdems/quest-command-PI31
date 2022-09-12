using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
public class Door : MonoBehaviour
{
    public bool _opened = false;
    private InteractableObject _interactableObject;

    private float _timer = 0, _animTime = 0.6f, _kTime = 1f;
    private Quaternion closeRot, openRot;
    

    private void Start()
    {
        _interactableObject = GetComponent<InteractableObject>();
        closeRot = Quaternion.Euler(0f, -180f, 0f);
        openRot = Quaternion.Euler(0f, -90f, 0f);
    }

    private void LateUpdate()
    {
        if (_interactableObject.hasInteract && !_opened)
        {
            _timer += Time.deltaTime;
            if (_timer > _animTime) { _timer = _animTime; }
            _kTime = _timer / _animTime;
            transform.rotation = Quaternion.Slerp(closeRot,openRot,_kTime);
            if (_kTime == 1f)
            {
                _opened = true;
                _timer = 0;
            }
        }
        else if (!_interactableObject.hasInteract && _opened)
        {
            _timer += Time.deltaTime;
            if (_timer > _animTime) { _timer = _animTime; }
            _kTime = _timer / _animTime;
            transform.rotation = Quaternion.Slerp(openRot,closeRot,_kTime);
            if (_kTime == 1f)
            {
                _opened = false;
                _timer = 0;
            }
        }
    }
}
