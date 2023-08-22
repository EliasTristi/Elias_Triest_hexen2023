using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public Dictionary<string, State> _states = new Dictionary<string, State>();

    private string _currentStateName;

    public string InitialState
    {
        set
        {
            _currentStateName = value;
            Debug.Log(_currentStateName);
            CurrentState.OnEnter();
        }
    }
    public State CurrentState => _states[_currentStateName];

    public void Register(string stateName, State state)
    {
        state.machine = this;
        _states.Add(stateName, state);
    }

    public void ChangeTo(string stateName)
    {
        CurrentState.OnExit();
        _currentStateName = stateName;
        CurrentState.OnEnter();
        Debug.Log($"changed state to {_currentStateName}");
    }
}
