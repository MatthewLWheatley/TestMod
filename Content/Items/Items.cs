using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.DataStructures;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

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
            if (!IsComplete())
            {
                // Show warning message
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
        }
        
        // Load modifier data
        public override void LoadData(TagCompound tag)
        {
            shotTypeModifier = tag.GetInt("shotType");
            damageTypeModifier = tag.GetInt("damageType");
            rateOfFireModifier = tag.GetInt("rateOfFire");
        }
        
        // Custom tooltip showing installed modifiers
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (IsComplete())
            {
                tooltips.Add(new TooltipLine(Mod, "ShotType", $"Shot Type: {GetModifierName(shotTypeModifier, "shot")}"));
                tooltips.Add(new TooltipLine(Mod, "DamageType", $"Damage Type: {GetModifierName(damageTypeModifier, "damage")}"));
                tooltips.Add(new TooltipLine(Mod, "RateOfFire", $"Rate of Fire: {GetModifierName(rateOfFireModifier, "rate")}"));
            }
            else
            {
                tooltips.Add(new TooltipLine(Mod, "Incomplete", "INCOMPLETE - Requires all modifiers"));
            }
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
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DirtBlock, 1);
            recipe.Register();
        }
    }
}