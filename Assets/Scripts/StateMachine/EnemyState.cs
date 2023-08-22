using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : State
{

    public override void OnEnter()
    {
        base.OnEnter();

        Debug.Log("in enemy state");


        this.OnExit();
    }

    public override void OnExit()
    {
        base.OnExit();
        var sm = new StateMachine();
        sm.ChangeTo(States.Enemy);
    }
}
