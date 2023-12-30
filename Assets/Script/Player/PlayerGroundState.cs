using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(Player _player, PlayerStateMachine _stateMachine, string _animBooName) : base(_player, _stateMachine, _animBooName)
    {
    }


    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        
        base.Update();
        
        if(Input.GetKeyDown(KeyCode.R))
            stateMachine.ChangeState(player.blackHole);

        if (Input.GetKeyDown(KeyCode.Mouse1) && HasNoSword())
            stateMachine.ChangeState(player.aimSowrd);

            if(Input.GetKeyDown(KeyCode.Q))
            stateMachine.ChangeState(player.counterAttack);
        
        if(Input.GetKeyDown(KeyCode.Mouse0))
            stateMachine.ChangeState(player.PrimaryAttackState);

        if (!player.IsGroundDetected())
            stateMachine.ChangeState(player.ariState);

            if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
            stateMachine.ChangeState(player.JumpStateState);

    }

    private bool HasNoSword()
    {
        if (!player.sword)
        {
            return true;
        }
        
        player.sword.GetComponent<Sword_Skill_Collider>().ReturnSword();
        return false;
    }
    
    public override void Exit()
    {
        base.Exit();
        
    }
}
