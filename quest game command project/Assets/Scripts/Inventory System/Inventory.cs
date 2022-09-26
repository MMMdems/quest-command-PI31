using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private int _maxSlots = 10;
    private int _currentItem = 0;
    [SerializeField] private List<CollectableItem> items = new List<CollectableItem>();

    private void Update()
    {
        
    }
}
