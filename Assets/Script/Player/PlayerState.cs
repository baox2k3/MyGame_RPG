using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState: MonoBehaviour
{
   protected PlayerStateMachine stateMachine;
   protected Player player;

   protected Rigidbody2D rb;
   
   protected float xInput;
   protected float yInput;
   private string animBooName;

   protected float stateTimer;
   protected bool triggerCalled;

   public PlayerState( Player _player,PlayerStateMachine _stateMachine, string _animBooName)
   {
      this.stateMachine = _stateMachine;
      this.player = _player;
      this.animBooName = _animBooName;
   }

   public virtual void Enter()
   {
     player.anim.SetBool(animBooName,true);
     rb = player.rb;
     triggerCalled = false;
   }

   public virtual void Update()
   {
      stateTimer -= Time.deltaTime;
      
      xInput = Input.GetAxisRaw("Horizontal");
      yInput = Input.GetAxisRaw("Vertical");
      player.anim.SetFloat("yVelocity",rb.velocity.y);
   }

   public virtual void Exit()
   {
       player.anim.SetBool(animBooName,false);
   }

   public virtual void AnimationFinishTrigger()
   {
      triggerCalled = true;
   } 
}
