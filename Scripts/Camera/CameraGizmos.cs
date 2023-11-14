#if UNITY_EDITOR
using UnityEngine;

public class CameraGizmos : MonoBehaviour
{
    [SerializeField] private CameraFollow camFollow;
    [SerializeField] private Camera cam;

    private readonly Vector2[] _viewportCorner = { new(0.0f, 0.0f), new(0.0f, 1.0f), new(1.0f, 1.0f), new(1.0f, 0.0f) };

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

        camFollow.OutSceneLimit();
        camFollow.DontFollowLimit();

        if (GizmosManager.ShowFollowLimit)
        {
            Gizmos.color = Color.yellow;

            Gizmos.DrawLineStrip(camFollow.RectFollowLimit, true);

            for (int i = 0; i < camFollow.RectFollowLimit.Length; i++)
            {
                Gizmos.DrawLine(camFollow.RectFollowLimit[i], camFollow.RectOutSceneLimit[i]);
            }
        }

        if (GizmosManager.ShowOutSceneLimit)
        {
            Gizmos.color = Color.red;

            Gizmos.DrawLineStrip(camFollow.RectOutSceneLimit, true);

            for (int i = 0; i < camFollow.RectOutSceneLimit.Length; i++)
            {
                Gizmos.DrawLine(camFollow.RectOutSceneLimit[i], cam.ViewportToWorldPoint(_viewportCorner[i]));
            }
        }
    }
}
#endif