using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private InteractableObject _interactable;
    private InteractableObject _prevInteractable;
    private RaycastHit _hit;
    
    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private KeyCode interactKey;

    private void Update()
    {
        Interaction();
    }

    private void Interaction()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        
        if (Physics.Raycast(ray, out _hit, interactDistance) && _hit.collider.TryGetComponent(out _interactable))
        {
            
            if (_interactable != _prevInteractable)
            {
                print("outlined " + _interactable.name);
                _interactable.OutlineOn();
                _prevInteractable = _interactable;
            }
            
            if (_interactable.type == InteractableObject.TypeInteract.Rotatable)
            {
                if (Input.GetKeyDown(interactKey))
                {
                    if (_interactable.hasInteract && _interactable.rotatable.Opened)
                    {
                        _interactable.hasInteract = false;
                    }
                    else if (!_interactable.hasInteract && !_interactable.rotatable.Opened)
                    {
                        _interactable.hasInteract = true;
                    }
                }
            }

            if (_interactable.type == InteractableObject.TypeInteract.Collectable)
            {
                if (Input.GetKeyDown(interactKey))
                {
                    if (!_interactable.hasInteract && !_interactable.collectable.InHand)
                    {
                        _interactable.hasInteract = true;
                    }
                }
            }
        }
        else if (_prevInteractable != null) { DisablePrevOutline(); }
    }
    
    private void DisablePrevOutline()
    {
        print("disoutlined " + _prevInteractable);
        _prevInteractable.OutlineOff();
        _prevInteractable = null;
    }
}
