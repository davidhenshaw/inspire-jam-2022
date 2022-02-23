using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHolder : MonoBehaviour
{
    GameObject _pickup;

    [Tooltip("The position where the pickup will be placed when interacted with")]
    [SerializeField]Transform _holdPos;


    public bool Place(GameObject obj)
    {
        if (_pickup)
            return false;

        if(obj.TryGetComponent<IPickup>(out IPickup pickup))
        {
            _pickup = obj;
            pickup.PrepareForPickup();
            obj.transform.position = _holdPos.position;
            obj.transform.parent = this.transform;
        }
        return true;
    }

    public GameObject Take()
    {
        if (!_pickup)
            return null;

        GameObject temp = null;
        if (_pickup.TryGetComponent<IPickup>(out IPickup pickup))
        {
            _pickup.transform.parent = null;
            pickup.PrepareForDrop();
            temp = _pickup;
            _pickup = null;
        }

        return temp;
    }
}
