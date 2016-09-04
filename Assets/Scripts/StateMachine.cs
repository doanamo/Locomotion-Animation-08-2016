using UnityEngine;
using System.Collections;

public abstract class State: ScriptableObject
{
    public virtual bool OnEnter(State previousState)
    {
        return true;
    }

    public virtual void HandleCommand<Type>(Type command)
    {
    }

    public virtual void OnUpdate()
    {
    }

    public virtual bool OnExit(State nextState)
    {
        return true;
    }
}

public class StateMachine : ScriptableObject
{
    public State currentState
    {
        get; private set;
    }

    public static StateMachine CreateInstance()
    {
        var instance = ScriptableObject.CreateInstance<StateMachine>();
        instance.Initialize();
        return instance;
    }

    public void Initialize()
    {
        currentState = null;
    }

    public bool ChangeState(State state)
    {
        if(currentState != null)
        {
            if(!currentState.OnExit(state))
                return false;
        }

        if(state != null)
        {
            if(!state.OnEnter(currentState))
                return false;
        }

        currentState = state;
        return true;
    }

    public void HandleCommand<Type>(Type command)
    {
        if(currentState != null)
        {
            currentState.HandleCommand(command);
        }
    }

    public void Update()
    {
        if(currentState != null)
        {
            currentState.OnUpdate();
        }
    }
}
