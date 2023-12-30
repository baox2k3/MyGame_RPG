using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdieState : PlayerGroundState
{
    public PlayerIdieState(Player _player, PlayerStateMachine _stateMachine, string _animBooName) : base(_player, _stateMachine, _animBooName)
    {
    }


    public override void Enter()
    {
        base.Enter();
        player.SetZeroVeloccity();
    }

    public override void Update()
    {
        base.Update();

        if(xInput == player.facingDir && player.IsWallDetected())
            return;
        
        if (xInput != 0 && !player.isBusy)
            stateMachine.ChangeState(player.moveState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
