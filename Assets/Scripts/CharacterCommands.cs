using UnityEngine;
using System.Collections;

public class MoveCommand : ScriptableObject
{
    public Vector3 direction;

    public static MoveCommand CreateInstance(Vector3 direction)
    {
        var instance = ScriptableObject.CreateInstance<MoveCommand>();
        instance.direction = direction;
        return instance;
    }
}
