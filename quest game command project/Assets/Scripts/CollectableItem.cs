using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
[RequireComponent(typeof(Rigidbody))]
public class CollectableItem : MonoBehaviour
{
    private Inventory _inventory;

    private InteractableObject _interactableObject;
    private Rigidbody _rb;

    [SerializeField] private float animTime = 0.3f;
    [SerializeField] private Transform hand;

    private float _timer = 0, _kTime = 1f;
    private Vector3 startPos;

    public bool InHand { get; private set; } = false;

    private void Start()
    {
        _inventory = FindObjectOfType<Inventory>();
        _interactableObject = GetComponent<InteractableObject>();
        _rb = GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        // Ќазначение стартовой позиции дл€ метода Slerp
        if (!InHand && !_interactableObject.hasInteract && startPos != transform.position) { startPos = transform.position; }
        
        
        if (Input.GetKeyDown(KeyCode.E) && _interactableObject.hasInteract && InHand)
        {
            _interactableObject.hasInteract = false;
        }

        

        if (_interactableObject.hasInteract && !InHand)
        {
            transform.rotation = Quaternion.LookRotation(hand.forward);
            
            _rb.useGravity = false;
            _rb.velocity = new Vector3(0, 0, 0);
            _rb.constraints = RigidbodyConstraints.FreezeAll;
            
            _timer += Time.deltaTime;
            if (_timer > animTime) { _timer = animTime; }
            _kTime = _timer / animTime;
            transform.position = Vector3.Slerp(startPos, hand.position, _kTime);
            if (_kTime == 1f)
            {
                transform.parent = hand;
                InHand = true;
                _timer = 0;
            }
        }
        else if (!_interactableObject.hasInteract && InHand)
        {
            _rb.constraints = RigidbodyConstraints.None;
            _rb.useGravity = true;
            
            transform.parent = null;
            _rb.AddForce(hand.forward*2, ForceMode.Impulse);
            InHand = false;
        }
        
    }
}
