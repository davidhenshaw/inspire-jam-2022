using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour, IPickup
{
    Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PrepareForPickup()
    {
        _rb.isKinematic = true;
    }

    public void PrepareForDrop()
    {
        _rb.isKinematic = false;
        transform.parent = null;
    }

}

public interface IPickup
{
    void PrepareForPickup();
    void PrepareForDrop();
}
