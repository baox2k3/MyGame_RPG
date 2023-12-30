using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimeryAttackState : PlayerState
{

    private int comboCountter;
    private float lastTimeAttacked;
    private float comboWindow = 2;
    public PlayerPrimeryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBooName) : base(_player, _stateMachine, _animBooName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        xInput = 0; 
        
        if (comboCountter > 2 || Time.time >= lastTimeAttacked + comboWindow)
            comboCountter = 0;
        
        player.anim.SetInteger("comboCounter",comboCountter);

        
        float attackDir = player.facingDir;

        if (xInput != 0)
            attackDir = xInput;

        
        player.SetVelocity(player.attackMovent[comboCountter].x * attackDir,player.attackMovent[comboCountter].y);
        
        stateTimer = .1f;
    }

    public override void Update()
    {
        base.Update();

        if(stateTimer < 0)
          player.SetZeroVeloccity();

        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("BusyFor", .15f);

        
        comboCountter++;
        lastTimeAttacked = Time.time;
     
    }
}
