using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Character_Starts : MonoBehaviour
{

    private EmtityFx fx;

    [Header("Major stats")]
    public Stat strength;//1 point increase damage by 1 and crit.powe by 1%
    public Stat agility;//1 point increase damage by 1% and crit.chance by 1%
    public Stat intelligence;// 1 poin increase magic damage by 1 and magic resistance by 3
    public Stat vitality;//1 point incredase heath by 3 or 5 points
    
    [Header("Offemsive stats")]
    public Stat damage;
    public Stat critChance;
    public Stat critPower; //defaul value 150%
    
    
    [Header("Defensive stats")]
    public Stat maxHealth;
    public Stat armor;
    public Stat evasion;
    public Stat magicResistance;

    [Header("Magic Stats")] 
    public Stat fireDamege;
    public Stat iceDamage;
    public Stat lighringDamage;


    public bool isIgnited;// does damage over time
    public bool isChilled;// reduce armor by 20%
    public bool isShocked;// reduce accuracy by 20%


    [SerializeField] private float ailmentsDuration = 4;
    private float ignitedTimer;
    private float chilledTimer;
    private float shockedTimer;


    
    private float igniteDamageCoodlown = .3f;
    private float igniteDamageTimer;
    private int igniteDamage;

    
    
    public int currentHealt;


    public System.Action onHealthChanged;
    
    
    protected virtual void Start()
    {
        critPower.SetDefaultValue(150);
        currentHealt = GetMaxHealthValue();

       fx = GetComponent<EmtityFx>();
        
        Debug.Log( "Characater start called");
    }
    
    protected virtual void Update()
    {
        ignitedTimer -= Time.deltaTime;
        chilledTimer -= Time.deltaTime;
        shockedTimer -= Time.deltaTime;

        igniteDamageTimer -= Time.deltaTime;

        if (ignitedTimer < 0)
            isIgnited = false;

        if (chilledTimer < 0)
            isChilled = false;

        if (shockedTimer < 0)
            isShocked = false;

        if (igniteDamageTimer < 0 && isIgnited)
        {
            Debug.Log("Take burn damageeeeeee" + igniteDamage);
            
            DecreaseHealthBy(igniteDamage);
            
            if(currentHealt < 0)
                Die();

            igniteDamageTimer = igniteDamageCoodlown;
        }

    }

    public virtual void DoDamage(Character_Starts _tagetStats)
    {
        if(TargetCanAvoidAttack(_tagetStats))
            return;

        int totalDamage = damage.GetValue() + strength.GetValue();

        
        if(CanCrit())
        {
            totalDamage = CalculateCriticalDamage(totalDamage);
            
        }
        
        
        totalDamage = CheckTargetArmor(_tagetStats, totalDamage);
        //_tagetStats.TakeDamage(totalDamage);
        DoMagicalDamage(_tagetStats);
    }


    public virtual void DoMagicalDamage(Character_Starts _targetStats)
    {
        int _fireDamage = fireDamege.GetValue();
        int _iceDamage = iceDamage.GetValue();
        int _lightingDamage = lighringDamage.GetValue();

        int totaMagiccalDamage = _fireDamage + _iceDamage + _lightingDamage + intelligence.GetValue();
        
        
        totaMagiccalDamage = CheckTargetResistance(_targetStats, totaMagiccalDamage);
        _targetStats.TakeDamage(totaMagiccalDamage);



        
        bool canApplyIgnite =_fireDamage > _iceDamage && _fireDamage > _lightingDamage;
        bool canApplyChill = _iceDamage > _fireDamage && _iceDamage > _lightingDamage;
        bool canApplyShock = _lightingDamage > _fireDamage && _lightingDamage > _iceDamage;

        while (!canApplyIgnite && !canApplyChill && !canApplyShock)
        {
            if (Random.value < .3f && _fireDamage > 0)
            {
                canApplyIgnite = true;
                _targetStats.ApplyAilments(canApplyIgnite,canApplyChill,canApplyShock); 
                Debug.Log("Applied fire");
                return;
            } 
            if (Random.value < .5f && _iceDamage > 0)
            {
                canApplyChill = true;
                _targetStats.ApplyAilments(canApplyIgnite,canApplyChill,canApplyShock); 
                Debug.Log("Applied ice");
                return;
            }
            if (Random.value < .5f && _lightingDamage > 0)
            {
                canApplyShock = true;
                _targetStats.ApplyAilments(canApplyIgnite,canApplyChill,canApplyShock);
                Debug.Log("Applied lighting");
                return;
            }
            
            
        }
        if(canApplyIgnite)
            _targetStats.SetupIgniteDamage(Mathf.RoundToInt(_fireDamage * .2f));
        
        _targetStats.ApplyAilments(canApplyIgnite,canApplyChill,canApplyShock);
        
    }
    
    

    private static int CheckTargetResistance(Character_Starts _targetStats, int totaMagiccalDamage)
    {
        totaMagiccalDamage -= _targetStats.magicResistance.GetValue() + (_targetStats.intelligence.GetValue() * 3);
        totaMagiccalDamage = Mathf.Clamp(totaMagiccalDamage, 0, int.MaxValue);
        return totaMagiccalDamage;
    }

    public void ApplyAilments(bool _ingite, bool _chill, bool _shock)
    {
        if(isIgnited || isChilled || isShocked)
            return;

        if (_ingite)
        {
           isIgnited = _ingite;
           ignitedTimer = ailmentsDuration;    
           
           fx.IgniteFxFor(ailmentsDuration);
        }

        if (_chill)
        {
            isChilled = _chill;
            chilledTimer = ailmentsDuration;
            
            fx.ChillFxFor(ailmentsDuration);
        }
        if (_shock)
        {
            isShocked = _shock;
            shockedTimer = ailmentsDuration;
            
            fx.ShockFxFor(ailmentsDuration);
        }
    }

    public void SetupIgniteDamage(int _damage) => igniteDamage = _damage; 
    
    public virtual void TakeDamage(int _damage)
    {
        DecreaseHealthBy(_damage);
        
        Debug.Log(_damage +" abc");

        if (currentHealt < 0)
            Die();

   
    }


    protected virtual void DecreaseHealthBy(int _damage)
    {
        currentHealt -= _damage;

        if (onHealthChanged != null)
            onHealthChanged();
        
    }

    protected virtual void Die()
    {
        
    }
    
    private int CheckTargetArmor(Character_Starts _tagetStats, int totalDamage)
    {
        if (_tagetStats.isChilled)
            totalDamage -= Mathf.RoundToInt(_tagetStats.armor.GetValue() * .8f);
        else
            totalDamage -= _tagetStats.armor.GetValue();
        
        
        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        return totalDamage;
    }

    private bool TargetCanAvoidAttack(Character_Starts _tagetStats)    {
        int totalEvasion = _tagetStats.evasion.GetValue() + _tagetStats.agility.GetValue();

        if (isShocked)
            totalEvasion += 20;
        
        if (Random.Range(0, 100) < totalEvasion)
        {
            return true;
        }

        return false;
    }

    private bool CanCrit()
    {
        int totaCriticalChane = critChance.GetValue() + agility.GetValue();

        if (Random.Range(0, 100) <= totaCriticalChane)
        {
            return true;
        }

        return false;
    }

    private int CalculateCriticalDamage(int _damage)
    {
        float totaCritPower = (critPower.GetValue() + strength.GetValue()) *.01f;
        float critDamage = +_damage * totaCritPower;

        return Mathf.RoundToInt(critDamage);
    }

    public int GetMaxHealthValue()
    {
        return maxHealth.GetValue() + vitality.GetValue() * 5;
    }
}
