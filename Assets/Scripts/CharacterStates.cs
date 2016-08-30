using UnityEngine;
using System.Collections;

public class StandingState : State
{
    private CharacterLogic character;
    private Animator animator;

    private float lingerTimer;

    public StandingState(CharacterLogic character)
    {
        this.character = character;
        animator = character.GetComponent<Animator>();
    }

    public override bool OnEnter(State previousState)
    {
        lingerTimer = Random.Range(2.0f, 6.0f);
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
        // Trigger a linger animation.
        lingerTimer = Mathf.MoveTowards(lingerTimer, 0.0f, Time.fixedDeltaTime);

        if(lingerTimer == 0.0f)
        {
            animator.SetTrigger("Linger");
            lingerTimer = Random.Range(4.0f, 10.0f);
        }
    }
}

[System.Serializable]
public class MovingState : State
{
    private CharacterLogic character;
    private Transform transform;
    private Rigidbody rigidbody;
    private Animator animator;

    public MoveCommand command;
    private bool commandReceived;

    public ControllerPID headingAngleController;
    public ControllerPID angularVelocityController;

    public MovingState(CharacterLogic character)
    {
        this.character = character;
        transform = character.GetComponent<Transform>();
        rigidbody = character.GetComponent<Rigidbody>();
        animator = character.GetComponent<Animator>();

        headingAngleController = new ControllerPID();
        angularVelocityController = new ControllerPID();
    }

    public override void HandleCommand<Type>(Type command)
    {
        if(typeof(Type) == typeof(MoveCommand))
        {
            this.command = command as MoveCommand;
            commandReceived = command != null;
        }
    }

    public override void OnUpdate()
    {
        if(commandReceived)
        {
            // Reset command received flag.
            commandReceived = false;

            // Hadnle turning the character around.
            float angle = Vector3.Angle(transform.forward, command.direction);

            if(angle >= 100.0f)
            {
                if(character.stateMachine.ChangeState(character.turningState))
                    return;
            }

            // Increase movement animation speed.
            float movementSpeed = animator.GetFloat("Movement");
            movementSpeed = Mathf.MoveTowards(movementSpeed, 1.0f, 2.0f * Time.fixedDeltaTime);
            animator.SetFloat("Movement", movementSpeed);
        }
        else
        {
            // Descrease movement animation speed.
            float movementSpeed = animator.GetFloat("Movement");
            movementSpeed = Mathf.MoveTowards(movementSpeed, 0.0f, 2.0f * Time.fixedDeltaTime);
            animator.SetFloat("Movement", movementSpeed);

            // Change state once movement speed is zero.
            if(movementSpeed == 0.0f && rigidbody.angularVelocity.magnitude <= 0.1f)
            {
                if(character.stateMachine.ChangeState(character.standingState))
                    return;
            }
        }

        // Rotate the character using physics forces regulated by two PID controllers.
        float targetAngle = Utility.AngleSigned(Vector3.forward, command.direction, Vector3.up);
        float angleError = Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle);
        float torqueAngleCorrection = headingAngleController.Update(angleError, Time.fixedDeltaTime);

        float angularVelocityError = -rigidbody.angularVelocity.y;
        float torqueAngularVelocityCorrection = angularVelocityController.Update(angularVelocityError, Time.fixedDeltaTime);

        rigidbody.AddTorque(transform.up * (torqueAngleCorrection + torqueAngularVelocityCorrection));
    }
}

public class TurningState : State
{
    State previousState;

    private CharacterLogic character;
    private Animator animator;

    MoveCommand cachedCommand;
    bool animationStarted;

    public TurningState(CharacterLogic character)
    {
        this.character = character;
        animator = character.GetComponent<Animator>();
    }

    public override bool OnEnter(State previousState)
    {
        this.previousState = previousState;

        cachedCommand = null;

        animator.SetTrigger("Turn");
        animationStarted = false;

        return true;
    }

    public override void HandleCommand<Type>(Type command)
    {
        if(typeof(Type) == typeof(MoveCommand))
        {
            cachedCommand = command as MoveCommand;
        }
    }

    public override void OnUpdate()
    {
        if(!animationStarted)
        {
            // Check if the animation started playing.
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("Turn"))
            {
                animationStarted = true;

                // Make the turning animation always end with a constant movement speed.
                animator.SetFloat("Movement", 0.6f);
            }
        }
        else
        {
            // Check if the turning animation is about to transist out.
            if(animator.IsInTransition(0))
            {
                if(character.stateMachine.ChangeState(previousState))
                {
                    previousState.HandleCommand(cachedCommand);
                    return;
                }
            }
        }
    }
}
