using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private RectTransform _rectInv;
    [SerializeField] private RectTransform _rectActive;
    
    [SerializeField] private int _currentItem = -1;
    [SerializeField] private int _moveItem = -1;

    [SerializeField] private int _maxSlots = 10;
    [SerializeField] private List<GameObject> slots = new List<GameObject>();
    [SerializeField] private List<Image> icons = new List<Image>();
    [SerializeField] private Sprite defaultIcon;
    [SerializeField] private List<CollectableItem> items = new List<CollectableItem>();

    // Параметры для анимации иконок
    [SerializeField] private float animTime = 0.05f;
    private float _timer = 0, _kTime = 1f;
    private Vector3 _inactivePos, _activePos;
    public bool _inAnim { get; private set; } = false;

    private void Start()
    {
        _rectInv = GetComponent<RectTransform>();
        InventoryRefresh();
    }

    private void Update()
    { 
        GetInputItemSwitch();
        if (_inAnim)
        {
            _timer += Time.deltaTime;
            if (_timer > animTime) { _timer = animTime; }
            _kTime = _timer / animTime;
            slots[_moveItem].transform.localPosition = Vector3.Slerp(_inactivePos, _activePos, _kTime);
            if (_kTime == 1f)
            {
                _inAnim = false;
                _timer = 0;
            }
        }
    }

    private void GetInputItemSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && items.Count > 0)
        {
            if (_currentItem == 0 && items[_currentItem].InHand) { SwitchStateItem(false); }

            else { SwitchStateItem(false); _currentItem = 0; SwitchStateItem(true); }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && items.Count > 1)
        {
            if (_currentItem == 1 && items[_currentItem].InHand) { SwitchStateItem(false); }
            
            else { SwitchStateItem(false); _currentItem = 1; SwitchStateItem(true); }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && items.Count > 2)
        {
            if (_currentItem == 2 && items[_currentItem].InHand) { SwitchStateItem(false); }

            else { SwitchStateItem(false); _currentItem = 2; SwitchStateItem(true); }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && items.Count > 3)
        {
            if (_currentItem == 3 && items[_currentItem].InHand) { SwitchStateItem(false); }
            
            else { SwitchStateItem(false); _currentItem = 3; SwitchStateItem(true); }
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && items.Count > 4)
        {
            if (_currentItem == 4 && items[_currentItem].InHand) { SwitchStateItem(false); }

            else { SwitchStateItem(false); _currentItem = 4; SwitchStateItem(true); }
        }

        if (Input.GetKeyDown(KeyCode.Alpha6) && items.Count > 5)
        {
            if (_currentItem == 5 && items[_currentItem].InHand) { SwitchStateItem(false); }
            
            else { SwitchStateItem(false); _currentItem = 5; SwitchStateItem(true); }
        }
        if (Input.GetKeyDown(KeyCode.Alpha7) && items.Count > 6)
        {
            if (_currentItem == 6 && items[_currentItem].InHand) { SwitchStateItem(false); }

            else { SwitchStateItem(false); _currentItem = 6; SwitchStateItem(true); }
        }

        if (Input.GetKeyDown(KeyCode.Alpha8) && items.Count > 7)
        {
            if (_currentItem == 7 && items[_currentItem].InHand) { SwitchStateItem(false); }
            
            else { SwitchStateItem(false); _currentItem = 7; SwitchStateItem(true); }
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha9) && items.Count > 8)
        {
            if (_currentItem == 8 && items[_currentItem].InHand) { SwitchStateItem(false); }

            else { SwitchStateItem(false); _currentItem = 8; SwitchStateItem(true); }
        }

        if (Input.GetKeyDown(KeyCode.Alpha0) && items.Count > 9)
        {
            if (_currentItem == 9 && items[_currentItem].InHand) { SwitchStateItem(false); }
            
            else { SwitchStateItem(false); _currentItem = 9; SwitchStateItem(true); }
        }
    }
    
    public void AddItem(CollectableItem item)
    {
        if (items.Count < _maxSlots) 
        {
            _currentItem = items.Count;
            items.Add(item);
            SwitchStateItem(true);
            InventoryRefresh();
            
            _inactivePos = slots[_currentItem].transform.localPosition;
            _activePos = new Vector3(slots[_currentItem].transform.localPosition.x, slots[_currentItem].transform.localPosition.y + 100);
            _moveItem = _currentItem;
            _inAnim = true;
            
        }
    }

    public void RemoveItem(CollectableItem item)
    {
        if (items.Count > 0) 
        {
            items.Remove(item);

            _inactivePos = slots[items.Count].transform.localPosition;
            _activePos = new Vector3(slots[items.Count].transform.localPosition.x, slots[items.Count].transform.localPosition.y - 100);
            _inAnim = true;
            _moveItem = items.Count;
            
            if (_currentItem > 0) { _currentItem--; }
            else { _currentItem = 0; }
            InventoryRefresh();
        }
    }

    private void InventoryRefresh()
    {
        int posStep = items.Count - 1;
        _rectInv.sizeDelta = new Vector2(100 + 125 * (posStep), 100);
        _rectInv.anchoredPosition = new Vector2(50 + (posStep) * 65, 50);

        _rectActive.anchoredPosition = new Vector2(50 + (_currentItem) * 120, 50);

        if (items.Count > 0)
        {
            foreach (var icon in icons) { icon.sprite = defaultIcon; }
            for (int i = 0; i < items.Count; i++) { icons[i].sprite = items[i].icon; }

            if (!items[_currentItem].InHand && _currentItem != -1)
            {
                _rectActive.anchoredPosition = new Vector2(50 + (-1) * 120, 50);
            }
        }
        else
        {
            _rectActive.anchoredPosition = new Vector2(50 + (-1) * 120, 50);
        }
    }

    private void SwitchStateItem(bool stateEnabled)
    {
        switch (stateEnabled)
        {
            case true: { items[_currentItem].InHand = true; items[_currentItem].gameObject.SetActive(true); break; }
            case false: { items[_currentItem].InHand = false; items[_currentItem].gameObject.SetActive(false); break; }
        }
        InventoryRefresh();
    }

    public bool CheckSpace() // Проверка, есть ли место в инвентаре
    {
        if (items.Count < _maxSlots && items.Count > 0)
        {
            if (!items[_currentItem].InHand) return true;
            else return false;
        }
        else return true;
    }
}
