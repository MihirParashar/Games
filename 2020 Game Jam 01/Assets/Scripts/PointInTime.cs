using UnityEngine;

public class PointInTime
{
    public Vector3 position;
    public Quaternion rotation;
    public bool isCrouching;

    public PointInTime(Vector3 _position, Quaternion _rotation, bool _isCrouching)
    {
        position = _position;
        rotation = _rotation;
        isCrouching = _isCrouching;
    }
}
