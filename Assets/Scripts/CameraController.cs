using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject lookAtTarget;
    public Vector3 targetOffset;
    public Vector3 cameraOffset;

    void LateUpdate()
    {
        if(lookAtTarget != null)
        {
            Vector3 targetPosition = lookAtTarget.transform.position + targetOffset;
            Vector3 cameraPosition = targetPosition + cameraOffset;
            Vector3 targetDirection = (targetPosition - cameraPosition).normalized;

            transform.position = cameraPosition;
            transform.rotation = Quaternion.LookRotation(targetDirection);
        }
    }
}
