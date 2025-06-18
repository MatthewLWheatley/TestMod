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
    // The main UI state class
    public class ModifierStationUI : UIState
    {
        public static bool Visible { get; set; }
        public static Vector2 StationPosition { get; set; } // Store station world position
        
        private UIPanel mainPanel;
        private UIText titleText;
        
        // Drag functionality
        private bool isDragging;
        private Vector2 dragOffset;
        
        // Item arrays for slots
        private Item[] weaponItems = new Item[1];
        private Item[] modifier1Items = new Item[1];  // Dynamic slot 1
        private Item[] modifier2Items = new Item[1];  // Dynamic slot 2  
        private Item[] modifier3Items = new Item[1];  // Dynamic slot 3
        private Item[] specialItems = new Item[1];    // Special effects slot
        
        // Weapon slot
        private UIItemSlot weaponSlot;
        
        // Modifier slots
        private UIItemSlot modifier1Slot;
        private UIItemSlot modifier2Slot;
        private UIItemSlot modifier3Slot;
        private UIItemSlot specialSlot;
        
        // Dynamic labels for modifier slots
        private UIText modifier1Label;
        private UIText modifier2Label;
        private UIText modifier3Label;
        private UIText specialLabel;
        
        // Buttons
        private UITextPanel<string> xCloseButton;
        
        // Track previous weapon state and slot states for auto-install/remove
        private Item previousWeapon = new Item();
        private Item[] previousModifier1 = new Item[1] { new Item() };
        private Item[] previousModifier2 = new Item[1] { new Item() };
        private Item[] previousModifier3 = new Item[1] { new Item() };
        private Item[] previousSpecial = new Item[1] { new Item() };
        
        private UIText pointBudgetText;
        private UIText pointWarningText;

        public override void OnInitialize()
        {
            // Initialize item arrays
            weaponItems[0] = new Item();
            modifier1Items[0] = new Item();
            modifier2Items[0] = new Item();
            modifier3Items[0] = new Item();
            specialItems[0] = new Item();

            // Main panel - lighter and more transparent
            mainPanel = new UIPanel();
            mainPanel.SetPadding(0);
            mainPanel.Left.Set(400, 0f);
            mainPanel.Top.Set(150, 0f);
            mainPanel.Width.Set(320, 0f);
            mainPanel.Height.Set(450, 0f);
            // Much lighter blue with transparency
            mainPanel.BackgroundColor = new Color(130, 150, 200, 180);

            // Add drag functionality to main panel
            mainPanel.OnLeftMouseDown += StartDragging;
            mainPanel.OnLeftMouseUp += StopDragging;

            Append(mainPanel);

            // Title
            titleText = new UIText("Modifier Station");
            titleText.Left.Set(10, 0f);
            titleText.Top.Set(10, 0f);
            mainPanel.Append(titleText);

            // X Close button (top right)
            xCloseButton = new UITextPanel<string>("X");
            xCloseButton.Left.Set(280, 0f);
            xCloseButton.Top.Set(5, 0f);
            xCloseButton.Width.Set(30, 0f);
            xCloseButton.Height.Set(25, 0f);
            xCloseButton.BackgroundColor = new Color(180, 40, 40, 200);
            xCloseButton.OnLeftClick += CloseUI;
            mainPanel.Append(xCloseButton);

            // Point budget display (moved to top)
            pointBudgetText = new UIText("Points: 0/0");
            pointBudgetText.Left.Set(10, 0f);
            pointBudgetText.Top.Set(35, 0f);
            mainPanel.Append(pointBudgetText);

            // Weapon slot (top of stack)
            weaponSlot = new UIItemSlot(weaponItems, 0, ItemSlot.Context.BankItem);
            weaponSlot.Left.Set(60, 0f);
            weaponSlot.Top.Set(65, 0f);
            weaponSlot.Width.Set(52, 0f);
            weaponSlot.Height.Set(52, 0f);
            mainPanel.Append(weaponSlot);

            // Weapon label
            UIText weaponLabel = new UIText("Modular Weapon");
            weaponLabel.Left.Set(130, 0f);
            weaponLabel.Top.Set(80, 0f);
            mainPanel.Append(weaponLabel);

            // Modifier Slot 1
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

            // Modifier Slot 2
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

            // Modifier Slot 3
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

            // Special Effects Slot
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

            // Point warning text
            pointWarningText = new UIText("");
            pointWarningText.Left.Set(10, 0f);
            pointWarningText.Top.Set(400, 0f);
            pointWarningText.Width.Set(300, 0f);
            pointWarningText.Height.Set(20, 0f);
            mainPanel.Append(pointWarningText);

            // Status text
            UIText statusText = new UIText("Place weapon to see modifier categories");
            statusText.Left.Set(10, 0f);
            statusText.Top.Set(420, 0f);
            statusText.Width.Set(300, 0f);
            statusText.Height.Set(25, 0f);
            statusText.TextColor = Color.LightGray;
            mainPanel.Append(statusText);
        }
        
        private void StartDragging(UIMouseEvent evt, UIElement listeningElement)
        {
            // Only start dragging if not clicking on item slots or buttons
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

            // Handle dragging
            if (isDragging && Main.mouseLeft)
            {
                Vector2 newPosition = Main.MouseScreen - dragOffset;

                // Keep UI within screen bounds
                newPosition.X = MathHelper.Clamp(newPosition.X, 0, Main.screenWidth - mainPanel.Width.Pixels);
                newPosition.Y = MathHelper.Clamp(newPosition.Y, 0, Main.screenHeight - mainPanel.Height.Pixels);

                mainPanel.Left.Set(newPosition.X, 0f);
                mainPanel.Top.Set(newPosition.Y, 0f);
                Recalculate();
            }

            // Auto-close if player moves too far from station (10 blocks = 160 pixels)
            if (Visible && Vector2.Distance(Main.LocalPlayer.Center, StationPosition) > 160f)
            {
                CloseUI(null, null);
            }

            // Handle auto-install/remove based on weapon slot changes
            HandleAutoModifierManagement();
            
            UpdatePointDisplay();
            UpdateModifierLabels();
        }

        private void UpdateModifierLabels()
        {
            Item currentWeapon = weaponItems[0];
            
            if (currentWeapon.type == ModContent.ItemType<ModularGun>())
            {
                // Gun-specific labels
                modifier1Label.SetText("Ammo Type");
                modifier1Label.TextColor = Color.White;
                
                modifier2Label.SetText("Damage Type");
                modifier2Label.TextColor = Color.White;
                
                modifier3Label.SetText("Shot Type");
                modifier3Label.TextColor = Color.White;
            }
            // else if (currentWeapon.type == ModContent.ItemType<ModularSword>()) // When you add swords
            // {
            //     // Sword-specific labels
            //     modifier1Label.SetText("Blade Type");
            //     modifier1Label.TextColor = Color.White;
                
            //     modifier2Label.SetText("Damage Type");
            //     modifier2Label.TextColor = Color.White;
                
            //     modifier3Label.SetText("Swing Type");
            //     modifier3Label.TextColor = Color.White;
            // }
            else
            {
                // No weapon or unknown weapon type
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
            
            // Check if a non-modular item was placed in weapon slot
            if (!currentWeapon.IsAir && currentWeapon.type != ModContent.ItemType<ModularGun>())
            {
                // Return item to player and clear slot
                Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_FromThis(), currentWeapon);
                weaponItems[0].TurnToAir();
                Main.NewText("Only modular weapons can be placed here!", Color.Red);
                return;
            }
            
            // Check if weapon was just placed (from air to modular gun)
            if (previousWeapon.IsAir && currentWeapon.type == ModContent.ItemType<ModularGun>())
            {
                // Auto-extract modifiers from newly placed weapon
                AutoExtractModifiers();
            }
            // Check if weapon was just removed (from modular gun to air)
            else if (previousWeapon.type == ModContent.ItemType<ModularGun>() && currentWeapon.IsAir)
            {
                // Clear all modifier slots to prevent duplication
                ClearAllModifierSlots();
            }
            
            // Check if modifiers were added while weapon is present
            if (currentWeapon.type == ModContent.ItemType<ModularGun>())
            {
                CheckAndInstallNewModifiers();
                CheckAndRemoveModifiers();
            }
            
            // Update previous states
            previousWeapon = currentWeapon.Clone();
            previousModifier1[0] = modifier1Items[0].Clone();
            previousModifier2[0] = modifier2Items[0].Clone();
            previousModifier3[0] = modifier3Items[0].Clone();
            previousSpecial[0] = specialItems[0].Clone();
        }

        private void UpdatePointDisplay()
        {
            ModularGun modularGun = weaponItems[0].ModItem as ModularGun;

            if (modularGun != null)
            {
                int currentPoints = modularGun.GetCurrentPointUsage();
                int maxPoints = modularGun.maxPointBudget;
                string tier = modularGun.weaponTier;

                pointBudgetText.SetText($"Points: {currentPoints}/{maxPoints} ({tier})");

                // Color coding for point usage
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

        private void CheckAndInstallNewModifiers()
        {
            ModularGun modularGun = weaponItems[0].ModItem as ModularGun;
            if (modularGun == null) return;

            bool installed = false;

            // Check modifier slot 1 (maps to AMMO TYPE for guns)
            if (previousModifier1[0].IsAir && !modifier1Items[0].IsAir && modularGun.ammoTypeModifier == -1)
            {
                modularGun.ammoTypeModifier = GetModifierID(modifier1Items[0].type, "ammo");
                if (modularGun.ammoTypeModifier != -1)
                {
                    installed = true;
                    Main.NewText($"Ammo type modifier installed", Color.Green);
                }
            }

            // Check modifier slot 2 (maps to DAMAGE TYPE for guns)
            if (previousModifier2[0].IsAir && !modifier2Items[0].IsAir && modularGun.damageTypeModifier == -1)
            {
                modularGun.damageTypeModifier = GetModifierID(modifier2Items[0].type, "damage");
                if (modularGun.damageTypeModifier != -1)
                {
                    installed = true;
                    Main.NewText($"Damage type modifier installed", Color.Green);
                }
            }

            // Check modifier slot 3 (maps to SHOT TYPE for guns)
            if (previousModifier3[0].IsAir && !modifier3Items[0].IsAir && modularGun.shotTypeModifier == -1)
            {
                modularGun.shotTypeModifier = GetModifierID(modifier3Items[0].type, "shot");
                if (modularGun.shotTypeModifier != -1)
                {
                    installed = true;
                    Main.NewText($"Shot type modifier installed", Color.Green);
                }
            }

            // Check special slot
            if (previousSpecial[0].IsAir && !specialItems[0].IsAir && modularGun.specialEffectModifier == -1)
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
            Main.NewText("Modifier slots cleared!", Color.Yellow);
        }

        private void CheckAndRemoveModifiers()
        {
            ModularGun modularGun = weaponItems[0].ModItem as ModularGun;
            if (modularGun == null) return;

            bool removed = false;

            // Check if modifier 1 was removed (AMMO TYPE)
            if (!previousModifier1[0].IsAir && modifier1Items[0].IsAir && modularGun.ammoTypeModifier != -1)
            {
                modularGun.ammoTypeModifier = -1;
                removed = true;
                Main.NewText("Ammo type modifier removed!", Color.Orange);
            }

            // Check if modifier 2 was removed (DAMAGE TYPE)
            if (!previousModifier2[0].IsAir && modifier2Items[0].IsAir && modularGun.damageTypeModifier != -1)
            {
                modularGun.damageTypeModifier = -1;
                removed = true;
                Main.NewText("Damage type modifier removed!", Color.Orange);
            }

            // Check if modifier 3 was removed (SHOT TYPE)
            if (!previousModifier3[0].IsAir && modifier3Items[0].IsAir && modularGun.shotTypeModifier != -1)
            {
                modularGun.shotTypeModifier = -1;
                removed = true;
                Main.NewText("Shot type modifier removed!", Color.Orange);
            }

            // Check if special modifier was removed
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
            ModularGun modularGun = weaponItems[0].ModItem as ModularGun;
            if (modularGun == null) return;

            bool extracted = false;

            // Extract to slot 1 (AMMO TYPE)
            if (modularGun.ammoTypeModifier != -1 && modifier1Items[0].IsAir)
            {
                modifier1Items[0].SetDefaults(GetItemTypeFromModifier(modularGun.ammoTypeModifier, "ammo"));
                modularGun.ammoTypeModifier = -1;
                extracted = true;
            }

            // Extract to slot 2 (DAMAGE TYPE)
            if (modularGun.damageTypeModifier != -1 && modifier2Items[0].IsAir)
            {
                modifier2Items[0].SetDefaults(GetItemTypeFromModifier(modularGun.damageTypeModifier, "damage"));
                modularGun.damageTypeModifier = -1;
                extracted = true;
            }

            // Extract to slot 3 (SHOT TYPE)
            if (modularGun.shotTypeModifier != -1 && modifier3Items[0].IsAir)
            {
                modifier3Items[0].SetDefaults(GetItemTypeFromModifier(modularGun.shotTypeModifier, "shot"));
                modularGun.shotTypeModifier = -1;
                extracted = true;
            }

            // Extract to special slot
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

            // Check weapon mod status BEFORE clearing slot
            ModularGun modularGun = weaponItems[0].ModItem as ModularGun;
            bool weaponHasMods = modularGun != null && modularGun.IsComplete();

            // Return weapon
            if (!weaponItems[0].IsAir)
            {
                player.QuickSpawnItem(player.GetSource_FromThis(), weaponItems[0]);
                weaponItems[0].TurnToAir();
            }

            // Only return modifier items if they're NOT installed in the weapon
            if (!weaponHasMods)
            {
                // Return modifier items that aren't installed
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
                // Just clear slots without returning items
                modifier1Items[0].TurnToAir();
                modifier2Items[0].TurnToAir();
                modifier3Items[0].TurnToAir();
                specialItems[0].TurnToAir();
            }

            Visible = false;
        }

        // Helper methods for modifier conversion
        private int GetModifierID(int itemType, string modifierType)
        {
            int result = -1;

            switch (modifierType)
            {
                case "ammo":
                    if (itemType == ModContent.ItemType<MagicAmmoModifier>()) result = 0;
                    else if (itemType == ModContent.ItemType<ArrowAmmoModifier>()) result = 1;
                    else if (itemType == ModContent.ItemType<BulletAmmoModifier>()) result = 2;
                    else if (itemType == ModContent.ItemType<RocketAmmoModifier>()) result = 3;
                    break;
                case "damage":
                    if (itemType == ModContent.ItemType<FireDamageModifier>()) result = 0;
                    else if (itemType == ModContent.ItemType<WaterDamageModifier>()) result = 1;
                    else if (itemType == ModContent.ItemType<LightningDamageModifier>()) result = 2;
                    else if (itemType == ModContent.ItemType<EarthDamageModifier>()) result = 3;
                    else if (itemType == ModContent.ItemType<WindDamageModifier>()) result = 4;
                    else if (itemType == ModContent.ItemType<SlimeDamageModifier>()) result = 5;
                    break;
                case "shot":
                    if (itemType == ModContent.ItemType<AutoFireModifier>()) result = 0;
                    else if (itemType == ModContent.ItemType<BurstFireModifier>()) result = 1;
                    else if (itemType == ModContent.ItemType<ChargeFireModifier>()) result = 2;
                    break;
                case "special":
                    if (itemType == ModContent.ItemType<PiercingModifier>()) result = 0;
                    else if (itemType == ModContent.ItemType<BouncingModifier>()) result = 1;
                    else if (itemType == ModContent.ItemType<HomingModifier>()) result = 2;
                    else if (itemType == ModContent.ItemType<LifeStealModifier>()) result = 3;
                    else if (itemType == ModContent.ItemType<CritBoostModifier>()) result = 4;
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
                case "rate":
                    return modifierID switch
                    {
                        0 => ModContent.ItemType<AutoFireModifier>(),
                        1 => ModContent.ItemType<BurstFireModifier>(),
                        2 => ModContent.ItemType<ChargeFireModifier>(),
                        _ => 0
                    };
                case "special":  // <-- ADD THIS MISSING CASE!
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
    
    // UI System class to manage the interface
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