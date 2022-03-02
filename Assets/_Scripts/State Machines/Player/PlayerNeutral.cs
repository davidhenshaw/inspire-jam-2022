using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNeutral : PlayerState
{
    public override void Enter(PlayerStateManager ctx)
    {
    }

    public override void Exit(PlayerStateManager ctx)
    {
    }

    public override void Update(PlayerStateManager ctx)
    {
    }

    public override void OnParticleCollision(PlayerStateManager ctx, GameObject other)
    {
        base.OnParticleCollision(ctx, other);

        ctx.SwitchState(ctx.HitState);
    }
}
