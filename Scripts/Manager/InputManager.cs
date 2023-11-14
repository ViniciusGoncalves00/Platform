using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static bool MoveInput { get; private set; }
    public static bool JumpInput { get; private set; }
    public static bool DashInput { get; private set; }
    public static bool SlideInput { get; private set; }
    
    private void Update()
    {
        MoveInput = Input.GetButton("Horizontal");
        JumpInput = Input.GetKeyDown(KeyCode.Space);
        DashInput = Input.GetKeyDown(KeyCode.LeftShift);
        SlideInput = Input.GetKeyDown(KeyCode.C);
    }
}