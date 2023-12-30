using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
   [Header("Attack details")]
   public Vector2[] attackMovent;

   public float counterAttackDuration = .2f;

   
   public bool isBusy { get; private set; }
   [Header("Move info")]
   public float moveSpeed = 12;
   public float jumForce;
   public float swordReturnImpact;



   [Header("Dash info")]
   public float dashSpeed;
   public float dashDuration;
   public float dashDir { get; private set; }

   public SkillManager skill { get; private set; }

   public GameObject sword { get; private set; }
   
   #region States
   public PlayerStateMachine stateMachine { get; private set; }
   public PlayerIdieState idleState { get; private set; }
   public PlayerMoveState moveState { get; private set; }
   public PlayerJumState JumpStateState { get; private set; }
   public PlayerAriState ariState { get; private set; }
  
   public PlayerDashState DashState { get; private set; }
   
   public PlayerWallSlideState wallSlide { get; private set; }
   
   public PlayerWallJumState wallJum { get; private set; }
   
   public PlayerPrimeryAttackState PrimaryAttackState { get; private set; }

   public PlayerCountAttackState counterAttack { get; private set; }
   
   public PlayerAimSworState aimSowrd { get; private set; }
   
   public PlayerCatchSwordState catchSword { get; private set;}

   public PlayerBlackholeState blackHole { get; private set; }
   
   public PlayerDeadState deadState { get; private set; }
   #endregion
  
   protected override void Awake()
   {
      base.Awake();
      
      stateMachine = new PlayerStateMachine();

      idleState = new PlayerIdieState( this,stateMachine,"ide");
      moveState = new PlayerMoveState(this, stateMachine, "move");
      JumpStateState = new PlayerJumState(this, stateMachine, "jum");
      ariState = new PlayerAriState(this, stateMachine, "jum");
      DashState = new PlayerDashState(this, stateMachine, "Dash");
      wallSlide = new PlayerWallSlideState(this, stateMachine, "wallSlide");
      wallJum = new PlayerWallJumState(this, stateMachine, "jum");
     
      PrimaryAttackState = new PlayerPrimeryAttackState(this, stateMachine, "attack");
      counterAttack = new PlayerCountAttackState(this, stateMachine, "countAttack");

      aimSowrd = new PlayerAimSworState(this, stateMachine, "AimSword");
      catchSword = new PlayerCatchSwordState(this, stateMachine, "CatchSword");
      
      blackHole = new PlayerBlackholeState(this, stateMachine,"jum");

      deadState = new PlayerDeadState(this, stateMachine, "die");
   }

   protected override void Start()
   {
      base.Start();

      skill = SkillManager.instance;
      
      stateMachine.Initialized(idleState);
   }
   

   protected override void Update()
   {
      base.Update();
      
      stateMachine.currentState.Update();

      checkforDashInput();

      if (Input.GetKeyDown(KeyCode.F))
         skill.crystal.CanUseKill();

   }

   public void AssignNewSword(GameObject _newSword)
   {
      sword = _newSword;
   }

   public void CatchTheSword()
   {
      stateMachine.ChangeState(catchSword);
      Destroy(sword);
   }


   
   public IEnumerator BusyFor(float _seconds)
   {
      isBusy = true;
      
      yield return new WaitForSeconds(_seconds);
      
      isBusy = false;
   }
   

   public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
   
   private void checkforDashInput()
   {
      if(IsWallDetected())
         return;
      
      //dashUsageTime -= Time.deltaTime;
      
      if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.dash.CanUseKill())
      {
         //dashUsageTime = dashCoodown;
         dashDir = Input.GetAxisRaw("Horizontal");

       if (dashDir == 0)
          dashDir = facingDir;
     

            stateMachine.ChangeState(DashState);
      }
   }

   public override void Die()
   {
      base.Die();
      stateMachine.ChangeState(deadState);
   }
}
