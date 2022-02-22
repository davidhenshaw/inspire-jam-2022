using metakazz.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : IState<PlayerStateManager>
{
    public abstract void Enter(PlayerStateManager ctx);

    public abstract void Exit(PlayerStateManager ctx);

    public abstract void Update(PlayerStateManager ctx);

    public virtual void OnParticleCollision(PlayerStateManager ctx, GameObject other)
    {

    }

}
