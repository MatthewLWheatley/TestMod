using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using TestMod.Content.Items;
using TestMod.Content.Systems;

namespace TestMod.Content.UI
{
    public class ModifierStationUI : UIState
    {

        public static bool Visible { get; set; }
        public static Vector2 StationPosition { get; set; }
        
        private UIPanel mainPanel;
        private UIPanel statsPanel; // NEW: Stats display panel
        private UIText titleText;
        
        private bool isDragging;
        private Vector2 dragOffset;
        
        private Item[] weaponItems = new Item[1];
        private Item[] modifier1Items = new Item[1];
        private Item[] modifier2Items = new Item[1];
        private Item[] modifier3Items = new Item[1];
        private Item[] specialItems = new Item[1];
        
        private UIItemSlot weaponSlot;
        private UIItemSlot modifier1Slot;
        private UIItemSlot modifier2Slot;
        private UIItemSlot modifier3Slot;
        private UIItemSlot specialSlot;
        
        private UIText modifier1Label;
        private UIText modifier2Label;
        private UIText modifier3Label;
        private UIText specialLabel;
        
        private UITextPanel<string> xCloseButton;
        
        private Item previousWeapon = new Item();
        private Item[] previousModifier1 = new Item[1] { new Item() };
        private Item[] previousModifier2 = new Item[1] { new Item() };
        private Item[] previousModifier3 = new Item[1] { new Item() };
        private Item[] previousSpecial = new Item[1] { new Item() };
        
        private UIText pointBudgetText;
        private UIText pointWarningText;

        // NEW: Stats display elements
        private UIText statsTitle;
        private UIText damageText;
        private UIText knockbackText;
        private UIText critText;
        private UIText useTimeText;
        private UIText manaText;
        private UIText ammoTypeText;
        private UIText debuffText;
        private UIText specialEffectText;
        private UIText tierBonusText;

        public override void OnInitialize()
        {
            weaponItems[0] = new Item();
            modifier1Items[0] = new Item();
            modifier2Items[0] = new Item();
            modifier3Items[0] = new Item();
            specialItems[0] = new Item();

            // Main panel (left side) - made slightly narrower
            mainPanel = new UIPanel();
            mainPanel.SetPadding(0);
            mainPanel.Left.Set(300, 0f);
            mainPanel.Top.Set(150, 0f);
            mainPanel.Width.Set(350, 0f); // Slightly wider for better spacing
            mainPanel.Height.Set(450, 0f);
            mainPanel.BackgroundColor = new Color(130, 150, 200, 180);

            mainPanel.OnLeftMouseDown += StartDragging;
            mainPanel.OnLeftMouseUp += StopDragging;

            Append(mainPanel);

            // NEW: Stats panel (right side)
            statsPanel = new UIPanel();
            statsPanel.SetPadding(8);
            statsPanel.Left.Set(660, 0f); // Position to the right of main panel
            statsPanel.Top.Set(150, 0f);
            statsPanel.Width.Set(280, 0f);
            statsPanel.Height.Set(450, 0f);
            statsPanel.BackgroundColor = new Color(100, 120, 160, 180);
            Append(statsPanel);

            titleText = new UIText("Modifier Station");
            titleText.Left.Set(10, 0f);
            titleText.Top.Set(10, 0f);
            mainPanel.Append(titleText);

            xCloseButton = new UITextPanel<string>("X");
            xCloseButton.Left.Set(310, 0f); // Adjusted for wider panel
            xCloseButton.Top.Set(5, 0f);
            xCloseButton.Width.Set(30, 0f);
            xCloseButton.Height.Set(25, 0f);
            xCloseButton.BackgroundColor = new Color(180, 40, 40, 200);
            xCloseButton.OnLeftClick += CloseUI;
            mainPanel.Append(xCloseButton);

            pointBudgetText = new UIText("Points: 0/0");
            pointBudgetText.Left.Set(10, 0f);
            pointBudgetText.Top.Set(35, 0f);
            mainPanel.Append(pointBudgetText);

            // Weapon slot
            weaponSlot = new UIItemSlot(weaponItems, 0, ItemSlot.Context.BankItem);
            weaponSlot.Left.Set(60, 0f);
            weaponSlot.Top.Set(65, 0f);
            weaponSlot.Width.Set(52, 0f);
            weaponSlot.Height.Set(52, 0f);
            mainPanel.Append(weaponSlot);

            UIText weaponLabel = new UIText("Modular Weapon");
            weaponLabel.Left.Set(130, 0f);
            weaponLabel.Top.Set(80, 0f);
            mainPanel.Append(weaponLabel);

            // Modifier slots
            modifier1Slot = new UIItemSlot(modifier1Items, 0, ItemSlot.Context.BankItem);
            modifier1Slot.Left.Set(60, 0f);
            modifier1Slot.Top.Set(135, 0f);
            modifier1Slot.Width.Set(52, 0f);
            modifier1Slot.Height.Set(52, 0f);
            mainPanel.Append(modifier1Slot);

            modifier1Label = new UIText("Modifier Slot 1");
            modifier1Label.Left.Set(130, 0f);
            modifier1Label.Top.Set(150, 0f);
            modifier1Label.TextColor = Color.Gray;
            mainPanel.Append(modifier1Label);

            modifier2Slot = new UIItemSlot(modifier2Items, 0, ItemSlot.Context.BankItem);
            modifier2Slot.Left.Set(60, 0f);
            modifier2Slot.Top.Set(205, 0f);
            modifier2Slot.Width.Set(52, 0f);
            modifier2Slot.Height.Set(52, 0f);
            mainPanel.Append(modifier2Slot);

            modifier2Label = new UIText("Modifier Slot 2");
            modifier2Label.Left.Set(130, 0f);
            modifier2Label.Top.Set(220, 0f);
            modifier2Label.TextColor = Color.Gray;
            mainPanel.Append(modifier2Label);

            modifier3Slot = new UIItemSlot(modifier3Items, 0, ItemSlot.Context.BankItem);
            modifier3Slot.Left.Set(60, 0f);
            modifier3Slot.Top.Set(275, 0f);
            modifier3Slot.Width.Set(52, 0f);
            modifier3Slot.Height.Set(52, 0f);
            mainPanel.Append(modifier3Slot);

            modifier3Label = new UIText("Modifier Slot 3");
            modifier3Label.Left.Set(130, 0f);
            modifier3Label.Top.Set(290, 0f);
            modifier3Label.TextColor = Color.Gray;
            mainPanel.Append(modifier3Label);

            specialSlot = new UIItemSlot(specialItems, 0, ItemSlot.Context.BankItem);
            specialSlot.Left.Set(60, 0f);
            specialSlot.Top.Set(345, 0f);
            specialSlot.Width.Set(52, 0f);
            specialSlot.Height.Set(52, 0f);
            mainPanel.Append(specialSlot);

            specialLabel = new UIText("Special Effects");
            specialLabel.Left.Set(130, 0f);
            specialLabel.Top.Set(360, 0f);
            specialLabel.TextColor = Color.Gold;
            mainPanel.Append(specialLabel);

            pointWarningText = new UIText("");
            pointWarningText.Left.Set(10, 0f);
            pointWarningText.Top.Set(400, 0f);
            pointWarningText.Width.Set(300, 0f);
            pointWarningText.Height.Set(20, 0f);
            mainPanel.Append(pointWarningText);

            UIText statusText = new UIText("Place weapon to see modifier categories");
            statusText.Left.Set(10, 0f);
            statusText.Top.Set(420, 0f);
            statusText.Width.Set(300, 0f);
            statusText.Height.Set(25, 0f);
            statusText.TextColor = Color.LightGray;
            mainPanel.Append(statusText);

            // NEW: Initialize stats panel elements
            InitializeStatsPanel();
        }

        private void InitializeStatsPanel()
        {
            float yOffset = 0f;
            float lineHeight = 22f;

            statsTitle = new UIText("Weapon Statistics", 0.9f);
            statsTitle.Left.Set(5, 0f);
            statsTitle.Top.Set(yOffset, 0f);
            statsTitle.TextColor = Color.Yellow;
            statsPanel.Append(statsTitle);
            yOffset += lineHeight + 5f;

            damageText = new UIText("Damage: --", 0.8f);
            damageText.Left.Set(5, 0f);
            damageText.Top.Set(yOffset, 0f);
            statsPanel.Append(damageText);
            yOffset += lineHeight;

            knockbackText = new UIText("Knockback: --", 0.8f);
            knockbackText.Left.Set(5, 0f);
            knockbackText.Top.Set(yOffset, 0f);
            statsPanel.Append(knockbackText);
            yOffset += lineHeight;

            critText = new UIText("Critical Chance: --", 0.8f);
            critText.Left.Set(5, 0f);
            critText.Top.Set(yOffset, 0f);
            statsPanel.Append(critText);
            yOffset += lineHeight;

            useTimeText = new UIText("Use Time: --", 0.8f);
            useTimeText.Left.Set(5, 0f);
            useTimeText.Top.Set(yOffset, 0f);
            statsPanel.Append(useTimeText);
            yOffset += lineHeight;

            manaText = new UIText("Mana Cost: --", 0.8f);
            manaText.Left.Set(5, 0f);
            manaText.Top.Set(yOffset, 0f);
            statsPanel.Append(manaText);
            yOffset += lineHeight + 10f;

            ammoTypeText = new UIText("Ammo: None", 0.8f);
            ammoTypeText.Left.Set(5, 0f);
            ammoTypeText.Top.Set(yOffset, 0f);
            ammoTypeText.TextColor = Color.LightBlue;
            statsPanel.Append(ammoTypeText);
            yOffset += lineHeight;

            debuffText = new UIText("Debuff: None", 0.8f);
            debuffText.Left.Set(5, 0f);
            debuffText.Top.Set(yOffset, 0f);
            debuffText.TextColor = Color.Orange;
            statsPanel.Append(debuffText);
            yOffset += lineHeight;

            specialEffectText = new UIText("Special: None", 0.8f);
            specialEffectText.Left.Set(5, 0f);
            specialEffectText.Top.Set(yOffset, 0f);
            specialEffectText.TextColor = Color.Gold;
            statsPanel.Append(specialEffectText);
            yOffset += lineHeight;

            tierBonusText = new UIText("Tier Bonus: --", 0.8f);
            tierBonusText.Left.Set(5, 0f);
            tierBonusText.Top.Set(yOffset, 0f);
            tierBonusText.TextColor = Color.LightGreen;
            statsPanel.Append(tierBonusText);
        }
        
        private void StartDragging(UIMouseEvent evt, UIElement listeningElement)
        {
            if (evt.Target == mainPanel || evt.Target == titleText)
            {
                isDragging = true;
                dragOffset = new Vector2(evt.MousePosition.X - mainPanel.Left.Pixels, 
                                       evt.MousePosition.Y - mainPanel.Top.Pixels);
            }
        }
        
        private void StopDragging(UIMouseEvent evt, UIElement listeningElement)
        {
            isDragging = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (isDragging && Main.mouseLeft)
            {
                Vector2 newPosition = Main.MouseScreen - dragOffset;

                // Ensure both panels stay on screen
                newPosition.X = MathHelper.Clamp(newPosition.X, 0, Main.screenWidth - (mainPanel.Width.Pixels + statsPanel.Width.Pixels + 10));
                newPosition.Y = MathHelper.Clamp(newPosition.Y, 0, Main.screenHeight - mainPanel.Height.Pixels);

                mainPanel.Left.Set(newPosition.X, 0f);
                mainPanel.Top.Set(newPosition.Y, 0f);
                
                // Move stats panel with main panel
                statsPanel.Left.Set(newPosition.X + mainPanel.Width.Pixels + 10, 0f);
                statsPanel.Top.Set(newPosition.Y, 0f);
                
                Recalculate();
            }

            if (Visible && Vector2.Distance(Main.LocalPlayer.Center, StationPosition) > 160f)
            {
                CloseUI(null, null);
            }

            HandleAutoModifierManagement();
            UpdatePointDisplay();
            UpdateModifierLabels();
            UpdateStatsDisplay(); // NEW: Update the stats display
        }

        // NEW: Method to update all stats displays with calculated values
        private void UpdateStatsDisplay()
        {
            BaseModularGun modularGun = weaponItems[0].ModItem as BaseModularGun;
            
            if (modularGun == null)
            {
                // Clear all stats when no weapon
                damageText.SetText("Damage: --");
                knockbackText.SetText("Knockback: --");
                critText.SetText("Critical Chance: --");
                useTimeText.SetText("Use Time: --");
                manaText.SetText("Mana Cost: --");
                ammoTypeText.SetText("Ammo: None");
                debuffText.SetText("Debuff: None");
                specialEffectText.SetText("Special: None");
                tierBonusText.SetText("Tier Bonus: --");
                return;
            }

            var weaponItem = weaponItems[0];
            
            // Calculate actual damage with all modifiers
            float calculatedDamage = CalculateActualDamage(modularGun, weaponItem);
            damageText.SetText($"Damage: {calculatedDamage:F0}");
            damageText.TextColor = calculatedDamage > weaponItem.damage ? Color.LightGreen : Color.White;
            
            // Calculate actual knockback with modifiers
            float calculatedKnockback = CalculateActualKnockback(modularGun, weaponItem);
            knockbackText.SetText($"Knockback: {calculatedKnockback:F1}");
            knockbackText.TextColor = calculatedKnockback > weaponItem.knockBack ? Color.LightGreen : Color.White;
            
            // Calculate actual crit with modifiers
            float calculatedCrit = CalculateActualCrit(modularGun, weaponItem);
            critText.SetText($"Critical Chance: {calculatedCrit:F0}%");
            critText.TextColor = calculatedCrit > weaponItem.crit ? Color.LightGreen : Color.White;
            
            // Calculate actual use time with modifiers
            int calculatedUseTime = CalculateActualUseTime(modularGun, weaponItem);
            useTimeText.SetText($"Use Time: {calculatedUseTime} frames");
            useTimeText.TextColor = calculatedUseTime < weaponItem.useTime ? Color.LightGreen : Color.White;
            
            // Calculate actual mana cost with modifiers
            int calculatedMana = CalculateActualManaCost(modularGun, weaponItem);
            manaText.SetText($"Mana Cost: {calculatedMana}");
            manaText.TextColor = calculatedMana > 0 ? Color.LightBlue : Color.Gray;

            // Ammo type info
            UpdateAmmoTypeDisplay(modularGun);
            
            // Debuff info
            UpdateDebuffDisplay(modularGun);
            
            // Special effects info
            UpdateSpecialEffectDisplay(modularGun);
            
            // Tier bonus info
            UpdateTierBonusDisplay(modularGun);
        }

        // NEW: Calculate actual damage including all modifier effects
        private float CalculateActualDamage(BaseModularGun modularGun, Item weaponItem)
        {
            if (!modularGun.IsComplete()) return weaponItem.damage;

            float damage = weaponItem.damage;
            
            // Apply shot type damage modifiers (copied from ModifyWeaponDamage)
            Content.Items.BaseModularGun.ModifierTier shotTier = GetShotTypeTierEnum(modularGun);
            float tierMultiplier = (float)shotTier / (float)BaseModularGun.ModifierTier.Basic; // 1x, 2x, or 4x

            switch (modularGun.shotTypeModifier % 3) // Get base shot type
            {
                case 0: // Auto Fire - reduced damage for rapid fire
                    float autoReduction = 0.8f - (0.1f * (tierMultiplier - 1));
                    damage *= autoReduction;
                    break;
                case 1: // Burst Fire - scales with tier
                    float burstBonus = 1.0f + (0.15f * (tierMultiplier - 1));
                    damage *= burstBonus;
                    break;
                case 2: // Charge Fire - major scaling with tier
                    float chargeBonus = 1.4f + (0.3f * (tierMultiplier - 1));
                    damage *= chargeBonus;
                    break;
            }

            // Apply ammo type tier bonus
            Content.Items.BaseModularGun.ModifierTier ammoTier = GetAmmoTypeTierEnum(modularGun);
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

        // NEW: Calculate actual knockback including modifiers
        private float CalculateActualKnockback(BaseModularGun modularGun, Item weaponItem)
        {
            if (!modularGun.IsComplete()) return weaponItem.knockBack;

            float knockback = weaponItem.knockBack;

            // Apply wind damage type knockback bonus
            if (modularGun.damageTypeModifier % 6 == 4) // Wind damage
            {
                Content.Items.BaseModularGun.ModifierTier damageTier = GetDamageTypeTierEnum(modularGun);
                float knockbackBonus = 1.5f + (0.5f * ((float)damageTier - 1)); // 1.5x, 2x, or 3x
                knockback *= knockbackBonus;
            }

            return knockback;
        }

        // NEW: Calculate actual crit including modifiers
        private float CalculateActualCrit(BaseModularGun modularGun, Item weaponItem)
        {
            float crit = weaponItem.crit;

            // Apply crit boost special effect
            if (modularGun.specialEffectModifier % 5 == 4) // Crit Boost modifier
            {
                Content.Items.BaseModularGun.ModifierTier specialTier = GetSpecialEffectTierEnum(modularGun);
                float critBonus = 10f * (float)specialTier; // 10%, 20%, or 40% crit
                crit += critBonus;
            }

            return crit;
        }

        // NEW: Calculate actual use time including modifiers
        private int CalculateActualUseTime(BaseModularGun modularGun, Item weaponItem)
        {
            if (!modularGun.IsComplete()) return weaponItem.useTime;

            Content.Items.BaseModularGun.ModifierTier shotTier = GetShotTypeTierEnum(modularGun);
            float speedBonus = 1.0f + (0.15f * ((int)shotTier - 1)); // 15% faster per tier

            int useTime = weaponItem.useTime;

            switch (modularGun.shotTypeModifier % 3) // Get base shot type
            {
                case 0: // Auto Fire - gets faster
                    useTime = (int)(weaponItem.useTime / 2 / speedBonus);
                    break;
                case 1: // Burst Fire - faster burst timing
                    useTime = (int)(weaponItem.useTime / speedBonus);
                    break;
                case 2: // Charge Fire - faster charge time at higher tiers
                    useTime = (int)(weaponItem.useTime * 2 / speedBonus);
                    break;
            }

            return useTime;
        }

        // NEW: Calculate actual mana cost including modifiers
        private int CalculateActualManaCost(BaseModularGun modularGun, Item weaponItem)
        {
            if (!modularGun.IsComplete()) return weaponItem.mana;

            if (modularGun.ammoTypeModifier % 4 == 0) // Magic ammo type
            {
                Content.Items.BaseModularGun.ModifierTier ammoTier = GetAmmoTypeTierEnum(modularGun);
                // Better efficiency at higher tiers: 12, 10, 8, 6 mana
                return 12 - (2 * (int)ammoTier);
            }

            return 0; // Non-magic weapons don't use mana
        }

        // NEW: Helper methods to get modifier tiers as enums
        private BaseModularGun.ModifierTier GetAmmoTypeTierEnum(BaseModularGun modularGun)
        {
            if (modularGun.ammoTypeModifier >= 8) return BaseModularGun.ModifierTier.Perfect;
            if (modularGun.ammoTypeModifier >= 4) return BaseModularGun.ModifierTier.Elite;
            return BaseModularGun.ModifierTier.Basic;
        }

        private BaseModularGun.ModifierTier GetShotTypeTierEnum(BaseModularGun modularGun)
        {
            if (modularGun.shotTypeModifier >= 6) return BaseModularGun.ModifierTier.Perfect;
            if (modularGun.shotTypeModifier >= 3) return BaseModularGun.ModifierTier.Elite;
            return BaseModularGun.ModifierTier.Basic;
        }

        private BaseModularGun.ModifierTier GetDamageTypeTierEnum(BaseModularGun modularGun)
        {
            if (modularGun.damageTypeModifier >= 12) return BaseModularGun.ModifierTier.Perfect;
            if (modularGun.damageTypeModifier >= 6) return BaseModularGun.ModifierTier.Elite;
            return BaseModularGun.ModifierTier.Basic;
        }

        private BaseModularGun.ModifierTier GetSpecialEffectTierEnum(BaseModularGun modularGun)
        {
            if (modularGun.specialEffectModifier >= 10) return BaseModularGun.ModifierTier.Perfect;
            if (modularGun.specialEffectModifier >= 5) return BaseModularGun.ModifierTier.Elite;
            return BaseModularGun.ModifierTier.Basic;
        }

        private void UpdateAmmoTypeDisplay(BaseModularGun modularGun)
        {
            if (modularGun.ammoTypeModifier == -1)
            {
                ammoTypeText.SetText("Ammo: None");
                ammoTypeText.TextColor = Color.Gray;
                return;
            }

            string ammoType = GetAmmoTypeString(modularGun.ammoTypeModifier);
            var tier = GetAmmoTypeTier(modularGun);
            
            ammoTypeText.SetText($"Ammo: {ammoType} ({tier})");
            ammoTypeText.TextColor = tier switch
            {
                "Basic" => Color.White,
                "Elite" => Color.Yellow,
                "Perfect" => Color.Purple,
                _ => Color.Gray
            };
        }

        private void UpdateDebuffDisplay(BaseModularGun modularGun)
        {
            if (modularGun.damageTypeModifier == -1)
            {
                debuffText.SetText("Debuff: None");
                debuffText.TextColor = Color.Gray;
                return;
            }

            var (debuffName, duration) = GetDebuffInfo(modularGun);
            debuffText.SetText($"Debuff: {debuffName} ({duration:F1}s)");
            debuffText.TextColor = Color.Orange;
        }

        private void UpdateSpecialEffectDisplay(BaseModularGun modularGun)
        {
            if (modularGun.specialEffectModifier == -1)
            {
                specialEffectText.SetText("Special: None");
                specialEffectText.TextColor = Color.Gray;
                return;
            }

            string specialInfo = GetSpecialEffectInfo(modularGun);
            specialEffectText.SetText($"Special: {specialInfo}");
            specialEffectText.TextColor = Color.Gold;
        }

        private void UpdateTierBonusDisplay(BaseModularGun modularGun)
        {
            var tierProgression = ModifierData.GetWeaponTierProgression();
            int tierIndex = tierProgression.IndexOf(modularGun.weaponTier);
            float multiplier = 1f + (tierIndex * 0.15f);
            
            tierBonusText.SetText($"Tier Bonus: +{((multiplier - 1) * 100):F0}% damage");
            tierBonusText.TextColor = tierIndex > 0 ? Color.LightGreen : Color.Gray;
        }

        // Helper methods for stat calculations
        private string GetAmmoTypeString(int ammoModifier)
        {
            return (ammoModifier % 4) switch // Get base type regardless of tier
            {
                0 => "Magic",
                1 => "Arrows",
                2 => "Bullets", 
                3 => "Rockets",
                _ => "Unknown"
            };
        }

        private string GetAmmoTypeTier(BaseModularGun modularGun)
        {
            if (modularGun.ammoTypeModifier >= 8) return "Perfect";
            if (modularGun.ammoTypeModifier >= 4) return "Elite";
            return "Basic";
        }

        private (string debuffName, float duration) GetDebuffInfo(BaseModularGun modularGun)
        {
            var tier = GetDamageTypeTier(modularGun);
            float tierMultiplier = tier switch
            {
                "Perfect" => 4f,
                "Elite" => 2f,
                _ => 1f
            };
            
            float baseDuration = 5f; // 5 seconds base
            float duration = baseDuration * tierMultiplier;
            
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
            
            return (debuffName, duration);
        }

        private string GetDamageTypeTier(BaseModularGun modularGun)
        {
            if (modularGun.damageTypeModifier >= 12) return "Perfect";
            if (modularGun.damageTypeModifier >= 6) return "Elite";
            return "Basic";
        }

        private BaseModularGun.ModifierTier GetSpecialEffectTier(BaseModularGun modularGun)
        {
            return GetSpecialEffectTierEnum(modularGun);
        }

        private string GetSpecialEffectInfo(BaseModularGun modularGun)
        {
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

        // Rest of the existing methods remain the same...
        private void UpdateModifierLabels()
        {
            Item currentWeapon = weaponItems[0];
            
            if (currentWeapon.ModItem is BaseModularGun)
            {
                modifier1Label.SetText("Ammo Type");
                modifier1Label.TextColor = Color.White;
                
                modifier2Label.SetText("Damage Type");
                modifier2Label.TextColor = Color.White;
                
                modifier3Label.SetText("Shot Type");
                modifier3Label.TextColor = Color.White;
            }
            else
            {
                modifier1Label.SetText("Modifier Slot 1");
                modifier1Label.TextColor = Color.Gray;
                
                modifier2Label.SetText("Modifier Slot 2");
                modifier2Label.TextColor = Color.Gray;
                
                modifier3Label.SetText("Modifier Slot 3");
                modifier3Label.TextColor = Color.Gray;
            }
        }

        private void HandleAutoModifierManagement()
        {
            Item currentWeapon = weaponItems[0];

            if (!currentWeapon.IsAir && !(currentWeapon.ModItem is BaseModularGun))
            {
                Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_FromThis(), currentWeapon);
                weaponItems[0].TurnToAir();
                Main.NewText("Only modular weapons can be placed here!", Color.Red);
                return;
            }

            if (previousWeapon.IsAir && (currentWeapon.ModItem is BaseModularGun))
            {
                AutoExtractModifiers();
            }
            else if (previousWeapon.type == ModContent.ItemType<BaseModularGun>() && currentWeapon.IsAir)
            {
                ClearAllModifierSlots();
            }

            if (currentWeapon.ModItem is BaseModularGun)
            {
                CheckAndInstallNewModifiers();
                CheckAndRemoveModifiers();
            }

            previousWeapon = currentWeapon.Clone();
            previousModifier1[0] = modifier1Items[0].Clone();
            previousModifier2[0] = modifier2Items[0].Clone();
            previousModifier3[0] = modifier3Items[0].Clone();
            previousSpecial[0] = specialItems[0].Clone();
        }

        private void UpdatePointDisplay()
        {
            BaseModularGun modularGun = weaponItems[0].ModItem as BaseModularGun;

            if (modularGun != null)
            {
                int currentPoints = modularGun.GetCurrentPointUsage();
                int maxPoints = modularGun.maxPointBudget;
                string tier = modularGun.weaponTier;

                pointBudgetText.SetText($"Points: {currentPoints}/{maxPoints} ({tier})");

                if (currentPoints > maxPoints)
                {
                    pointBudgetText.TextColor = Color.Red;
                    pointWarningText.SetText("⚠ OVER BUDGET - Weapon cannot be used!");
                    pointWarningText.TextColor = Color.Red;
                }
                else if (currentPoints == maxPoints)
                {
                    pointBudgetText.TextColor = Color.Yellow;
                    pointWarningText.SetText("At maximum capacity");
                    pointWarningText.TextColor = Color.Yellow;
                }
                else
                {
                    pointBudgetText.TextColor = Color.White;
                    pointWarningText.SetText($"{maxPoints - currentPoints} points remaining");
                    pointWarningText.TextColor = Color.LightGreen;
                }
            }
            else
            {
                pointBudgetText.SetText("Points: -/- (No Weapon)");
                pointBudgetText.TextColor = Color.Gray;
                pointWarningText.SetText("");
            }
        }

        // [Keep all the existing methods: CheckAndInstallNewModifiers, ClearAllModifierSlots, 
        // CheckAndRemoveModifiers, AutoExtractModifiers, CloseUI, GetModifierID, 
        // GetItemTypeFromModifier, ToggleUI, OpenUI, CloseUI, OpenUIAtStation]

        // ... (rest of existing methods remain unchanged)

        private void CheckAndInstallNewModifiers()
        {
            BaseModularGun modularGun = weaponItems[0].ModItem as BaseModularGun;
            if (modularGun == null) return;

            bool installed = false;

            if (!modifier1Items[0].IsAir && (previousModifier1[0].IsAir || previousModifier1[0].type != modifier1Items[0].type))
            {
                int newModifierID = GetModifierID(modifier1Items[0].type, "ammo");
                if (newModifierID != -1)
                {
                    modularGun.ammoTypeModifier = newModifierID;
                    installed = true;
                    Main.NewText($"Ammo type modifier {(previousModifier1[0].IsAir ? "installed" : "swapped")}", Color.Green);
                }
            }

            if (!modifier2Items[0].IsAir && (previousModifier2[0].IsAir || previousModifier2[0].type != modifier2Items[0].type))
            {
                int newModifierID = GetModifierID(modifier2Items[0].type, "damage");
                if (newModifierID != -1)
                {
                    modularGun.damageTypeModifier = newModifierID;
                    installed = true;
                    Main.NewText($"Damage type modifier {(previousModifier2[0].IsAir ? "installed" : "swapped")}", Color.Green);
                }
            }

            if (!modifier3Items[0].IsAir && (previousModifier3[0].IsAir || previousModifier3[0].type != modifier3Items[0].type))
            {
                modularGun.shotTypeModifier = GetModifierID(modifier3Items[0].type, "shot");
                if (modularGun.shotTypeModifier != -1)
                {
                    installed = true;
                    Main.NewText($"Shot type modifier installed", Color.Green);
                }
            }

            if (!specialItems[0].IsAir &&(previousSpecial[0].IsAir || previousSpecial[0].type != specialItems[0].type))
            {
                modularGun.specialEffectModifier = GetModifierID(specialItems[0].type, "special");
                if (modularGun.specialEffectModifier != -1)
                {
                    installed = true;
                    Main.NewText($"Special effect modifier installed", Color.Green);
                }
            }

            if (installed)
            {
                Main.NewText("Modifier installation complete!", Color.Cyan);
            }
        }

        private void ClearAllModifierSlots()
        {
            modifier1Items[0].TurnToAir();
            modifier2Items[0].TurnToAir();
            modifier3Items[0].TurnToAir();
            specialItems[0].TurnToAir();
        }

        private void CheckAndRemoveModifiers()
        {
            BaseModularGun modularGun = weaponItems[0].ModItem as BaseModularGun;
            if (modularGun == null) return;

            bool removed = false;

            if (!previousModifier1[0].IsAir && modifier1Items[0].IsAir && modularGun.ammoTypeModifier != -1)
            {
                modularGun.ammoTypeModifier = -1;
                removed = true;
                Main.NewText("Ammo type modifier removed!", Color.Orange);
            }

            if (!previousModifier2[0].IsAir && modifier2Items[0].IsAir && modularGun.damageTypeModifier != -1)
            {
                modularGun.damageTypeModifier = -1;
                removed = true;
                Main.NewText("Damage type modifier removed!", Color.Orange);
            }

            if (!previousModifier3[0].IsAir && modifier3Items[0].IsAir && modularGun.shotTypeModifier != -1)
            {
                modularGun.shotTypeModifier = -1;
                removed = true;
                Main.NewText("Shot type modifier removed!", Color.Orange);
            }

            if (!previousSpecial[0].IsAir && specialItems[0].IsAir && modularGun.specialEffectModifier != -1)
            {
                modularGun.specialEffectModifier = -1;
                removed = true;
                Main.NewText("Special effect modifier removed!", Color.Orange);
            }

            if (removed)
            {
                Main.NewText("Modifier removal complete!", Color.Yellow);
            }
        }

        private void AutoExtractModifiers()
        {
            BaseModularGun modularGun = weaponItems[0].ModItem as BaseModularGun;
            if (modularGun == null) return;

            bool extracted = false;

            if (modularGun.ammoTypeModifier != -1 && modifier1Items[0].IsAir)
            {
                modifier1Items[0].SetDefaults(GetItemTypeFromModifier(modularGun.ammoTypeModifier, "ammo"));
                modularGun.ammoTypeModifier = -1;
                extracted = true;
            }

            if (modularGun.damageTypeModifier != -1 && modifier2Items[0].IsAir)
            {
                modifier2Items[0].SetDefaults(GetItemTypeFromModifier(modularGun.damageTypeModifier, "damage"));
                modularGun.damageTypeModifier = -1;
                extracted = true;
            }

            if (modularGun.shotTypeModifier != -1 && modifier3Items[0].IsAir)
            {
                modifier3Items[0].SetDefaults(GetItemTypeFromModifier(modularGun.shotTypeModifier, "shot"));
                modularGun.shotTypeModifier = -1;
                extracted = true;
            }

            if (modularGun.specialEffectModifier != -1 && specialItems[0].IsAir)
            {
                specialItems[0].SetDefaults(GetItemTypeFromModifier(modularGun.specialEffectModifier, "special"));
                modularGun.specialEffectModifier = -1;
                extracted = true;
            }

            if (extracted)
            {
                Main.NewText("Modifiers extracted!", Color.Orange);
            }
        }

        private void CloseUI(UIMouseEvent evt, UIElement listeningElement)
        {
            Player player = Main.LocalPlayer;

            BaseModularGun modularGun = weaponItems[0].ModItem as BaseModularGun;
            bool weaponHasMods = modularGun != null && modularGun.IsComplete();

            if (!weaponItems[0].IsAir)
            {
                player.QuickSpawnItem(player.GetSource_FromThis(), weaponItems[0]);
                weaponItems[0].TurnToAir();
            }

            if (!weaponHasMods)
            {
                if (!modifier1Items[0].IsAir)
                {
                    player.QuickSpawnItem(player.GetSource_FromThis(), modifier1Items[0]);
                    modifier1Items[0].TurnToAir();
                }
                if (!modifier2Items[0].IsAir)
                {
                    player.QuickSpawnItem(player.GetSource_FromThis(), modifier2Items[0]);
                    modifier2Items[0].TurnToAir();
                }
                if (!modifier3Items[0].IsAir)
                {
                    player.QuickSpawnItem(player.GetSource_FromThis(), modifier3Items[0]);
                    modifier3Items[0].TurnToAir();
                }
                if (!specialItems[0].IsAir)
                {
                    player.QuickSpawnItem(player.GetSource_FromThis(), specialItems[0]);
                    specialItems[0].TurnToAir();
                }
            }
            else
            {
                modifier1Items[0].TurnToAir();
                modifier2Items[0].TurnToAir();
                modifier3Items[0].TurnToAir();
                specialItems[0].TurnToAir();
            }

            Visible = false;
        }

        private int GetModifierID(int itemType, string modifierType)
        {
            int result = -1;

            switch (modifierType)
            {
                case "ammo":
                    if (itemType == ModContent.ItemType<MagicAmmoModifier>()) return 0;
                    if (itemType == ModContent.ItemType<ArrowAmmoModifier>()) return 1;
                    if (itemType == ModContent.ItemType<BulletAmmoModifier>()) return 2;
                    if (itemType == ModContent.ItemType<RocketAmmoModifier>()) return 3;

                    // Elite tier (same IDs, different items)
                    if (itemType == ModContent.ItemType<EliteMagicAmmoModifier>()) return 4;
                    if (itemType == ModContent.ItemType<EliteArrowAmmoModifier>()) return 5;
                    if (itemType == ModContent.ItemType<EliteBulletAmmoModifier>()) return 6;
                    if (itemType == ModContent.ItemType<EliteRocketAmmoModifier>()) return 7;

                    // Perfect tier
                    if (itemType == ModContent.ItemType<PerfectMagicAmmoModifier>()) return 8;
                    if (itemType == ModContent.ItemType<PerfectArrowAmmoModifier>()) return 9;
                    if (itemType == ModContent.ItemType<PerfectBulletAmmoModifier>()) return 10;
                    if (itemType == ModContent.ItemType<PerfectRocketAmmoModifier>()) return 11;
                    break;

                case "damage":
                    if (itemType == ModContent.ItemType<FireDamageModifier>()) return 0;
                    else if (itemType == ModContent.ItemType<WaterDamageModifier>()) return 1;
                    else if (itemType == ModContent.ItemType<LightningDamageModifier>()) return 2;
                    else if (itemType == ModContent.ItemType<EarthDamageModifier>()) return 3;
                    else if (itemType == ModContent.ItemType<WindDamageModifier>()) return 4;
                    else if (itemType == ModContent.ItemType<SlimeDamageModifier>()) return 5;

                    else if (itemType == ModContent.ItemType<EliteFireDamageModifier>()) return 6;
                    else if (itemType == ModContent.ItemType<EliteWaterDamageModifier>()) return 7;
                    else if (itemType == ModContent.ItemType<EliteLightningDamageModifier>()) return 8;
                    else if (itemType == ModContent.ItemType<EliteEarthDamageModifier>()) return 9;
                    else if (itemType == ModContent.ItemType<EliteWindDamageModifier>()) return 10;
                    else if (itemType == ModContent.ItemType<EliteSlimeDamageModifier>()) return 11;

                    else if (itemType == ModContent.ItemType<PerfectFireDamageModifier>()) return 12;
                    else if (itemType == ModContent.ItemType<PerfectWaterDamageModifier>()) return 13;
                    else if (itemType == ModContent.ItemType<PerfectLightningDamageModifier>()) return 14;
                    else if (itemType == ModContent.ItemType<PerfectEarthDamageModifier>()) return 15;
                    else if (itemType == ModContent.ItemType<PerfectWindDamageModifier>()) return 16;
                    else if (itemType == ModContent.ItemType<PerfectSlimeDamageModifier>()) return 17;
                    break;

                case "shot":
                    if (itemType == ModContent.ItemType<AutoFireModifier>()) return 0;
                    else if (itemType == ModContent.ItemType<BurstFireModifier>()) return 1;
                    else if (itemType == ModContent.ItemType<ChargeFireModifier>()) return 2;

                    else if (itemType == ModContent.ItemType<EliteAutoFireModifier>()) return 3;
                    else if (itemType == ModContent.ItemType<EliteBurstFireModifier>()) return 4;
                    else if (itemType == ModContent.ItemType<EliteChargeFireModifier>()) return 5;

                    else if (itemType == ModContent.ItemType<PerfectAutoFireModifier>()) return 6;
                    else if (itemType == ModContent.ItemType<PerfectBurstFireModifier>()) return 7;
                    else if (itemType == ModContent.ItemType<PerfectChargeFireModifier>()) return 8;
                    break;

                case "special":
                    if (itemType == ModContent.ItemType<PiercingModifier>()) return 0;
                    else if (itemType == ModContent.ItemType<BouncingModifier>()) return 1;
                    else if (itemType == ModContent.ItemType<HomingModifier>()) return 2;
                    else if (itemType == ModContent.ItemType<LifeStealModifier>()) return 3;
                    else if (itemType == ModContent.ItemType<CritBoostModifier>()) return 4;

                    else if (itemType == ModContent.ItemType<ElitePiercingModifier>()) return 5;
                    else if (itemType == ModContent.ItemType<EliteBouncingModifier>()) return 6;
                    else if (itemType == ModContent.ItemType<EliteHomingModifier>()) return 7;
                    else if (itemType == ModContent.ItemType<EliteLifeStealModifier>()) return 8;
                    else if (itemType == ModContent.ItemType<EliteCritBoostModifier>()) return 9;

                    else if (itemType == ModContent.ItemType<PerfectPiercingModifier>()) return 10;
                    else if (itemType == ModContent.ItemType<PerfectBouncingModifier>()) return 11;
                    else if (itemType == ModContent.ItemType<PerfectHomingModifier>()) return 12;
                    else if (itemType == ModContent.ItemType<PerfectLifeStealModifier>()) return 13;
                    else if (itemType == ModContent.ItemType<PerfectCritBoostModifier>()) return 14;
                    break;
            }

            return result;
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
                case "rate":
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

        public static void ToggleUI()
        {
            Visible = !Visible;
        }
        
        public static void OpenUI()
        {
            Visible = true;
        }
        
        public static void CloseUI()
        {
            Visible = false;
        }
        
        public static void OpenUIAtStation(Vector2 stationWorldPosition)
        {
            StationPosition = stationWorldPosition;
            Visible = true;
        }
    }
    
    public class ModifierUISystem : ModSystem
    {
        private UserInterface modifierStationInterface;
        internal ModifierStationUI modifierStationUI;
        
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "TestMod: Modifier Station UI",
                    delegate
                    {
                        if (ModifierStationUI.Visible)
                        {
                            modifierStationInterface.Draw(Main.spriteBatch, new GameTime());
                        }
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
        
        public override void PostSetupContent()
        {
            modifierStationUI = new ModifierStationUI();
            modifierStationUI.Activate();
            modifierStationInterface = new UserInterface();
            modifierStationInterface.SetState(modifierStationUI);
        }
        
        public override void UpdateUI(GameTime gameTime)
        {
            if (ModifierStationUI.Visible)
            {
                modifierStationInterface?.Update(gameTime);
            }
        }
    }
}