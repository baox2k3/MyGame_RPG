using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCountAttackState : PlayerState
{
    private bool canCreateClone;
    public PlayerCountAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBooName) : base(_player, _stateMachine, _animBooName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        canCreateClone = true;
        stateTimer = player.counterAttackDuration;
        player.anim.SetBool("sussessfulAttack",false);
    }

    public override void Update()
    {
        base.Update();
        
        player.SetZeroVeloccity();
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                if (hit.GetComponent<Enemy>().CanBeStunned())
                {
                    stateTimer = 10;
                    player.anim.SetBool("sussessfulAttack",true);

                    if (canCreateClone)
                    {
                        canCreateClone = false;
                      player.skill.clone.CanCreateCloneOnCounterAttack(hit.transform);
                    }
                }
            }
        }
        
        if(stateTimer < 0 || triggerCalled)
            stateMachine.ChangeState(player.idleState);
        
    }

    public override void Exit()
    {
        base.Exit();
        
       
    }
}
