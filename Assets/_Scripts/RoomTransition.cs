using System.Collections;
using System;
using UnityEngine;

namespace metakazz
{
    public class RoomTransition : MonoBehaviour
    {
        public static event Action<RoomManager> Transitioned;

        public RoomManager RoomA;
        public RoomManager RoomB;


        public void Transition(RoomManager fromRoom)
        {
            if (!Contains(fromRoom))
            {
                Debug.LogWarning("This RoomTransition does not contain " + fromRoom.ToString());
                return;
            }

            var toRoom = fromRoom.Equals(RoomA) ? RoomB : RoomA;

            fromRoom.Vcam.Priority = 0;
            toRoom.Vcam.Priority = 10;
            toRoom.SetTarget(fromRoom.Target);

            Transitioned.Invoke(toRoom);
        }

        bool Contains(RoomManager room)
        {
            return room.Equals(RoomA) || room.Equals(RoomB);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.TryGetComponent(out Player player))
            {
                Transition(GameManager.Instance.CurrentRoom);
            }
        }
    }
}
