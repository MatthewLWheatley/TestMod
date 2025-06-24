using System.Collections.Generic;
using Humanizer;
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
            modifierPointCosts[ModContent.ItemType<EliteMagicAmmoModifier>()] = 2;
            modifierPointCosts[ModContent.ItemType<EliteArrowAmmoModifier>()] = 4;
            modifierPointCosts[ModContent.ItemType<EliteBulletAmmoModifier>()] = 6;
            modifierPointCosts[ModContent.ItemType<EliteRocketAmmoModifier>()] = 8;

            // Perfect Tier (same cost, best effects)
            modifierPointCosts[ModContent.ItemType<PerfectMagicAmmoModifier>()] = 4;
            modifierPointCosts[ModContent.ItemType<PerfectArrowAmmoModifier>()] = 8;
            modifierPointCosts[ModContent.ItemType<PerfectBulletAmmoModifier>()] = 12;
            modifierPointCosts[ModContent.ItemType<PerfectRocketAmmoModifier>()] = 16;

            // ==================== DAMAGE TYPE MODIFIERS ====================

            // Basic Tier
            modifierPointCosts[ModContent.ItemType<FireDamageModifier>()] = 2;
            modifierPointCosts[ModContent.ItemType<WaterDamageModifier>()] = 2;
            modifierPointCosts[ModContent.ItemType<WindDamageModifier>()] = 2;
            modifierPointCosts[ModContent.ItemType<LightningDamageModifier>()] = 3;
            modifierPointCosts[ModContent.ItemType<EarthDamageModifier>()] = 3;
            modifierPointCosts[ModContent.ItemType<SlimeDamageModifier>()] = 3;

            // Elite Tier
            modifierPointCosts[ModContent.ItemType<EliteFireDamageModifier>()] = 4;
            modifierPointCosts[ModContent.ItemType<EliteWaterDamageModifier>()] = 4;
            modifierPointCosts[ModContent.ItemType<EliteWindDamageModifier>()] = 4;
            modifierPointCosts[ModContent.ItemType<EliteLightningDamageModifier>()] = 6;
            modifierPointCosts[ModContent.ItemType<EliteEarthDamageModifier>()] = 6;
            modifierPointCosts[ModContent.ItemType<EliteSlimeDamageModifier>()] = 6;

            // Perfect Tier
            modifierPointCosts[ModContent.ItemType<PerfectFireDamageModifier>()] = 8;
            modifierPointCosts[ModContent.ItemType<PerfectWaterDamageModifier>()] = 8;
            modifierPointCosts[ModContent.ItemType<PerfectWindDamageModifier>()] = 8;
            modifierPointCosts[ModContent.ItemType<PerfectLightningDamageModifier>()] = 12;
            modifierPointCosts[ModContent.ItemType<PerfectEarthDamageModifier>()] = 12;
            modifierPointCosts[ModContent.ItemType<PerfectSlimeDamageModifier>()] = 12;

            // ==================== SHOT TYPE MODIFIERS ====================

            // Basic Tier
            modifierPointCosts[ModContent.ItemType<BurstFireModifier>()] = 2;
            modifierPointCosts[ModContent.ItemType<ChargeFireModifier>()] = 3;
            modifierPointCosts[ModContent.ItemType<AutoFireModifier>()] = 4;

            // Elite Tier
            modifierPointCosts[ModContent.ItemType<EliteBurstFireModifier>()] = 4;
            modifierPointCosts[ModContent.ItemType<EliteChargeFireModifier>()] = 6;
            modifierPointCosts[ModContent.ItemType<EliteAutoFireModifier>()] = 8;

            // Perfect Tier
            modifierPointCosts[ModContent.ItemType<PerfectBurstFireModifier>()] = 8;
            modifierPointCosts[ModContent.ItemType<PerfectChargeFireModifier>()] = 12;
            modifierPointCosts[ModContent.ItemType<PerfectAutoFireModifier>()] = 16;

            // ==================== SPECIAL EFFECTS (BOSS DROPS) ====================

            // Base special effects
            modifierPointCosts[ModContent.ItemType<PiercingModifier>()] = 3;
            modifierPointCosts[ModContent.ItemType<BouncingModifier>()] = 3;
            modifierPointCosts[ModContent.ItemType<CritBoostModifier>()] = 3;
            modifierPointCosts[ModContent.ItemType<HomingModifier>()] = 4;
            modifierPointCosts[ModContent.ItemType<LifeStealModifier>()] = 4;

            // Enhanced special effects (crafted from base + components)
            modifierPointCosts[ModContent.ItemType<ElitePiercingModifier>()] = 6;
            modifierPointCosts[ModContent.ItemType<EliteBouncingModifier>()] = 6;
            modifierPointCosts[ModContent.ItemType<EliteCritBoostModifier>()] = 6;
            modifierPointCosts[ModContent.ItemType<EliteHomingModifier>()] = 8;
            modifierPointCosts[ModContent.ItemType<EliteLifeStealModifier>()] = 8;

            modifierPointCosts[ModContent.ItemType<PerfectPiercingModifier>()] = 12;
            modifierPointCosts[ModContent.ItemType<PerfectBouncingModifier>()] = 12;
            modifierPointCosts[ModContent.ItemType<PerfectCritBoostModifier>()] = 12;
            modifierPointCosts[ModContent.ItemType<PerfectHomingModifier>()] = 16;
            modifierPointCosts[ModContent.ItemType<PerfectLifeStealModifier>()] = 16;
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
                2 => "Elite",
                3 => "Perfect",
                _ => "Unknown"
            };
        }

        public static Dictionary<string, int> WeaponPointBudgets = new Dictionary<string, int>()
        {
            // Pre-hardmode progression (Basic tier focus)
            { "Copper", 8 },      // Basic only: 4+3+4 = 11 max
            { "Tin", 8 },
            { "Iron", 10 },       // Can fit basic loadout comfortably
            { "Lead", 10 },
            { "Silver", 12 },     // Room for some elite pieces
            { "Tungsten", 12 },
            { "Gold", 15 },       // Mix of basic/elite
            { "Platinum", 15 },

            // Early hardmode (Elite tier unlocked)
            { "Cobalt", 20 },     // Elite focus: 8+6+8 = 22 max without special
            { "Palladium", 20 },
            { "Mythril", 25 },    // Room for elite + basic special
            { "Orichalcum", 25 },
            { "Adamantite", 30 }, // Full elite loadout possible
            { "Titanium", 30 },

            // Post-mechanical bosses (Perfect tier preparation)
            { "Hallowed", 40 },   // Perfect pieces start becoming viable

            // Post-Plantera (Perfect tier focus)
            { "Chlorophyte", 55 }, // Most perfect combinations possible

            // Post-Moon Lord (Perfect tier mastery)
            { "Luminite", 70 }    // Full perfect loadout: 16+12+16+16 = 60 + wiggle room
        };

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
            // Use a loop to consume the correct amount of each component type
            for (int i = 0; i < Basic; i++)
            {
                player.ConsumeItem(ModContent.ItemType<BasicModularComponent>());
            }

            for (int i = 0; i < Elite; i++)
            {
                player.ConsumeItem(ModContent.ItemType<EliteModularComponent>());
            }

            for (int i = 0; i < Perfect; i++)
            {
                player.ConsumeItem(ModContent.ItemType<PerfectModularComponent>());
            }
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
    public class ModularWeaponPlayer : ModPlayer
    {
        private int[] transferringModifiers = null;

        public void SetTransferringModifiers(int ammo, int damage, int shot, int special)
        {
            transferringModifiers = new int[] { ammo, damage, shot, special };
        }

        public bool HasTransferringModifiers()
        {
            return transferringModifiers != null;
        }

        public int[] GetAndClearTransferringModifiers()
        {
            var result = transferringModifiers;
            transferringModifiers = null;
            return result;
        }
    }
}