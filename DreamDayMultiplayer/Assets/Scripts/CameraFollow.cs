using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region Variables
    [SerializeField] private Transform transformToFollow;
    [SerializeField] private Vector3 offset;
    #endregion

    private void FixedUpdate() {
        //Setting our camera position to the transform that
        //we want to follow's position with an added offset.
        transform.position = transformToFollow.position + offset;
    }
}
