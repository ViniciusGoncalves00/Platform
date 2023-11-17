using UnityEngine;

public sealed class GizmosExtend
{
    public static void DrawArea(Vector2 leftBottom, Vector2 rightTop)
    {
        var leftTop = new Vector2(leftBottom.x, rightTop.y);
        var rightBottom = new Vector2(rightTop.x, leftBottom.y);
            
        Gizmos.DrawLine(leftBottom, leftTop);
        Gizmos.DrawLine(leftTop, rightTop);
        Gizmos.DrawLine(rightTop,rightBottom);
        Gizmos.DrawLine(rightBottom, leftBottom);
    }
}