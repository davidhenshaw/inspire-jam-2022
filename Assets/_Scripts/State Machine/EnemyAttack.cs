using metakazz.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : EnemyState
{
    Transform _target;
    bool _done = true;

    public override void Enter(EnemyStateManager ctx)
    {
        Debug.Log("Start Attack");
        _target = ctx.Target;
    }

    public override void Exit(EnemyStateManager ctx)
    {
        Debug.Log("End Attack");
    }

    public override void Update(EnemyStateManager ctx)
    {
        if (!_target)
            return;
        var finalRot = Quaternion.LookRotation(_target.position - ctx.transform.position, Vector3.up);
        ctx.transform.rotation = Quaternion.RotateTowards(ctx.transform.rotation, finalRot, 0.5f);

        if(_done)
            ctx.StartCoroutine(DoAction_co(ctx));
    }

    IEnumerator DoAction_co(EnemyStateManager ctx)
    {
        _done = false;
        yield return new WaitForSeconds(ctx.AttackInterval);
        ctx.Fire();
        _done = true;
    }
}
