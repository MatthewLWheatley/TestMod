using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

public class ModularProjectileEffects : GlobalProjectile
{
    public override bool InstancePerEntity => true;
    
    public bool hasBouncing = false;
    public int bouncesLeft = 0;
    
    // NEW: Homing properties
    public bool hasHoming = false;
    public float homingStrength = 0.02f; // How aggressively it homes (0.05f = weak, 0.2f = strong)
    public int homingRange = 200; // Pixel range to detect enemies
    
    public override void AI(Projectile projectile)
    {
        if (hasHoming)
        {
            DoHomingBehavior(projectile);
        }
    }
    
    private void DoHomingBehavior(Projectile projectile)
    {
        NPC target = FindNearestEnemy(projectile);
        
        if (target != null)
        {
            // Calculate direction to target
            Vector2 directionToTarget = target.Center - projectile.Center;
            directionToTarget.Normalize();
            
            // Current velocity direction
            Vector2 currentDirection = projectile.velocity;
            currentDirection.Normalize();
            
            // Smoothly rotate toward target (this is the "strength" part)
            Vector2 newDirection = Vector2.Lerp(currentDirection, directionToTarget, homingStrength);
            newDirection.Normalize();
            
            // Apply new direction while maintaining speed
            float currentSpeed = projectile.velocity.Length();
            projectile.velocity = newDirection * currentSpeed;
        }
    }
    
    private NPC FindNearestEnemy(Projectile projectile)
    {
        NPC closestNPC = null;
        float closestDistance = homingRange;
        
        for (int i = 0; i < Main.maxNPCs; i++)
        {
            NPC npc = Main.npc[i];
            
            if (npc.active && !npc.friendly && npc.lifeMax > 5) // Ignore critters
            {
                float distance = Vector2.Distance(projectile.Center, npc.Center);
                
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestNPC = npc;
                }
            }
        }
        
        return closestNPC;
    }
    
    public override bool OnTileCollide(Projectile projectile, Vector2 oldVelocity)
    {
        if (hasBouncing && bouncesLeft > 0)
        {
            bouncesLeft--;
            
            // Bounce logic
            if (projectile.velocity.X != oldVelocity.X)
                projectile.velocity.X = -oldVelocity.X;
            if (projectile.velocity.Y != oldVelocity.Y)
                projectile.velocity.Y = -oldVelocity.Y;
                
            return false; // Don't die on collision
        }
        
        return base.OnTileCollide(projectile, oldVelocity);
    }
}