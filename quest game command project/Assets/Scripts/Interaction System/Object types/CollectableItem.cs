using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(InteractableObject))]
[RequireComponent(typeof(Rigidbody))]
public class CollectableItem : MonoBehaviour
{
    private InteractableObject _interactableObject;

    [Header("Input")] [SerializeField] private InputtingManager inputSetting;
    
    [Header("Inventory parameters")]
    [SerializeField] private Transform itemHolder;
    public Sprite icon;
    private Inventory _inventory;
    private bool _inInv = false;
    
    [Header("Physics parameters")]
    [SerializeField] private float dropForce = 2;
    private Rigidbody _rb;

    [Header("Animation parameters")]
    [SerializeField] private float animTime = 0.3f;
    private float _timer = 0, _kTime = 1f;
    private Vector3 _startPos;

    public bool InHand = false;

    private void Start()
    {
        inputSetting = FindObjectOfType<InputtingManager>();
        _inventory = FindObjectOfType<Inventory>();
        
        _interactableObject = GetComponent<InteractableObject>();
        _rb = GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        // Назначение стартовой позиции для метода Slerp
        if (!InHand && !_interactableObject.hasInteract && _startPos != transform.position) { _startPos = transform.position; }

        if (_interactableObject.hasInteract && !InHand && !_inventory.CheckSpace()) { _interactableObject.hasInteract = false; }
        
        // "Примагничивание" предмета в руку
        if (_interactableObject.hasInteract && !InHand && _inventory.CheckSpace() )
        {
            
            transform.rotation = Quaternion.LookRotation(itemHolder.forward);
            
            SwitchRigidbody(false);
            
            _timer += Time.deltaTime;
            if (_timer > animTime) { _timer = animTime; }
            _kTime = _timer / animTime;
            transform.position = Vector3.Slerp(_startPos, itemHolder.position, _kTime);
            if (_kTime == 1f)
            {
                transform.parent = itemHolder;
                InHand = true;
                _timer = 0;
            }
        }
        
        if (_interactableObject.hasInteract && InHand && !_inInv)
        {
            _inInv = true;
            _inventory.AddItem(this);
        }
        
        //Выброс предмета из рук
        if (Input.GetKeyDown(inputSetting.DropKey) && _interactableObject.hasInteract && InHand && _inInv && !_inventory._inAnim)
        {
            _interactableObject.hasInteract = false;
            _inInv = false;
            _inventory.RemoveItem(this);
        }

        else if (!_interactableObject.hasInteract && InHand)
        {
            SwitchRigidbody(true);
            
            InHand = false;
            transform.parent = null;
            _rb.AddForce(itemHolder.forward * dropForce, ForceMode.Impulse);
        }
        
    }

    private void SwitchRigidbody(bool isEnabled)
    {
        if (isEnabled)
        {
            _rb.constraints = RigidbodyConstraints.None;
            _rb.useGravity = true;
        }
        else if (!isEnabled)
        {
            _rb.useGravity = false;
            _rb.velocity = new Vector3(0, 0, 0);
            _rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
