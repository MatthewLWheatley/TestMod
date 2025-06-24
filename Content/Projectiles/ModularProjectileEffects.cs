using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using TestMod.Content.Items;

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

    public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (projectile.owner >= 0 && projectile.owner < Main.maxPlayers)
        {
            Player player = Main.player[projectile.owner];
            Item heldItem = player.HeldItem;

            if (heldItem.ModItem is BaseModularGun modularGun && modularGun.IsComplete())
            {
                var damageTier = modularGun.GetSpecialEffectTier();
                float tierMultiplier = (float)damageTier; // 1x, 2x, or 4x

                // Base durations (in frames, 60 = 1 second)
                int baseDuration = 300; // 5 seconds
                int scaledDuration = (int)(baseDuration * tierMultiplier);

                switch (modularGun.damageTypeModifier)
                {
                    case 0: // Fire - longer burn + chance for stronger debuff
                        target.AddBuff(BuffID.OnFire, scaledDuration);
                        if (damageTier >= BaseModularGun.ModifierTier.Elite && Main.rand.NextFloat() < 0.3f)
                        {
                            target.AddBuff(BuffID.CursedInferno, scaledDuration / 2); // Elite+: chance for cursed fire
                        }
                        break;

                    case 1: // Water - longer slow + chance for freeze
                        target.AddBuff(BuffID.Slow, scaledDuration);
                        if (damageTier >= BaseModularGun.ModifierTier.Perfect && Main.rand.NextFloat() < 0.2f)
                        {
                            target.AddBuff(BuffID.Frozen, 60); // Perfect: chance to freeze for 1 second
                        }
                        break;

                    case 2: // Lightning - longer ichor + chance for electrified
                        target.AddBuff(BuffID.Ichor, scaledDuration);
                        if (damageTier >= BaseModularGun.ModifierTier.Elite && Main.rand.NextFloat() < 0.25f)
                        {
                            target.AddBuff(BuffID.Electrified, scaledDuration / 3);
                        }
                        break;

                    case 3: // Earth - longer bleeding + chance for broken armor
                        target.AddBuff(BuffID.Bleeding, scaledDuration);
                        if (damageTier >= BaseModularGun.ModifierTier.Perfect && Main.rand.NextFloat() < 0.2f)
                        {
                            target.AddBuff(BuffID.BrokenArmor, scaledDuration / 2);
                        }
                        break;

                    case 4: // Wind - knockback already handled, but add confusion at higher tiers
                        if (damageTier >= BaseModularGun.ModifierTier.Elite && Main.rand.NextFloat() < 0.15f)
                        {
                            target.AddBuff(BuffID.Confused, scaledDuration / 4);
                        }
                        break;

                    case 5: // Slime - longer poison + chance for stronger poison
                        target.AddBuff(BuffID.Poisoned, scaledDuration);
                        if (damageTier >= BaseModularGun.ModifierTier.Perfect && Main.rand.NextFloat() < 0.3f)
                        {
                            target.AddBuff(BuffID.Venom, scaledDuration / 2);
                        }
                        break;
                }

                // Life steal effect
                if (modularGun.specialEffectModifier == 3 || modularGun.specialEffectModifier == 8 || modularGun.specialEffectModifier == 13) // Life Steal modifier
                {

                    var specialTier = modularGun.GetSpecialEffectTier(); // You'll need to make this public
                    float stealRate = 0.02f * (float)specialTier;

                    int healAmount = (int)(damageDone * stealRate);
                    if (healAmount > 0)
                    {
                        player.statLife += healAmount;
                        if (player.statLife > player.statLifeMax2)
                            player.statLife = player.statLifeMax2;
                        player.HealEffect(healAmount);
                    }
                }
            }
        }
    }
}