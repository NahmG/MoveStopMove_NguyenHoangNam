using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StateMachine
{
    private BaseState currentState;
    private Dictionary<STATE, BaseState> states;
    public bool IsDebug;

    public STATE CurrentState => currentState.Id;
    public Dictionary<STATE, BaseState> States => states;

    public StateMachine()
    {
        states = new Dictionary<STATE, BaseState>();
    }

    public void Start(STATE id)
    {
        currentState = states[id];
        currentState?.Enter();

        if (IsDebug)
        {
            Debug.Log($"Change state to {id}");
        }
    }

    public void Stop()
    {
        currentState?.Exit();
        currentState = null;
    }

    public void AddState(STATE id, BaseState state)
    {
        if (!states.ContainsKey(id))
        {
            states.Add(id, state);
            states[id]._OnStateChanged += ChangeState;
        }
    }

    public void RemoveState(STATE id)
    {
        if (states.ContainsKey(id))
        {
            states[id]._OnStateChanged -= ChangeState;
            states.Remove(id);
        }
    }
    public void ChangeState(STATE id)
    {
        if (IsDebug)
        {
            Debug.Log($"Change state from {currentState?.Id} to {id}");
        }

        if (!states.ContainsKey(id))
        {
            Debug.Log($"Key {id} STATE Invalid");
        }
        currentState?.Exit();
        currentState = states[id];
        currentState?.Enter();
    }
    public void Update()
    {
        currentState?.Update();
    }
    public void FixedUpdate()
    {
        currentState?.FixedUpdate();
    }
}
