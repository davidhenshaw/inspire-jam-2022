using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterController _controller;
    [SerializeField] Collider _pickupCollider;
    GameObject _heldObject;
    [SerializeField] float _pickupDistance;
    [SerializeField] LayerMask _interactableMask;
    [SerializeField] Transform _heldObjectTf;

    [SerializeField] int _hp;


    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            TryPickup();
        }
    }

    private void TryPickup()
    {
        // Do a Boxcast in the direction the player is facing
        var boxSize = _pickupCollider.bounds.size;
        if(Physics.BoxCast(
            transform.position,
            boxSize,
            transform.forward,
            out RaycastHit hitInfo,
            transform.rotation,
            _pickupDistance,
            _interactableMask))
        {
            HandleInteraction(hitInfo);
        }
        else if(_heldObject)
        {
            HandleDrop();
        }
    }

    private void HandleInteraction(RaycastHit hitInfo)
    {
        if (hitInfo.collider.TryGetComponent<PickupHolder>(out PickupHolder holder))
        {
            HandlePlace(holder);
            return;
        }

        if (hitInfo.collider.TryGetComponent<IPickup>(out IPickup pickup))
        {
            HandlePickup(pickup, hitInfo.collider.gameObject);
            return;
        }
    }

    private void HandlePlace(PickupHolder holder)
    {
        if(_heldObject)
        {
            holder.Place(_heldObject);
            _heldObject = null;
        }
        else
        {
            var obj = holder.Take();
            if (!obj)
                return;
            var pickup = _heldObject.GetComponent<IPickup>();
            HandlePickup(pickup, obj);
        }
    }

    private void HandlePickup(IPickup pickup, GameObject obj)
    {
        _heldObject = obj;
        pickup.PrepareForPickup(); 
            
        _heldObject.transform.position = _heldObjectTf.position;
        _heldObject.transform.SetParent(transform);
    }

    public void HandleDrop()
    {
        if (!_heldObject)
            return;

        if (_heldObject.TryGetComponent<IPickup>(out IPickup pickup))
        {
            pickup.PrepareForDrop();
        }
        _heldObject.transform.parent = null;
        _heldObject = null;
    }
}
