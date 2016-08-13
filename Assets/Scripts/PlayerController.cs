using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public CharacterLogic character;

    void Start()
    {
        Assert.IsNotNull(character, "Player controlled character is null!");
    }

    void Update()
    {
        Vector3 direction = new Vector3();
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");
        direction.Normalize();

        if(direction != Vector3.zero)
        {
            character.HandleCommand(new MoveCommand(direction));
        }
    }
}

