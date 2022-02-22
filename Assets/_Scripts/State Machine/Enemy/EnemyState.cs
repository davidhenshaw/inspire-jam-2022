using metakazz.FSM;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : IState<EnemyStateManager>
{
    // Animator parameters
    public const string ANIM_BOOL_RUNNING = "isRunning";
    public const string ANIM_BOOL_JUMPING = "isJumping";
    public const string ANIM_BOOL_FALLING = "isFalling";
    public const string ANIM_BOOL_HURTING = "isHurt";
    public const string ANIM_TRIGGER_DIE = "die";

    public abstract void Enter(EnemyStateManager ctx);

    public abstract void Exit(EnemyStateManager ctx);

    public abstract void Update(EnemyStateManager ctx);
}
