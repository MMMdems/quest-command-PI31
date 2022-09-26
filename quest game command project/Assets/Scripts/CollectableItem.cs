using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
[RequireComponent(typeof(Rigidbody))]
public class CollectableItem : MonoBehaviour
{
    public Sprite icon;
    private Inventory _inventory;
    
    private InteractableObject _interactableObject;
    private Rigidbody _rb;
    
    [SerializeField] private float animTime = 0.3f;
    [SerializeField] private Transform hand;
    [SerializeField] private float dropForce = 2;

    private float _timer = 0, _kTime = 1f;
    private Vector3 _startPos;

    public bool InHand { get; private set; } = false;

    private void Start()
    {
        _inventory = FindObjectOfType<Inventory>();
        _interactableObject = GetComponent<InteractableObject>();
        _rb = GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        // Назначение стартовой позиции для метода Slerp
        if (!InHand && !_interactableObject.hasInteract && _startPos != transform.position) { _startPos = transform.position; }
        
        // "Примагничивание" предмета в руку
        if (_interactableObject.hasInteract && !InHand)
        {
            transform.rotation = Quaternion.LookRotation(hand.forward);
            
            _rb.useGravity = false;
            _rb.velocity = new Vector3(0, 0, 0);
            _rb.constraints = RigidbodyConstraints.FreezeAll;
            
            _timer += Time.deltaTime;
            if (_timer > animTime) { _timer = animTime; }
            _kTime = _timer / animTime;
            transform.position = Vector3.Slerp(_startPos, hand.position, _kTime);
            if (_kTime == 1f)
            {
                transform.parent = hand;
                InHand = true;
                _timer = 0;
            }
        }
        
        //Выброс предмета из рук
        if (Input.GetKeyDown(KeyCode.E) && _interactableObject.hasInteract && InHand)
        {
            _interactableObject.hasInteract = false;
        }
        
        else if (!_interactableObject.hasInteract && InHand)
        {
            _rb.constraints = RigidbodyConstraints.None;
            _rb.useGravity = true;
            
            transform.parent = null;
            _rb.AddForce(hand.forward * dropForce, ForceMode.Impulse);
            InHand = false;
        }
        
    }
}
