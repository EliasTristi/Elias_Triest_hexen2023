using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public StateMachine machine { get; set; }
    public virtual void OnEnter() { }
    public virtual void OnExit() { }
}
