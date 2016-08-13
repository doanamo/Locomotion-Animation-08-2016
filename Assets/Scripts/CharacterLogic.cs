using UnityEngine;
using System.Collections;

public class CharacterLogic : MonoBehaviour
{
    public new Rigidbody rigidbody;
    public StateMachine stateMachine;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        stateMachine = new StateMachine();
        stateMachine.ChangeState(new IdleState(this));
    }

    public void HandleCommand<Type>(Type command)
    {
        stateMachine.HandleCommand(command);
    }

    void FixedUpdate()
    {
        stateMachine.Update();
    }
}
