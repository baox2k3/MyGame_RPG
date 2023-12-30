using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimSworState : PlayerState
{
    public PlayerAimSworState(Player _player, PlayerStateMachine _stateMachine, string _animBooName) : base(_player, _stateMachine, _animBooName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.skill.sword.DotsActive(true);
        

    }

    public override void Update()
    {
        base.Update();
        
        player.SetZeroVeloccity();
        
        if(Input.GetKeyUp(KeyCode.Mouse1))
            stateMachine.ChangeState(player.idleState);

        Vector2 mouePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if(player.transform.position.x > mouePosition.x && player.facingDir == 1 )
            player.Flip();
        else if(player.transform.position.x < mouePosition.x && player.facingDir == -1)
            player.Flip();

    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine("BusyFor", .2f);
    }
}
