using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using TestMod.Content.Items;

namespace TestMod.Content.UI
{
    // The main UI state class
    public class ModifierStationUI : UIState
    {
        public static bool Visible { get; set; }
        private UIPanel mainPanel;
        private UIText titleText;
        
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
        private UITextPanel<string> installButton;
        private UITextPanel<string> removeButton;
        private UITextPanel<string> closeButton;
        
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
            Append(mainPanel);
            
            // Title
            titleText = new UIText("Modifier Station");
            titleText.Left.Set(10, 0f);
            titleText.Top.Set(10, 0f);
            mainPanel.Append(titleText);
            
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
            
            // Install button
            installButton = new UITextPanel<string>("Install");
            installButton.Left.Set(20, 0f);
            installButton.Top.Set(260, 0f);
            installButton.Width.Set(80, 0f);
            installButton.Height.Set(30, 0f);
            installButton.OnLeftClick += InstallModifiers;
            mainPanel.Append(installButton);
            
            // Remove button
            removeButton = new UITextPanel<string>("Remove");
            removeButton.Left.Set(110, 0f);
            removeButton.Top.Set(260, 0f);
            removeButton.Width.Set(80, 0f);
            removeButton.Height.Set(30, 0f);
            removeButton.OnLeftClick += RemoveModifiers;
            mainPanel.Append(removeButton);
            
            // Close button
            closeButton = new UITextPanel<string>("Close");
            closeButton.Left.Set(300, 0f);
            closeButton.Top.Set(260, 0f);
            closeButton.Width.Set(80, 0f);
            closeButton.Height.Set(30, 0f);
            closeButton.OnLeftClick += CloseUI;
            mainPanel.Append(closeButton);
        }
        
        private void InstallModifiers(UIMouseEvent evt, UIElement listeningElement)
        {
            if (weaponItems[0].type != ModContent.ItemType<ModularGun>()) 
            {
                Main.NewText("Place a modular weapon in the weapon slot!", Color.Red);
                return;
            }
            
            ModularGun modularGun = weaponItems[0].ModItem as ModularGun;
            if (modularGun == null) return;
            
            Player player = Main.LocalPlayer;
            bool installed = false;
            
            // Install shot type modifier
            if (!shotTypeItems[0].IsAir && modularGun.shotTypeModifier == -1)
            {
                // For testing, just use item type as modifier ID
                modularGun.shotTypeModifier = GetModifierID(shotTypeItems[0].type, "shot");
                if (modularGun.shotTypeModifier != -1)
                {
                    shotTypeItems[0].TurnToAir();
                    installed = true;
                }
            }
            
            // Install damage type modifier
            if (!damageTypeItems[0].IsAir && modularGun.damageTypeModifier == -1)
            {
                modularGun.damageTypeModifier = GetModifierID(damageTypeItems[0].type, "damage");
                if (modularGun.damageTypeModifier != -1)
                {
                    damageTypeItems[0].TurnToAir();
                    installed = true;
                }
            }
            
            // Install rate of fire modifier
            if (!rateOfFireItems[0].IsAir && modularGun.rateOfFireModifier == -1)
            {
                modularGun.rateOfFireModifier = GetModifierID(rateOfFireItems[0].type, "rate");
                if (modularGun.rateOfFireModifier != -1)
                {
                    rateOfFireItems[0].TurnToAir();
                    installed = true;
                }
            }
            
            if (installed)
            {
                Main.NewText("Modifiers installed!", Color.Green);
            }
            else
            {
                Main.NewText("No compatible modifiers to install!", Color.Yellow);
            }
        }
        
        private void RemoveModifiers(UIMouseEvent evt, UIElement listeningElement)
        {
            if (weaponItems[0].type != ModContent.ItemType<ModularGun>()) 
            {
                Main.NewText("Place a modular weapon in the weapon slot!", Color.Red);
                return;
            }
            
            ModularGun modularGun = weaponItems[0].ModItem as ModularGun;
            if (modularGun == null) return;
            
            Player player = Main.LocalPlayer;
            bool removed = false;
            
            // Remove shot type modifier
            if (modularGun.shotTypeModifier != -1 && shotTypeItems[0].IsAir)
            {
                // Create modifier item and put it in slot
                shotTypeItems[0].SetDefaults(GetItemTypeFromModifier(modularGun.shotTypeModifier, "shot"));
                modularGun.shotTypeModifier = -1;
                removed = true;
            }
            
            // Remove damage type modifier
            if (modularGun.damageTypeModifier != -1 && damageTypeItems[0].IsAir)
            {
                damageTypeItems[0].SetDefaults(GetItemTypeFromModifier(modularGun.damageTypeModifier, "damage"));
                modularGun.damageTypeModifier = -1;
                removed = true;
            }
            
            // Remove rate of fire modifier
            if (modularGun.rateOfFireModifier != -1 && rateOfFireItems[0].IsAir)
            {
                rateOfFireItems[0].SetDefaults(GetItemTypeFromModifier(modularGun.rateOfFireModifier, "rate"));
                modularGun.rateOfFireModifier = -1;
                removed = true;
            }
            
            if (removed)
            {
                Main.NewText("Modifiers removed!", Color.Green);
            }
            else
            {
                Main.NewText("No modifiers to remove or slots are full!", Color.Yellow);
            }
        }
        
        private void CloseUI(UIMouseEvent evt, UIElement listeningElement)
        {
            // Return items to player inventory
            Player player = Main.LocalPlayer;
            
            if (!weaponItems[0].IsAir)
            {
                player.QuickSpawnItem(player.GetSource_FromThis(), weaponItems[0]);
                weaponItems[0].TurnToAir();
            }
            if (!shotTypeItems[0].IsAir)
            {
                player.QuickSpawnItem(player.GetSource_FromThis(), shotTypeItems[0]);
                shotTypeItems[0].TurnToAir();
            }
            if (!damageTypeItems[0].IsAir)
            {
                player.QuickSpawnItem(player.GetSource_FromThis(), damageTypeItems[0]);
                damageTypeItems[0].TurnToAir();
            }
            if (!rateOfFireItems[0].IsAir)
            {
                player.QuickSpawnItem(player.GetSource_FromThis(), rateOfFireItems[0]);
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