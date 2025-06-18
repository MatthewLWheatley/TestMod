using System.Collections.Generic;
using Terraria.ModLoader;
using TestMod.Content.Items;

namespace TestMod.Content.Systems
{
    public static class ModifierData
    {
        // Point costs for each modifier (itemType -> point cost)
        private static Dictionary<int, int> modifierPointCosts = new Dictionary<int, int>();
        
        // Weapon point budgets by tier - UPDATED to new system
        public static Dictionary<string, int> WeaponPointBudgets = new Dictionary<string, int>()
        {
            { "Copper", 8 },
            { "Iron", 11 },
            { "Silver", 14 },
            { "Gold", 17 },
            { "Cobalt", 20 },
            { "Mythril", 24 },
            { "Adamantite", 28 },
            { "Hallowed", 32 }
        };

        public static void InitializeModifierCosts()
        {
            // Ammo Type Modifiers (1-4 points)
            modifierPointCosts[ModContent.ItemType<MagicAmmoModifier>()] = 1;
            modifierPointCosts[ModContent.ItemType<ArrowAmmoModifier>()] = 2;
            modifierPointCosts[ModContent.ItemType<BulletAmmoModifier>()] = 3;
            modifierPointCosts[ModContent.ItemType<RocketAmmoModifier>()] = 4;

            // Damage Type Modifiers (2-3 points) 
            modifierPointCosts[ModContent.ItemType<FireDamageModifier>()] = 2;
            modifierPointCosts[ModContent.ItemType<WaterDamageModifier>()] = 2;
            modifierPointCosts[ModContent.ItemType<WindDamageModifier>()] = 2;
            modifierPointCosts[ModContent.ItemType<LightningDamageModifier>()] = 3;
            modifierPointCosts[ModContent.ItemType<EarthDamageModifier>()] = 3;
            modifierPointCosts[ModContent.ItemType<SlimeDamageModifier>()] = 3;

            // Shot Type Modifiers (2-4 points)
            modifierPointCosts[ModContent.ItemType<BurstFireModifier>()] = 2;
            modifierPointCosts[ModContent.ItemType<ChargeFireModifier>()] = 3;
            modifierPointCosts[ModContent.ItemType<AutoFireModifier>()] = 4;

            // Special Effect Modifiers (3-4 points) - Boss drops only
            modifierPointCosts[ModContent.ItemType<PiercingModifier>()] = 3;
            modifierPointCosts[ModContent.ItemType<BouncingModifier>()] = 3;
            modifierPointCosts[ModContent.ItemType<CritBoostModifier>()] = 3;
            modifierPointCosts[ModContent.ItemType<HomingModifier>()] = 4;
            modifierPointCosts[ModContent.ItemType<LifeStealModifier>()] = 4;
        }
        
        public static int GetModifierPointCost(int itemType)
        {
            return modifierPointCosts.TryGetValue(itemType, out int cost) ? cost : 0;
        }
        
        public static int GetWeaponPointBudget(string tier)
        {
            return WeaponPointBudgets.TryGetValue(tier, out int budget) ? budget : 8; // Default to copper
        }
        
        // Helper to get modifier tier from point cost (for UI display)
        public static string GetModifierTier(int pointCost)
        {
            return pointCost switch
            {
                1 => "Basic",
                2 => "Standard", 
                3 => "Advanced",
                4 => "Superior",
                5 => "Elite",
                _ => "Unknown"
            };
        }

        // NEW: Helper to check if modifier is boss-gated
        public static bool IsBossDropModifier(int itemType)
        {
            return itemType == ModContent.ItemType<PiercingModifier>() ||
                   itemType == ModContent.ItemType<BouncingModifier>() ||
                   itemType == ModContent.ItemType<HomingModifier>() ||
                   itemType == ModContent.ItemType<LifeStealModifier>() ||
                   itemType == ModContent.ItemType<CritBoostModifier>();
        }

        // NEW: Get boss drop source for special modifiers
        public static string GetBossDropSource(int itemType)
        {
            if (itemType == ModContent.ItemType<PiercingModifier>()) return "King Slime";
            if (itemType == ModContent.ItemType<BouncingModifier>()) return "Brain of Cthulhu";
            if (itemType == ModContent.ItemType<HomingModifier>()) return "Skeletron";
            if (itemType == ModContent.ItemType<LifeStealModifier>()) return "Wall of Flesh";
            if (itemType == ModContent.ItemType<CritBoostModifier>()) return "Eye of Cthulhu";
            
            return "Unknown";
        }
    }
}