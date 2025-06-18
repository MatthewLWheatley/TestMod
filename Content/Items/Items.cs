using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.DataStructures;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using TestMod.Content.Systems;


namespace TestMod.Content.Items
{
    public class ModularGun : ModItem
    {
        // NEW: 4-category modifier system
        public int ammoTypeModifier = -1;      // Magic, Arrow, Bullet, Rocket
        public int damageTypeModifier = -1;    // Fire, Water, Lightning, Earth, Wind, Slime
        public int shotTypeModifier = -1;      // Auto Fire, Burst Fire, Charge Fire
        public int specialEffectModifier = -1; // Piercing, Bouncing, Homing, Life Steal, Crit Boost

        // Base weapon stats
        private int baseDamage = 15;
        private float baseKnockback = 2f;
        private int baseCrit = 4;
        private int baseUseTime = 30;

        // Point budget properties
        public string weaponTier = "Copper";
        public int maxPointBudget = 8; // Updated to new budget system

        public override void SetDefaults()
        {
            Item.damage = baseDamage;
            Item.DamageType = DamageClass.Magic;
            Item.width = 40;
            Item.height = 20;
            Item.useTime = baseUseTime;
            Item.useAnimation = baseUseTime;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = baseKnockback;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = false;
            Item.shoot = ProjectileID.MagicMissile;
            Item.shootSpeed = 16f;
            Item.mana = 10;
            Item.crit = baseCrit;
            weaponTier = "Copper";
            maxPointBudget = ModifierData.GetWeaponPointBudget(weaponTier);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 2);
        }

        public int GetCurrentPointUsage()
        {
            int total = 0;

            if (ammoTypeModifier != -1)
            {
                int itemType = GetItemTypeFromModifier(ammoTypeModifier, "ammo");
                total += ModifierData.GetModifierPointCost(itemType);
            }

            if (damageTypeModifier != -1)
            {
                int itemType = GetItemTypeFromModifier(damageTypeModifier, "damage");
                total += ModifierData.GetModifierPointCost(itemType);
            }

            if (shotTypeModifier != -1)
            {
                int itemType = GetItemTypeFromModifier(shotTypeModifier, "shot");
                total += ModifierData.GetModifierPointCost(itemType);
            }

            if (specialEffectModifier != -1)
            {
                int itemType = GetItemTypeFromModifier(specialEffectModifier, "special");
                total += ModifierData.GetModifierPointCost(itemType);
            }

            return total;
        }

        public bool CanInstallModifier(int itemType, string modifierType)
        {
            int modifierCost = ModifierData.GetModifierPointCost(itemType);
            int currentUsage = GetCurrentPointUsage();

            // Subtract current modifier cost if replacing
            switch (modifierType)
            {
                case "ammo":
                    if (ammoTypeModifier != -1)
                    {
                        int currentType = GetItemTypeFromModifier(ammoTypeModifier, "ammo");
                        currentUsage -= ModifierData.GetModifierPointCost(currentType);
                    }
                    break;
                case "damage":
                    if (damageTypeModifier != -1)
                    {
                        int currentType = GetItemTypeFromModifier(damageTypeModifier, "damage");
                        currentUsage -= ModifierData.GetModifierPointCost(currentType);
                    }
                    break;
                case "shot":
                    if (shotTypeModifier != -1)
                    {
                        int currentType = GetItemTypeFromModifier(shotTypeModifier, "shot");
                        currentUsage -= ModifierData.GetModifierPointCost(currentType);
                    }
                    break;
                case "special":
                    if (specialEffectModifier != -1)
                    {
                        int currentType = GetItemTypeFromModifier(specialEffectModifier, "special");
                        currentUsage -= ModifierData.GetModifierPointCost(currentType);
                    }
                    break;
            }

            return (currentUsage + modifierCost) <= maxPointBudget;
        }

        // Check if weapon has required modifiers (first 3 slots, special is optional)
        public bool IsComplete()
        {
            return ammoTypeModifier != -1 &&
                   damageTypeModifier != -1 &&
                   shotTypeModifier != -1;
            // Special effect is optional
        }

        // Prevent shooting if incomplete or over budget
        public override bool CanUseItem(Player player)
        {
            if (GetCurrentPointUsage() > maxPointBudget)
            {
                Main.NewText("Weapon is over point budget!", Color.Red);
                return false;
            }

            if (!IsComplete())
            {
                Main.NewText("Weapon requires ammo, damage, and shot type modifiers!", Color.Red);
                return false;
            }
            return base.CanUseItem(player);
        }

        // Apply modifier effects when shooting
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (!IsComplete()) return false;

            // Adjust spawn position
            Vector2 spawnPosition = position + Vector2.Normalize(velocity) * 25f;

            // Apply ammo type effects
            int projectileType = GetProjectileFromAmmoType();
            
            // Apply shot type effects
            ApplyShotTypeEffects(source, spawnPosition, velocity, projectileType, damage, knockback, player);

            return false; // We handle projectile spawning manually
        }

        private int GetProjectileFromAmmoType()
        {
            return ammoTypeModifier switch
            {
                0 => ProjectileID.MagicMissile,        // Magic
                1 => ProjectileID.WoodenArrowFriendly, // Arrow
                2 => ProjectileID.Bullet,              // Bullet
                3 => ProjectileID.RocketI,             // Rocket
                _ => ProjectileID.WoodenArrowFriendly  // Default
            };
        }

        private void ApplyShotTypeEffects(EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int projectileType, int damage, float knockback, Player player)
        {
            switch (shotTypeModifier)
            {
                case 0: // Auto Fire - single shot (handled by useTime/autoReuse)
                    SpawnProjectileWithEffects(source, position, velocity, projectileType, damage, knockback, player);
                    break;
                    
                case 1: // Burst Fire - 3 projectiles with spread
                    for (int i = 0; i < 3; i++)
                    {
                        Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(15));
                        SpawnProjectileWithEffects(source, position, perturbedSpeed, projectileType, (int)(damage * 0.8f), knockback, player);
                    }
                    break;
                    
                case 2: // Charge Fire - single powerful shot (damage boost handled in ModifyWeaponDamage)
                    SpawnProjectileWithEffects(source, position, velocity, projectileType, damage, knockback, player);
                    break;
                    
                default:
                    SpawnProjectileWithEffects(source, position, velocity, projectileType, damage, knockback, player);
                    break;
            }
        }

        private void SpawnProjectileWithEffects(EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int projectileType, int damage, float knockback, Player player)
        {
            // Spawn the projectile
            int projIndex = Projectile.NewProjectile(source, position, velocity, projectileType, damage, knockback, player.whoAmI);
            
            if (projIndex >= 0 && projIndex < Main.maxProjectiles)
            {
                Projectile proj = Main.projectile[projIndex];
                
                // Apply special effects
                ApplySpecialEffects(proj);
            }
        }

        private void ApplySpecialEffects(Projectile projectile)
        {
            if (specialEffectModifier == -1) return;

            switch (specialEffectModifier)
            {
                case 0: // Piercing
                    projectile.penetrate += 1;
                    break;
                    
                case 1: // Bouncing
                    //projectile += 1;
                    break;
                    
                case 2: // Homing - would need custom projectile AI
                    // TODO: Implement homing behavior
                    break;
                    
                case 3: // Life Steal - handled in OnHitNPC
                    break;
                    
                case 4: // Crit Boost - handled in ModifyWeaponCrit
                    break;
            }
        }

        // Modify damage based on shot type and special effects
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (!IsComplete()) return;

            // Shot type damage modifiers
            switch (shotTypeModifier)
            {
                case 0: // Auto Fire - reduced damage for rapid fire
                    damage *= 0.8f;
                    break;
                case 1: // Burst Fire - already handled in shot spawning (0.8x per shot)
                    break;
                case 2: // Charge Fire - increased damage
                    damage *= 1.4f;
                    break;
            }
        }

        public override void ModifyWeaponCrit(Player player, ref float crit)
        {
            if (!IsComplete()) return;

            // Special effect: Crit Boost
            if (specialEffectModifier == 4) // Crit Boost modifier
            {
                crit += 10f; // +10% crit chance
            }
        }

        public override void ModifyWeaponKnockback(Player player, ref StatModifier knockback)
        {
            if (!IsComplete()) return;

            // Damage type knockback modifiers
            switch (damageTypeModifier)
            {
                case 4: // Wind - extra knockback
                    knockback *= 1.5f;
                    break;
            }
        }

        // Update weapon properties based on modifiers
        public override void UpdateInventory(Player player)
        {
            if (!IsComplete()) return;

            // Update damage class based on ammo type
            switch (ammoTypeModifier)
            {
                case 0: // Magic
                    Item.DamageType = DamageClass.Magic;
                    Item.useAmmo = AmmoID.None;
                    Item.mana = 10; // Consumes mana instead
                    break;
                case 1: // Arrow
                    Item.DamageType = DamageClass.Ranged;
                    Item.useAmmo = AmmoID.Arrow;
                    Item.mana = 0;
                    break;
                case 2: // Bullet
                    Item.DamageType = DamageClass.Ranged;
                    Item.useAmmo = AmmoID.Bullet;
                    Item.mana = 0;
                    break;
                case 3: // Rocket
                    Item.DamageType = DamageClass.Ranged;
                    Item.useAmmo = AmmoID.Rocket;
                    Item.mana = 0;
                    break;
            }

            // Update use time based on shot type
            switch (shotTypeModifier)
            {
                case 0: // Auto Fire
                    Item.useTime = baseUseTime / 2;
                    Item.useAnimation = baseUseTime / 2;
                    Item.autoReuse = true;
                    break;
                case 1: // Burst Fire
                    Item.useTime = baseUseTime;
                    Item.useAnimation = baseUseTime;
                    Item.autoReuse = false;
                    break;
                case 2: // Charge Fire
                    Item.useTime = baseUseTime * 2;
                    Item.useAnimation = baseUseTime * 2;
                    Item.autoReuse = false;
                    break;
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!IsComplete()) return;

            // Apply damage type debuffs
            switch (damageTypeModifier)
            {
                case 0: // Fire
                    target.AddBuff(BuffID.OnFire, 300); // 5 seconds
                    break;
                case 1: // Water
                    target.AddBuff(BuffID.Slow, 180); // 3 seconds
                    break;
                case 2: // Lightning
                    target.AddBuff(BuffID.Ichor, 240); // 4 seconds (defense reduction)
                    break;
                case 3: // Earth
                    target.AddBuff(BuffID.Bleeding, 360); // 6 seconds
                    break;
                case 4: // Wind - knockback already handled
                    break;
                case 5: // Slime
                    target.AddBuff(BuffID.Poisoned, 420); // 7 seconds
                    break;
            }

            // Handle life steal (already implemented)
            if (specialEffectModifier == 3) // Life Steal modifier
            {
                int healAmount = (int)(damageDone * 0.02f);
                if (healAmount > 0)
                {
                    player.statLife += healAmount;
                    if (player.statLife > player.statLifeMax2)
                        player.statLife = player.statLifeMax2;
                    player.HealEffect(healAmount);
                }
            }
        }

        // Save modifier data
        public override void SaveData(TagCompound tag)
        {
            tag["ammoType"] = ammoTypeModifier;
            tag["damageType"] = damageTypeModifier;
            tag["shotType"] = shotTypeModifier;
            tag["specialEffect"] = specialEffectModifier;
            tag["weaponTier"] = weaponTier;
            tag["maxPointBudget"] = maxPointBudget;
        }

        // Load modifier data
        public override void LoadData(TagCompound tag)
        {
            ammoTypeModifier = tag.GetInt("ammoType");
            damageTypeModifier = tag.GetInt("damageType");
            shotTypeModifier = tag.GetInt("shotType");
            specialEffectModifier = tag.GetInt("specialEffect");
            weaponTier = tag.GetString("weaponTier");
            maxPointBudget = tag.GetInt("maxPointBudget");

            // Fallback for old saves
            if (string.IsNullOrEmpty(weaponTier))
            {
                weaponTier = "Copper";
                maxPointBudget = ModifierData.GetWeaponPointBudget(weaponTier);
            }
        }

        // Updated tooltip for 4-slot system
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        int currentPoints = GetCurrentPointUsage();
        TooltipLine pointLine = new TooltipLine(Mod, "PointBudget",
            $"Points: {currentPoints}/{maxPointBudget} ({weaponTier} Tier)");
        pointLine.OverrideColor = currentPoints <= maxPointBudget ? Color.White : Color.Red;
        tooltips.Add(pointLine);

        // Show all 4 modifier slots
        AddModifierTooltip(tooltips, "Ammo Type", ammoTypeModifier, "ammo");
        AddModifierTooltip(tooltips, "Damage Type", damageTypeModifier, "damage");
        AddModifierTooltip(tooltips, "Shot Type", shotTypeModifier, "shot");
        AddModifierTooltip(tooltips, "Special Effect", specialEffectModifier, "special");

        if (currentPoints > maxPointBudget)
        {
            TooltipLine overbudgetLine = new TooltipLine(Mod, "Overbudget", "OVER BUDGET - Cannot be used!");
            overbudgetLine.OverrideColor = Color.Red;
            tooltips.Add(overbudgetLine);
        }
        else if (!IsComplete())
        {
            TooltipLine incompleteLine = new TooltipLine(Mod, "Incomplete", "INCOMPLETE - Requires ammo, damage, and shot modifiers");
            incompleteLine.OverrideColor = Color.Orange;
            tooltips.Add(incompleteLine);
        }
    }

    private void AddModifierTooltip(List<TooltipLine> tooltips, string slotName, int modifierID, string modifierType)
    {
        string modifierName = modifierID != -1 ? GetModifierName(modifierID, modifierType) : "Empty";
        Color modifierColor = modifierID != -1 ? Color.White : Color.Gray;

        if (modifierID != -1)
        {
            int itemType = GetItemTypeFromModifier(modifierID, modifierType);
            int cost = ModifierData.GetModifierPointCost(itemType);
            modifierName += $" ({cost}pt)";
        }

        TooltipLine line = new TooltipLine(Mod, slotName, $"{slotName}: {modifierName}");
        line.OverrideColor = modifierColor;
        tooltips.Add(line);
    }

        private string GetModifierName(int modifierID, string type)
        {
            switch (type)
            {
                case "ammo":
                    return modifierID switch
                    {
                        0 => "Magic",
                        1 => "Arrow",
                        2 => "Bullet",
                        3 => "Rocket",
                        _ => "Unknown"
                    };
                case "damage":
                    return modifierID switch
                    {
                        0 => "Fire",
                        1 => "Water",
                        2 => "Lightning",
                        3 => "Earth",
                        4 => "Wind",
                        5 => "Slime",
                        _ => "Unknown"
                    };
                case "shot":
                    return modifierID switch
                    {
                        0 => "Auto",
                        1 => "Burst",
                        2 => "Charge",
                        _ => "Unknown"
                    };
                case "special":
                    return modifierID switch
                    {
                        0 => "Piercing",
                        1 => "Bouncing",
                        2 => "Homing",
                        3 => "Life Steal",
                        4 => "Crit Boost",
                        _ => "Unknown"
                    };
                default:
                    return "Unknown";
            }
        }

        private int GetItemTypeFromModifier(int modifierID, string modifierType)
        {
            switch (modifierType)
            {
                case "ammo":
                    return modifierID switch
                    {
                        0 => ModContent.ItemType<MagicAmmoModifier>(),
                        1 => ModContent.ItemType<ArrowAmmoModifier>(),
                        2 => ModContent.ItemType<BulletAmmoModifier>(),
                        3 => ModContent.ItemType<RocketAmmoModifier>(),
                        _ => 0
                    };
                case "damage":
                    return modifierID switch
                    {
                        0 => ModContent.ItemType<FireDamageModifier>(),
                        1 => ModContent.ItemType<WaterDamageModifier>(),
                        2 => ModContent.ItemType<LightningDamageModifier>(),
                        3 => ModContent.ItemType<EarthDamageModifier>(),
                        4 => ModContent.ItemType<WindDamageModifier>(),
                        5 => ModContent.ItemType<SlimeDamageModifier>(),
                        _ => 0
                    };
                case "shot":
                    return modifierID switch
                    {
                        0 => ModContent.ItemType<AutoFireModifier>(),
                        1 => ModContent.ItemType<BurstFireModifier>(),
                        2 => ModContent.ItemType<ChargeFireModifier>(),
                        _ => 0
                    };
                case "special":
                    return modifierID switch
                    {
                        0 => ModContent.ItemType<PiercingModifier>(),
                        1 => ModContent.ItemType<BouncingModifier>(),
                        2 => ModContent.ItemType<HomingModifier>(),
                        3 => ModContent.ItemType<LifeStealModifier>(),
                        4 => ModContent.ItemType<CritBoostModifier>(),
                        _ => 0
                    };
            }
            return 0;
        }

        public override void AddRecipes()
        {
            // Basic copper version
            Recipe copperRecipe = CreateRecipe();
            copperRecipe.AddIngredient(ItemID.CopperBar, 5);
            copperRecipe.AddIngredient(ItemID.Wood, 10);
            copperRecipe.AddTile(TileID.WorkBenches);
            copperRecipe.Register();
        }
    }

    public class ModifierStationItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 24;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Blue;
            Item.createTile = ModContent.TileType<Content.Tiles.ModifierStation>();
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Modifier Station");
            // Tooltip.SetDefault("Used to install and remove weapon modifiers");
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.WorkBench, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}