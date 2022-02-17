using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Collider _pickupCollider;
    GameObject _heldObject;
    [SerializeField] float _pickupDistance;
    [SerializeField] LayerMask _pickupMask;
    [SerializeField] Transform _heldObjectTf;

    // Start is called before the first frame update
    void Start()
    {
        
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
        if (_heldObject)
        {
            HandleDrop();
            return;
        }

        // Do a Boxcast in the direction the player is facing
        var boxSize = new Vector3(1, 1, 1);
        if(Physics.BoxCast(
            transform.position,
            boxSize,
            transform.forward,
            out RaycastHit hitInfo,
            transform.rotation,
            _pickupDistance,
            _pickupMask))
        {
            HandlePickup(hitInfo);
        }
    }

    private void HandlePickup(RaycastHit hitInfo)
    {
        if(hitInfo.collider.TryGetComponent<IPickup>(out IPickup pickup))
        {
            _heldObject = hitInfo.collider.gameObject;
            pickup.PrepareForPickup(); 
        }
            
        _heldObject.transform.position = _heldObjectTf.position;
        _heldObject.transform.SetParent(transform);
    }

    private void HandleDrop()
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
