using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private int _maxSlots = 8;
    private int _currentItem = 0;
    [SerializeField] private List<GameObject> items = new List<GameObject>();
    [SerializeField] private List<GameObject> slots = new List<GameObject>();

    private void Update()
    {
        
        if (Input.GetKey(KeyCode.Alpha1))
        {
            
        }
        for (int i = 0; i < items.Count; i++)
        {
            items[i].SetActive(false);
        }
    }
}
