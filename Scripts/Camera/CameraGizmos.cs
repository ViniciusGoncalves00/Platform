using System;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraGizmos : MonoBehaviour
{
    [SerializeField] private CameraFollow camFollow;
    [SerializeField] private Camera cam;

    private void Start()
    {
        camFollow = gameObject.GetComponent<CameraFollow>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)), 0.2f);
        Gizmos.DrawWireSphere(camFollow.PreviousPlayerPosition, 0.3f);
        Gizmos.DrawWireSphere(camFollow.PlayerPosition, 0.2f);
        Gizmos.DrawLine(camFollow.PreviousPlayerPosition, camFollow.PlayerPosition);

        #region Window Constraint

        Gizmos.DrawLine(camFollow.RectFollowLimit[0], camFollow.RectFollowLimit[1]);
        Gizmos.DrawLine(camFollow.RectFollowLimit[1], camFollow.RectFollowLimit[2]);
        Gizmos.DrawLine(camFollow.RectFollowLimit[2], camFollow.RectFollowLimit[3]);
        Gizmos.DrawLine(camFollow.RectFollowLimit[3], camFollow.RectFollowLimit[0]);

        Gizmos.DrawLine(camFollow.RectFollowLimit[0], camFollow.RectOutSceneLimit[0]);
        Gizmos.DrawLine(camFollow.RectFollowLimit[1], camFollow.RectOutSceneLimit[1]);
        Gizmos.DrawLine(camFollow.RectFollowLimit[2], camFollow.RectOutSceneLimit[2]);
        Gizmos.DrawLine(camFollow.RectFollowLimit[3], camFollow.RectOutSceneLimit[3]);

        #endregion

        Gizmos.color = Color.red;

        Gizmos.DrawLine(camFollow.RectOutSceneLimit[0], camFollow.RectOutSceneLimit[1]);
        Gizmos.DrawLine(camFollow.RectOutSceneLimit[1], camFollow.RectOutSceneLimit[2]);
        Gizmos.DrawLine(camFollow.RectOutSceneLimit[2], camFollow.RectOutSceneLimit[3]);
        Gizmos.DrawLine(camFollow.RectOutSceneLimit[3], camFollow.RectOutSceneLimit[0]);

        Gizmos.DrawLine(camFollow.RectOutSceneLimit[0], cam.ViewportToWorldPoint(new Vector3(0, 0, 0)));
        Gizmos.DrawLine(camFollow.RectOutSceneLimit[1], cam.ViewportToWorldPoint(new Vector3(0, 1, 0)));
        Gizmos.DrawLine(camFollow.RectOutSceneLimit[2], cam.ViewportToWorldPoint(new Vector3(1, 1, 0)));
        Gizmos.DrawLine(camFollow.RectOutSceneLimit[3], cam.ViewportToWorldPoint(new Vector3(1, 0, 0)));
    }
}