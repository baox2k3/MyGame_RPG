using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SkeletonAnimationTrigger : MonoBehaviour
{
   private Enemy_Skeleton enemy => GetComponentInParent<Enemy_Skeleton>();

   private void AnimationTrigger()
   {
      enemy.AnimationFinishTrigger();
   }

   private void AttackTrigger()
   {
      Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

      foreach (var hit in colliders)
      {
         if(hit.GetComponent<Player>() != null)
         {
            PlayerStats tager = hit.GetComponent<PlayerStats>();
            enemy.start.DoDamage(tager);
            //hit.GetComponent<Player>().DamageEffect();
         }
      }
   }

   protected void OpenCounterWindow() => enemy.OpenCounterAttackWindow();
   protected void closeCounterWindow() => enemy.CloseCounterAttackWindow();
}
