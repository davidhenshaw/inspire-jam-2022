using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace metakazz
{
    public class RoomManager : MonoBehaviour
    {
        public CinemachineVirtualCamera Vcam;
        public Transform Target;

        private void Awake()
        {
            Vcam = GetComponentInChildren<CinemachineVirtualCamera>();
        }

        // Start is called before the first frame update
        void Start()
        {
            Vcam.Follow = Target;
            //Vcam.Priority = 10;
        }

        public void SetTarget(Transform target)
        {
            Target = target;
            Vcam.Follow = Target;
        }
    }
}