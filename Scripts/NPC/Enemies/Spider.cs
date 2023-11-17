using UnityEngine;

public class Spider : GroundEnemy
{
    public override void Search()
    {
        base.Search();
        
        if (Vector2.Distance(Rigidbody2D.position, Target) < 0.1f)
        {
            RandomIndex = Random.Range(0, waypoints.Length);
        }
        
        Target = new Vector2(waypoints[RandomIndex].x, Rigidbody2D.position.y);
        
        Rigidbody2D.position = Vector3.MoveTowards(Rigidbody2D.position, Target, npcSo.velocitySearch * Time.deltaTime);
    }
    
    public override void Chase()
    {
        base.Chase();

        var target = Vector3.MoveTowards(Rigidbody2D.position, Player.Rigidbody2D.position, npcSo.velocityChase * Time.deltaTime);
        Rigidbody2D.position = new Vector2(target.x, Rigidbody2D.position.y);
    }
    
    public override void Attack()
    {
        base.Attack();
    }
}