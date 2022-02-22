using metakazz.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager :  StateMachine<PlayerStateManager>
{
    public PlayerState HitState => new PlayerHit();
    public PlayerState NeutralState => new PlayerNeutral();
    public PlayerState RecoveryState => new PlayerRecovery();

    public float StunDuration = 1;
    public float RecoveryDuration = 2.5f;

    public float KnockbackForce = 10;

    CharacterController _controller;
    CharacterMovement _mover;
    Player _player;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _mover = GetComponent<CharacterMovement>();
        _player = GetComponent<Player>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Init(NeutralState);
    }

    public void Knockback(Vector3 dir, float force)
    {
        _mover.Knockback(dir, force);
    }

    public void DropItem()
    {
        _player.HandleDrop();
    }

    public override void Update()
    {
        base.Update();
    }

    private void OnParticleCollision(GameObject other)
    {
        var playerState = _currState as PlayerState;
        playerState.OnParticleCollision(this, other);
    }
}
