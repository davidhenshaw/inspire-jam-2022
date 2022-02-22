using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : PlayerState
{
    bool _done = false;
    public override void Enter(PlayerStateManager ctx)
    {
        ctx.Knockback(ctx.transform.forward * -1, ctx.KnockbackForce);
        ctx.DropItem();
        ctx.StartCoroutine(Stun_co(ctx.StunDuration));
    }

    public override void Exit(PlayerStateManager ctx)
    {
    }

    public override void Update(PlayerStateManager ctx)
    {
        if (_done)
            ctx.SwitchState(ctx.RecoveryState);
    }

    IEnumerator Stun_co(float time)
    {
        yield return new WaitForSeconds(time);
        _done = true;
    }
}
