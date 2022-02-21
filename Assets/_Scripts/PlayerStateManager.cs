using metakazz.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour, IStateMachine<PlayerStateManager>
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }    
    
    public void SwitchState(IState<PlayerStateManager> newState)
    {
        throw new System.NotImplementedException();
    }

}
