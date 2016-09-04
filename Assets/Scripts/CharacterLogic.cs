using UnityEngine;
using System.Collections;

public class CharacterLogic : MonoBehaviour
{
    public StateMachine stateMachine;

    public StandingState standingState;
    public MovingState movingState;
    public TurningState turningState;

    void Start()
    {
        standingState = StandingState.CreateInstance(this);
        movingState = MovingState.CreateInstance(this);
        turningState = TurningState.CreateInstance(this);

        stateMachine = ScriptableObject.CreateInstance<StateMachine>();
        stateMachine.ChangeState(standingState);
    }

    public void HandleCommand<Type>(Type command)
    {
        stateMachine.HandleCommand(command);
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        stateMachine.Update();
    }
}
