using UnityEngine;
using System.Collections;

public class IdleState : State
{
    private CharacterLogic character;
    private Animator animator;

    private float lingerTimer;

    public IdleState(CharacterLogic character)
    {
        this.character = character;
        this.animator = character.GetComponent<Animator>();
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
            if(character.stateMachine.ChangeState(character.movingState))
            {
                character.movingState.HandleCommand(command);
                return;
            }
        }
    }

    public override void OnUpdate()
    {
        // Stop character movement animation.
        float verticalSpeed = animator.GetFloat("Movement");
        verticalSpeed = Mathf.MoveTowards(verticalSpeed, 0.0f, Time.fixedDeltaTime);
        animator.SetFloat("Movement", verticalSpeed);

        // Trigger a linger animation.
        lingerTimer += Time.fixedDeltaTime;

        if(lingerTimer > 8.0f)
        {
            animator.SetTrigger("Linger");
            lingerTimer = 0.0f;
        }
    }
}

public class MovingState : State
{
    private CharacterLogic character;
    private Animator animator;

    private Vector3 direction;

    public MovingState(CharacterLogic character)
    {
        this.character = character;
        this.animator = character.GetComponent<Animator>();
    }

    public override bool OnEnter(State previousState)
    {
        direction = Vector3.zero;
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
            float verticalSpeed = animator.GetFloat("Movement");
            verticalSpeed = Mathf.MoveTowards(verticalSpeed, 1.0f, Time.fixedDeltaTime);
            animator.SetFloat("Movement", verticalSpeed);

            // Rotate facing direction.
            Transform transform = character.gameObject.transform;
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

            // Reset direction value.
            direction = Vector3.zero;
        }
        else
        {
            if(character.stateMachine.ChangeState(character.idleState))
                return;
        }
    }
}
