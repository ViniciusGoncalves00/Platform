using UnityEngine;

public class GizmosManager : MonoBehaviour
{
    public static bool ShowFollowLimit { get; private set; }
    public static bool ShowOutSceneLimit { get; private set; }
    public static bool ShowPlayerGroundTrigger { get; private set; }
    public static bool ShowEnemyGroundTrigger { get; private set; }
    public static bool ShowChaseTrigger { get; private set; }
    public static bool ShowAttackTrigger { get; private set; }
    public static bool ShowTouchTrigger { get; private set; }
    public static bool ShowWaypoints { get; private set; }
    
    
    [Header("Camera")]
    public bool showFollowLimit;
    public bool showOutSceneLimit;
    
    [Header("Player")]
    public bool showPlayerGroundTrigger;

    [Header("Enemies")]
    public bool showEnemyGroundTrigger;
    public bool showChaseTrigger;
    public bool showAttackTrigger;
    public bool showWaypoints;
    

    private void OnValidate()
    {
        ShowFollowLimit = showFollowLimit;
        ShowOutSceneLimit = showOutSceneLimit;
        ShowPlayerGroundTrigger = showPlayerGroundTrigger;
        ShowEnemyGroundTrigger = showEnemyGroundTrigger;
        ShowChaseTrigger = showChaseTrigger;
        ShowAttackTrigger = showAttackTrigger;
        ShowTouchTrigger = showTouchTrigger;
        ShowWaypoints = showWaypoints;
    }
}
