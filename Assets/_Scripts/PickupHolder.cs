using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PickupHolder : MonoBehaviour
{
    GameObject _pickup;

    [Tooltip("The position where the pickup will be placed when interacted with")]
    [SerializeField]Transform _holdPos;

    public UnityEvent OnPlace;
    public UnityEvent OnPickup;


    public bool Place(GameObject obj)
    {
        if (_pickup)
            return false;

        if(obj.TryGetComponent<IPickup>(out IPickup pickup))
        {
            _pickup = obj;
            pickup.PrepareForPickup();
            obj.transform.SetPositionAndRotation(_holdPos.position, _holdPos.rotation);
            obj.transform.parent = this.transform;
        }

        OnPlace?.Invoke();
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
        OnPickup?.Invoke();
        return temp;
    }
}
