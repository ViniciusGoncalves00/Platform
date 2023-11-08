using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private CameraData _cameraData;

    private void Start()
    {
        _cameraData = gameObject.GetComponent<CameraData>();
    }

    private void Update()
    {
        var playerPosition = _cameraData.player.transform.position;

        var x = playerPosition.x;
        var y = playerPosition.y + 1.0f;
        var z = -10.0f;
        
        var targetPosition = new Vector3(x, y, z);
        
        transform.position = Vector3.Lerp(transform.position, targetPosition, _cameraData.camSpeedToFollowPlayer * Time.deltaTime);
    }
}
