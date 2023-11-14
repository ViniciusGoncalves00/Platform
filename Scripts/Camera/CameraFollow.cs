using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] internal CameraData cameraData;
    [SerializeField] internal Camera cam;

    internal Vector3 PlayerPosition;
    internal Vector2 PreviousPlayerPosition;

    internal Vector3 BottomLeft;
    internal Vector3 TopLeft;
    internal Vector3 TopRight;
    internal Vector3 BottomRight;

    internal readonly Vector3[] RectFollowLimit = new Vector3[4];
    internal readonly Vector3[] RectOutSceneLimit = new Vector3[4];

    private CameraState _cameraState;

    private Vector2 _offset;
    private Vector2 _offsetOutScene;
    private float _clipPlane;

    private enum CameraState
    {
        FollowPlayer = 1,
        FollowGuide = 2
    }

    private void Start()
    {
        _cameraState = CameraState.FollowPlayer;

        _offset = cameraData.offset;
        _offsetOutScene = cameraData.offsetOutScene;
        _clipPlane = cam.nearClipPlane;
    }

    private void Update()
    {
        switch (_cameraState)
        {
            case CameraState.FollowPlayer:
                FollowPlayer();
                break;
            case CameraState.FollowGuide:
                FollowGuide();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void FollowPlayer()
    {
        OutSceneLimit();

        DontFollowLimit();

        PlayerPosition = cameraData.player.transform.position;

        PreviousPlayerPosition.x = Mathf.Lerp(PreviousPlayerPosition.x, PlayerPosition.x,
            Time.deltaTime * cameraData.camSpeedToFollowPlayer);
        PreviousPlayerPosition.y = Mathf.Lerp(PreviousPlayerPosition.y, PlayerPosition.y,
            Time.deltaTime * cameraData.camSpeedToFollowPlayer);

        var x = PlayerPosition.x;
        var y = PlayerPosition.y;
        var z = -10.0f;

        var targetPosition = new Vector3(x, y, z);

        if (PreviousPlayerPosition.x < RectFollowLimit[0].x || PreviousPlayerPosition.y < RectFollowLimit[0].y ||
            PreviousPlayerPosition.x > RectFollowLimit[2].x || PreviousPlayerPosition.y > RectFollowLimit[2].y)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition,
                cameraData.camSpeedToFollowPlayer * Time.deltaTime);
        }
    }

    public void DontFollowLimit()
    {
        RectFollowLimit[0] = cam.ViewportToWorldPoint(new Vector3(0 + _offset.x, 0 + _offset.y, _clipPlane));
        RectFollowLimit[1] = cam.ViewportToWorldPoint(new Vector3(0 + _offset.x, 1 - _offset.y, _clipPlane));
        RectFollowLimit[2] = cam.ViewportToWorldPoint(new Vector3(1 - _offset.x, 1 - _offset.y, _clipPlane));
        RectFollowLimit[3] = cam.ViewportToWorldPoint(new Vector3(1 - _offset.x, 0 + _offset.y, _clipPlane));
    }

    public void OutSceneLimit()
    {
        RectOutSceneLimit[0] = cam.ViewportToWorldPoint(new Vector3(0 + _offsetOutScene.x, 0 + _offsetOutScene.y, _clipPlane));
        RectOutSceneLimit[1] = cam.ViewportToWorldPoint(new Vector3(0 + _offsetOutScene.x, 1 - _offsetOutScene.y, _clipPlane));
        RectOutSceneLimit[2] = cam.ViewportToWorldPoint(new Vector3(1 - _offsetOutScene.x, 1 - _offsetOutScene.y, _clipPlane));
        RectOutSceneLimit[3] = cam.ViewportToWorldPoint(new Vector3(1 - _offsetOutScene.x, 0 + _offsetOutScene.y, _clipPlane));
    }

    private void FollowGuide()
    {
    }
}