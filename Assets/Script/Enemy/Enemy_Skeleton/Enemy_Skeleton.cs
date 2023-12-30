using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Enemy
{
    #region States

    public SkeletonIdeState idleState { get; private set; }
    public SkeletonMoveState moveState { get; private set; }
    
    public SkeletonBattleState battleState { get; private set; }
    
    public SkeletonAttackState attackState { get; private set; }
    
    public SkeletonStunState stunnedState { get; private set; }
    
    public SkeletonDeadState deadState { get; private set; } 
    #endregion
    
    
    
    protected override void Awake()
    {
        base.Awake();

        idleState = new SkeletonIdeState(this, stateMachine, "ide", this);
        moveState = new SkeletonMoveState(this, stateMachine, "move", this);
        battleState = new SkeletonBattleState(this, stateMachine, "move", this);
        attackState = new SkeletonAttackState(this, stateMachine, "attack", this);
        stunnedState = new SkeletonStunState(this, stateMachine, "stun", this);
        deadState = new SkeletonDeadState(this, stateMachine, "die", this);
    }

    protected override void Update()
    {
        base.Update();
        
        if (Input.GetKeyDown(KeyCode.U))
        {
            stateMachine.ChangeState(stunnedState);
        }
       
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            stateMachine.ChangeState(stunnedState);
            return true;
        }

        return false;
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }
}
