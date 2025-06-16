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
        private Item[] shotTypeItems = new Item[1];
        private Item[] damageTypeItems = new Item[1];
        private Item[] rateOfFireItems = new Item[1];
        
        // Weapon slot
        private UIItemSlot weaponSlot;
        
        // Modifier slots
        private UIItemSlot shotTypeSlot;
        private UIItemSlot damageTypeSlot;
        private UIItemSlot rateOfFireSlot;
        
        // Buttons
        private UITextPanel<string> xCloseButton; // New X button
        
        // Track previous weapon state and slot states for auto-install/remove
        private Item previousWeapon = new Item();
        private Item[] previousShotType = new Item[1] { new Item() };
        private Item[] previousDamageType = new Item[1] { new Item() };
        private Item[] previousRateOfFire = new Item[1] { new Item() };
        
        private UIText pointBudgetText;
        private UIText pointWarningText;

        public override void OnInitialize()
        {
            // Initialize item arrays
            for (int i = 0; i < weaponItems.Length; i++)
                weaponItems[i] = new Item();
            for (int i = 0; i < shotTypeItems.Length; i++)
                shotTypeItems[i] = new Item();
            for (int i = 0; i < damageTypeItems.Length; i++)
                damageTypeItems[i] = new Item();
            for (int i = 0; i < rateOfFireItems.Length; i++)
                rateOfFireItems[i] = new Item();

            // Main panel
            mainPanel = new UIPanel();
            mainPanel.SetPadding(0);
            mainPanel.Left.Set(400, 0f);
            mainPanel.Top.Set(200, 0f);
            mainPanel.Width.Set(400, 0f);
            mainPanel.Height.Set(300, 0f);
            mainPanel.BackgroundColor = new Color(73, 94, 171);

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
            xCloseButton.Left.Set(365, 0f);
            xCloseButton.Top.Set(5, 0f);
            xCloseButton.Width.Set(30, 0f);
            xCloseButton.Height.Set(25, 0f);
            xCloseButton.BackgroundColor = new Color(180, 40, 40);
            xCloseButton.OnLeftClick += CloseUI;
            mainPanel.Append(xCloseButton);

            // Weapon slot
            weaponSlot = new UIItemSlot(weaponItems, 0, ItemSlot.Context.BankItem);
            weaponSlot.Left.Set(20, 0f);
            weaponSlot.Top.Set(50, 0f);
            weaponSlot.Width.Set(52, 0f);
            weaponSlot.Height.Set(52, 0f);
            mainPanel.Append(weaponSlot);

            // Weapon label
            UIText weaponLabel = new UIText("Weapon:");
            weaponLabel.Left.Set(80, 0f);
            weaponLabel.Top.Set(65, 0f);
            mainPanel.Append(weaponLabel);

            // Modifier slots with labels
            // Shot Type
            shotTypeSlot = new UIItemSlot(shotTypeItems, 0, ItemSlot.Context.BankItem);
            shotTypeSlot.Left.Set(20, 0f);
            shotTypeSlot.Top.Set(120, 0f);
            shotTypeSlot.Width.Set(52, 0f);
            shotTypeSlot.Height.Set(52, 0f);
            mainPanel.Append(shotTypeSlot);

            UIText shotLabel = new UIText("Shot Type:");
            shotLabel.Left.Set(80, 0f);
            shotLabel.Top.Set(135, 0f);
            mainPanel.Append(shotLabel);

            // Damage Type
            damageTypeSlot = new UIItemSlot(damageTypeItems, 0, ItemSlot.Context.BankItem);
            damageTypeSlot.Left.Set(200, 0f);
            damageTypeSlot.Top.Set(120, 0f);
            damageTypeSlot.Width.Set(52, 0f);
            damageTypeSlot.Height.Set(52, 0f);
            mainPanel.Append(damageTypeSlot);

            UIText damageLabel = new UIText("Damage Type:");
            damageLabel.Left.Set(260, 0f);
            damageLabel.Top.Set(135, 0f);
            mainPanel.Append(damageLabel);

            // Rate of Fire
            rateOfFireSlot = new UIItemSlot(rateOfFireItems, 0, ItemSlot.Context.BankItem);
            rateOfFireSlot.Left.Set(110, 0f);
            rateOfFireSlot.Top.Set(190, 0f);
            rateOfFireSlot.Width.Set(52, 0f);
            rateOfFireSlot.Height.Set(52, 0f);
            mainPanel.Append(rateOfFireSlot);

            UIText rateLabel = new UIText("Rate of Fire:");
            rateLabel.Left.Set(170, 0f);
            rateLabel.Top.Set(205, 0f);
            mainPanel.Append(rateLabel);

            // ADD: Point budget display
            pointBudgetText = new UIText("Points: 0/6 (Copper)");
            pointBudgetText.Left.Set(200, 0f);
            pointBudgetText.Top.Set(10, 0f);
            mainPanel.Append(pointBudgetText);

            // ADD: Point warning text
            pointWarningText = new UIText("");
            pointWarningText.Left.Set(20, 0f);
            pointWarningText.Top.Set(240, 0f);
            pointWarningText.Width.Set(360, 0f);
            pointWarningText.Height.Set(20, 0f);
            mainPanel.Append(pointWarningText);

            // Update the status text position
            UIText statusText = new UIText("Auto-install: Place weapon to extract mods, add mods to auto-install");
            statusText.Left.Set(20, 0f);
            statusText.Top.Set(270, 0f); // Moved down to make room for warning
            statusText.Width.Set(360, 0f);
            statusText.Height.Set(30, 0f);
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
            previousShotType[0] = shotTypeItems[0].Clone();
            previousDamageType[0] = damageTypeItems[0].Clone();
            previousRateOfFire[0] = rateOfFireItems[0].Clone();
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

            // Check if shot type modifier was just added
            if (previousShotType[0].IsAir && !shotTypeItems[0].IsAir && modularGun.shotTypeModifier == -1)
            {
                if (modularGun.CanInstallModifier(shotTypeItems[0].type, "shot"))
                {
                    modularGun.shotTypeModifier = GetModifierID(shotTypeItems[0].type, "shot");
                    if (modularGun.shotTypeModifier != -1)
                    {
                        installed = true;
                    }
                }
                else
                {
                    // Return the modifier to player and clear slot
                    Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_FromThis(), shotTypeItems[0]);
                    shotTypeItems[0].TurnToAir();
                    Main.NewText($"Not enough points! This modifier costs {ModifierData.GetModifierPointCost(shotTypeItems[0].type)} points.", Color.Red);
                    return;
                }
            }

            // Check if damage type modifier was just added
            if (previousDamageType[0].IsAir && !damageTypeItems[0].IsAir && modularGun.damageTypeModifier == -1)
            {
                if (modularGun.CanInstallModifier(damageTypeItems[0].type, "damage"))
                {
                    modularGun.damageTypeModifier = GetModifierID(damageTypeItems[0].type, "damage");
                    if (modularGun.damageTypeModifier != -1)
                    {
                        installed = true;
                    }
                }
                else
                {
                    Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_FromThis(), damageTypeItems[0]);
                    damageTypeItems[0].TurnToAir();
                    Main.NewText($"Not enough points! This modifier costs {ModifierData.GetModifierPointCost(damageTypeItems[0].type)} points.", Color.Red);
                    return;
                }
            }

            // Check if rate of fire modifier was just added
            if (previousRateOfFire[0].IsAir && !rateOfFireItems[0].IsAir && modularGun.rateOfFireModifier == -1)
            {
                if (modularGun.CanInstallModifier(rateOfFireItems[0].type, "rate"))
                {
                    modularGun.rateOfFireModifier = GetModifierID(rateOfFireItems[0].type, "rate");
                    if (modularGun.rateOfFireModifier != -1)
                    {
                        installed = true;
                    }
                }
                else
                {
                    Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_FromThis(), rateOfFireItems[0]);
                    rateOfFireItems[0].TurnToAir();
                    Main.NewText($"Not enough points! This modifier costs {ModifierData.GetModifierPointCost(rateOfFireItems[0].type)} points.", Color.Red);
                    return;
                }
            }

            if (installed)
            {
                Main.NewText("Modifier installed!", Color.Green);
            }
        }

        private void ClearAllModifierSlots()
        {
            shotTypeItems[0].TurnToAir();
            damageTypeItems[0].TurnToAir();
            rateOfFireItems[0].TurnToAir();
            Main.NewText("Modifier slots cleared!", Color.Yellow);
        }
        
        private void CheckAndRemoveModifiers()
        {
            ModularGun modularGun = weaponItems[0].ModItem as ModularGun;
            if (modularGun == null) return;
            
            bool removed = false;
            
            // Check if shot type modifier was removed
            if (!previousShotType[0].IsAir && shotTypeItems[0].IsAir && modularGun.shotTypeModifier != -1)
            {
                modularGun.shotTypeModifier = -1;
                removed = true;
            }
            
            // Check if damage type modifier was removed
            if (!previousDamageType[0].IsAir && damageTypeItems[0].IsAir && modularGun.damageTypeModifier != -1)
            {
                modularGun.damageTypeModifier = -1;
                removed = true;
            }
            
            // Check if rate of fire modifier was removed
            if (!previousRateOfFire[0].IsAir && rateOfFireItems[0].IsAir && modularGun.rateOfFireModifier != -1)
            {
                modularGun.rateOfFireModifier = -1;
                removed = true;
            }
            
            if (removed)
            {
                Main.NewText("Modifier removed!", Color.Orange);
            }
        }
        
        private void AutoExtractModifiers()
        {
            ModularGun modularGun = weaponItems[0].ModItem as ModularGun;
            if (modularGun == null) return;
            
            bool extracted = false;
            
            // Extract shot type modifier
            if (modularGun.shotTypeModifier != -1 && shotTypeItems[0].IsAir)
            {
                shotTypeItems[0].SetDefaults(GetItemTypeFromModifier(modularGun.shotTypeModifier, "shot"));
                modularGun.shotTypeModifier = -1;
                extracted = true;
            }
            
            // Extract damage type modifier
            if (modularGun.damageTypeModifier != -1 && damageTypeItems[0].IsAir)
            {
                damageTypeItems[0].SetDefaults(GetItemTypeFromModifier(modularGun.damageTypeModifier, "damage"));
                modularGun.damageTypeModifier = -1;
                extracted = true;
            }
            
            // Extract rate of fire modifier
            if (modularGun.rateOfFireModifier != -1 && rateOfFireItems[0].IsAir)
            {
                rateOfFireItems[0].SetDefaults(GetItemTypeFromModifier(modularGun.rateOfFireModifier, "rate"));
                modularGun.rateOfFireModifier = -1;
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
                // Return modifier items...
            }
            else
            {
                // Just clear slots without returning items
                shotTypeItems[0].TurnToAir();
                damageTypeItems[0].TurnToAir();
                rateOfFireItems[0].TurnToAir();
            }

            Visible = false;
        }
        
        // Helper methods for modifier conversion
        private int GetModifierID(int itemType, string modifierType)
        {
            switch (modifierType)
            {
                case "shot":
                    if (itemType == ModContent.ItemType<Content.Items.StraightShotModifier>()) return 0;
                    if (itemType == ModContent.ItemType<Content.Items.BurstShotModifier>()) return 1;
                    if (itemType == ModContent.ItemType<Content.Items.BoltShotModifier>()) return 2;
                    if (itemType == ModContent.ItemType<Content.Items.ProjectileShotModifier>()) return 3;
                    break;
                case "damage":
                    if (itemType == ModContent.ItemType<Content.Items.FireDamageModifier>()) return 0;
                    if (itemType == ModContent.ItemType<Content.Items.WaterDamageModifier>()) return 1;
                    if (itemType == ModContent.ItemType<Content.Items.LightningDamageModifier>()) return 2;
                    if (itemType == ModContent.ItemType<Content.Items.EarthDamageModifier>()) return 3;
                    break;
                case "rate":
                    if (itemType == ModContent.ItemType<Content.Items.AutoFireModifier>()) return 0;
                    if (itemType == ModContent.ItemType<Content.Items.BurstFireModifier>()) return 1;
                    if (itemType == ModContent.ItemType<Content.Items.ChargeFireModifier>()) return 2;
                    break;
            }
            return -1;
        }
        
        private int GetItemTypeFromModifier(int modifierID, string modifierType)
        {
            switch (modifierType)
            {
                case "shot":
                    return modifierID switch
                    {
                        0 => ModContent.ItemType<Content.Items.StraightShotModifier>(),
                        1 => ModContent.ItemType<Content.Items.BurstShotModifier>(),
                        2 => ModContent.ItemType<Content.Items.BoltShotModifier>(),
                        3 => ModContent.ItemType<Content.Items.ProjectileShotModifier>(),
                        _ => ItemID.None
                    };
                case "damage":
                    return modifierID switch
                    {
                        0 => ModContent.ItemType<Content.Items.FireDamageModifier>(),
                        1 => ModContent.ItemType<Content.Items.WaterDamageModifier>(),
                        2 => ModContent.ItemType<Content.Items.LightningDamageModifier>(),
                        3 => ModContent.ItemType<Content.Items.EarthDamageModifier>(),
                        _ => ItemID.None
                    };
                case "rate":
                    return modifierID switch
                    {
                        0 => ModContent.ItemType<Content.Items.AutoFireModifier>(),
                        1 => ModContent.ItemType<Content.Items.BurstFireModifier>(),
                        2 => ModContent.ItemType<Content.Items.ChargeFireModifier>(),
                        _ => ItemID.None
                    };
            }
            return ItemID.None;
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