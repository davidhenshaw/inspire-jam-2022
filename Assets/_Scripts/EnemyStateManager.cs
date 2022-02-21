using metakazz.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour, IStateMachine<EnemyStateManager>
{
    public EnemyState Idle => new EnemyIdle();
    public EnemyState Attack => new EnemyAttack();

    public Transform Target;


    public float WaitTime = 1;
    public float AttackInterval = 1;

    EnemyState _currState;
    ParticleSystem _particles;
    // Start is called before the first frame update
    void Start()
    {
        _particles = GetComponentInChildren<ParticleSystem>();

        _currState = Idle;
        _currState.Enter(this);
    }

    // Update is called once per frame
    void Update()
    {
        _currState.Update(this);
    }

    public void SwitchState(IState<EnemyStateManager> newState)
    {
        _currState.Exit(this);
        newState.Enter(this);

        _currState =  newState as EnemyState;
    }

    public void Fire()
    {
        _particles.Play();
    }
}
