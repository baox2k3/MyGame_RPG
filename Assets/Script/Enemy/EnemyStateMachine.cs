using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine 
{
    public EnemyState curremtSate { get; private set; }

    public void Initialize(EnemyState _staetState)
    {
        curremtSate = _staetState;
        curremtSate.Enter();
    }

    public void ChangeState(EnemyState _newState)
    {
        curremtSate.Exit();
        curremtSate = _newState;
        curremtSate.Enter();
    }
}
