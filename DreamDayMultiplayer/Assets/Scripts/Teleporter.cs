using UnityEngine;

public class Teleporter : MonoBehaviour
{
    #region Variables
    [SerializeField] private Transform destinationTransform;
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //If we make a collision with an object
            //that has a Player tag, then teleport
            //our player.
            TeleportPlayer(other.transform, destinationTransform);
        }
    }

    //Function that teleports our specified player transform
    //to the specified position.
    private void TeleportPlayer(Transform targetPlayer, Transform teleportDestination)
    {
        targetPlayer.position = teleportDestination.position;
        targetPlayer.rotation = teleportDestination.rotation;
    }
}
