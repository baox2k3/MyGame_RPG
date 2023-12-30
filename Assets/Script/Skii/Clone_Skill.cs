using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone_Skill : Skill
{
    [Header("Clone info")]
    [SerializeField] private GameObject clonPrefab;

    [SerializeField] private float cloneDuaration;
    [Space]
    [SerializeField] private bool canAttack;

    [SerializeField] private bool createCloneOnDashStar;
    [SerializeField] private bool createCloneOnDashOver;
    [SerializeField] private bool canCreateCloneOnCounterAttack;
   
    [Header("Clone can duplicate")]
    [SerializeField] private bool canDuplicateClone;
    [SerializeField] private float chanceToDuplicate;

    [Header("Crystal instead of clone")] 
    public bool crystalInseaOfClone;
    
    public void CreateClone(Transform _clonePosition,Vector3 _offset)
    {
        if (crystalInseaOfClone)
        {
            SkillManager.instance.crystal.CreateCrystal();
            return;
        }
        
        
        GameObject newClone = Instantiate(clonPrefab);
        
        newClone.GetComponent<Clone_Skill_Controller>().SetupClone(_clonePosition,cloneDuaration,canAttack,_offset,FindClosestEnemy(newClone.transform),canDuplicateClone,chanceToDuplicate);
    }

    public void CreateCloneOnDashStart()
    {
        if(createCloneOnDashStar)
            CreateClone(player.transform,Vector3.zero);
    }

    public void CreateCloneOnDashOver()
    {
        if( createCloneOnDashOver)
            CreateClone(player.transform,Vector3.zero);
    }

    public void CanCreateCloneOnCounterAttack(Transform _enemyTransform)
    {
        if (canCreateCloneOnCounterAttack)
            StartCoroutine(CreateCloneWithDelay(_enemyTransform, new Vector3(2 * player.facingDir, 0)));
    }

    private IEnumerator CreateCloneWithDelay(Transform _transform,Vector3 _offset)
    {
        yield return new WaitForSeconds(.4f);
            CreateClone(_transform,_offset);
    }
}
