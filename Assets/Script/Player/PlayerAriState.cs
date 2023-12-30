using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAriState : PlayerState
{
    public PlayerAriState(Player _player, PlayerStateMachine _stateMachine, string _animBooName) : base(_player, _stateMachine, _animBooName)
    {
    }


    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        
        if(player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlide);
        
        if(player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);
        
        if(xInput != 0)
            player.SetVelocity(player.moveSpeed * .8f * xInput, rb.velocity.y);
            
    }

    public override void Exit()
    {
        base.Exit();
        
    }

}
