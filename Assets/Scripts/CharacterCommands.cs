using UnityEngine;
using System.Collections;

public class MoveCommand
{
    public Vector3 direction
    {
        get; private set;
    }

    public MoveCommand(Vector3 direction)
    {
        this.direction = direction;
    }
}
