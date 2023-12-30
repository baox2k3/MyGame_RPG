using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] protected float cooldown;
    protected float cooldownTimer;

    protected Player player;

    protected virtual void Start()
    {
        player = Playermanager.instance.player;
    }

    protected virtual  void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }

    public virtual bool CanUseKill()
    {
        if (cooldownTimer < 0 )
        {
            UseKill();
            cooldownTimer = cooldown;
            return true;
        }
        
        Debug.Log("Skill is on coodown");
        return false;
    }

    public virtual void UseKill()
    {
        // do some skill spesific things
        
    }

    protected virtual Transform FindClosestEnemy(Transform _checkTransform)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_checkTransform.position, 25);

        float closesDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                float distanceToEnemy = Vector2.Distance(_checkTransform.position, hit.transform.position);

                if (distanceToEnemy < closesDistance)
                {
                    closesDistance = distanceToEnemy;
                    closestEnemy = hit.transform;
                }
            }
        }

        return closestEnemy;
    }
}
