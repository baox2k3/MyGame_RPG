using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumState : PlayerState
{
    public PlayerWallJumState(Player _player, PlayerStateMachine _stateMachine, string _animBooName) : base(_player, _stateMachine, _animBooName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = .1f;
        player.SetVelocity(5 * -player.facingDir,player.jumForce);
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
            stateMachine.ChangeState(player.ariState);
        
        if(player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);
    }

    public override void Exit()
    {
        base.Exit();
        
    }
}
