using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : EnemyState
{
    public bool _done = false;

    public override void Enter(EnemyStateManager ctx)
    {
       //Debug.Log("Start Idle");
        ctx.StartCoroutine(DoAction_co(ctx));
    }

    public override void Exit(EnemyStateManager ctx)
    {
        //Debug.Log("End Idle");
    }

    public override void Update(EnemyStateManager ctx)
    {
        if (_done)
            ctx.SwitchState(ctx.Attack);
    }

    IEnumerator DoAction_co(EnemyStateManager ctx)
    {
        yield return new WaitForSeconds(ctx.WaitTime);
        _done = true;
    }
}
