using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace metakazz
{
    public class GameManager : PersistentSingleton<GameManager>
    {
        //public GameObject StartingRoom;
        public RoomManager[] _rooms;
        public RoomManager CurrentRoom;

        public Player Player;

        protected override void Awake()
        {
            base.Awake();
            CurrentRoom = _rooms[0];
            CurrentRoom.Target = Player.transform;
        }

        void Start()
        {
            RoomTransition.Transitioned += OnRoomTransition;
        }

        private void OnRoomTransition(RoomManager toRoom)
        {
            CurrentRoom = toRoom;
            CurrentRoom.Target = Player.transform;
        }
    }
}
