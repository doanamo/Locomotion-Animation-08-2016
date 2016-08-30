using UnityEngine;

public class Utility
{
    // Gets a signed angle between two vectors along an axis.
    public static float AngleSigned(Vector3 from, Vector3 to, Vector3 axis)
    {
        return Mathf.Atan2(
            Vector3.Dot(axis, Vector3.Cross(from, to)),
            Vector3.Dot(from, to)) * Mathf.Rad2Deg;
    }
}
