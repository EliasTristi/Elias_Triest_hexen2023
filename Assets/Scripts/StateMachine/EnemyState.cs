using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : State
{
    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExit()
    {
        base.OnExit();
        var sm = new StateMachine();
        sm.ChangeTo(States.Enemy);
    }
}
