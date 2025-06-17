using System.Collections.Generic;
using Terraria.ModLoader;
using TestMod.Content.Items;

namespace TestMod.Content.Systems
{
    public static class ModifierData
    {
        // Point costs for each modifier (itemType -> point cost)
        private static Dictionary<int, int> modifierPointCosts = new Dictionary<int, int>();
        
        // Weapon point budgets by tier
        public static Dictionary<string, int> WeaponPointBudgets = new Dictionary<string, int>()
        {
            { "Copper", 6 },
            { "Iron", 9 },
            { "Silver", 12 },
            { "Gold", 15 },
            { "Cobalt", 18 },
            { "Mythril", 22 },
            { "Adamantite", 26 },
            { "Hallowed", 30 }
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
        }
        
        public static int GetModifierPointCost(int itemType)
        {
            return modifierPointCosts.TryGetValue(itemType, out int cost) ? cost : 0;
        }
        
        public static int GetWeaponPointBudget(string tier)
        {
            return WeaponPointBudgets.TryGetValue(tier, out int budget) ? budget : 6; // Default to copper
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
                _ => "Unknown"
            };
        }
    }
}