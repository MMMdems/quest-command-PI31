using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class InteractableObject : MonoBehaviour
{
    public string objectName;
    
    public enum TypeInteract
     {
         Rotatable, Collectable
     }

    public enum TypeObject
    {
        Key, Door, Clue, Dialog, Tool, Puzzle
    }
    
    public TypeInteract typeInteract;
    public TypeObject typeObject;
    
    public Rotatable rotatable;
    public CollectableItem collectable;
    
    public bool hasInteract = false;

    private Outline _outline;
    void Start()
    {
        _outline = GetComponent<Outline>();
        _outline.OutlineWidth = 0;

        if (typeInteract == TypeInteract.Rotatable)
        {
            rotatable = GetComponent<Rotatable>();
        }

        else if (typeInteract == TypeInteract.Collectable)
        {
            collectable = GetComponent<CollectableItem>();
        }
    }

    public void OutlineOn()
    {
        _outline.OutlineWidth = 4;
    }

    public void OutlineOff()
    {
        _outline.OutlineWidth = 0;
    }
}
