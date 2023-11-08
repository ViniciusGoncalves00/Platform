using System;
using System.Collections.Generic;
using UnityEngine;

public class CameraSize : MonoBehaviour
{
    private CameraData _cameraData;
    private Camera _camera;
    
    private List<Collider2D> _listSmall = new();
    private List<Collider2D> _listMedium = new();
    private ContactFilter2D _groundContactFilter;
    
    private float _timer;

    private float _newCamSize;
    private float _newCamSpeed;

    private CamSize _actualSize;
    private CamSize _newSize;

    private enum CamSize
    {
        Small = 1,
        Medium = 2,
        Large = 3
    }

    private void Awake()
    {
        _cameraData = gameObject.GetComponent<CameraData>();
        _camera = gameObject.GetComponent<Camera>();
    }

    private void Start()
    {
        _groundContactFilter = new ContactFilter2D
        {
            useLayerMask = true,
            layerMask = _cameraData.groundLayer
        };
    }

    private void FixedUpdate()
    {
        Physics2D.OverlapCollider(_cameraData.colliderSmall, _groundContactFilter, _listSmall);
        Physics2D.OverlapCollider(_cameraData.colliderMedium, _groundContactFilter, _listMedium);
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        NewCamSize();
        ShouldResetTimer();

        if (_timer > _cameraData.timeToSwitchSize)
        {
            SetNewCamSize();
        }
    }

    private void NewCamSize()
    {
        if (_listSmall.Count != 0)
        {
            _newSize = CamSize.Small;
        }
        else if (_listMedium.Count != 0)
        {
            _newSize = CamSize.Medium;
        }
        else
        {
            _newSize = CamSize.Large;
        }
    }

    private void ShouldResetTimer()
    {
        if (_actualSize != _newSize)
        {
            _timer = 0;
            _actualSize = _newSize;
            
            GetNewCamSize();
        }
    }

    private void GetNewCamSize()
    {
        switch (_actualSize)
        {
            case CamSize.Small:
                _newCamSize = _cameraData.camSmallSize;
                _newCamSpeed = _cameraData.camSpeedToSmall;
                break;
            case CamSize.Medium:
                _newCamSize = _cameraData.camMediumSize;
                _newCamSpeed = _cameraData.camSpeedToMedium;
                break;
            case CamSize.Large:
                _newCamSize = _cameraData.camLargeSize;
                _newCamSpeed = _cameraData.camSpeedToLarge;
                break;
            default:
                _newCamSize = _camera.orthographicSize;
                break;
        }
    }

    private void SetNewCamSize()
    {
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _newCamSize, Time.deltaTime / _newCamSpeed);
    }
}