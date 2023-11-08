using UnityEngine;

public class PlayerIllumination : MonoBehaviour
{
    public GameObject lightFront;
    public GameObject lightBack;
    public Player player;
    public Quaternion playerRotation;

    private void Update()
    {
        playerRotation = player.transform.rotation;
        lightFront.transform.eulerAngles = new Vector3(playerRotation.x, playerRotation.y, playerRotation.z + -90 * player.LookDirection);
        lightBack.transform.eulerAngles = new Vector3(playerRotation.x, playerRotation.y, playerRotation.z + 90 * player.LookDirection);
    }
}
