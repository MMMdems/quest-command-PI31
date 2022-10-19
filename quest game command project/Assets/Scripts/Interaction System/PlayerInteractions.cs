using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractions : MonoBehaviour
{
    private InteractableObject _interactable;
    private InteractableObject _prevInteractable;
    private RaycastHit _hit;
    
    
    [SerializeField] private GameObject objectInfo;
    [SerializeField] private Text textInfoObject;
    [SerializeField] private Image iconInfoObject;
    
    [SerializeField] private List<Sprite> objectTypeIcons = new List<Sprite>();
    [SerializeField] private int currentIndexIcon;

    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private InputtingManager inputSetting;

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
                DisablePrevOutline();
                CheckObjectIcon();
                EnableCurrentOutline();
            }

            CheckTypeInteractable();
            
        }
        else if (_prevInteractable != null) { DisablePrevOutline(); }
    }

    private void CheckTypeInteractable()
    {
        if (_interactable.typeInteract == InteractableObject.TypeInteract.Rotatable)
        {
            if (Input.GetKeyDown(inputSetting.InteractKey))
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

        else if (_interactable.typeInteract == InteractableObject.TypeInteract.Collectable)
        {
            if (Input.GetKeyDown(inputSetting.InteractKey))
            {
                if (!_interactable.hasInteract && !_interactable.collectable.InHand)
                {
                    _interactable.hasInteract = true;
                }
            }
        }
    }

    private void CheckObjectIcon()
    {
        // 0 - Ключ, 1 - Головоломка, 2 - Подсказка, 3 - Инструмент, 
        switch (_interactable.typeObject)
        {
            case InteractableObject.TypeObject.Key: { currentIndexIcon = 0; break; }
            case InteractableObject.TypeObject.Puzzle: { currentIndexIcon = 1; break; }
            case InteractableObject.TypeObject.Clue: { currentIndexIcon = 2; break; }
            case InteractableObject.TypeObject.Tool: { currentIndexIcon = 3; break; }
        }
    }
    
    private void EnableCurrentOutline()
    {
        print("outlined " + _interactable.name);
        objectInfo.SetActive(true);
        textInfoObject.text = _interactable.objectName;
        iconInfoObject.sprite = objectTypeIcons[currentIndexIcon];
        
        _interactable.OutlineOn();
        _prevInteractable = _interactable;
    }

    private void DisablePrevOutline()
    {
        print("disoutlined " + _prevInteractable);
        objectInfo.SetActive(false);
        textInfoObject.text = "";
        iconInfoObject.sprite = null;

        if (_prevInteractable != null) _prevInteractable.OutlineOff();
        _prevInteractable = null;
    }
}
