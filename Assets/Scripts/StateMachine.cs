using System.Collections;

public abstract class State
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

public class StateMachine
{
    public State currentState
    {
        get; private set;
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
