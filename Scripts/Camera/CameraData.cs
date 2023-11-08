using UnityEngine;

public class CameraData : MonoBehaviour
{
    [Space(5)]
    [Header("Size")]
    [SerializeField] internal int camSmallSize = 6;
    [SerializeField] internal int camMediumSize = 9;
    [SerializeField] internal int camLargeSize = 15;
    
    [Space(5)]
    [Header("Speed")]
    [SerializeField] internal float camSpeedToSmall = 1.0f;
    [SerializeField] internal float camSpeedToMedium = 2.5f;
    [SerializeField] internal float camSpeedToLarge = 4.0f;

    [Space(10)]
    [SerializeField] internal float camSpeedToFollowPlayer = 3.0f;
    
    [Space(5)]
    [Header("Time")]
    [SerializeField] internal float timeToSwitchSize = 0.5f;

    [Space(5)]
    [Header("Shake")]
    [SerializeField] internal float shakeDuration = 1.0f;
    [SerializeField] internal float shakeIntensity = 1.0f;
    [SerializeField] internal float shakeTimer = 1.0f;

    [Space(5)]
    [Header("Player")]
    [SerializeField] internal Player player;
    
    [Space(5)]
    [Header("Physics")]
    [SerializeField] internal LayerMask groundLayer;
    [SerializeField] internal Collider2D colliderSmall;
    [SerializeField] internal Collider2D colliderMedium;
}