using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.ProceduralImage;
using DG.Tweening;

public class UISwitchToggle : MonoBehaviour
{   
    private Toggle _toggle;
    
    [SerializeField] private RectTransform handleRectTransform;
    private Vector2 _handlePos;

    [SerializeField] private ProceduralImage backgroundImage, handleImage;
    private Color _backgroundDefaultColor, _handleDefaultColor;
    [SerializeField] private Color backgroundActiveColor, handleActiveColor;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
        _handlePos = handleRectTransform.anchoredPosition;

        _backgroundDefaultColor = backgroundImage.color;
        _handleDefaultColor = handleImage.color;
        
        _toggle.onValueChanged.AddListener(Switch);

        if (_toggle.isOn) { Switch(true); }
        
    }

    private void Switch(bool active)
    {
        handleRectTransform.DOAnchorPos(active ? _handlePos * (-1) : _handlePos, .4f).SetEase(Ease.InOutBack);
        backgroundImage.DOColor(active ? backgroundActiveColor : _backgroundDefaultColor, .6f);
        handleImage.DOColor(active ? handleActiveColor : _handleDefaultColor, .6f);
    }

    private void OnDestroy()
    {
        _toggle.onValueChanged.RemoveListener(Switch);
    }
}
