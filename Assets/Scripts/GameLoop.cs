using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    private StateMachine StateMachine;

    [SerializeField]
    private GameObject _entity;
    [SerializeField]
    private int _entityAmount = 8;

    void Start()
    {
        StateMachine = new StateMachine();
        StateMachine.Register(States.Menu, new MenuState());
        StateMachine.Register(States.Game, new PlayingState(_entity, _entityAmount));

        StateMachine.InitialState = States.Menu;
    }
}
