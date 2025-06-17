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
    // This is a basic item template.
    // Please see tModLoader's ExampleMod for every other example:
    // https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
    public class ModularGun : ModItem
    {
        // Stored modifier IDs - these get saved/loaded
        public int shotTypeModifier = -1;
        public int damageTypeModifier = -1;
        public int rateOfFireModifier = -1;

        // Base weapon stats
        private int baseDamage = 15;
        private float baseKnockback = 2f;
        private int baseCrit = 4;
        private int baseUseTime = 30;

        // NEW: Point budget properties
        public string weaponTier = "Copper";  // Default tier
        public int maxPointBudget = 6;        // Will be set based on tier

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
            Item.autoReuse = false; // This will be modified by rate of fire modifiers
            Item.shoot = ProjectileID.MagicMissile; // Default magic projectile
            Item.shootSpeed = 16f;
            Item.mana = 10; // Mana cost
            Item.crit = baseCrit;
            weaponTier = "Copper";
            Item.UseSound = SoundID.Item11;
            Item.useStyle = ItemUseStyleID.Shoot;
            //Item.holdStyle = ItemHoldStyleID.HoldUp;
            Item.noUseGraphic = false;
        }


        public override Vector2? HoldoutOffset()
        {
            // Adjust holdout position for better visual alignment
            return new Vector2(-10, 2); // Adjust as needed
        }

        public override void SetStaticDefaults()
        {
            // For newer tModLoader versions, use these instead:
            // DisplayName.SetDefault is deprecated
            // Use the localization system or just remove these lines
            // The display name and tooltip can be set via .hjson files instead

            // If you want to keep it simple for now, just comment these out:
            // DisplayName.SetDefault("Modular Magic Gun");
            // Tooltip.SetDefault("A customizable magical weapon that requires modifiers to function\n" +
            //                  "Use at a Modifier Station to install components\n" +
            //                  "Consumes mana instead of ammunition");
        }

        public int GetCurrentPointUsage()
        {
            int total = 0;

            if (shotTypeModifier != -1)
            {
                int itemType = GetItemTypeFromModifier(shotTypeModifier, "shot");
                total += ModifierData.GetModifierPointCost(itemType);
            }

            if (damageTypeModifier != -1)
            {
                int itemType = GetItemTypeFromModifier(damageTypeModifier, "damage");
                total += ModifierData.GetModifierPointCost(itemType);
            }

            if (rateOfFireModifier != -1)
            {
                int itemType = GetItemTypeFromModifier(rateOfFireModifier, "rate");
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
                case "shot":
                    if (shotTypeModifier != -1)
                    {
                        int currentType = GetItemTypeFromModifier(shotTypeModifier, "shot");
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
                case "rate":
                    if (rateOfFireModifier != -1)
                    {
                        int currentType = GetItemTypeFromModifier(rateOfFireModifier, "rate");
                        currentUsage -= ModifierData.GetModifierPointCost(currentType);
                    }
                    break;
            }

            return (currentUsage + modifierCost) <= maxPointBudget;
        }

        // Check if weapon has all required modifiers
        public bool IsComplete()
        {
            return shotTypeModifier != -1 &&
                   damageTypeModifier != -1 &&
                   rateOfFireModifier != -1;
        }

        // Prevent shooting if incomplete
        public override bool CanUseItem(Player player)
        {
            if (GetCurrentPointUsage() > maxPointBudget)
            {
                Main.NewText("Weapon is over point budget!", Color.Red);
                return false;
            }

            if (!IsComplete())
            {
                Main.NewText("Weapon requires all modifier slots to be filled!", Color.Red);
                return false;
            }
            return base.CanUseItem(player);
        }

        // Apply modifier effects when shooting
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (!IsComplete()) return false;

            // Modify projectile based on shot type modifier
            switch (shotTypeModifier)
            {
                case 0: // Straight shot
                    type = ProjectileID.WoodenArrowFriendly; // Non-homing straight projectile
                    break;
                case 1: // Burst shot (shotgun-like)
                    // Fire multiple projectiles
                    for (int i = 0; i < 3; i++)
                    {
                        Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(15));
                        Projectile.NewProjectile(source, position, perturbedSpeed, ProjectileID.WoodenArrowFriendly, damage / 2, knockback, player.whoAmI);
                    }
                    return false; // Don't fire the original projectile
                case 2: // Bolt shot
                    type = ProjectileID.UnholyArrow; // Fast straight projectile
                    break;
                case 3: // Projectile (grenade-like)
                    type = ProjectileID.Grenade; // Explosive projectile
                    break;
            }

            // Apply damage type effects - this would modify the projectile or add effects
            // For now, just change damage color or add visual effects

            return true; // Fire the modified projectile
        }

        // Modify weapon stats based on rate of fire modifier
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            // Adjust damage based on modifiers
            switch (rateOfFireModifier)
            {
                case 0: // Auto - lower damage, faster fire
                    damage *= 0.8f;
                    break;
                case 1: // Burst - moderate damage
                    damage *= 1.0f;
                    break;
                case 2: // Charge - higher damage, slower fire
                    damage *= 1.5f;
                    break;
            }
        }

        public override void ModifyWeaponKnockback(Player player, ref StatModifier knockback)
        {
            // Modify knockback based on damage type
            switch (damageTypeModifier)
            {
                case 3: // Earth - extra knockback
                    knockback *= 1.5f;
                    break;
            }
        }

        // Update weapon properties based on modifiers
        public override void UpdateInventory(Player player)
        {
            if (!IsComplete()) return;

            // Modify use time based on rate of fire
            switch (rateOfFireModifier)
            {
                case 0: // Auto
                    Item.useTime = baseUseTime / 2;
                    Item.useAnimation = baseUseTime / 2;
                    Item.autoReuse = true;
                    break;
                case 1: // Burst
                    Item.useTime = baseUseTime;
                    Item.useAnimation = baseUseTime;
                    Item.autoReuse = false;
                    break;
                case 2: // Charge
                    Item.useTime = baseUseTime * 2;
                    Item.useAnimation = baseUseTime * 2;
                    Item.autoReuse = false;
                    break;
            }
        }

        // Save modifier data
        public override void SaveData(TagCompound tag)
        {
            tag["shotType"] = shotTypeModifier;
            tag["damageType"] = damageTypeModifier;
            tag["rateOfFire"] = rateOfFireModifier;
            tag["weaponTier"] = weaponTier;
            tag["maxPointBudget"] = maxPointBudget;
        }

        // Load modifier data
        public override void LoadData(TagCompound tag)
        {
            shotTypeModifier = tag.GetInt("shotType");
            damageTypeModifier = tag.GetInt("damageType");
            rateOfFireModifier = tag.GetInt("rateOfFire");
            weaponTier = tag.GetString("weaponTier");
            maxPointBudget = tag.GetInt("maxPointBudget");

            // Fallback for old saves
            if (string.IsNullOrEmpty(weaponTier))
            {
                weaponTier = "Copper";
                maxPointBudget = 6;
            }
        }

        // Custom tooltip showing installed modifiers
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Point budget display
            int currentPoints = GetCurrentPointUsage();
            TooltipLine pointLine = new TooltipLine(Mod, "PointBudget",
                $"Points: {currentPoints}/{maxPointBudget} ({weaponTier} Tier)");
            pointLine.OverrideColor = currentPoints <= maxPointBudget ? Color.White : Color.Red;
            tooltips.Add(pointLine);

            // Existing modifier display code...
            string shotTypeName = shotTypeModifier != -1 ? GetModifierName(shotTypeModifier, "shot") : "Empty";
            Color shotTypeColor = shotTypeModifier != -1 ? Color.White : Color.Gray;

            string damageTypeName = damageTypeModifier != -1 ? GetModifierName(damageTypeModifier, "damage") : "Empty";
            Color damageTypeColor = damageTypeModifier != -1 ? Color.White : Color.Gray;

            string rateOfFireName = rateOfFireModifier != -1 ? GetModifierName(rateOfFireModifier, "rate") : "Empty";
            Color rateOfFireColor = rateOfFireModifier != -1 ? Color.White : Color.Gray;

            // Show point costs
            if (shotTypeModifier != -1)
            {
                int itemType = GetItemTypeFromModifier(shotTypeModifier, "shot");
                int cost = ModifierData.GetModifierPointCost(itemType);
                shotTypeName += $" ({cost}pt)";
            }

            if (damageTypeModifier != -1)
            {
                int itemType = GetItemTypeFromModifier(damageTypeModifier, "damage");
                int cost = ModifierData.GetModifierPointCost(itemType);
                damageTypeName += $" ({cost}pt)";
            }

            if (rateOfFireModifier != -1)
            {
                int itemType = GetItemTypeFromModifier(rateOfFireModifier, "rate");
                int cost = ModifierData.GetModifierPointCost(itemType);
                rateOfFireName += $" ({cost}pt)";
            }

            TooltipLine shotLine = new TooltipLine(Mod, "ShotType", $"Shot Type: {shotTypeName}");
            shotLine.OverrideColor = shotTypeColor;
            tooltips.Add(shotLine);

            TooltipLine damageLine = new TooltipLine(Mod, "DamageType", $"Damage Type: {damageTypeName}");
            damageLine.OverrideColor = damageTypeColor;
            tooltips.Add(damageLine);

            TooltipLine rateLine = new TooltipLine(Mod, "RateOfFire", $"Rate of Fire: {rateOfFireName}");
            rateLine.OverrideColor = rateOfFireColor;
            tooltips.Add(rateLine);

            if (currentPoints > maxPointBudget)
            {
                TooltipLine overbudgetLine = new TooltipLine(Mod, "Overbudget", "OVER BUDGET - Cannot be used!");
                overbudgetLine.OverrideColor = Color.Red;
                tooltips.Add(overbudgetLine);
            }
            else if (!IsComplete())
            {
                TooltipLine incompleteLine = new TooltipLine(Mod, "Incomplete", "INCOMPLETE - Requires all modifiers");
                incompleteLine.OverrideColor = Color.Orange;
                tooltips.Add(incompleteLine);
            }
        }

        private int GetItemTypeFromModifier(int modifierID, string modifierType)
        {
            switch (modifierType)
            {
                case "shot":
                    return modifierID switch
                    {
                        0 => ModContent.ItemType<StraightShotModifier>(),
                        1 => ModContent.ItemType<BurstShotModifier>(),
                        2 => ModContent.ItemType<BoltShotModifier>(),
                        3 => ModContent.ItemType<ProjectileShotModifier>(),
                        _ => 0
                    };
                case "damage":
                    return modifierID switch
                    {
                        0 => ModContent.ItemType<FireDamageModifier>(),
                        1 => ModContent.ItemType<WaterDamageModifier>(),
                        2 => ModContent.ItemType<LightningDamageModifier>(),
                        3 => ModContent.ItemType<EarthDamageModifier>(),
                        _ => 0
                    };
                case "rate":
                    return modifierID switch
                    {
                        0 => ModContent.ItemType<AutoFireModifier>(),
                        1 => ModContent.ItemType<BurstFireModifier>(),
                        2 => ModContent.ItemType<ChargeFireModifier>(),
                        _ => 0
                    };
            }
            return 0;
        }

        // Helper method to get modifier names for display
        private string GetModifierName(int modifierID, string type)
        {
            switch (type)
            {
                case "shot":
                    return modifierID switch
                    {
                        0 => "Straight",
                        1 => "Burst",
                        2 => "Bolt",
                        3 => "Projectile",
                        _ => "Unknown"
                    };
                case "damage":
                    return modifierID switch
                    {
                        0 => "Fire",
                        1 => "Water",
                        2 => "Lightning",
                        3 => "Earth",
                        _ => "Unknown"
                    };
                case "rate":
                    return modifierID switch
                    {
                        0 => "Auto",
                        1 => "Burst",
                        2 => "Charge",
                        _ => "Unknown"
                    };
                default:
                    return "Unknown";
            }
        }

        public override void AddRecipes()
        {
            // Basic copper version
            Recipe copperRecipe = CreateRecipe();
            copperRecipe.AddIngredient(ItemID.CopperBar, 5);
            copperRecipe.AddIngredient(ItemID.Wood, 10);
            copperRecipe.AddTile(TileID.WorkBenches);
            copperRecipe.Register();

            // TODO: Add recipes for higher tiers later
            // Iron version would require Iron Bars + Copper Modular Gun + Modular Components
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

    // Shot Type Modifiers
    public class StraightShotModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1; // Cannot stack
            Item.value = Item.buyPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.White;
        }

        // NEW: Show point cost in tooltip
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Shot Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DirtBlock, 1);
            recipe.Register();
        }
    }
    
    public class BurstShotModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.White;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Shot Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DirtBlock, 1);
            recipe.Register();
        }
    }
    
    public class BoltShotModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.White;
        }
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Shot Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DirtBlock, 1);
            recipe.Register();
        }
    }
    
    public class ProjectileShotModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.White;
        }
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Shot Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DirtBlock, 1);
            recipe.Register();
        }
    }
    
    // Damage Type Modifiers
    public class FireDamageModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.Orange;
        }
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Damage Type Modifiers");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DirtBlock, 1);
            recipe.Register();
        }
    }
    
    public class WaterDamageModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.Blue;
        }
                
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Damage Type Modifiers");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DirtBlock, 1);
            recipe.Register();
        }
    }
    
    public class LightningDamageModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.Yellow;
        }
                
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Damage Type Modifiers");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DirtBlock, 1);
            recipe.Register();
        }
    }
    
    public class EarthDamageModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.Green;
        }
                
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Damage Type Modifiers");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DirtBlock, 1);
            recipe.Register();
        }
    }
    
    // Rate of Fire Modifiers
    public class AutoFireModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.Pink;
        }
                
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Rate of Fire Modifiers");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DirtBlock, 1);
            recipe.Register();
        }
    }
    
    public class BurstFireModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.Pink;
        }
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Rate of Fire Modifiers");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DirtBlock, 1);
            recipe.Register();
        }
    }
    
    public class ChargeFireModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.Pink;
        }
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Rate of Fire Modifiers");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DirtBlock, 1);
            recipe.Register();
        }
    }
}