using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;

namespace ModularWeapons.Content.Tiles
{
    public class ModifierStation : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
            TileObjectData.newTile.Height = 1;              // Changed from 2 to 1
            TileObjectData.newTile.Width = 2;               // Changed from 3 to 2
            TileObjectData.newTile.CoordinateHeights = new[] { 16 }; // Changed from { 16, 18 } to just { 16 }
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.Origin = new Point16(0, 0); // Changed from (1, 1) to (0, 0)
            TileObjectData.addTile(Type);

            TileID.Sets.HasOutlines[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;

            AddMapEntry(new Color(120, 85, 60), CreateMapEntryName());

            DustType = DustID.WoodFurniture;

            Main.tileLighted[Type] = true;
        }
        
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.1f;
            g = 0.2f;
            b = 0.4f;
        }

        public override bool RightClick(int i, int j)
        {
            Player player = Main.LocalPlayer;

            Vector2 stationWorldPosition = new Vector2(i * 16 + 16, j * 16 + 8); // Center of the 2x1 tile

            Content.UI.ModifierStationUI.OpenUIAtStation(stationWorldPosition);
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
            //Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 16, ModContent.ItemType<Items.ModifierStationItem>());
        }
    }
}