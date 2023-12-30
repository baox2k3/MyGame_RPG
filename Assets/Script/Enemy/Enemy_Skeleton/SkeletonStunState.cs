using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStunState : EnemyState
{
    private Enemy_Skeleton enemy;
    public SkeletonStunState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,Enemy_Skeleton enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        
        enemy.fx.InvokeRepeating("RedColorBlink",0,.1f);
        
        
        
        stateTimer = 1;
        rb.velocity = new Vector2(-enemy.facingDir * enemy.stundirection.x,enemy.stundirection.y);
    }
    
    
    public override void Exit()
    {
        base.Exit();
        enemy.fx.Invoke("CancelColorChange",0);
    }
    
    
    public override void Update()
    {
        base.Update();
        if(stateTimer < 0 )
            stateMachine.ChangeState(enemy.idleState);
    }
}
