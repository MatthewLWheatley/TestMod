using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;

namespace TestMod.Content.Tiles
{
    public class ModifierStation : ModTile
    {
        public override void SetStaticDefaults()
        {
            // Basic tile properties
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.Width = 3;
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.Origin = new Point16(1, 1);
            TileObjectData.addTile(Type);

            // Interaction properties
            TileID.Sets.HasOutlines[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;
            
            // Set the name (or use localization file)
            // DisplayName.SetDefault("Modifier Station");
            
            // Add to the furniture map entry
            AddMapEntry(new Color(120, 85, 60), CreateMapEntryName());
            
            // Set dust type for breaking effects
            DustType = DustID.WoodFurniture;
            
            // Add light emission
            Main.tileLighted[Type] = true;
        }
        
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            // Emit a soft blue light
            r = 0.1f;
            g = 0.2f;
            b = 0.4f;
        }
        
        public override bool RightClick(int i, int j)
        {
            Player player = Main.LocalPlayer;
            
            // Open the modifier UI
            Content.UI.ModifierStationUI.OpenUI();
            Main.NewText("Modifier Station opened!", Color.Green);
            
            return true;
        }
        
        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = ModContent.ItemType<Content.Items.ModifierStationItem>();
        }
        
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            // Drop the item when broken
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 48, 32, ModContent.ItemType<Items.ModifierStationItem>());
        }
    }
}