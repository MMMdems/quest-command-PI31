using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class InteractableObject : MonoBehaviour
{
    public bool hasInteract = false;

    public TypeInteract type;
    
    public enum TypeInteract
    {
        Door, Lever
    }
    
    private Outline _outline;
    void Start()
    {
        _outline = GetComponent<Outline>();
        _outline.OutlineWidth = 0;
    }

    public void SwitchOutline()
    {
        switch (_outline.OutlineWidth)
        {
            case 0: { _outline.OutlineWidth = 2; break; }
            case 2: { _outline.OutlineWidth = 0; break; }
        }
    }
}
