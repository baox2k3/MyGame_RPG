using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{

  [SerializeField]protected LayerMask whatIsPlayer;

  [Header("Stunned info")] 
  public float stunDuration;
  public Vector2 stundirection;
  protected bool canBeStunned;
  [SerializeField] protected GameObject countereImage;
  
  
  [Header("Move info")] 
  public float moveSpeed;
  public float idleTime;
  public float battleTime;
  private float defaultMoveSpeed;

  [Header("Attack info")] 
  public float attackDistance;
  public float attackCooldown;
  [HideInInspector]public float lastTimeAttacked;
  
  public EnemyStateMachine  stateMachine { get; private set; }
  
  public String lastAnimBoolName { get; private set; }
  
  protected override void Awake()
  {
    base.Awake();
    stateMachine = new EnemyStateMachine();
    defaultMoveSpeed = moveSpeed;
  }

  protected override void Update()
  {
    base.Update();
    
    stateMachine.curremtSate.Update();
    
  }

  public virtual void AssignLastAnimName(String _animBoolName)
  {
    lastAnimBoolName = _animBoolName;
  }

  public virtual void FreezeTime(bool _timerFroze)
  {
    if (_timerFroze)
    {
      moveSpeed = 0;
      anim.speed = 0;
    }
    else
    {
      moveSpeed = defaultMoveSpeed;
      anim.speed = 1;
    }
  }


  protected virtual IEnumerator FreezeTimerFor(float _seconds)
  {
    FreezeTime(true);
    yield return new WaitForSeconds(_seconds);
    FreezeTime(false);
  }

  #region Counter Attack Windown
  public virtual void OpenCounterAttackWindow()
  {
    canBeStunned = true;
    countereImage.SetActive(true);
  }
  public virtual void CloseCounterAttackWindow()
  {
    canBeStunned = false;
    countereImage.SetActive(false);
  }
  #endregion

  public virtual bool CanBeStunned()
  {
    if (canBeStunned)
    {
      CloseCounterAttackWindow();
      return true;
    }

    return false;
  }

  public virtual void AnimationFinishTrigger() => stateMachine.curremtSate.AnimationFinishTrigger();
  public virtual RaycastHit2D IsPlayDetected() =>
    Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, 50, whatIsPlayer);

  protected override void OnDrawGizmos()
  {
    base.OnDrawGizmos();
    Gizmos.color = Color.yellow;
    Gizmos.DrawLine(transform.position,new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));
    
  }
}
