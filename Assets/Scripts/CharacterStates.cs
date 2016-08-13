using UnityEngine;
using System.Collections;

public class IdleState : State
{
    private CharacterLogic character;

    public IdleState(CharacterLogic character)
    {
        this.character = character;
    }

    public override bool OnEnter(State previousState)
    {
        Debug.Log("OnEnter: Idle State");
        return true;
    }

    public override void HandleCommand<Type>(Type command)
    {
        if(typeof(Type) == typeof(MoveCommand))
        {
            MoveCommand moveCommand = (MoveCommand)(object)command;
            MovingState movingState = new MovingState(character, moveCommand);
            if(character.stateMachine.ChangeState(movingState))
                return;
        }
    }

    public override void OnUpdate()
    {
    }
}

public class MovingState : State
{
    private CharacterLogic character;
    private Vector3 direction;

    public MovingState(CharacterLogic character, MoveCommand command)
    {
        this.character = character;
        this.direction = command.direction;
    }

    public override bool OnEnter(State previousState)
    {
        Debug.Log("OnEnter: Moving State");
        return true;
    }

    public override void HandleCommand<Type>(Type command)
    {
        if(typeof(Type) == typeof(MoveCommand))
        {
            MoveCommand moveCommand = (MoveCommand)(object)command;
            this.direction = moveCommand.direction;
        }
    }

    public override void OnUpdate()
    {
        if(direction != Vector3.zero)
        {
            Rigidbody rigidbody = character.rigidbody;
            rigidbody.AddForce(direction, ForceMode.VelocityChange);
            rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, 4.0f);

            direction = Vector3.zero;
        }
        else
        {
            IdleState idleState = new IdleState(character);
            if(character.stateMachine.ChangeState(idleState))
                return;
        }
    }
}
