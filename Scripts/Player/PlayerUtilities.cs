using System;
using UnityEngine;

public struct PlayerUtilities
{
    public static void InGroundAcceleration(Player player, float typeAcceleration, float typeDeceleration)
    {
        var targetVelocity = player.MoveInput * player.Data.runMaxVelocity;
        var currentVelocity = player.Rigidbody2D.velocity;

        if (Math.Sign(currentVelocity.x) != Math.Sign(targetVelocity) && currentVelocity.x != 0) // sinais opostos
        {
            currentVelocity = Vector3.MoveTowards(currentVelocity, Vector3.zero, typeDeceleration * Time.deltaTime);
        }
        else
        {
            currentVelocity = Vector3.MoveTowards(currentVelocity, Vector3.right * targetVelocity, typeAcceleration * Time.deltaTime);
        }
        
        player.Rigidbody2D.velocity = currentVelocity;
        
        // if (Math.Sign(player.Rigidbody2D.velocity.x) != Math.Sign(player.Data.MaxVelocity))
        // {
        //     player.Rigidbody2D.velocity += new Vector2(player.MoveInput * typeAcceleration * Time.deltaTime, 0);
        // }
        
        // if (player.Rigidbody2D.velocity.x > -player.Data.MaxVelocity && player.Rigidbody2D.velocity.x < player.Data.MaxVelocity)
        // {
        //     player.Rigidbody2D.velocity += new Vector2(player.MoveInput * typeAcceleration * Time.deltaTime, 0);
        // }
        //
        // if (player.Rigidbody2D.velocity.x > player.Data.MaxVelocity || player.Rigidbody2D.velocity.x < -player.Data.MaxVelocity)
        // {
        //     player.Rigidbody2D.velocity = new Vector2(player.MoveInput * typeAcceleration, 0);
        // }
    }

    public static void InAirAcceleration(Player player, float typeAcceleration)
    {
        var targetVelocity = player.MoveInput * player.Data.runMaxVelocity;
        var currentVelocity = player.Rigidbody2D.velocity;
        
        if (Math.Sign(currentVelocity.x) != Math.Sign(targetVelocity) && currentVelocity.x != 0) // sinais opostos
        {
            currentVelocity = Vector3.MoveTowards(currentVelocity, Vector3.zero, typeAcceleration * Time.deltaTime);
        }
        else
        {
            currentVelocity = Vector3.MoveTowards(currentVelocity, Vector3.right * targetVelocity, typeAcceleration * Time.deltaTime);
        }
        
        player.Rigidbody2D.velocity = new Vector2(currentVelocity.x ,player.Rigidbody2D.velocity.y) ;
        
        // if (player.Rigidbody2D.velocity.x > -player.Data.MaxVelocity && player.Rigidbody2D.velocity.x < player.Data.MaxVelocity)
        // {
        //     player.Rigidbody2D.velocity += new Vector2(player.MoveInput * typeAcceleration * Time.deltaTime, 0);
        // }
    }
    
    public static void CheckDirectionToFace(Player player,bool isMovingRight)
    {
        player.GetComponent<SpriteRenderer>().flipX = !isMovingRight;
    }
    
    public static void SetNewVelocity(Player player, float x, float y)
    {
        player.Rigidbody2D.velocity = new Vector2(x, y);
    }

    public static void SetGravityScale(Player player, float scale)
    {
        player.Rigidbody2D.gravityScale = scale;
    }
}
