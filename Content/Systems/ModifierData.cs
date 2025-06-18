using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using TestMod.Content.Items;

namespace TestMod.Content.Systems
{
    public static class ModifierData
    {
        private static Dictionary<int, int> modifierPointCosts = new Dictionary<int, int>();

        public static void InitializeModifierCosts()
        {
            // ==================== AMMO TYPE MODIFIERS ====================

            // Basic Tier
            modifierPointCosts[ModContent.ItemType<MagicAmmoModifier>()] = 1;
            modifierPointCosts[ModContent.ItemType<ArrowAmmoModifier>()] = 2;
            modifierPointCosts[ModContent.ItemType<BulletAmmoModifier>()] = 3;
            modifierPointCosts[ModContent.ItemType<RocketAmmoModifier>()] = 4;

            // Elite Tier (same cost, better effects)
            modifierPointCosts[ModContent.ItemType<EliteMagicAmmoModifier>()] = 1;
            modifierPointCosts[ModContent.ItemType<EliteArrowAmmoModifier>()] = 2;
            modifierPointCosts[ModContent.ItemType<EliteBulletAmmoModifier>()] = 3;
            modifierPointCosts[ModContent.ItemType<EliteRocketAmmoModifier>()] = 4;

            // Perfect Tier (same cost, best effects)
            modifierPointCosts[ModContent.ItemType<PerfectMagicAmmoModifier>()] = 1;
            modifierPointCosts[ModContent.ItemType<PerfectArrowAmmoModifier>()] = 2;
            modifierPointCosts[ModContent.ItemType<PerfectBulletAmmoModifier>()] = 3;
            modifierPointCosts[ModContent.ItemType<PerfectRocketAmmoModifier>()] = 4;

            // ==================== DAMAGE TYPE MODIFIERS ====================

            // Basic Tier
            modifierPointCosts[ModContent.ItemType<FireDamageModifier>()] = 2;
            modifierPointCosts[ModContent.ItemType<WaterDamageModifier>()] = 2;
            modifierPointCosts[ModContent.ItemType<WindDamageModifier>()] = 2;
            modifierPointCosts[ModContent.ItemType<LightningDamageModifier>()] = 3;
            modifierPointCosts[ModContent.ItemType<EarthDamageModifier>()] = 3;
            modifierPointCosts[ModContent.ItemType<SlimeDamageModifier>()] = 3;

            // Elite Tier
            modifierPointCosts[ModContent.ItemType<EliteFireDamageModifier>()] = 2;
            modifierPointCosts[ModContent.ItemType<EliteWaterDamageModifier>()] = 2;
            modifierPointCosts[ModContent.ItemType<EliteWindDamageModifier>()] = 2;
            modifierPointCosts[ModContent.ItemType<EliteLightningDamageModifier>()] = 3;
            modifierPointCosts[ModContent.ItemType<EliteEarthDamageModifier>()] = 3;
            modifierPointCosts[ModContent.ItemType<EliteSlimeDamageModifier>()] = 3;

            // Perfect Tier
            modifierPointCosts[ModContent.ItemType<PerfectFireDamageModifier>()] = 2;
            modifierPointCosts[ModContent.ItemType<PerfectWaterDamageModifier>()] = 2;
            modifierPointCosts[ModContent.ItemType<PerfectWindDamageModifier>()] = 2;
            modifierPointCosts[ModContent.ItemType<PerfectLightningDamageModifier>()] = 3;
            modifierPointCosts[ModContent.ItemType<PerfectEarthDamageModifier>()] = 3;
            modifierPointCosts[ModContent.ItemType<PerfectSlimeDamageModifier>()] = 3;

            // ==================== SHOT TYPE MODIFIERS ====================

            // Basic Tier
            modifierPointCosts[ModContent.ItemType<BurstFireModifier>()] = 2;
            modifierPointCosts[ModContent.ItemType<ChargeFireModifier>()] = 3;
            modifierPointCosts[ModContent.ItemType<AutoFireModifier>()] = 4;

            // Elite Tier
            modifierPointCosts[ModContent.ItemType<EliteBurstFireModifier>()] = 2;
            modifierPointCosts[ModContent.ItemType<EliteChargeFireModifier>()] = 3;
            modifierPointCosts[ModContent.ItemType<EliteAutoFireModifier>()] = 4;

            // Perfect Tier
            modifierPointCosts[ModContent.ItemType<PerfectBurstFireModifier>()] = 2;
            modifierPointCosts[ModContent.ItemType<PerfectChargeFireModifier>()] = 3;
            modifierPointCosts[ModContent.ItemType<PerfectAutoFireModifier>()] = 4;

            // ==================== SPECIAL EFFECTS (BOSS DROPS) ====================

            // Base special effects
            modifierPointCosts[ModContent.ItemType<PiercingModifier>()] = 3;
            modifierPointCosts[ModContent.ItemType<BouncingModifier>()] = 3;
            modifierPointCosts[ModContent.ItemType<CritBoostModifier>()] = 3;
            modifierPointCosts[ModContent.ItemType<HomingModifier>()] = 4;
            modifierPointCosts[ModContent.ItemType<LifeStealModifier>()] = 4;

            // Enhanced special effects (crafted from base + components)
            modifierPointCosts[ModContent.ItemType<ElitePiercingModifier>()] = 3; // Same cost but better effects
            modifierPointCosts[ModContent.ItemType<EliteBouncingModifier>()] = 3;
            modifierPointCosts[ModContent.ItemType<EliteCritBoostModifier>()] = 3;
            modifierPointCosts[ModContent.ItemType<EliteHomingModifier>()] = 4;
            modifierPointCosts[ModContent.ItemType<EliteLifeStealModifier>()] = 4;
            
            modifierPointCosts[ModContent.ItemType<PerfectPiercingModifier>()] = 3;
            modifierPointCosts[ModContent.ItemType<PerfectBouncingModifier>()] = 3;
            modifierPointCosts[ModContent.ItemType<PerfectCritBoostModifier>()] = 3;
            modifierPointCosts[ModContent.ItemType<PerfectHomingModifier>()] = 4;
            modifierPointCosts[ModContent.ItemType<PerfectLifeStealModifier>()] = 4;
        }

        public static int GetModifierPointCost(int itemType)
        {
            return modifierPointCosts.TryGetValue(itemType, out int cost) ? cost : 0;
        }

        public static int GetWeaponPointBudget(string tier)
        {
            return WeaponPointBudgets.TryGetValue(tier, out int budget) ? budget : 100;
        }

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

        public static Dictionary<string, int> WeaponPointBudgets = new Dictionary<string, int>()
        {
            // Pre-hardmode progression
            { "Copper", 8 },
            { "Tin", 8 },
            { "Iron", 9 },
            { "Lead", 9 },
            { "Silver", 12 },
            { "Tungsten", 12 },
            { "Gold", 15 },
            { "Platinum", 15 },
            
            // Hardmode progression
            { "Cobalt", 18 },
            { "Palladium", 18 },
            { "Mythril", 22 },
            { "Orichalcum", 22 },
            { "Adamantite", 26 },
            { "Titanium", 26 },
            
            // Post-mechanical bosses
            { "Hallowed", 30 },
            
            // Post-Plantera
            { "Chlorophyte", 34 },
            
            // Post-Moon Lord
            { "Luminite", 40 }
        };

        // Component requirements for weapon crafting
        public static Dictionary<string, ComponentCost> WeaponCraftingCosts = new Dictionary<string, ComponentCost>()
        {
            // Pre-hardmode weapons
            { "Copper", new ComponentCost(5, 0, 0) },
            { "Tin", new ComponentCost(5, 0, 0) },
            { "Iron", new ComponentCost(8, 0, 0) },
            { "Lead", new ComponentCost(8, 0, 0) },
            { "Silver", new ComponentCost(12, 0, 0) },
            { "Tungsten", new ComponentCost(12, 0, 0) },
            { "Gold", new ComponentCost(15, 0, 0) },
            { "Platinum", new ComponentCost(15, 0, 0) },
            
            // Hardmode weapons (require Elite components)
            { "Cobalt", new ComponentCost(0, 5, 0) },
            { "Palladium", new ComponentCost(0, 5, 0) },
            { "Mythril", new ComponentCost(0, 8, 0) },
            { "Orichalcum", new ComponentCost(0, 8, 0) },
            { "Adamantite", new ComponentCost(0, 12, 0) },
            { "Titanium", new ComponentCost(0, 12, 0) },
            
            // Post-mechanical bosses (Elite + Perfect)
            { "Hallowed", new ComponentCost(0, 15, 1) },
            
            // Post-Plantera (Elite + Perfect)
            { "Chlorophyte", new ComponentCost(0, 5, 3) },
            
            // Post-Moon Lord (Perfect only)
            { "Luminite", new ComponentCost(0, 0, 8) }
        };

        // Upgrade costs (from previous tier + components)
        public static Dictionary<string, ComponentCost> WeaponUpgradeCosts = new Dictionary<string, ComponentCost>()
        {
            // Pre-hardmode upgrades
            { "ToIron", new ComponentCost(3, 0, 0) },
            { "ToLead", new ComponentCost(3, 0, 0) },
            { "ToSilver", new ComponentCost(4, 0, 0) },
            { "ToTungsten", new ComponentCost(4, 0, 0) },
            { "ToGold", new ComponentCost(5, 0, 0) },
            { "ToPlatinum", new ComponentCost(5, 0, 0) },
            
            // Hardmode upgrades
            { "ToCobalt", new ComponentCost(8, 2, 0) },
            { "ToPalladium", new ComponentCost(8, 2, 0) },
            { "ToMythril", new ComponentCost(0, 3, 0) },
            { "ToOrichalcum", new ComponentCost(0, 3, 0) },
            { "ToAdamantite", new ComponentCost(0, 4, 0) },
            { "ToTitanium", new ComponentCost(0, 4, 0) },
            
            // Post-mechanical upgrades
            { "ToHallowed", new ComponentCost(0, 5, 1) },
            
            // Post-Plantera upgrades
            { "ToChlorophyte", new ComponentCost(0, 3, 2) },
            
            // Post-Moon Lord upgrades
            { "ToLuminite", new ComponentCost(0, 0, 5) }
        };

        public static ComponentCost GetWeaponCraftingCost(string weaponTier)
        {
            return WeaponCraftingCosts.TryGetValue(weaponTier, out ComponentCost cost) ? cost : new ComponentCost(0, 0, 0);
        }

        public static ComponentCost GetWeaponUpgradeCost(string targetTier)
        {
            string upgradeKey = $"To{targetTier}";
            return WeaponUpgradeCosts.TryGetValue(upgradeKey, out ComponentCost cost) ? cost : new ComponentCost(0, 0, 0);
        }

        // Helper method to get weapon tier progression
        public static List<string> GetWeaponTierProgression()
        {
            return new List<string>
            {
                // Pre-hardmode
                "Copper", "Tin", "Iron", "Lead", "Silver", "Tungsten", "Gold", "Platinum",
                // Hardmode
                "Cobalt", "Palladium", "Mythril", "Orichalcum", "Adamantite", "Titanium",
                // Post-mechanical
                "Hallowed",
                // Post-Plantera
                "Chlorophyte",
                // Post-Moon Lord
                "Luminite"
            };
        }

        // Get alternative ores for a given tier
        public static List<string> GetAlternativeOres(string tier)
        {
            return tier switch
            {
                "Copper" => new List<string> { "Tin" },
                "Tin" => new List<string> { "Copper" },
                "Iron" => new List<string> { "Lead" },
                "Lead" => new List<string> { "Iron" },
                "Silver" => new List<string> { "Tungsten" },
                "Tungsten" => new List<string> { "Silver" },
                "Gold" => new List<string> { "Platinum" },
                "Platinum" => new List<string> { "Gold" },
                "Cobalt" => new List<string> { "Palladium" },
                "Palladium" => new List<string> { "Cobalt" },
                "Mythril" => new List<string> { "Orichalcum" },
                "Orichalcum" => new List<string> { "Mythril" },
                "Adamantite" => new List<string> { "Titanium" },
                "Titanium" => new List<string> { "Adamantite" },
                _ => new List<string>()
            };
        }

        // Check if a weapon tier is unlocked
        public static bool IsWeaponTierUnlocked(string tier)
        {
            return tier switch
            {
                // Pre-hardmode tiers are always available
                "Copper" or "Tin" or "Iron" or "Lead" or "Silver" or "Tungsten" or "Gold" or "Platinum" => true,

                // Hardmode tiers
                "Cobalt" or "Palladium" or "Mythril" or "Orichalcum" or "Adamantite" or "Titanium" => Main.hardMode,

                // Post-mechanical boss tiers
                "Hallowed" => NPC.downedMechBossAny,

                // Post-Plantera tiers
                "Chlorophyte" => NPC.downedPlantBoss,

                // Post-Moon Lord tiers
                "Luminite" => NPC.downedMoonlord,

                _ => false
            };
        }

        // Get unlock condition text for weapon tiers
        public static string GetWeaponTierUnlockCondition(string tier)
        {
            return tier switch
            {
                "Copper" or "Tin" or "Iron" or "Lead" or "Silver" or "Tungsten" or "Gold" or "Platinum" => "Available",
                "Cobalt" or "Palladium" or "Mythril" or "Orichalcum" or "Adamantite" or "Titanium" => "Requires Hardmode",
                "Hallowed" => "Requires any Mechanical Boss defeated",
                "Chlorophyte" => "Requires Plantera defeated",
                "Luminite" => "Requires Moon Lord defeated",
                _ => "Unknown requirement"
            };
        }

    }

    // Component cost helper class
    public class ComponentCost
    {
        public int Basic { get; }
        public int Elite { get; }
        public int Perfect { get; }

        public ComponentCost(int basicAmount, int eliteAmount, int perfectAmount)
        {
            Basic = basicAmount;
            Elite = eliteAmount;
            Perfect = perfectAmount;
        }

        public bool CanAfford(Player player)
        {
            return player.CountItem(ModContent.ItemType<BasicModularComponent>()) >= Basic &&
                   player.CountItem(ModContent.ItemType<EliteModularComponent>()) >= Elite &&
                   player.CountItem(ModContent.ItemType<PerfectModularComponent>()) >= Perfect;
        }

        public void ConsumeItems(Player player)
        {
            if (Basic > 0) player.ConsumeItem(ModContent.ItemType<BasicModularComponent>(), Basic);
            if (Elite > 0) player.ConsumeItem(ModContent.ItemType<EliteModularComponent>(), Elite);
            if (Perfect > 0) player.ConsumeItem(ModContent.ItemType<PerfectModularComponent>(), Perfect);
        }

        public override string ToString()
        {
            List<string> parts = new List<string>();
            if (Basic > 0) parts.Add($"{Basic} Basic");
            if (Elite > 0) parts.Add($"{Elite} Elite");
            if (Perfect > 0) parts.Add($"{Perfect} Perfect");
            return string.Join(", ", parts) + " Components";
        }
    }
}