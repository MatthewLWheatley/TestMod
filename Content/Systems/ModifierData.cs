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

    public static class WeaponStatsCalculator
    {
        public static WeaponStats CalculateStats(BaseModularGun modularGun, Item weaponItem)
        {
            if (modularGun == null || !modularGun.IsComplete())
            {
                return new WeaponStats
                {
                    Damage = weaponItem.damage,
                    Knockback = weaponItem.knockBack,
                    CritChance = weaponItem.crit,
                    UseTime = weaponItem.useTime,
                    ManaCost = weaponItem.mana,
                    AmmoInfo = "None",
                    DebuffInfo = "None",
                    SpecialInfo = "None",
                    TierBonus = 0f
                };
            }

            var stats = new WeaponStats();
            
            // Calculate damage
            stats.Damage = CalculateActualDamage(modularGun, weaponItem);
            
            // Calculate knockback
            stats.Knockback = CalculateActualKnockback(modularGun, weaponItem);
            
            // Calculate crit
            stats.CritChance = CalculateActualCrit(modularGun, weaponItem);
            
            // Calculate use time
            stats.UseTime = CalculateActualUseTime(modularGun, weaponItem);
            
            // Calculate mana cost
            stats.ManaCost = CalculateActualManaCost(modularGun, weaponItem);
            
            // Get descriptive info
            stats.AmmoInfo = GetAmmoTypeDescription(modularGun);
            stats.DebuffInfo = GetDebuffDescription(modularGun);
            stats.SpecialInfo = GetSpecialEffectDescription(modularGun);
            stats.TierBonus = GetTierBonusMultiplier(modularGun);

            return stats;
        }

        private static float CalculateActualDamage(BaseModularGun modularGun, Item weaponItem)
        {
            float damage = weaponItem.damage;
            
            // Apply shot type damage modifiers
            var shotTier = GetShotTypeTier(modularGun);
            float tierMultiplier = (float)shotTier / (float)BaseModularGun.ModifierTier.Basic;

            switch (modularGun.shotTypeModifier % 3)
            {
                case 0: // Auto Fire
                    float autoReduction = 0.8f - (0.1f * (tierMultiplier - 1));
                    damage *= autoReduction;
                    break;
                case 1: // Burst Fire
                    float burstBonus = 1.0f + (0.15f * (tierMultiplier - 1));
                    damage *= burstBonus;
                    break;
                case 2: // Charge Fire
                    float chargeBonus = 1.4f + (0.3f * (tierMultiplier - 1));
                    damage *= chargeBonus;
                    break;
            }

            // Apply ammo type tier bonus
            var ammoTier = GetAmmoTypeTier(modularGun);
            if (ammoTier > BaseModularGun.ModifierTier.Basic)
            {
                float ammoBonus = 1.0f + (0.1f * ((float)ammoTier - 1));
                damage *= ammoBonus;
            }

            // Apply weapon tier bonus
            var tierProgression = ModifierData.GetWeaponTierProgression();
            int tierIndex = tierProgression.IndexOf(modularGun.weaponTier);
            float weaponTierMultiplier = 1f + (tierIndex * 0.15f);
            damage *= weaponTierMultiplier;

            return damage;
        }

        private static float CalculateActualKnockback(BaseModularGun modularGun, Item weaponItem)
        {
            float knockback = weaponItem.knockBack;

            // Apply wind damage type knockback bonus
            if (modularGun.damageTypeModifier % 6 == 4) // Wind damage
            {
                var damageTier = GetDamageTypeTier(modularGun);
                float knockbackBonus = 1.5f + (0.5f * ((float)damageTier - 1));
                knockback *= knockbackBonus;
            }

            return knockback;
        }

        private static float CalculateActualCrit(BaseModularGun modularGun, Item weaponItem)
        {
            float crit = weaponItem.crit;

            // Apply crit boost special effect
            if (modularGun.specialEffectModifier % 5 == 4) // Crit Boost modifier
            {
                var specialTier = GetSpecialEffectTier(modularGun);
                float critBonus = 10f * (float)specialTier;
                crit += critBonus;
            }

            return crit;
        }

        private static int CalculateActualUseTime(BaseModularGun modularGun, Item weaponItem)
        {
            var shotTier = GetShotTypeTier(modularGun);
            float speedBonus = 1.0f + (0.15f * ((int)shotTier - 1));

            int useTime = weaponItem.useTime;

            switch (modularGun.shotTypeModifier % 3)
            {
                case 0: // Auto Fire
                    useTime = (int)(weaponItem.useTime / 2 / speedBonus);
                    break;
                case 1: // Burst Fire
                    useTime = (int)(weaponItem.useTime / speedBonus);
                    break;
                case 2: // Charge Fire
                    useTime = (int)(weaponItem.useTime * 2 / speedBonus);
                    break;
            }

            return useTime;
        }

        private static int CalculateActualManaCost(BaseModularGun modularGun, Item weaponItem)
        {
            if (modularGun.ammoTypeModifier % 4 == 0) // Magic ammo type
            {
                var ammoTier = GetAmmoTypeTier(modularGun);
                return 12 - (2 * (int)ammoTier);
            }
            return 0;
        }

        private static string GetAmmoTypeDescription(BaseModularGun modularGun)
        {
            string ammoType = (modularGun.ammoTypeModifier % 4) switch
            {
                0 => "Magic",
                1 => "Arrows",
                2 => "Bullets",
                3 => "Rockets",
                _ => "Unknown"
            };

            string tier = GetAmmoTypeTier(modularGun) switch
            {
                BaseModularGun.ModifierTier.Perfect => "Perfect",
                BaseModularGun.ModifierTier.Elite => "Elite",
                _ => "Basic"
            };

            return $"{ammoType} ({tier})";
        }

        private static string GetDebuffDescription(BaseModularGun modularGun)
        {
            if (modularGun.damageTypeModifier == -1) return "None";

            var tier = GetDamageTypeTier(modularGun);
            float tierMultiplier = tier switch
            {
                BaseModularGun.ModifierTier.Perfect => 4f,
                BaseModularGun.ModifierTier.Elite => 2f,
                _ => 1f
            };

            float duration = 5f * tierMultiplier;

            string debuffName = (modularGun.damageTypeModifier % 6) switch
            {
                0 => "On Fire!",
                1 => "Slow",
                2 => "Ichor",
                3 => "Bleeding",
                4 => "Knockback+",
                5 => "Poisoned",
                _ => "Unknown"
            };

            return $"{debuffName} ({duration:F1}s)";
        }

        private static string GetSpecialEffectDescription(BaseModularGun modularGun)
        {
            if (modularGun.specialEffectModifier == -1) return "None";

            var tier = GetSpecialEffectTier(modularGun);
            string tierName = tier.ToString();

            return (modularGun.specialEffectModifier % 5) switch
            {
                0 => $"Piercing +{(int)tier} ({tierName})",
                1 => $"Bouncing x{(int)tier * 2} ({tierName})",
                2 => $"Homing {tier} ({tierName})",
                3 => $"Life Steal {(int)tier * 2}% ({tierName})",
                4 => $"Crit +{(int)tier * 10}% ({tierName})",
                _ => "Unknown"
            };
        }

        private static float GetTierBonusMultiplier(BaseModularGun modularGun)
        {
            var tierProgression = ModifierData.GetWeaponTierProgression();
            int tierIndex = tierProgression.IndexOf(modularGun.weaponTier);
            return 1f + (tierIndex * 0.15f);
        }

        // Helper methods for tier detection
        private static BaseModularGun.ModifierTier GetAmmoTypeTier(BaseModularGun modularGun)
        {
            if (modularGun.ammoTypeModifier >= 8) return BaseModularGun.ModifierTier.Perfect;
            if (modularGun.ammoTypeModifier >= 4) return BaseModularGun.ModifierTier.Elite;
            return BaseModularGun.ModifierTier.Basic;
        }

        private static BaseModularGun.ModifierTier GetShotTypeTier(BaseModularGun modularGun)
        {
            if (modularGun.shotTypeModifier >= 6) return BaseModularGun.ModifierTier.Perfect;
            if (modularGun.shotTypeModifier >= 3) return BaseModularGun.ModifierTier.Elite;
            return BaseModularGun.ModifierTier.Basic;
        }

        private static BaseModularGun.ModifierTier GetDamageTypeTier(BaseModularGun modularGun)
        {
            if (modularGun.damageTypeModifier >= 12) return BaseModularGun.ModifierTier.Perfect;
            if (modularGun.damageTypeModifier >= 6) return BaseModularGun.ModifierTier.Elite;
            return BaseModularGun.ModifierTier.Basic;
        }

        private static BaseModularGun.ModifierTier GetSpecialEffectTier(BaseModularGun modularGun)
        {
            if (modularGun.specialEffectModifier >= 10) return BaseModularGun.ModifierTier.Perfect;
            if (modularGun.specialEffectModifier >= 5) return BaseModularGun.ModifierTier.Elite;
            return BaseModularGun.ModifierTier.Basic;
        }
    }

    public class WeaponStats
    {
        public float Damage { get; set; }
        public float Knockback { get; set; }
        public float CritChance { get; set; }
        public int UseTime { get; set; }
        public int ManaCost { get; set; }
        public string AmmoInfo { get; set; } = "";
        public string DebuffInfo { get; set; } = "";
        public string SpecialInfo { get; set; } = "";
        public float TierBonus { get; set; }
    }
}