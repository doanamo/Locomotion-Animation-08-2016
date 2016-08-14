using UnityEngine;
using System.Collections;

public class IdleState : State
{
    private CharacterLogic character;
    private float lingerTimer;

    public IdleState(CharacterLogic character)
    {
        this.character = character;
    }

    public override bool OnEnter(State previousState)
    {
        lingerTimer = 0.0f;

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
        float verticalSpeed = character.animator.GetFloat("Movement");
        verticalSpeed = Mathf.MoveTowards(verticalSpeed, 0.0f, Time.fixedDeltaTime);
        character.animator.SetFloat("Movement", verticalSpeed);

        lingerTimer += Time.fixedDeltaTime;

        if(lingerTimer > 8.0f)
        {
            character.animator.SetTrigger("Linger");
            lingerTimer = 0.0f;
        }
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
            // Set animation speed.
            float verticalSpeed = character.animator.GetFloat("Movement");
            verticalSpeed = Mathf.MoveTowards(verticalSpeed, 1.0f, Time.fixedDeltaTime);
            character.animator.SetFloat("Movement", verticalSpeed);

            // Rotate facing direction.
            Transform transform = character.gameObject.transform;
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

            // Reset direction value.
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
