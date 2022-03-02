using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RoomManager : MonoBehaviour
{
    public CinemachineVirtualCamera Vcam;

    private void Awake()
    {
        Vcam = GetComponentInChildren<CinemachineVirtualCamera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Vcam.Priority = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

