using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    float _totalReceptacles;
    float _numPoweredReceptacles = 0;

    void Start()
    {
        
    }

    void GetReceptacles()
    {
        var receptacles = FindObjectsOfType<PickupHolder>();
        

    }

    void DisposeResources()
    {
        
    }
}
