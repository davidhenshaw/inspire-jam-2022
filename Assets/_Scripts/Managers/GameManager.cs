using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    //public GameObject StartingRoom;
    public RoomManager[] _rooms;
    RoomManager _currRoom;

    void Start()
    {
        _currRoom = _rooms[0];
    }

}
