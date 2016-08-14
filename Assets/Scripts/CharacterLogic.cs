using UnityEngine;
using System.Collections;

public class CharacterLogic : MonoBehaviour
{
    [HideInInspector] public new Rigidbody rigidbody;
    [HideInInspector] public Animator animator;

    public StateMachine stateMachine;

    private float speed = 0.0f;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        stateMachine = new StateMachine();
        stateMachine.ChangeState(new IdleState(this));
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
