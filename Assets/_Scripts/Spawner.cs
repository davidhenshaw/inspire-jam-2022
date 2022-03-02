using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject _prefab;
    [Tooltip("The maximum number of times this spawner can spawn when triggered.\nSubsequent triggers will do nothing.")]
    public int MaxSpawns = 1;
    int _numSpawns = 0;

    public void Trigger()
    {
        if (_numSpawns >= MaxSpawns)
            return;

        Instantiate(_prefab, transform.position, Quaternion.identity);
        _numSpawns++;
    }
}
