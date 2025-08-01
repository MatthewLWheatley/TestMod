using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.DataStructures;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ModularWeapons.Content.Systems;
using System.Linq;


namespace ModularWeapons.Content.Items
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

        public ModifierTier GetSpecialEffectTier()
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
            Item.maxStack = 1;
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

            Item ammoUsed;
            int projectileType = GetProjectileFromAmmoType(player, out ammoUsed);

            // Consume ammo if not magic type
            if (!IsMagicAmmoType() && !ammoUsed.IsAir)
            {
                ammoUsed.stack--;
                if (ammoUsed.stack <= 0)
                {
                    ammoUsed.TurnToAir();
                }
            }

            ApplyShotTypeEffects(source, spawnPosition, velocity, projectileType, damage, knockback, player);

            return false;
        }

        private int GetProjectileFromAmmoType(Player player, out Item ammoItem)
        {
            ammoItem = new Item(); // Default empty item

            // Magic doesn't need ammo
            if (IsMagicAmmoType())
            {
                return ProjectileID.MagicMissile; // Or whatever magic projectile you want
            }

            int requiredAmmoType = GetRequiredAmmoID();
            if (requiredAmmoType == AmmoID.None)
            {
                return ProjectileID.WoodenArrowFriendly; // Fallback
            }

            // First check the 4 dedicated ammo slots (54-57)
            for (int i = 54; i <= 57; i++)
            {
                Item item = player.inventory[i];
                if (!item.IsAir && item.ammo == requiredAmmoType && item.stack > 0)
                {
                    ammoItem = item;

                    // DEBUG: Let's see what's actually happening
                    Main.NewText($"Found ammo: {item.Name} (ID: {item.type}) shoots projectile: {item.shoot}", Color.Yellow);

                    // Get the correct projectile since vanilla data is apparently broken
                    int correctProjectile = GetCorrectProjectileForAmmo(item);
                    Main.NewText($"Using corrected projectile: {correctProjectile}", Color.LightGreen);

                    return correctProjectile;
                }
            }

            // Then search backwards through main inventory (53 down to 0)
            for (int i = 53; i >= 0; i--)
            {
                Item item = player.inventory[i];
                if (!item.IsAir && item.ammo == requiredAmmoType && item.stack > 0)
                {
                    ammoItem = item;

                    // DEBUG: Let's see what's actually happening
                    Main.NewText($"Found ammo: {item.Name} (ID: {item.type}) shoots projectile: {item.shoot}", Color.Yellow);

                    // Get the correct projectile since vanilla data is apparently broken
                    int correctProjectile = GetCorrectProjectileForAmmo(item);
                    Main.NewText($"Using corrected projectile: {correctProjectile}", Color.LightGreen);

                    return correctProjectile;
                }
            }

            // No ammo found - return default based on ammo type
            Main.NewText($"No ammo found for type {requiredAmmoType}, using fallback", Color.Red);
            return GetFallbackProjectile();
        }

        private int GetCorrectProjectileForAmmo(Item ammoItem)
        {
            // Manual mapping
            return ammoItem.type switch
            {
                // Arrows
                ItemID.WoodenArrow => ProjectileID.WoodenArrowFriendly,
                ItemID.FlamingArrow => ProjectileID.FlamingArrow,
                ItemID.UnholyArrow => ProjectileID.UnholyArrow,
                ItemID.JestersArrow => ProjectileID.JestersArrow,
                ItemID.HellfireArrow => ProjectileID.HellfireArrow,
                ItemID.HolyArrow => ProjectileID.HolyArrow,
                ItemID.CursedArrow => ProjectileID.CursedArrow,
                ItemID.FrostburnArrow => ProjectileID.FrostburnArrow,
                ItemID.ChlorophyteArrow => ProjectileID.ChlorophyteArrow,
                ItemID.IchorArrow => ProjectileID.IchorArrow,
                ItemID.VenomArrow => ProjectileID.VenomArrow,
                ItemID.BoneArrow => ProjectileID.BoneArrow,
                ItemID.MoonlordArrow => ProjectileID.MoonlordArrow,

                // Bullets
                ItemID.MusketBall => ProjectileID.Bullet,
                ItemID.SilverBullet => ProjectileID.SilverBullet,
                ItemID.MeteorShot => ProjectileID.MeteorShot,
                ItemID.PartyBullet => ProjectileID.PartyBullet,
                ItemID.GoldenBullet => ProjectileID.GoldenBullet,
                ItemID.HighVelocityBullet => ProjectileID.BulletHighVelocity,
                ItemID.CrystalBullet => ProjectileID.CrystalBullet,
                ItemID.CursedBullet => ProjectileID.CursedBullet,
                ItemID.ChlorophyteBullet => ProjectileID.ChlorophyteBullet,
                ItemID.IchorBullet => ProjectileID.IchorBullet,
                ItemID.VenomBullet => ProjectileID.VenomBullet,
                ItemID.ExplodingBullet => ProjectileID.ExplosiveBullet,
                ItemID.EndlessMusketPouch => ProjectileID.Bullet,
                ItemID.NanoBullet => ProjectileID.NanoBullet,
                ItemID.MoonlordBullet => ProjectileID.MoonlordBullet,

                // Rockets
                ItemID.RocketI => ProjectileID.RocketI,
                ItemID.RocketII => ProjectileID.RocketII,
                ItemID.RocketIII => ProjectileID.RocketIII,
                ItemID.RocketIV => ProjectileID.RocketIV,
                ItemID.MiniNukeI => ProjectileID.MiniNukeRocketI,
                ItemID.MiniNukeII => ProjectileID.MiniNukeRocketII,
                ItemID.ClusterRocketI => ProjectileID.ClusterRocketI,
                ItemID.ClusterRocketII => ProjectileID.ClusterRocketII,
                ItemID.DryRocket => ProjectileID.DryRocket,
                ItemID.WetRocket => ProjectileID.WetRocket,
                ItemID.LavaRocket => ProjectileID.LavaRocket,
                ItemID.HoneyRocket => ProjectileID.HoneyRocket,

                _ => ammoItem.shoot
            };
        }

        private int GetRequiredAmmoID()
        {
            if (IsArrowAmmoType()) return AmmoID.Arrow;
            if (IsBulletAmmoType()) return AmmoID.Bullet;
            if (IsRocketAmmoType()) return AmmoID.Rocket;
            return AmmoID.None;
        }

        private int GetFallbackProjectile()
        {
            if (IsArrowAmmoType()) return ProjectileID.WoodenArrowFriendly;
            if (IsBulletAmmoType()) return ProjectileID.Bullet;
            if (IsRocketAmmoType()) return ProjectileID.RocketI;
            return ProjectileID.WoodenArrowFriendly;
        }

        private void ApplyShotTypeEffects(EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int projectileType, int damage, float knockback, Player player)
        {
            switch (shotTypeModifier)
            {
                case 0: // Auto Fire
                    SpawnProjectileWithEffects(source, position, velocity, projectileType, damage, knockback, player);
                    break;

                case 1: // Burst Fire
                    for (int i = 0; i < 3; i++)
                    {
                        Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(15));
                        SpawnProjectileWithEffects(source, position, perturbedSpeed, projectileType, (int)(damage * 0.8f), knockback, player);
                    }
                    break;

                case 2: // Charge Fire
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
            var globalProj = projectile.GetGlobalProjectile<Content.Projectiles.ModularProjectileEffects>();

            // Use modulo to get the base effect type, ignoring tier
            int baseEffectType = specialEffectModifier % 5;

            switch (baseEffectType)
            {
                case 0: // Piercing - more penetration at higher tiers
                    int pierceAmount = (int)specialTier; // 1, 2, or 4 enemies
                    projectile.penetrate += pierceAmount;
                    Main.NewText($"Applied piercing: +{pierceAmount} penetration (tier: {specialTier})", Color.Purple);
                    break;

                case 1: // Bouncing - more bounces and better angles
                    globalProj.hasBouncing = true;
                    globalProj.bouncesLeft = (int)specialTier * 2; // 2, 4, or 8 bounces
                    Main.NewText($"Applied bouncing: {globalProj.bouncesLeft} bounces (tier: {specialTier})", Color.Orange);
                    break;

                case 2: // Homing - much stronger homing at higher tiers
                    globalProj.hasHoming = true;
                    // Stronger homing: 0.02, 0.05, 0.12 (exponential improvement)
                    globalProj.homingStrength = 0.02f * (float)specialTier * (float)specialTier / 2;
                    // Longer range: 100, 200, 400 pixels
                    globalProj.homingRange = 100 * (int)specialTier;
                    Main.NewText($"Applied homing: strength {globalProj.homingStrength:F3}, range {globalProj.homingRange} (tier: {specialTier})", Color.Cyan);
                    break;

                case 3: // Life Steal - handled in OnHitNPC with scaling
                    Main.NewText($"Applied life steal (tier: {specialTier})", Color.Red);
                    break;

                case 4: // Crit Boost - much better at higher tiers
                        // Handled in ModifyWeaponCrit
                    Main.NewText($"Applied crit boost (tier: {specialTier})", Color.Yellow);
                    break;

                default:
                    Main.NewText($"Unknown special effect: {baseEffectType} (modifier ID: {specialEffectModifier})", Color.Gray);
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

            if (damageTypeModifier == 4)
            {
                ModifierTier damageTier = GetDamageTypeTier();
                float knockbackBonus = 1.5f + (0.5f * ((float)damageTier - 1));
                knockback *= knockbackBonus;
            }
        }

        private bool IsMagicAmmoType()
        {
            return ammoTypeModifier == 0 || ammoTypeModifier == 4 || ammoTypeModifier == 8; // Basic, Elite, Perfect Magic
        }

        private bool IsArrowAmmoType()
        {
            return ammoTypeModifier == 1 || ammoTypeModifier == 5 || ammoTypeModifier == 9; // Basic, Elite, Perfect Arrow
        }

        private bool IsBulletAmmoType()
        {
            return ammoTypeModifier == 2 || ammoTypeModifier == 6 || ammoTypeModifier == 10; // Basic, Elite, Perfect Bullet
        }

        private bool IsRocketAmmoType()
        {
            return ammoTypeModifier == 3 || ammoTypeModifier == 7 || ammoTypeModifier == 11; // Basic, Elite, Perfect Rocket
        }

        public override void UpdateInventory(Player player)
        {
            if (!IsComplete()) return;

            ModifierTier ammoTier = GetAmmoTypeTier();
            ModifierTier shotTier = GetShotTypeTier();

            if (IsMagicAmmoType())
            {
                Item.DamageType = DamageClass.Magic;
                Item.useAmmo = AmmoID.None;
                Item.mana = 12 - (2 * (int)ammoTier);
            }
            else if (IsArrowAmmoType())
            {
                Item.DamageType = DamageClass.Ranged;
                Item.useAmmo = AmmoID.Arrow;
                Item.mana = 0;
            }
            else if (IsBulletAmmoType())
            {
                Item.DamageType = DamageClass.Ranged;
                Item.useAmmo = AmmoID.Bullet;
                Item.mana = 0;
            }
            else if (IsRocketAmmoType())
            {
                Item.DamageType = DamageClass.Ranged;
                Item.useAmmo = AmmoID.Rocket;
                Item.mana = 0;
            }
            else
            {
                Item.DamageType = DamageClass.Magic;
                Item.useAmmo = AmmoID.None;
                Item.mana = 10;
            }

            float speedBonus = 1.0f + (0.1f * ((int)shotTier - 1));

            switch (shotTypeModifier % 3)
            {
                case 0:
                    int autoFrames = shotTypeModifier switch
                    {
                        0 => 8,
                        3 => 4,
                        6 => 2,
                        _ => 8
                    };
                    Item.useTime = autoFrames;
                    Item.useAnimation = autoFrames;
                    Item.autoReuse = true;
                    break;

                case 1:
                    Item.useTime = (int)(baseUseTime / speedBonus);
                    Item.useAnimation = (int)(baseUseTime / speedBonus);
                    Item.autoReuse = false;
                    break;

                case 2:
                    Item.useTime = (int)(baseUseTime * 2 / speedBonus);
                    Item.useAnimation = (int)(baseUseTime * 2 / speedBonus);
                    Item.autoReuse = false;
                    break;
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!IsComplete()) return;
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
                ModContent.GetInstance<ModularWeapons>().Logger.Info("Loading weapon data...");
                ammoTypeModifier = tag.GetInt("ammoType");
                damageTypeModifier = tag.GetInt("damageType");
                shotTypeModifier = tag.GetInt("shotType");
                specialEffectModifier = tag.GetInt("specialEffect");
                weaponTier = tag.GetString("weaponTier");
                maxPointBudget = tag.GetInt("maxPointBudget");

                ModContent.GetInstance<ModularWeapons>().Logger.Info($"Loaded: ammo={ammoTypeModifier}, damage={damageTypeModifier}, shot={shotTypeModifier}");

                // Fallback for old saves
                if (string.IsNullOrEmpty(weaponTier))
                {
                    weaponTier = "Copper";
                    maxPointBudget = ModifierData.GetWeaponPointBudget(weaponTier);
                }
            }
            catch (System.Exception ex)
            {
                ModContent.GetInstance<ModularWeapons>().Logger.Error($"Error loading weapon data: {ex}");
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
                        4 => "Elite Magic",
                        5 => "Elite Arrow",
                        6 => "Elite Bullet", 
                        7 => "Elite Rocket",
                        8 => "Perfect Magic",
                        9 => "Perfect Arrow",
                        10 => "Perfect Bullet",
                        11 => "Perfect Rocket",
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
                        6 => "Elite Fire",
                        7 => "Elite Water",
                        8 => "Elite Lightning",
                        9 => "Elite Earth",
                        10 => "Elite Wind",
                        11 => "Elite Slime",
                        12 => "Perfect Fire",
                        13 => "Perfect Water",
                        14 => "Perfect Lightning",
                        15 => "Perfect Earth",
                        16 => "Perfect Wind",
                        17 => "Perfect Slime",
                        _ => "Unknown"
                    };
                case "shot":
                    return modifierID switch
                    {
                        0 => "Auto",
                        1 => "Burst",
                        2 => "Charge",
                        3 => "Elite Auto",
                        4 => "Elite Burst",
                        5 => "Elite Charge",
                        6 => "Perfect Auto",
                        7 => "Perfect Burst",
                        8 => "Perfect Charge",
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
                        5 => "Elite Piercing",
                        6 => "Elite Bouncing",
                        7 => "Elite Homing",
                        8 => "Elite Life Steal",
                        9 => "Elite Crit Boost",
                        10 => "Perfect Piercing",
                        11 => "Perfect Bouncing",
                        12 => "Perfect Homing",
                        13 => "Perfect Life Steal",
                        14 => "Perfect Crit Boost",
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
                        4 => ModContent.ItemType<EliteMagicAmmoModifier>(),
                        5 => ModContent.ItemType<EliteArrowAmmoModifier>(),
                        6 => ModContent.ItemType<EliteBulletAmmoModifier>(),
                        7 => ModContent.ItemType<EliteRocketAmmoModifier>(),
                        8 => ModContent.ItemType<PerfectMagicAmmoModifier>(),
                        9 => ModContent.ItemType<PerfectArrowAmmoModifier>(),
                        10 => ModContent.ItemType<PerfectBulletAmmoModifier>(),
                        11 => ModContent.ItemType<PerfectRocketAmmoModifier>(),
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
                        6 => ModContent.ItemType<EliteFireDamageModifier>(),
                        7 => ModContent.ItemType<EliteWaterDamageModifier>(),
                        8 => ModContent.ItemType<EliteLightningDamageModifier>(),
                        9 => ModContent.ItemType<EliteEarthDamageModifier>(),
                        10 => ModContent.ItemType<EliteWindDamageModifier>(),
                        11 => ModContent.ItemType<EliteSlimeDamageModifier>(),
                        12 => ModContent.ItemType<PerfectFireDamageModifier>(),
                        13 => ModContent.ItemType<PerfectWaterDamageModifier>(),
                        14 => ModContent.ItemType<PerfectLightningDamageModifier>(),
                        15 => ModContent.ItemType<PerfectEarthDamageModifier>(),
                        16 => ModContent.ItemType<PerfectWindDamageModifier>(),
                        17 => ModContent.ItemType<PerfectSlimeDamageModifier>(),
                        _ => 0
                    };
                case "shot":
                    return modifierID switch
                    {
                        0 => ModContent.ItemType<AutoFireModifier>(),
                        1 => ModContent.ItemType<BurstFireModifier>(),
                        2 => ModContent.ItemType<ChargeFireModifier>(),
                        3 => ModContent.ItemType<EliteAutoFireModifier>(),
                        4 => ModContent.ItemType<EliteBurstFireModifier>(),
                        5 => ModContent.ItemType<EliteChargeFireModifier>(),
                        6 => ModContent.ItemType<PerfectAutoFireModifier>(),
                        7 => ModContent.ItemType<PerfectBurstFireModifier>(),
                        8 => ModContent.ItemType<PerfectChargeFireModifier>(),
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
                        5 => ModContent.ItemType<ElitePiercingModifier>(),
                        6 => ModContent.ItemType<EliteBouncingModifier>(),
                        7 => ModContent.ItemType<EliteHomingModifier>(),
                        8 => ModContent.ItemType<EliteLifeStealModifier>(),
                        9 => ModContent.ItemType<EliteCritBoostModifier>(),
                        10 => ModContent.ItemType<PerfectPiercingModifier>(),
                        11 => ModContent.ItemType<PerfectBouncingModifier>(),
                        12 => ModContent.ItemType<PerfectHomingModifier>(),
                        13 => ModContent.ItemType<PerfectLifeStealModifier>(),
                        14 => ModContent.ItemType<PerfectCritBoostModifier>(),
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

                        ModContent.GetInstance<ModularWeapons>().Logger.Info(
                            $"Transferred modifiers from {sourceGun.weaponTier} to {this.weaponTier}: " +
                            $"ammo={ammoTypeModifier}, damage={damageTypeModifier}, shot={shotTypeModifier}, special={specialEffectModifier}"
                        );

                        break;
                    }
                }

                ModContent.GetInstance<ModularWeapons>().Logger.Info(
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

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<TitaniumModularGun>(), 1);
            recipe2.AddIngredient(ItemID.HallowedBar, 10);
            recipe2.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 8);
            recipe2.AddIngredient(ModContent.ItemType<PerfectModularComponent>(), 2);
            recipe2.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe2.Register();
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