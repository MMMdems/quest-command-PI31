using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class InteractableObject : MonoBehaviour
{
    public bool hasInteract = false;

    public TypeInteract type;

    public Rotatable rotatable;
    public CollectableItem collectable;

    public enum TypeInteract
    {
        Rotatable, Collectable
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

        if (type == TypeInteract.Collectable)
        {
            collectable = GetComponent<CollectableItem>();
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
