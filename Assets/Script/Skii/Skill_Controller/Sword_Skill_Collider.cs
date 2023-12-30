using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Skill_Collider : MonoBehaviour
{
   private Animator anim;
   private Rigidbody2D rb;
   private CircleCollider2D cd;
   private Player player;


   private bool canRotate = true;
   private bool isReturning;

   private float freezeTimeDuration;
   private float returnSpeed = 12;
   
   [Header("Pierce info")]
   private float pierceAmount;
   
   [Header("Bounce info")]
   private float bounceSpeed;
   private bool isBouncing;
   private int bouneAmount;
   private List<Transform> enemyTarget;
   private int targetIndext;


   [Header("Spin info")] 
   private float maxTravelDistance;

   private float spinDuration;
   private float spinTimer;
   private float spinDirection;
   private bool wasStopped;
   private bool isSpinning;

   private float hitTimer;
   private float hitcooldown;

   private void Awake()
   {
      anim = GetComponentInChildren<Animator>();
      rb = GetComponent<Rigidbody2D>();
      cd = GetComponent<CircleCollider2D>();
      
   }

   private void DestroyMe()
   {
      Destroy(gameObject);
   }
   public void SetupSword(Vector2 _dir, float _gravityScale,Player _player,float _freezeTimeDuration,float _returnSpeed)
   {
      player = _player;
      freezeTimeDuration = _freezeTimeDuration;
      returnSpeed = _returnSpeed;
      
      rb.velocity = _dir;
      rb.gravityScale = _gravityScale;
      
      if(pierceAmount <= 0)
         anim.SetBool("Rotation",true);
      
      
      Invoke("DestroyMe",7);

      spinDirection = Mathf.Clamp(rb.velocity.x, -1, 1);
      
      Invoke("DestroyMe",7);
   }

   public void Setupbounce(bool _isBouncing, int _amountOfBounces,float _bounceSpped)
   {
      isBouncing = _isBouncing;
      bouneAmount = _amountOfBounces;
      bounceSpeed = _bounceSpped;


      enemyTarget = new List<Transform>();
   }

   public void SetupPierce(int _pierceAmount)
   {
      pierceAmount = _pierceAmount;
   }

   public void SetupSpin(bool _isSpinning, float _maxTravelDistance, float _spinDuration, float _hitCooldown)
   {
      isSpinning = _isSpinning;
      maxTravelDistance = _maxTravelDistance;
      spinDuration = _spinDuration;
      hitcooldown = _hitCooldown;
   }
   
   public void ReturnSword()
   {
      rb.constraints = RigidbodyConstraints2D.FreezeAll;
      //rb.isKinematic = false;
      transform.parent = null;
      isReturning = true;
   }

   private void Update()
   {
      if(canRotate)
         transform.right = rb.velocity;

      if (isReturning)
      {
         transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnSpeed * Time.deltaTime);
         
         if(Vector2.Distance(transform.position, player.transform.position) < 1)
            player.CatchTheSword();
      }

      BounceLogic();
      
      SpinLogic();
   }

   private void SpinLogic()
   {
      if (isSpinning)
      {
         if (Vector2.Distance(player.transform.position, transform.position) > maxTravelDistance && !wasStopped)
         {
            StopWhenSpinning();
         }

         if (wasStopped)
         {
            spinTimer -= Time.deltaTime;
            
            transform.position = Vector2.MoveTowards(transform.position,new Vector2(transform.position.x + spinDirection,transform.position.y),1.5f * Time.deltaTime);
            
            if (spinTimer < 0)
            {
               isReturning = true;
               isSpinning = false;
            }

            hitTimer -= Time.deltaTime;
            if (hitTimer < 0)
            {
               hitTimer = hitcooldown;

               Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1);

               foreach (var hit in colliders)
               {
                  if (hit.GetComponent<Enemy>() != null)
                    SwordSkillDamage(hit.GetComponent<Enemy>());
               }
            }
         }
      }
   }

   private void StopWhenSpinning()
   {
      wasStopped = true;
      rb.constraints = RigidbodyConstraints2D.FreezePosition;
      spinTimer = spinDuration;
   }

   private void BounceLogic()
   {
      if (isBouncing && enemyTarget.Count > 0)
      {
         transform.position =
            Vector2.MoveTowards(transform.position, enemyTarget[targetIndext].position, bounceSpeed * Time.deltaTime);

         if (Vector2.Distance(transform.position, enemyTarget[targetIndext].position) < .1f)
         {
            
            SwordSkillDamage(enemyTarget[targetIndext].GetComponent<Enemy>());

            targetIndext++;
            bouneAmount--;

            if (bouneAmount <= 0)
            {
               isBouncing = false;
               isReturning = true;
            }

            if (targetIndext >= enemyTarget.Count)
               targetIndext = 0;
         }
      }
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      if(isReturning)
         return;


      if (other.GetComponent<Enemy>() != null)
      {
         Enemy enemy = other.GetComponent<Enemy>();
         SwordSkillDamage(enemy);
      }
      
      SetupTargetsForBounce(other);
      
      StruckInto(other);
   }

   private void SwordSkillDamage(Enemy enemy)
   {
      enemy.DamageEffect();
      enemy.StartCoroutine("FreezeTimerFor", freezeTimeDuration);
   }

   private void SetupTargetsForBounce(Collider2D other)
   {
      if (other.GetComponent<Enemy>() != null)
      {
         if (isBouncing && enemyTarget.Count <= 0)
         {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);

            foreach (var hit in colliders)
            {
               if (hit.GetComponent<Enemy>() != null)
                  enemyTarget.Add(hit.transform);
            }
         }
      }
   }

   private void StruckInto(Collider2D other)
   {

      if (pierceAmount > 0 && other.GetComponent<Enemy>() != null)
      {
         pierceAmount--;
         return;
      }
      
      if(isSpinning)
      {
         StopWhenSpinning();
         return;
      }
      
      canRotate = false;
      cd.enabled = false;

      rb.isKinematic = true;
      rb.constraints = RigidbodyConstraints2D.FreezeAll;

      if(isBouncing && enemyTarget.Count > 0)
         return;
      
      anim.SetBool("Rotation",false);
      transform.parent = other.transform;
   }
}
