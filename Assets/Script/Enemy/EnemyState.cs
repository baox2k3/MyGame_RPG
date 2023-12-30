using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine stateMachine;
    protected Enemy enemyBase;
    protected Rigidbody2D rb;

    private string animBollName;
    
    protected float stateTimer;
    protected bool triggerCalled;

    public EnemyState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName)
    {
        this.enemyBase = _enemyBase;
        this.stateMachine = _stateMachine;
        this.animBollName = _animBoolName;
    }
    

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void Enter()
    {
        triggerCalled = false;
        rb = enemyBase.rb;
        enemyBase.anim.SetBool(animBollName,true);
    }

    public virtual void Exit()
    {
        enemyBase.anim.SetBool(animBollName,false);
        enemyBase.AssignLastAnimName(animBollName);
        
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
