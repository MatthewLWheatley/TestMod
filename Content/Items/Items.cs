using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.DataStructures;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using TestMod.Content.Systems;
using System.Linq;


namespace TestMod.Content.Items
{
    public abstract class BaseModularGun : ModItem
    {
        public int ammoTypeModifier = -1;
        public int damageTypeModifier = -1;
        public int shotTypeModifier = -1;
        public int specialEffectModifier = -1;


        protected int baseDamage;
        protected float baseKnockback;
        protected int baseCrit;
        protected int baseUseTime;
        public string weaponTier;
        public int maxPointBudget;

        public enum ModifierTier
        {
            Basic = 1,
            Elite = 2,
            Perfect = 4
        }

        private ModifierTier GetAmmoTypeTier()
        {
            if (ammoTypeModifier == -1) return ModifierTier.Basic;

            int itemType = GetItemTypeFromModifier(ammoTypeModifier, "ammo");

            // Check if it's a Perfect tier item
            if (itemType == ModContent.ItemType<PerfectMagicAmmoModifier>() ||
                itemType == ModContent.ItemType<PerfectArrowAmmoModifier>() ||
                itemType == ModContent.ItemType<PerfectBulletAmmoModifier>() ||
                itemType == ModContent.ItemType<PerfectRocketAmmoModifier>())
            {
                return ModifierTier.Perfect;
            }

            // Check if it's an Elite tier item
            if (itemType == ModContent.ItemType<EliteMagicAmmoModifier>() ||
                itemType == ModContent.ItemType<EliteArrowAmmoModifier>() ||
                itemType == ModContent.ItemType<EliteBulletAmmoModifier>() ||
                itemType == ModContent.ItemType<EliteRocketAmmoModifier>())
            {
                return ModifierTier.Elite;
            }

            return ModifierTier.Basic;
        }

        private ModifierTier GetDamageTypeTier()
        {
            if (damageTypeModifier == -1) return ModifierTier.Basic;

            int itemType = GetItemTypeFromModifier(damageTypeModifier, "damage");

            // Perfect tier check
            if (itemType == ModContent.ItemType<PerfectFireDamageModifier>() ||
                itemType == ModContent.ItemType<PerfectWaterDamageModifier>() ||
                itemType == ModContent.ItemType<PerfectLightningDamageModifier>() ||
                itemType == ModContent.ItemType<PerfectEarthDamageModifier>() ||
                itemType == ModContent.ItemType<PerfectWindDamageModifier>() ||
                itemType == ModContent.ItemType<PerfectSlimeDamageModifier>())
            {
                return ModifierTier.Perfect;
            }

            // Elite tier check
            if (itemType == ModContent.ItemType<EliteFireDamageModifier>() ||
                itemType == ModContent.ItemType<EliteWaterDamageModifier>() ||
                itemType == ModContent.ItemType<EliteLightningDamageModifier>() ||
                itemType == ModContent.ItemType<EliteEarthDamageModifier>() ||
                itemType == ModContent.ItemType<EliteWindDamageModifier>() ||
                itemType == ModContent.ItemType<EliteSlimeDamageModifier>())
            {
                return ModifierTier.Elite;
            }

            return ModifierTier.Basic;
        }

        private ModifierTier GetShotTypeTier()
        {
            if (shotTypeModifier == -1) return ModifierTier.Basic;

            int itemType = GetItemTypeFromModifier(shotTypeModifier, "shot");

            // Perfect tier
            if (itemType == ModContent.ItemType<PerfectAutoFireModifier>() ||
                itemType == ModContent.ItemType<PerfectBurstFireModifier>() ||
                itemType == ModContent.ItemType<PerfectChargeFireModifier>())
            {
                return ModifierTier.Perfect;
            }

            // Elite tier
            if (itemType == ModContent.ItemType<EliteAutoFireModifier>() ||
                itemType == ModContent.ItemType<EliteBurstFireModifier>() ||
                itemType == ModContent.ItemType<EliteChargeFireModifier>())
            {
                return ModifierTier.Elite;
            }

            return ModifierTier.Basic;
        }

        private ModifierTier GetSpecialEffectTier()
        {
            if (specialEffectModifier == -1) return ModifierTier.Basic;

            int itemType = GetItemTypeFromModifier(specialEffectModifier, "special");

            // Perfect tier
            if (itemType == ModContent.ItemType<PerfectPiercingModifier>() ||
                itemType == ModContent.ItemType<PerfectBouncingModifier>() ||
                itemType == ModContent.ItemType<PerfectHomingModifier>() ||
                itemType == ModContent.ItemType<PerfectLifeStealModifier>() ||
                itemType == ModContent.ItemType<PerfectCritBoostModifier>())
            {
                return ModifierTier.Perfect;
            }

            // Elite tier
            if (itemType == ModContent.ItemType<ElitePiercingModifier>() ||
                itemType == ModContent.ItemType<EliteBouncingModifier>() ||
                itemType == ModContent.ItemType<EliteHomingModifier>() ||
                itemType == ModContent.ItemType<EliteLifeStealModifier>() ||
                itemType == ModContent.ItemType<EliteCritBoostModifier>())
            {
                return ModifierTier.Elite;
            }

            return ModifierTier.Basic;
        }

        protected abstract void SetTierDefaults();

        public override void SetDefaults()
        {
            SetTierDefaults();

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
            Item.rare = GetRarityForTier();
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = false;
            Item.shoot = ProjectileID.MagicMissile;
            Item.shootSpeed = 16f;
            Item.mana = 10;
            Item.crit = baseCrit;
            maxPointBudget = ModifierData.GetWeaponPointBudget(weaponTier);
            Item.maxStack = 10;
        }

        private int GetRarityForTier()
        {
            var tierProgression = ModifierData.GetWeaponTierProgression();
            int tierIndex = tierProgression.IndexOf(weaponTier);

            return tierIndex switch
            {
                <= 3 => ItemRarityID.White,
                <= 7 => ItemRarityID.Blue,
                <= 10 => ItemRarityID.LightRed,
                <= 12 => ItemRarityID.Pink,
                <= 14 => ItemRarityID.Yellow,
                _ => ItemRarityID.Red
            };
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

        public bool IsComplete()
        {
            return ammoTypeModifier != -1 &&
                   damageTypeModifier != -1 &&
                   shotTypeModifier != -1;
        }

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

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (!IsComplete()) return false;

            Vector2 spawnPosition = position + Vector2.Normalize(velocity) * 25f;

            int projectileType = GetProjectileFromAmmoType();

            ApplyShotTypeEffects(source, spawnPosition, velocity, projectileType, damage, knockback, player);

            return false;
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
            int projIndex = Projectile.NewProjectile(source, position, velocity, projectileType, damage, knockback, player.whoAmI);

            if (projIndex >= 0 && projIndex < Main.maxProjectiles)
            {
                Projectile proj = Main.projectile[projIndex];

                ApplySpecialEffects(proj);
            }
        }

        private void ApplySpecialEffects(Projectile projectile)
        {
            if (specialEffectModifier == -1) return;

            ModifierTier specialTier = GetSpecialEffectTier();
            var globalProj = projectile.GetGlobalProjectile<ModularProjectileEffects>();

            switch (specialEffectModifier)
            {
                case 0: // Piercing - more penetration at higher tiers
                    int pierceAmount = (int)specialTier; // 1, 2, or 4 enemies
                    projectile.penetrate += pierceAmount;
                    break;

                case 1: // Bouncing - more bounces and better angles
                    globalProj.hasBouncing = true;
                    globalProj.bouncesLeft = (int)specialTier * 2; // 2, 4, or 8 bounces
                    break;

                case 2: // Homing - much stronger homing at higher tiers
                    globalProj.hasHoming = true;
                    // Stronger homing: 0.02, 0.05, 0.12 (exponential improvement)
                    globalProj.homingStrength = 0.02f * (float)specialTier * (float)specialTier / 2;
                    // Longer range: 100, 200, 400 pixels
                    globalProj.homingRange = 100 * (int)specialTier;
                    break;

                case 3: // Life Steal - handled in OnHitNPC with scaling
                    break;

                case 4: // Crit Boost - much better at higher tiers
                        // Handled in ModifyWeaponCrit
                    break;
            }
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (!IsComplete()) return;

            ModifierTier shotTier = GetShotTypeTier();
            float tierMultiplier = (float)shotTier / (float)ModifierTier.Basic; // 1x, 2x, or 4x

            switch (shotTypeModifier)
            {
                case 0: // Auto Fire - reduced damage for rapid fire, but scales with tier
                    float autoReduction = 0.8f - (0.1f * (tierMultiplier - 1)); // Better efficiency at higher tiers
                    damage *= autoReduction;
                    break;

                case 1: // Burst Fire - scales with tier
                    float burstBonus = 1.0f + (0.15f * (tierMultiplier - 1)); // +15% per tier above basic
                    damage *= burstBonus;
                    break;

                case 2: // Charge Fire - major scaling with tier
                    float chargeBonus = 1.4f + (0.3f * (tierMultiplier - 1)); // +30% per tier above basic
                    damage *= chargeBonus;
                    break;
            }

            // Additional damage bonus based on ammo type tier
            ModifierTier ammoTier = GetAmmoTypeTier();
            if (ammoTier > ModifierTier.Basic)
            {
                float ammoBonus = 1.0f + (0.1f * ((float)ammoTier - 1)); // +10% per tier level
                damage *= ammoBonus;
            }
        }

        public override void ModifyWeaponCrit(Player player, ref float crit)
        {
            if (!IsComplete()) return;

            if (specialEffectModifier == 4) // Crit Boost modifier
            {
                ModifierTier specialTier = GetSpecialEffectTier();
                float critBonus = 10f * (float)specialTier; // 10%, 20%, or 40% crit!
                crit += critBonus;
            }
        }

        public override void ModifyWeaponKnockback(Player player, ref StatModifier knockback)
        {
            if (!IsComplete()) return;

            if (damageTypeModifier == 4) // Wind - extra knockback
            {
                ModifierTier damageTier = GetDamageTypeTier();
                float knockbackBonus = 1.5f + (0.5f * ((float)damageTier - 1)); // 1.5x, 2x, or 3x
                knockback *= knockbackBonus;
            }
        }

        public override void UpdateInventory(Player player)
        {
            if (!IsComplete()) return;

            ModifierTier ammoTier = GetAmmoTypeTier();
            ModifierTier shotTier = GetShotTypeTier();

            switch (ammoTypeModifier)
            {
                case 0: // Magic - mana efficiency improves with tier
                    Item.DamageType = DamageClass.Magic;
                    Item.useAmmo = AmmoID.None;
                    // Better efficiency at higher tiers: 10, 8, 5 mana
                    Item.mana = 12 - (2 * (int)ammoTier);
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

            // Shot type timing improves with tier
            float speedBonus = 1.0f + (0.15f * ((int)shotTier - 1)); // 15% faster per tier

            switch (shotTypeModifier)
            {
                case 0: // Auto Fire - gets faster and more efficient
                    Item.useTime = (int)(baseUseTime / 2 / speedBonus);
                    Item.useAnimation = (int)(baseUseTime / 2 / speedBonus);
                    Item.autoReuse = true;
                    break;

                case 1: // Burst Fire - faster burst timing
                    Item.useTime = (int)(baseUseTime / speedBonus);
                    Item.useAnimation = (int)(baseUseTime / speedBonus);
                    Item.autoReuse = false;
                    break;

                case 2: // Charge Fire - faster charge time at higher tiers
                    Item.useTime = (int)(baseUseTime * 2 / speedBonus);
                    Item.useAnimation = (int)(baseUseTime * 2 / speedBonus);
                    Item.autoReuse = false;
                    break;
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!IsComplete()) return;

            ModifierTier damageTier = GetDamageTypeTier();
            float tierMultiplier = (float)damageTier; // 1x, 2x, or 4x

            // Base durations (in frames, 60 = 1 second)
            int baseDuration = 300; // 5 seconds
            int scaledDuration = (int)(baseDuration * tierMultiplier);

            switch (damageTypeModifier)
            {
                case 0: // Fire - longer burn + chance for stronger debuff
                    target.AddBuff(BuffID.OnFire, scaledDuration);
                    if (damageTier >= ModifierTier.Elite && Main.rand.NextFloat() < 0.3f)
                    {
                        target.AddBuff(BuffID.CursedInferno, scaledDuration / 2); // Elite+: chance for cursed fire
                    }
                    break;

                case 1: // Water - longer slow + chance for freeze
                    target.AddBuff(BuffID.Slow, scaledDuration);
                    if (damageTier >= ModifierTier.Perfect && Main.rand.NextFloat() < 0.2f)
                    {
                        target.AddBuff(BuffID.Frozen, 60); // Perfect: chance to freeze for 1 second
                    }
                    break;

                case 2: // Lightning - longer ichor + chance for electrified
                    target.AddBuff(BuffID.Ichor, scaledDuration);
                    if (damageTier >= ModifierTier.Elite && Main.rand.NextFloat() < 0.25f)
                    {
                        target.AddBuff(BuffID.Electrified, scaledDuration / 3);
                    }
                    break;

                case 3: // Earth - longer bleeding + chance for broken armor
                    target.AddBuff(BuffID.Bleeding, scaledDuration);
                    if (damageTier >= ModifierTier.Perfect && Main.rand.NextFloat() < 0.2f)
                    {
                        target.AddBuff(BuffID.BrokenArmor, scaledDuration / 2);
                    }
                    break;

                case 4: // Wind - knockback already handled, but add confusion at higher tiers
                    if (damageTier >= ModifierTier.Elite && Main.rand.NextFloat() < 0.15f)
                    {
                        target.AddBuff(BuffID.Confused, scaledDuration / 4);
                    }
                    break;

                case 5: // Slime - longer poison + chance for stronger poison
                    target.AddBuff(BuffID.Poisoned, scaledDuration);
                    if (damageTier >= ModifierTier.Perfect && Main.rand.NextFloat() < 0.3f)
                    {
                        target.AddBuff(BuffID.Venom, scaledDuration / 2);
                    }
                    break;
            }

            // Life steal scaling with special effect tier
            if (specialEffectModifier == 3) // Life Steal
            {
                ModifierTier specialTier = GetSpecialEffectTier();
                float stealRate = 0.02f * (float)specialTier; // 2%, 4%, or 8%

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

        public override void SaveData(TagCompound tag)
        {
            tag["ammoType"] = ammoTypeModifier;
            tag["damageType"] = damageTypeModifier;
            tag["shotType"] = shotTypeModifier;
            tag["specialEffect"] = specialEffectModifier;
            tag["weaponTier"] = weaponTier;
            tag["maxPointBudget"] = maxPointBudget;
        }

        public override void LoadData(TagCompound tag)
        {
            try
            {
                ModContent.GetInstance<TestMod>().Logger.Info("Loading weapon data...");
                ammoTypeModifier = tag.GetInt("ammoType");
                damageTypeModifier = tag.GetInt("damageType");
                shotTypeModifier = tag.GetInt("shotType");
                specialEffectModifier = tag.GetInt("specialEffect");
                weaponTier = tag.GetString("weaponTier");
                maxPointBudget = tag.GetInt("maxPointBudget");

                ModContent.GetInstance<TestMod>().Logger.Info($"Loaded: ammo={ammoTypeModifier}, damage={damageTypeModifier}, shot={shotTypeModifier}");

                // Fallback for old saves
                if (string.IsNullOrEmpty(weaponTier))
                {
                    weaponTier = "Copper";
                    maxPointBudget = ModifierData.GetWeaponPointBudget(weaponTier);
                }
            }
            catch (System.Exception ex)
            {
                ModContent.GetInstance<TestMod>().Logger.Error($"Error loading weapon data: {ex}");
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Change the name line to include tier
            var nameLine = tooltips.First(x => x.Name == "ItemName");
            if (nameLine != null)
            {
                nameLine.Text = $"{weaponTier} Modular Gun";
            }
            int currentPoints = GetCurrentPointUsage();
            TooltipLine pointLine = new TooltipLine(Mod, "PointBudget",
                $"Points: {currentPoints}/{maxPointBudget} ({weaponTier} Tier)");
            pointLine.OverrideColor = currentPoints <= maxPointBudget ? Color.White : Color.Red;
            tooltips.Add(pointLine);

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
            Recipe copperRecipe = CreateRecipe();
            copperRecipe.AddIngredient(ItemID.CopperBar, 5);
            copperRecipe.AddIngredient(ItemID.Wood, 10);
            copperRecipe.AddTile(TileID.WorkBenches);
            copperRecipe.Register();
        }

        public void UpgradeToTier(string targetTier)
        {
            weaponTier = targetTier;
            maxPointBudget = ModifierData.GetWeaponPointBudget(targetTier);

            // Update base stats based on tier
            UpdateStatsForTier();
        }
        private void UpdateStatsForTier()
        {
            // Scale stats based on tier progression
            var tierProgression = ModifierData.GetWeaponTierProgression();
            int tierIndex = tierProgression.IndexOf(weaponTier);

            float multiplier = 1f + (tierIndex * 0.15f); // 15% increase per tier

            Item.damage = (int)(baseDamage * multiplier);
            Item.knockBack = baseKnockback * (1f + tierIndex * 0.1f);
            Item.crit = baseCrit + (tierIndex * 2); // +2% crit per tier
            Item.value = Item.buyPrice(0, 1 + tierIndex, 0, 0);

            // Update rarity based on tier
            Item.rare = tierIndex switch
            {
                <= 3 => ItemRarityID.White,
                <= 7 => ItemRarityID.Blue,
                <= 10 => ItemRarityID.LightRed,
                <= 12 => ItemRarityID.Pink,
                <= 14 => ItemRarityID.Yellow,
                _ => ItemRarityID.Red
            };
        }

        public override void OnCreated(ItemCreationContext context)
        {
            if (context is RecipeItemCreationContext recipeContext)
            {
                List<Item> consumedItems = recipeContext.ConsumedItems;
                Recipe recipe = recipeContext.Recipe;

                foreach (Item consumedItem in consumedItems)
                {
                    if (consumedItem.ModItem is BaseModularGun sourceGun)
                    {
                        this.ammoTypeModifier = sourceGun.ammoTypeModifier;
                        this.damageTypeModifier = sourceGun.damageTypeModifier;
                        this.shotTypeModifier = sourceGun.shotTypeModifier;
                        this.specialEffectModifier = sourceGun.specialEffectModifier;

                        ModContent.GetInstance<TestMod>().Logger.Info(
                            $"Transferred modifiers from {sourceGun.weaponTier} to {this.weaponTier}: " +
                            $"ammo={ammoTypeModifier}, damage={damageTypeModifier}, shot={shotTypeModifier}, special={specialEffectModifier}"
                        );

                        break;
                    }
                }

                ModContent.GetInstance<TestMod>().Logger.Info(
                    $"Crafted {weaponTier} weapon using recipe with {consumedItems.Count} consumed items"
                );
            }
        }
    }

    public class CopperModularGun : BaseModularGun
    {
        protected override void SetTierDefaults()
        {
            baseDamage = 15;
            baseKnockback = 2f;
            baseCrit = 4;
            baseUseTime = 30;
            weaponTier = "Copper";
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CopperBar, 5);
            recipe.AddIngredient(ItemID.Wood, 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }

    public class TinModularGun : BaseModularGun
    {
        protected override void SetTierDefaults()
        {
            baseDamage = 15;
            baseKnockback = 2f;
            baseCrit = 4;
            baseUseTime = 30;
            weaponTier = "Tin";
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.TinBar, 5);
            recipe.AddIngredient(ItemID.Wood, 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }

    public class IronModularGun : BaseModularGun
    {
        protected override void SetTierDefaults()
        {
            baseDamage = 18;
            baseKnockback = 2.2f;
            baseCrit = 6;
            baseUseTime = 28;
            weaponTier = "Iron";
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CopperModularGun>(), 1);
            recipe.AddIngredient(ItemID.IronBar, 5);
            recipe.AddIngredient(ModContent.ItemType<BasicModularComponent>(), 3);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<TinModularGun>(), 1);
            recipe2.AddIngredient(ItemID.IronBar, 5);
            recipe2.AddIngredient(ModContent.ItemType<BasicModularComponent>(), 3);
            recipe2.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe2.Register();
        }
    }

    public class LeadModularGun : BaseModularGun
    {
        protected override void SetTierDefaults()
        {
            baseDamage = 18;
            baseKnockback = 2.2f;
            baseCrit = 6;
            baseUseTime = 28;
            weaponTier = "Lead";
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CopperModularGun>(), 1);
            recipe.AddIngredient(ItemID.LeadBar, 5);
            recipe.AddIngredient(ModContent.ItemType<BasicModularComponent>(), 3);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<TinModularGun>(), 1);
            recipe2.AddIngredient(ItemID.LeadBar, 5);
            recipe2.AddIngredient(ModContent.ItemType<BasicModularComponent>(), 3);
            recipe2.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe2.Register();
        }
    }

    public class SilverModularGun : BaseModularGun
    {
        protected override void SetTierDefaults()
        {
            baseDamage = 22;
            baseKnockback = 2.4f;
            baseCrit = 8;
            baseUseTime = 26;
            weaponTier = "Silver";
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<IronModularGun>(), 1);
            recipe.AddIngredient(ItemID.SilverBar, 5);
            recipe.AddIngredient(ModContent.ItemType<BasicModularComponent>(), 4);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<LeadModularGun>(), 1);
            recipe2.AddIngredient(ItemID.SilverBar, 5);
            recipe2.AddIngredient(ModContent.ItemType<BasicModularComponent>(), 4);
            recipe2.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe2.Register();
        }
    }

    public class TungstenModularGun : BaseModularGun
    {
        protected override void SetTierDefaults()
        {
            baseDamage = 22;
            baseKnockback = 2.4f;
            baseCrit = 8;
            baseUseTime = 26;
            weaponTier = "Tungsten";
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<IronModularGun>(), 1);
            recipe.AddIngredient(ItemID.TungstenBar, 5);
            recipe.AddIngredient(ModContent.ItemType<BasicModularComponent>(), 4);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<LeadModularGun>(), 1);
            recipe2.AddIngredient(ItemID.TungstenBar, 5);
            recipe2.AddIngredient(ModContent.ItemType<BasicModularComponent>(), 4);
            recipe2.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe2.Register();
        }
    }

    public class GoldModularGun : BaseModularGun
    {
        protected override void SetTierDefaults()
        {
            baseDamage = 26;
            baseKnockback = 2.6f;
            baseCrit = 10;
            baseUseTime = 24;
            weaponTier = "Gold";
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SilverModularGun>(), 1);
            recipe.AddIngredient(ItemID.GoldBar, 5);
            recipe.AddIngredient(ModContent.ItemType<BasicModularComponent>(), 5);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<TungstenModularGun>(), 1);
            recipe2.AddIngredient(ItemID.GoldBar, 5);
            recipe2.AddIngredient(ModContent.ItemType<BasicModularComponent>(), 5);
            recipe2.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe2.Register();
        }
    }

    public class PlatinumModularGun : BaseModularGun
    {
        protected override void SetTierDefaults()
        {
            baseDamage = 26;
            baseKnockback = 2.6f;
            baseCrit = 10;
            baseUseTime = 24;
            weaponTier = "Platinum";
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SilverModularGun>(), 1);
            recipe.AddIngredient(ItemID.PlatinumBar, 5);
            recipe.AddIngredient(ModContent.ItemType<BasicModularComponent>(), 5);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<TungstenModularGun>(), 1);
            recipe2.AddIngredient(ItemID.PlatinumBar, 5);
            recipe2.AddIngredient(ModContent.ItemType<BasicModularComponent>(), 5);
            recipe2.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe2.Register();
        }
    }

    // Hardmode Ore Progression (Cobalt through Titanium)

    public class CobaltModularGun : BaseModularGun
    {
        protected override void SetTierDefaults()
        {
            baseDamage = 32;
            baseKnockback = 3.0f;
            baseCrit = 12;
            baseUseTime = 22;
            weaponTier = "Cobalt";
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<GoldModularGun>(), 1);
            recipe.AddIngredient(ItemID.CobaltBar, 8);
            recipe.AddIngredient(ModContent.ItemType<BasicModularComponent>(), 8);
            recipe.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 2);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<PlatinumModularGun>(), 1);
            recipe2.AddIngredient(ItemID.CobaltBar, 8);
            recipe2.AddIngredient(ModContent.ItemType<BasicModularComponent>(), 8);
            recipe2.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 2);
            recipe2.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe2.Register();
        }
    }

    public class PalladiumModularGun : BaseModularGun
    {
        protected override void SetTierDefaults()
        {
            baseDamage = 32;
            baseKnockback = 3.0f;
            baseCrit = 12;
            baseUseTime = 22;
            weaponTier = "Palladium";
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<GoldModularGun>(), 1);
            recipe.AddIngredient(ItemID.PalladiumBar, 8);
            recipe.AddIngredient(ModContent.ItemType<BasicModularComponent>(), 8);
            recipe.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 2);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<PlatinumModularGun>(), 1);
            recipe2.AddIngredient(ItemID.PalladiumBar, 8);
            recipe2.AddIngredient(ModContent.ItemType<BasicModularComponent>(), 8);
            recipe2.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 2);
            recipe2.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe2.Register();
        }
    }

    public class MythrilModularGun : BaseModularGun
    {
        protected override void SetTierDefaults()
        {
            baseDamage = 38;
            baseKnockback = 3.2f;
            baseCrit = 14;
            baseUseTime = 20;
            weaponTier = "Mythril";
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CobaltModularGun>(), 1);
            recipe.AddIngredient(ItemID.MythrilBar, 8);
            recipe.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 3);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<PalladiumModularGun>(), 1);
            recipe2.AddIngredient(ItemID.MythrilBar, 8);
            recipe2.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 3);
            recipe2.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe2.Register();
        }
    }

    public class OrichalcumModularGun : BaseModularGun
    {
        protected override void SetTierDefaults()
        {
            baseDamage = 38;
            baseKnockback = 3.2f;
            baseCrit = 14;
            baseUseTime = 20;
            weaponTier = "Orichalcum";
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CobaltModularGun>(), 1);
            recipe.AddIngredient(ItemID.OrichalcumBar, 8);
            recipe.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 3);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<PalladiumModularGun>(), 1);
            recipe2.AddIngredient(ItemID.OrichalcumBar, 8);
            recipe2.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 3);
            recipe2.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe2.Register();
        }
    }

    public class AdamantiteModularGun : BaseModularGun
    {
        protected override void SetTierDefaults()
        {
            baseDamage = 44;
            baseKnockback = 3.4f;
            baseCrit = 16;
            baseUseTime = 18;
            weaponTier = "Adamantite";
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MythrilModularGun>(), 1);
            recipe.AddIngredient(ItemID.AdamantiteBar, 8);
            recipe.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 4);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<OrichalcumModularGun>(), 1);
            recipe2.AddIngredient(ItemID.AdamantiteBar, 8);
            recipe2.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 4);
            recipe2.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe2.Register();
        }
    }

    public class TitaniumModularGun : BaseModularGun
    {
        protected override void SetTierDefaults()
        {
            baseDamage = 44;
            baseKnockback = 3.4f;
            baseCrit = 16;
            baseUseTime = 18;
            weaponTier = "Titanium";
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MythrilModularGun>(), 1);
            recipe.AddIngredient(ItemID.TitaniumBar, 8);
            recipe.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 7);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<OrichalcumModularGun>(), 1);
            recipe2.AddIngredient(ItemID.TitaniumBar, 8);
            recipe2.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 7);
            recipe2.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe2.Register();
        }
    }

    // Post-Mechanical Bosses

    public class HallowedModularGun : BaseModularGun
    {
        protected override void SetTierDefaults()
        {
            baseDamage = 52;
            baseKnockback = 3.6f;
            baseCrit = 18;
            baseUseTime = 16;
            weaponTier = "Hallowed";
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AdamantiteModularGun>(), 1);
            recipe.AddIngredient(ItemID.HallowedBar, 10);
            recipe.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 8);
            recipe.AddIngredient(ModContent.ItemType<PerfectModularComponent>(), 2);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();
        }
    }

    // Post-Plantera

    public class ChlorophyteModularGun : BaseModularGun
    {
        protected override void SetTierDefaults()
        {
            baseDamage = 60;
            baseKnockback = 3.8f;
            baseCrit = 20;
            baseUseTime = 14;
            weaponTier = "Chlorophyte";
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<HallowedModularGun>(), 1);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
            recipe.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 5);
            recipe.AddIngredient(ModContent.ItemType<PerfectModularComponent>(), 8);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();
        }
    }

    // Post-Moon Lord

    public class LuminiteModularGun : BaseModularGun
    {
        protected override void SetTierDefaults()
        {
            baseDamage = 75;
            baseKnockback = 4.0f;
            baseCrit = 25;
            baseUseTime = 12;
            weaponTier = "Luminite";
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ChlorophyteModularGun>(), 1);
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddIngredient(ModContent.ItemType<PerfectModularComponent>(), 10);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();
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