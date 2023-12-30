using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash_Kill : Skill
{
    public override void UseKill()
    {
        base.UseKill();
        
        Debug.Log("Created clone behind");
    }
}
