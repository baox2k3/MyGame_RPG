using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumState : PlayerState
{


    public PlayerJumState(Player _player, PlayerStateMachine _stateMachine, string _animBooName) : base(_player, _stateMachine, _animBooName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        rb.velocity = new Vector2(rb.velocity.x, player.jumForce);
    }

    public override void Update()
    {
        base.Update();
        if (rb.velocity.y < 0)
            stateMachine.ChangeState(player.ariState);
    }

    public override void Exit()
    {
        base.Exit();
      
    }
}
