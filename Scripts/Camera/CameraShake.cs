using System;
using System.Timers;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShake : MonoBehaviour
{
    private CameraData _cameraData;
    
    private float _timer;
    
    private void Start()
    {
        _cameraData = gameObject.GetComponent<CameraData>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
    }

    public void ShakeCamera()
    {
        _timer = 0;
        
        var position = transform.position;
        
        var x = position.x + Random.Range(-1.0f, 1.0f) * _cameraData.shakeIntensity;
        var y = position.y + Random.Range(-1.0f, 1.0f) * _cameraData.shakeIntensity;
        var z = position.z;

        if (_timer < _cameraData.shakeDuration)
        {
            transform.position = new Vector3(x, y, z);
        }
    }
}