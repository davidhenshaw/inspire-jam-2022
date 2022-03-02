using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecovery : PlayerState
{
    bool _done;

    public override void Enter(PlayerStateManager ctx)
    {
        ctx.StartCoroutine(Recover_co(ctx.RecoveryDuration));
    }

    public override void Exit(PlayerStateManager ctx)
    {
    }

    public override void Update(PlayerStateManager ctx)
    {
        if (_done)
            ctx.SwitchState(ctx.NeutralState);
    }

    IEnumerator Recover_co(float time)
    {
        yield return new WaitForSeconds(time);
        _done = true;
    }
}
