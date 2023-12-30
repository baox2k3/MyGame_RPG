using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundState    
{
    public PlayerMoveState( Player _player,PlayerStateMachine _stateMachine, string _animBooName) : base( _player,_stateMachine, _animBooName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(xInput * player.moveSpeed,rb.velocity.y);
        
        if (xInput == 0 || player.IsWallDetected())
          stateMachine.ChangeState(player.idleState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
