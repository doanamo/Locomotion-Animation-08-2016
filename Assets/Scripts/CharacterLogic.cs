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
        standingState = new StandingState(this);
        movingState = new MovingState(this);
        turningState = new TurningState(this);

        stateMachine = new StateMachine();
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
