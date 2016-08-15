using UnityEngine;
using System.Collections;

public class CharacterLogic : MonoBehaviour
{
    public StateMachine stateMachine;

    public IdleState idleState;
    public MovingState movingState;

    void Start()
    {
        idleState = new IdleState(this);
        movingState = new MovingState(this);

        stateMachine = new StateMachine();
        stateMachine.ChangeState(idleState);
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
