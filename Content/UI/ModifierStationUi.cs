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

        public override void OnInitialize()
        {
            weaponItems[0] = new Item();
            modifier1Items[0] = new Item();
            modifier2Items[0] = new Item();
            modifier3Items[0] = new Item();
            specialItems[0] = new Item();

            mainPanel = new UIPanel();
            mainPanel.SetPadding(0);
            mainPanel.Left.Set(400, 0f);
            mainPanel.Top.Set(150, 0f);
            mainPanel.Width.Set(320, 0f);
            mainPanel.Height.Set(450, 0f);
            mainPanel.BackgroundColor = new Color(130, 150, 200, 180);

            mainPanel.OnLeftMouseDown += StartDragging;
            mainPanel.OnLeftMouseUp += StopDragging;

            Append(mainPanel);

            titleText = new UIText("Modifier Station");
            titleText.Left.Set(10, 0f);
            titleText.Top.Set(10, 0f);
            mainPanel.Append(titleText);

            xCloseButton = new UITextPanel<string>("X");
            xCloseButton.Left.Set(280, 0f);
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

                newPosition.X = MathHelper.Clamp(newPosition.X, 0, Main.screenWidth - mainPanel.Width.Pixels);
                newPosition.Y = MathHelper.Clamp(newPosition.Y, 0, Main.screenHeight - mainPanel.Height.Pixels);

                mainPanel.Left.Set(newPosition.X, 0f);
                mainPanel.Top.Set(newPosition.Y, 0f);
                Recalculate();
            }

            if (Visible && Vector2.Distance(Main.LocalPlayer.Center, StationPosition) > 160f)
            {
                CloseUI(null, null);
            }

            HandleAutoModifierManagement();
            
            UpdatePointDisplay();
            UpdateModifierLabels();
        }

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

            if (currentWeapon.type == ModContent.ItemType<BaseModularGun>())
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

        private void CheckAndInstallNewModifiers()
        {
            BaseModularGun modularGun = weaponItems[0].ModItem as BaseModularGun;
            if (modularGun == null) return;

            bool installed = false;

            if (previousModifier1[0].IsAir && !modifier1Items[0].IsAir && modularGun.ammoTypeModifier == -1)
            {
                modularGun.ammoTypeModifier = GetModifierID(modifier1Items[0].type, "ammo");
                if (modularGun.ammoTypeModifier != -1)
                {
                    installed = true;
                    Main.NewText($"Ammo type modifier installed", Color.Green);
                }
            }

            if (previousModifier2[0].IsAir && !modifier2Items[0].IsAir && modularGun.damageTypeModifier == -1)
            {
                modularGun.damageTypeModifier = GetModifierID(modifier2Items[0].type, "damage");
                if (modularGun.damageTypeModifier != -1)
                {
                    installed = true;
                    Main.NewText($"Damage type modifier installed", Color.Green);
                }
            }

            if (previousModifier3[0].IsAir && !modifier3Items[0].IsAir && modularGun.shotTypeModifier == -1)
            {
                modularGun.shotTypeModifier = GetModifierID(modifier3Items[0].type, "shot");
                if (modularGun.shotTypeModifier != -1)
                {
                    installed = true;
                    Main.NewText($"Shot type modifier installed", Color.Green);
                }
            }

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