using UnityEngine;

public class PlayerData : MonoBehaviour, IDataPersistence
{
    [Header("Health")]
    [Range(0,10)] public float health = 3.0f;
    
    [Header("Energy")]
    [Range(0,10)] public float energy = 3.0f;
    
    [Header("Movement")]
    [Range(0,50)] public float runAcceleration = 1.0f;
    [Range(0,50)] public float runDeceleration = 1.0f;
    [Range(0,50)] public float runMaxVelocity = 1.0f;
    
    [Header("Jump")]
    [Range(0,1000)] public float jumpForce = 1.0f;
    [Space(5)]
    
    [Space(5)]
    [Range(0,20)] public float airAcceleration = 1.0f;
    [Range(0,20)] public float fallMaxVelocity = 10.0f;
    [Space(5)]
    [Range(1,2)] public int maxJump = 1;
    
    [Space(5)]
    [Header("Dash")]
    [Range(0,1000)] public float dashForce = 200.0f;
    [Range(1,2)] public int dashMax = 1;
    
    [Header("Gravity")]
    [HideInInspector] public float withoutGravity;
    [Tooltip("Sets the gravity when the player is rising into the air")]
    [Range(0,10)] public float gravityUp = 1.0f;
    [Tooltip("Sets the gravity when the player is falling into the air")] 
    [Range(0,10)] public float gravityFall = 1.0f;
    [Tooltip("Sets the gravity below the Gravity Up, when the player is near to max height of her jump, to maintaining the player a few more time in air")] 
    [Range(0,1)] public float gravityPeak = 1.0f;

    [Header("Unlocks")]
    public bool UnlockedDash;
    public bool UnlockedDoubleJump;
    public bool UnlockedSlide;

    private void OnValidate()
    {
        
    }
    
    public PlayerData()
    {
        health = 3.0f;
        energy = 3.0f;
        maxJump = 1;
        dashMax = 1;
    }

    public void LoadData(PlayerData data)
    {
        health = data.health;
        energy = data.energy;
        maxJump = data.maxJump;
        dashMax = data.dashMax;
    }

    public void SaveData(ref PlayerData data)
    {
        data.health = health;
        data.energy = energy;
        data.maxJump = maxJump;
        data.dashMax = dashMax;
    }
}
