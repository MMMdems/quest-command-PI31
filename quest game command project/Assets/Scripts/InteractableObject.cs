using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class InteractableObject : MonoBehaviour
{
    public bool hasInteract = false;
    
    public TypeInteract type;

    public Rotatable rotatable;

    public enum TypeInteract
    {
        Rotatable
    }

    private Outline _outline;
    void Start()
    {
        _outline = GetComponent<Outline>();
        _outline.OutlineWidth = 0;

        if (type == TypeInteract.Rotatable)
        {
            rotatable = GetComponent<Rotatable>();
        }
        
    }

    public void OutlineOn()
    {
        _outline.OutlineWidth = 2;
    }
    
    public void OutlineOff()
    {
        _outline.OutlineWidth = 0;
    }
}
