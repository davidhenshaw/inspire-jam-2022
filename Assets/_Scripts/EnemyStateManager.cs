using metakazz.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : StateMachine<EnemyStateManager>
{
    public EnemyState Idle => new EnemyIdle();
    public EnemyState Attack => new EnemyAttack();

    public Transform Target;

    public float WaitTime = 1;
    public float AttackInterval = 1;

    ParticleSystem _particles;
    // Start is called before the first frame update
    void Start()
    {
        _particles = GetComponentInChildren<ParticleSystem>();
        if(!Target)
            Target = GetTarget();
        Init(Idle);
    }

    public void Fire()
    {
        _particles.Play();
    }

    public Transform GetTarget()
    {
        var tf = FindObjectOfType<Player>().transform;
        return tf;
    }
}
