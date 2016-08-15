using UnityEngine;
using System.Collections;

public class CharacterLogic : MonoBehaviour
{
    public StateMachine stateMachine;

    public StandingState standingState;
    public MovingState movingState;

    void Start()
    {
        standingState = new StandingState(this);
        movingState = new MovingState(this);

        stateMachine = new StateMachine();
        stateMachine.ChangeState(standingState);
    }

    public void HandleCommand<Type>(Type command)
    {
        stateMachine.HandleCommand(command);
    }

    void Update()
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward, Color.red);
    }

    void FixedUpdate()
    {
        stateMachine.Update();
    }
}
