using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.DataStructures;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using TestMod.Content.Systems;


namespace TestMod.Content.Items
{
    // ==================== AMMO TYPE MODIFIERS ====================
    // Replace your old "Shot Type" modifiers with these

    public class MagicAmmoModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 5, 0);
            Item.rare = ItemRarityID.Blue;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Ammo Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Infinite ammo, consumes mana instead");
            effectLine.OverrideColor = Color.LightGray;
            tooltips.Add(effectLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FallenStar, 3);
            recipe.AddIngredient(ItemID.ManaCrystal, 1);
            recipe.AddIngredient(ItemID.IronBar, 2);
            recipe.AddIngredient(ModContent.ItemType<BasicModularComponent>());
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.FallenStar, 3);
            recipe2.AddIngredient(ItemID.ManaCrystal, 1);
            recipe2.AddIngredient(ItemID.LeadBar, 2);
            recipe2.AddIngredient(ModContent.ItemType<BasicModularComponent>());
            recipe2.AddTile(TileID.WorkBenches);
            recipe2.Register();
        }
    }

    public class ArrowAmmoModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.White;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Ammo Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Uses arrow ammunition - cheap and plentiful");
            effectLine.OverrideColor = Color.LightGray;
            tooltips.Add(effectLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.WoodenArrow, 50);
            recipe.AddIngredient(ItemID.IronBar, 3);
            recipe.AddIngredient(ItemID.Lens, 1);
            recipe.AddIngredient(ModContent.ItemType<BasicModularComponent>());
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.WoodenArrow, 50);
            recipe2.AddIngredient(ItemID.LeadBar, 3);
            recipe2.AddIngredient(ItemID.Lens, 1);
            recipe2.AddIngredient(ModContent.ItemType<BasicModularComponent>());
            recipe2.AddTile(TileID.Anvils);
            recipe2.Register();
        }
    }

    public class BulletAmmoModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 15, 0);
            Item.rare = ItemRarityID.Green;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Ammo Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Uses bullet ammunition - mid-tier damage/cost");
            effectLine.OverrideColor = Color.LightGray;
            tooltips.Add(effectLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MusketBall, 100);
            recipe.AddIngredient(ItemID.GoldBar, 3);
            recipe.AddIngredient(ItemID.Lens, 2);
            recipe.AddIngredient(ModContent.ItemType<BasicModularComponent>());
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.MusketBall, 100);
            recipe2.AddIngredient(ItemID.PlatinumBar, 3);
            recipe2.AddIngredient(ItemID.Lens, 2);
            recipe2.AddIngredient(ModContent.ItemType<BasicModularComponent>());
            recipe2.AddTile(TileID.Anvils);
            recipe2.Register();
        }
    }

    public class RocketAmmoModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 25, 0);
            Item.rare = ItemRarityID.Orange;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Ammo Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Uses rocket ammunition - high damage/cost");
            effectLine.OverrideColor = Color.LightGray;
            tooltips.Add(effectLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.RocketI, 10);
            recipe.AddIngredient(ItemID.Dynamite, 100);
            recipe.AddIngredient(ItemID.CobaltBar, 3);
            recipe.AddIngredient(ModContent.ItemType<BasicModularComponent>());
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.RocketI, 10);
            recipe2.AddIngredient(ItemID.Dynamite, 100);
            recipe2.AddIngredient(ItemID.PalladiumBar, 3);
            recipe2.AddIngredient(ModContent.ItemType<BasicModularComponent>());
            recipe2.AddTile(TileID.MythrilAnvil);
            recipe2.Register();
        }
    }

    public class EliteMagicAmmoModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 5, 0);
            Item.rare = ItemRarityID.Blue;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Ammo Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Infinite ammo, consumes mana instead");
            effectLine.OverrideColor = Color.LightGray;
            tooltips.Add(effectLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MagicAmmoModifier>(), 1);
            recipe.AddIngredient(ItemID.FallenStar, 10);
            recipe.AddIngredient(ItemID.GoldBar, 3);
            recipe.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 3);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<MagicAmmoModifier>(), 1);
            recipe2.AddIngredient(ItemID.FallenStar, 10);
            recipe2.AddIngredient(ItemID.PlatinumBar, 3);
            recipe2.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 3);
            recipe2.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe2.Register();
        }
    }

    public class EliteArrowAmmoModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.White;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Ammo Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Uses arrow ammunition - cheap and plentiful");
            effectLine.OverrideColor = Color.LightGray;
            tooltips.Add(effectLine);
        }

        public override void AddRecipes() // EliteArrowAmmoModifier
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ArrowAmmoModifier>(), 1);
            recipe.AddIngredient(ItemID.WoodenArrow, 200);
            recipe.AddIngredient(ItemID.GoldBar, 5);
            recipe.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 3);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<ArrowAmmoModifier>(), 1);
            recipe2.AddIngredient(ItemID.WoodenArrow, 200);
            recipe2.AddIngredient(ItemID.PlatinumBar, 5);
            recipe2.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 3);
            recipe2.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe2.Register();
        }
    }

    public class EliteBulletAmmoModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 15, 0);
            Item.rare = ItemRarityID.Green;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Ammo Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Uses bullet ammunition - mid-tier damage/cost");
            effectLine.OverrideColor = Color.LightGray;
            tooltips.Add(effectLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BulletAmmoModifier>(), 1);
            recipe.AddIngredient(ItemID.SilverBullet, 200);
            recipe.AddIngredient(ItemID.CobaltBar, 5);
            recipe.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 3);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<BulletAmmoModifier>(), 1);
            recipe2.AddIngredient(ItemID.SilverBullet, 200);
            recipe2.AddIngredient(ItemID.PalladiumBar, 5);
            recipe2.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 3);
            recipe2.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe2.Register();
        }
    }

    public class EliteRocketAmmoModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 25, 0);
            Item.rare = ItemRarityID.Orange;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Ammo Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Uses rocket ammunition - high damage/cost");
            effectLine.OverrideColor = Color.LightGray;
            tooltips.Add(effectLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RocketAmmoModifier>(), 1);
            recipe.AddIngredient(ItemID.RocketII, 25);
            recipe.AddIngredient(ItemID.HallowedBar, 8);
            recipe.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 3);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();
        }
    }

    public class PerfectMagicAmmoModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 5, 0);
            Item.rare = ItemRarityID.Blue;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Ammo Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Infinite ammo, consumes mana instead");
            effectLine.OverrideColor = Color.LightGray;
            tooltips.Add(effectLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EliteMagicAmmoModifier>(), 1);
            recipe.AddIngredient(ItemID.FallenStar, 25);
            recipe.AddIngredient(ItemID.HallowedBar, 5);
            recipe.AddIngredient(ModContent.ItemType<PerfectModularComponent>(), 5);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();
        }
    }

    public class PerfectArrowAmmoModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.White;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Ammo Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Uses arrow ammunition - cheap and plentiful");
            effectLine.OverrideColor = Color.LightGray;
            tooltips.Add(effectLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EliteArrowAmmoModifier>(), 1);
            recipe.AddIngredient(ItemID.HolyArrow, 100);
            recipe.AddIngredient(ItemID.HallowedBar, 5);
            recipe.AddIngredient(ModContent.ItemType<PerfectModularComponent>(), 5);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();
        }
    }

    public class PerfectBulletAmmoModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 15, 0);
            Item.rare = ItemRarityID.Green;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Ammo Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Uses bullet ammunition - mid-tier damage/cost");
            effectLine.OverrideColor = Color.LightGray;
            tooltips.Add(effectLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EliteBulletAmmoModifier>(), 1);
            recipe.AddIngredient(ItemID.CrystalBullet, 150);
            recipe.AddIngredient(ItemID.HallowedBar, 5);
            recipe.AddIngredient(ModContent.ItemType<PerfectModularComponent>(), 5);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();
        }
    }

    public class PerfectRocketAmmoModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 25, 0);
            Item.rare = ItemRarityID.Orange;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Ammo Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Uses rocket ammunition - high damage/cost");
            effectLine.OverrideColor = Color.LightGray;
            tooltips.Add(effectLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EliteRocketAmmoModifier>(), 1);
            recipe.AddIngredient(ItemID.RocketIV, 50);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddIngredient(ModContent.ItemType<PerfectModularComponent>(), 5);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();
        }
    }


    // ==================== DAMAGE TYPE MODIFIERS ====================
    // Keep your existing Fire, Water, Lightning, Earth and add these two

    public class FireDamageModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.Orange;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Damage Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Applies 'On Fire!' debuff");
            effectLine.OverrideColor = Color.Orange;
            tooltips.Add(effectLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Torch, 10);
            recipe.AddIngredient(ItemID.Gel, 5);
            recipe.AddIngredient(ItemID.Wood, 15);
            recipe.AddIngredient(ModContent.ItemType<BasicModularComponent>());
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }

    public class WaterDamageModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.Blue;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Damage Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Applies 'Slow' debuff");
            effectLine.OverrideColor = Color.LightBlue;
            tooltips.Add(effectLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Coral, 5);
            recipe.AddIngredient(ItemID.Seashell, 3);
            recipe.AddIngredient(ItemID.WaterBucket, 1);
            recipe.AddIngredient(ModContent.ItemType<BasicModularComponent>());
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }

    public class LightningDamageModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 15, 0);
            Item.rare = ItemRarityID.Yellow;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Damage Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Applies 'Ichor' debuff (defense reduction)");
            effectLine.OverrideColor = Color.Yellow;
            tooltips.Add(effectLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.WireBulb, 5);
            recipe.AddIngredient(ItemID.IronBar, 3);
            recipe.AddIngredient(ItemID.FallenStar, 2);
            recipe.AddIngredient(ModContent.ItemType<BasicModularComponent>());
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }

    public class EarthDamageModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 15, 0);
            Item.rare = ItemRarityID.Green;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Damage Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Applies 'Bleeding' debuff");
            effectLine.OverrideColor = Color.Red;
            tooltips.Add(effectLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.StoneBlock, 50);
            recipe.AddIngredient(ItemID.Diamond, 5);
            recipe.AddIngredient(ItemID.IronOre, 10);
            recipe.AddIngredient(ModContent.ItemType<BasicModularComponent>());
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.StoneBlock, 50);
            recipe2.AddIngredient(ItemID.Amber, 5);
            recipe2.AddIngredient(ItemID.IronOre, 10);
            recipe2.AddIngredient(ModContent.ItemType<BasicModularComponent>());
            recipe2.AddTile(TileID.Anvils);
            recipe2.Register();

            Recipe recipe3 = CreateRecipe();
            recipe3.AddIngredient(ItemID.StoneBlock, 50);
            recipe3.AddIngredient(ItemID.Ruby, 5);
            recipe3.AddIngredient(ItemID.IronOre, 10);
            recipe3.AddIngredient(ModContent.ItemType<BasicModularComponent>());
            recipe3.AddTile(TileID.Anvils);
            recipe3.Register();

            Recipe recipe4 = CreateRecipe();
            recipe4.AddIngredient(ItemID.StoneBlock, 50);
            recipe4.AddIngredient(ItemID.Emerald, 5);
            recipe4.AddIngredient(ItemID.IronOre, 10);
            recipe4.AddIngredient(ModContent.ItemType<BasicModularComponent>());
            recipe4.AddTile(TileID.Anvils);
            recipe4.Register();

            Recipe recipe5 = CreateRecipe();
            recipe5.AddIngredient(ItemID.StoneBlock, 50);
            recipe5.AddIngredient(ItemID.Sapphire, 5);
            recipe5.AddIngredient(ItemID.IronOre, 10);
            recipe5.AddIngredient(ModContent.ItemType<BasicModularComponent>());
            recipe5.AddTile(TileID.Anvils);
            recipe5.Register();

            Recipe recipe6 = CreateRecipe();
            recipe6.AddIngredient(ItemID.StoneBlock, 50);
            recipe6.AddIngredient(ItemID.Topaz, 5);
            recipe6.AddIngredient(ItemID.IronOre, 10);
            recipe6.AddIngredient(ModContent.ItemType<BasicModularComponent>());
            recipe6.AddTile(TileID.Anvils);
            recipe6.Register();

            Recipe recipe7 = CreateRecipe();
            recipe7.AddIngredient(ItemID.StoneBlock, 50);
            recipe7.AddIngredient(ItemID.Amethyst, 5);
            recipe7.AddIngredient(ItemID.IronOre, 10);
            recipe7.AddIngredient(ModContent.ItemType<BasicModularComponent>());
            recipe7.AddTile(TileID.Anvils);
            recipe7.Register();
        }
    }

    public class WindDamageModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.LightPurple;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Damage Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "+50% knockback bonus");
            effectLine.OverrideColor = Color.LightGreen;
            tooltips.Add(effectLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CloudinaBottle, 1);
            recipe.AddIngredient(ItemID.PinWheel, 1);
            recipe.AddIngredient(ItemID.SilverBar, 2);
            recipe.AddIngredient(ModContent.ItemType<BasicModularComponent>());
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }

    public class SlimeDamageModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 15, 0);
            Item.rare = ItemRarityID.Purple;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Damage Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Applies 'Poisoned' debuff");
            effectLine.OverrideColor = Color.Purple;
            tooltips.Add(effectLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Gel, 25);
            recipe.AddIngredient(ItemID.PinkGel, 5);
            recipe.AddIngredient(ItemID.IronBar, 3);
            recipe.AddIngredient(ModContent.ItemType<BasicModularComponent>());
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.Gel, 25);
            recipe2.AddIngredient(ItemID.PinkGel, 5);
            recipe2.AddIngredient(ItemID.LeadBar, 3);
            recipe2.AddIngredient(ModContent.ItemType<BasicModularComponent>());
            recipe2.AddTile(TileID.Anvils);
            recipe2.Register();
        }
    }

    public class EliteFireDamageModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.Orange;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Damage Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Applies 'On Fire!' debuff");
            effectLine.OverrideColor = Color.Orange;
            tooltips.Add(effectLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FireDamageModifier>(), 1);
            recipe.AddIngredient(ItemID.Torch, 50);
            recipe.AddIngredient(ItemID.Hellstone, 10);
            recipe.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 3);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();
        }
    }

    public class EliteWaterDamageModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.Blue;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Damage Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Applies 'Slow' debuff");
            effectLine.OverrideColor = Color.LightBlue;
            tooltips.Add(effectLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<WaterDamageModifier>(), 1);
            recipe.AddIngredient(ItemID.Coral, 20);
            recipe.AddIngredient(ItemID.SharkFin, 3);
            recipe.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 3);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();
        }
    }

    public class EliteLightningDamageModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 15, 0);
            Item.rare = ItemRarityID.Yellow;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Damage Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Applies 'Ichor' debuff (defense reduction)");
            effectLine.OverrideColor = Color.Yellow;
            tooltips.Add(effectLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<LightningDamageModifier>(), 1);
            recipe.AddIngredient(ItemID.LightningBug, 25);
            recipe.AddIngredient(ItemID.SoulofLight, 5);
            recipe.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 3);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();
        }
    }

    public class EliteEarthDamageModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 15, 0);
            Item.rare = ItemRarityID.Green;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Damage Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Applies 'Bleeding' debuff");
            effectLine.OverrideColor = Color.Red;
            tooltips.Add(effectLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EarthDamageModifier>(), 1);
            recipe.AddIngredient(ItemID.StoneBlock, 200);
            recipe.AddIngredient(ItemID.Diamond, 10);
            recipe.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 3);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<EarthDamageModifier>(), 1);
            recipe2.AddIngredient(ItemID.StoneBlock, 200);
            recipe2.AddIngredient(ItemID.Amber, 10);
            recipe2.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 3);
            recipe2.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe2.Register();

            Recipe recipe3 = CreateRecipe();
            recipe3.AddIngredient(ModContent.ItemType<EarthDamageModifier>(), 1);
            recipe3.AddIngredient(ItemID.StoneBlock, 200);
            recipe3.AddIngredient(ItemID.Ruby, 10);
            recipe3.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 3);
            recipe3.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe3.Register();

            Recipe recipe4 = CreateRecipe();
            recipe4.AddIngredient(ModContent.ItemType<EarthDamageModifier>(), 1);
            recipe4.AddIngredient(ItemID.StoneBlock, 200);
            recipe4.AddIngredient(ItemID.Emerald, 10);
            recipe4.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 3);
            recipe4.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe4.Register();

            Recipe recipe5 = CreateRecipe();
            recipe5.AddIngredient(ModContent.ItemType<EarthDamageModifier>(), 1);
            recipe5.AddIngredient(ItemID.StoneBlock, 200);
            recipe5.AddIngredient(ItemID.Sapphire, 10);
            recipe5.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 3);
            recipe5.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe5.Register();

            Recipe recipe6 = CreateRecipe();
            recipe6.AddIngredient(ModContent.ItemType<EarthDamageModifier>(), 1);
            recipe6.AddIngredient(ItemID.StoneBlock, 200);
            recipe6.AddIngredient(ItemID.Topaz, 10);
            recipe6.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 3);
            recipe6.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe6.Register();

            Recipe recipe7 = CreateRecipe();
            recipe7.AddIngredient(ModContent.ItemType<EarthDamageModifier>(), 1);
            recipe7.AddIngredient(ItemID.StoneBlock, 200);
            recipe7.AddIngredient(ItemID.Amethyst, 10);
            recipe7.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 3);
            recipe7.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe7.Register();
        }
    }

    public class EliteWindDamageModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.LightPurple;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Damage Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "+50% knockback bonus");
            effectLine.OverrideColor = Color.LightGreen;
            tooltips.Add(effectLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<WindDamageModifier>(), 1);
            recipe.AddIngredient(ItemID.Feather, 10);
            recipe.AddIngredient(ItemID.SoulofFlight, 5);
            recipe.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 3);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();
        }
    }

    public class EliteSlimeDamageModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 15, 0);
            Item.rare = ItemRarityID.Purple;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Damage Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Applies 'Poisoned' debuff");
            effectLine.OverrideColor = Color.Purple;
            tooltips.Add(effectLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SlimeDamageModifier>(), 1);
            recipe.AddIngredient(ItemID.Gel, 100);
            recipe.AddIngredient(ItemID.SlimeStaff, 1); // King Slime drop
            recipe.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 3);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();
        }
    }

    public class PerfectFireDamageModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.Orange;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Damage Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Applies 'On Fire!' debuff");
            effectLine.OverrideColor = Color.Orange;
            tooltips.Add(effectLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EliteFireDamageModifier>(), 1);
            recipe.AddIngredient(ItemID.LivingFireBlock, 20);
            recipe.AddIngredient(ItemID.HellstoneBar, 8);
            recipe.AddIngredient(ModContent.ItemType<PerfectModularComponent>(), 5);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();
        }
    }

    public class PerfectWaterDamageModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.Blue;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Damage Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Applies 'Slow' debuff");
            effectLine.OverrideColor = Color.LightBlue;
            tooltips.Add(effectLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EliteWaterDamageModifier>(), 1);
            recipe.AddIngredient(ItemID.SoulofFlight, 10);
            recipe.AddIngredient(ItemID.Trident, 1);
            recipe.AddIngredient(ModContent.ItemType<PerfectModularComponent>(), 4);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();
        }
    }

    public class PerfectLightningDamageModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 15, 0);
            Item.rare = ItemRarityID.Yellow;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Damage Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Applies 'Ichor' debuff (defense reduction)");
            effectLine.OverrideColor = Color.Yellow;
            tooltips.Add(effectLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EliteLightningDamageModifier>(), 1);
            recipe.AddIngredient(ItemID.SoulofLight, 15);
            recipe.AddIngredient(ItemID.LightShard, 5);
            recipe.AddIngredient(ModContent.ItemType<PerfectModularComponent>(), 5);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();
        }
    }

    public class PerfectEarthDamageModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 15, 0);
            Item.rare = ItemRarityID.Green;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Damage Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Applies 'Bleeding' debuff");
            effectLine.OverrideColor = Color.Red;
            tooltips.Add(effectLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EliteEarthDamageModifier>(), 1);
            recipe.AddIngredient(ItemID.Diamond, 15);
            recipe.AddIngredient(ItemID.LifeCrystal, 3);
            recipe.AddIngredient(ModContent.ItemType<PerfectModularComponent>(), 5);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<EliteEarthDamageModifier>(), 1);
            recipe2.AddIngredient(ItemID.Amber, 15);
            recipe2.AddIngredient(ItemID.LifeCrystal, 3);
            recipe2.AddIngredient(ModContent.ItemType<PerfectModularComponent>(), 5);
            recipe2.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe2.Register();

            Recipe recipe3 = CreateRecipe();
            recipe3.AddIngredient(ModContent.ItemType<EliteEarthDamageModifier>(), 1);
            recipe3.AddIngredient(ItemID.Ruby, 15);
            recipe3.AddIngredient(ItemID.LifeCrystal, 3);
            recipe3.AddIngredient(ModContent.ItemType<PerfectModularComponent>(), 5);
            recipe3.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe3.Register();

            Recipe recipe4 = CreateRecipe();
            recipe4.AddIngredient(ModContent.ItemType<EliteEarthDamageModifier>(), 1);
            recipe4.AddIngredient(ItemID.Emerald, 15);
            recipe4.AddIngredient(ItemID.LifeCrystal, 3);
            recipe4.AddIngredient(ModContent.ItemType<PerfectModularComponent>(), 5);
            recipe4.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe4.Register();

            Recipe recipe5 = CreateRecipe();
            recipe5.AddIngredient(ModContent.ItemType<EliteEarthDamageModifier>(), 1);
            recipe5.AddIngredient(ItemID.Sapphire, 15);
            recipe5.AddIngredient(ItemID.LifeCrystal, 3);
            recipe5.AddIngredient(ModContent.ItemType<PerfectModularComponent>(), 5);
            recipe5.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe5.Register();

            Recipe recipe6 = CreateRecipe();
            recipe6.AddIngredient(ModContent.ItemType<EliteEarthDamageModifier>(), 1);
            recipe6.AddIngredient(ItemID.Topaz, 15);
            recipe6.AddIngredient(ItemID.LifeCrystal, 3);
            recipe6.AddIngredient(ModContent.ItemType<PerfectModularComponent>(), 5);
            recipe6.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe6.Register();

            Recipe recipe7 = CreateRecipe();
            recipe7.AddIngredient(ModContent.ItemType<EliteEarthDamageModifier>(), 1);
            recipe7.AddIngredient(ItemID.Amethyst, 15);
            recipe7.AddIngredient(ItemID.LifeCrystal, 3);
            recipe7.AddIngredient(ModContent.ItemType<PerfectModularComponent>(), 5);
            recipe7.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe7.Register();
        }
    }

    public class PerfectWindDamageModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.LightPurple;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Damage Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "+50% knockback bonus");
            effectLine.OverrideColor = Color.LightGreen;
            tooltips.Add(effectLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EliteWindDamageModifier>(), 1);
            recipe.AddIngredient(ItemID.SoulofFlight, 15);
            recipe.AddIngredient(ItemID.Jetpack, 1);
            recipe.AddIngredient(ModContent.ItemType<PerfectModularComponent>(), 5);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();
        }
    }

    public class PerfectSlimeDamageModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 15, 0);
            Item.rare = ItemRarityID.Purple;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Damage Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Applies 'Poisoned' debuff");
            effectLine.OverrideColor = Color.Purple;
            tooltips.Add(effectLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EliteSlimeDamageModifier>(), 1);
            recipe.AddIngredient(ItemID.QueenSlimeBossBag, 1); // Or alternative Queen Slime materials
            recipe.AddIngredient(ItemID.Gel, 250);
            recipe.AddIngredient(ModContent.ItemType<PerfectModularComponent>(), 5);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();
        }
    }


    // ==================== SHOT TYPE MODIFIERS ====================
    // Rename your "Rate of Fire" modifiers to "Shot Type"

    public class AutoFireModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 25, 0);
            Item.rare = ItemRarityID.Pink;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Shot Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Hold to continuously fire, reduced damage per shot");
            effectLine.OverrideColor = Color.LightGray;
            tooltips.Add(effectLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ClockworkAssaultRifle, 1); // High-tier gun
            recipe.AddIngredient(ItemID.Cog, 10);
            recipe.AddIngredient(ItemID.Wire, 50);
            recipe.AddIngredient(ModContent.ItemType<BasicModularComponent>(), 2);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }

    public class BurstFireModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.Pink;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Shot Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Single click fires multiple projectiles, balanced damage");
            effectLine.OverrideColor = Color.LightGray;
            tooltips.Add(effectLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.IronBar, 5);
            recipe.AddIngredient(ItemID.Lens, 2);
            recipe.AddIngredient(ItemID.Bomb, 1);
            recipe.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }

    public class ChargeFireModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 15, 0);
            Item.rare = ItemRarityID.Pink;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Shot Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Hold to charge, release for high damage shot");
            effectLine.OverrideColor = Color.LightGray;
            tooltips.Add(effectLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.GoldBar, 5);
            recipe.AddIngredient(ItemID.ManaCrystal, 2);
            recipe.AddIngredient(ItemID.Diamond, 1);
            recipe.AddIngredient(ModContent.ItemType<BasicModularComponent>(), 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.GoldBar, 5);
            recipe2.AddIngredient(ItemID.ManaCrystal, 2);
            recipe2.AddIngredient(ItemID.Topaz, 1);
            recipe2.AddIngredient(ModContent.ItemType<BasicModularComponent>(), 1);
            recipe2.AddTile(TileID.Anvils);
            recipe2.Register();

            Recipe recipe3 = CreateRecipe();
            recipe3.AddIngredient(ItemID.GoldBar, 5);
            recipe3.AddIngredient(ItemID.ManaCrystal, 2);
            recipe3.AddIngredient(ItemID.Emerald, 1);
            recipe3.AddIngredient(ModContent.ItemType<BasicModularComponent>(), 1);
            recipe3.AddTile(TileID.Anvils);
            recipe3.Register();

            Recipe recipe4 = CreateRecipe();
            recipe4.AddIngredient(ItemID.GoldBar, 5);
            recipe4.AddIngredient(ItemID.ManaCrystal, 2);
            recipe4.AddIngredient(ItemID.Amethyst, 1);
            recipe4.AddIngredient(ModContent.ItemType<BasicModularComponent>(), 1);
            recipe4.AddTile(TileID.Anvils);
            recipe4.Register();

            Recipe recipe5 = CreateRecipe();
            recipe5.AddIngredient(ItemID.GoldBar, 5);
            recipe5.AddIngredient(ItemID.ManaCrystal, 2);
            recipe5.AddIngredient(ItemID.Amber, 1);
            recipe5.AddIngredient(ModContent.ItemType<BasicModularComponent>(), 1);
            recipe5.AddTile(TileID.Anvils);
            recipe5.Register();

            Recipe recipe6 = CreateRecipe();
            recipe6.AddIngredient(ItemID.GoldBar, 5);
            recipe6.AddIngredient(ItemID.ManaCrystal, 2);
            recipe6.AddIngredient(ItemID.Ruby, 1);
            recipe6.AddIngredient(ModContent.ItemType<BasicModularComponent>(), 1);
            recipe6.AddTile(TileID.Anvils);
            recipe6.Register();

            Recipe recipe7 = CreateRecipe();
            recipe7.AddIngredient(ItemID.GoldBar, 5);
            recipe7.AddIngredient(ItemID.ManaCrystal, 2);
            recipe7.AddIngredient(ItemID.Sapphire, 1);
            recipe7.AddIngredient(ModContent.ItemType<BasicModularComponent>(), 1);
            recipe7.AddTile(TileID.Anvils);
            recipe7.Register();
        }
    }

    public class EliteAutoFireModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 25, 0);
            Item.rare = ItemRarityID.Pink;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Shot Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Hold to continuously fire, reduced damage per shot");
            effectLine.OverrideColor = Color.LightGray;
            tooltips.Add(effectLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AutoFireModifier>(), 1);
            recipe.AddIngredient(ItemID.Cog, 25);
            recipe.AddIngredient(ItemID.Timer1Second, 5);
            recipe.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 3);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();
        }
    }

    public class EliteBurstFireModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.Pink;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Shot Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Single click fires multiple projectiles, balanced damage");
            effectLine.OverrideColor = Color.LightGray;
            tooltips.Add(effectLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BurstFireModifier>(), 1);
            recipe.AddIngredient(ItemID.Cog, 5);
            recipe.AddIngredient(ItemID.Wire, 25);
            recipe.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 3);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();
        }
    }

    public class EliteChargeFireModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 15, 0);
            Item.rare = ItemRarityID.Pink;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Shot Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Hold to charge, release for high damage shot");
            effectLine.OverrideColor = Color.LightGray;
            tooltips.Add(effectLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ChargeFireModifier>(), 1);
            recipe.AddIngredient(ItemID.Diamond, 3);
            recipe.AddIngredient(ItemID.CrystalShard, 5);
            recipe.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 3);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<ChargeFireModifier>(), 1);
            recipe2.AddIngredient(ItemID.Topaz, 3);
            recipe2.AddIngredient(ItemID.CrystalShard, 5);
            recipe2.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 3);
            recipe2.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe2.Register();

            Recipe recipe3 = CreateRecipe();
            recipe3.AddIngredient(ModContent.ItemType<ChargeFireModifier>(), 1);
            recipe3.AddIngredient(ItemID.Amethyst, 3);
            recipe3.AddIngredient(ItemID.CrystalShard, 5);
            recipe3.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 3);
            recipe3.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe3.Register();

            Recipe recipe4 = CreateRecipe();
            recipe4.AddIngredient(ModContent.ItemType<ChargeFireModifier>(), 1);
            recipe4.AddIngredient(ItemID.Amber, 3);
            recipe4.AddIngredient(ItemID.CrystalShard, 5);
            recipe4.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 3);
            recipe4.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe4.Register();

            Recipe recipe5 = CreateRecipe();
            recipe5.AddIngredient(ModContent.ItemType<ChargeFireModifier>(), 1);
            recipe5.AddIngredient(ItemID.Emerald, 3);
            recipe5.AddIngredient(ItemID.CrystalShard, 5);
            recipe5.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 3);
            recipe5.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe5.Register();

            Recipe recipe6 = CreateRecipe();
            recipe6.AddIngredient(ModContent.ItemType<ChargeFireModifier>(), 1);
            recipe6.AddIngredient(ItemID.Sapphire, 3);
            recipe6.AddIngredient(ItemID.CrystalShard, 5);
            recipe6.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 3);
            recipe6.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe6.Register();

            Recipe recipe7 = CreateRecipe();
            recipe7.AddIngredient(ModContent.ItemType<ChargeFireModifier>(), 1);
            recipe7.AddIngredient(ItemID.Ruby, 3);
            recipe7.AddIngredient(ItemID.CrystalShard, 5);
            recipe7.AddIngredient(ModContent.ItemType<EliteModularComponent>(), 3);
            recipe7.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe7.Register();
        }
    }

    public class PerfectAutoFireModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 25, 0);
            Item.rare = ItemRarityID.Pink;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Shot Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Hold to continuously fire, reduced damage per shot");
            effectLine.OverrideColor = Color.LightGray;
            tooltips.Add(effectLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EliteAutoFireModifier>(), 1);
            recipe.AddIngredient(ItemID.Megashark, 1); // End-game gun reference
            recipe.AddIngredient(ItemID.Cog, 50);
            recipe.AddIngredient(ModContent.ItemType<PerfectModularComponent>(), 5);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();
        }
    }

    public class PerfectBurstFireModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.Pink;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Shot Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Single click fires multiple projectiles, balanced damage");
            effectLine.OverrideColor = Color.LightGray;
            tooltips.Add(effectLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EliteBurstFireModifier>(), 1);
            recipe.AddIngredient(ItemID.Cog, 15);
            recipe.AddIngredient(ItemID.MechanicalEye, 1); // Mechanical boss material
            recipe.AddIngredient(ModContent.ItemType<PerfectModularComponent>(), 5);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();
        }
    }

    public class PerfectChargeFireModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 15, 0);
            Item.rare = ItemRarityID.Pink;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Shot Type Modifier");
            typeLine.OverrideColor = Color.LightBlue;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Hold to charge, release for high damage shot");
            effectLine.OverrideColor = Color.LightGray;
            tooltips.Add(effectLine);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EliteChargeFireModifier>(), 1);
            recipe.AddIngredient(ItemID.Diamond, 8);
            recipe.AddIngredient(ItemID.MagicPowerPotion, 5);
            recipe.AddIngredient(ModContent.ItemType<PerfectModularComponent>(), 5);
            recipe.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<EliteChargeFireModifier>(), 1);
            recipe2.AddIngredient(ItemID.Topaz, 8);
            recipe2.AddIngredient(ItemID.MagicPowerPotion, 5);
            recipe2.AddIngredient(ModContent.ItemType<PerfectModularComponent>(), 5);
            recipe2.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe2.Register();

            Recipe recipe3 = CreateRecipe();
            recipe3.AddIngredient(ModContent.ItemType<EliteChargeFireModifier>(), 1);
            recipe3.AddIngredient(ItemID.Ruby, 8);
            recipe3.AddIngredient(ItemID.MagicPowerPotion, 5);
            recipe3.AddIngredient(ModContent.ItemType<PerfectModularComponent>(), 5);
            recipe3.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe3.Register();

            Recipe recipe4 = CreateRecipe();
            recipe4.AddIngredient(ModContent.ItemType<EliteChargeFireModifier>(), 1);
            recipe4.AddIngredient(ItemID.Amethyst, 8);
            recipe4.AddIngredient(ItemID.MagicPowerPotion, 5);
            recipe4.AddIngredient(ModContent.ItemType<PerfectModularComponent>(), 5);
            recipe4.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe4.Register();

            Recipe recipe5 = CreateRecipe();
            recipe5.AddIngredient(ModContent.ItemType<EliteChargeFireModifier>(), 1);
            recipe5.AddIngredient(ItemID.Sapphire, 8);
            recipe5.AddIngredient(ItemID.MagicPowerPotion, 5);
            recipe5.AddIngredient(ModContent.ItemType<PerfectModularComponent>(), 5);
            recipe5.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe5.Register();

            Recipe recipe6 = CreateRecipe();
            recipe6.AddIngredient(ModContent.ItemType<EliteChargeFireModifier>(), 1);
            recipe6.AddIngredient(ItemID.Amber, 8);
            recipe6.AddIngredient(ItemID.MagicPowerPotion, 5);
            recipe6.AddIngredient(ModContent.ItemType<PerfectModularComponent>(), 5);
            recipe6.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe6.Register();

            Recipe recipe7 = CreateRecipe();
            recipe7.AddIngredient(ModContent.ItemType<EliteChargeFireModifier>(), 1);
            recipe7.AddIngredient(ItemID.Emerald, 8);
            recipe7.AddIngredient(ItemID.MagicPowerPotion, 5);
            recipe7.AddIngredient(ModContent.ItemType<PerfectModularComponent>(), 5);
            recipe7.AddTile(ModContent.TileType<Content.Tiles.ModifierStation>());
            recipe7.Register();
        }
    }


    // ==================== SPECIAL EFFECT MODIFIERS ====================
    // These are unique modifiers that add special effects to projectiles

    public class PiercingModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 5, 0, 0); // Higher value since boss drop
            Item.rare = ItemRarityID.Expert; // Special rarity for boss drops
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Special Effect Modifier");
            typeLine.OverrideColor = Color.Gold;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Projectiles penetrate 1 enemy");
            effectLine.OverrideColor = Color.LightGray;
            tooltips.Add(effectLine);

            TooltipLine bossLine = new TooltipLine(Mod, "BossDrop", "King Slime Boss Drop");
            bossLine.OverrideColor = Color.Cyan;
            tooltips.Add(bossLine);
        }

        // NO RECIPE - Boss drop only!
    }

    public class BouncingModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Expert;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Special Effect Modifier");
            typeLine.OverrideColor = Color.Gold;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Projectiles ricochet once");
            effectLine.OverrideColor = Color.LightGray;
            tooltips.Add(effectLine);

            TooltipLine bossLine = new TooltipLine(Mod, "BossDrop", "Brain of Cthulhu Boss Drop");
            bossLine.OverrideColor = Color.Cyan;
            tooltips.Add(bossLine);
        }

        // NO RECIPE - Boss drop only!
    }

    public class HomingModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 7, 50, 0); // Higher value for 4-point modifier
            Item.rare = ItemRarityID.Master; // Even rarer for 4-point effects
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Special Effect Modifier");
            typeLine.OverrideColor = Color.Gold;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Slight target seeking behavior");
            effectLine.OverrideColor = Color.LightGray;
            tooltips.Add(effectLine);

            TooltipLine bossLine = new TooltipLine(Mod, "BossDrop", "Skeletron Boss Drop");
            bossLine.OverrideColor = Color.Cyan;
            tooltips.Add(bossLine);
        }

        // NO RECIPE - Boss drop only!
    }

    public class LifeStealModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 7, 50, 0);
            Item.rare = ItemRarityID.Master;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Special Effect Modifier");
            typeLine.OverrideColor = Color.Gold;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "2% damage converted to health");
            effectLine.OverrideColor = Color.LightGray;
            tooltips.Add(effectLine);

            TooltipLine bossLine = new TooltipLine(Mod, "BossDrop", "Wall of Flesh Boss Drop");
            bossLine.OverrideColor = Color.Cyan;
            tooltips.Add(bossLine);
        }

        // NO RECIPE - Boss drop only!
    }

    public class CritBoostModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Expert;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Special Effect Modifier");
            typeLine.OverrideColor = Color.Gold;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "+10% critical strike chance");
            effectLine.OverrideColor = Color.LightGray;
            tooltips.Add(effectLine);

            TooltipLine bossLine = new TooltipLine(Mod, "BossDrop", "Eye of Cthulhu Boss Drop");
            bossLine.OverrideColor = Color.Cyan;
            tooltips.Add(bossLine);
        }

        // NO RECIPE - Boss drop only!
    }


    public class ElitePiercingModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 5, 0, 0); // Higher value since boss drop
            Item.rare = ItemRarityID.Expert; // Special rarity for boss drops
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Special Effect Modifier");
            typeLine.OverrideColor = Color.Gold;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Projectiles penetrate 1 enemy");
            effectLine.OverrideColor = Color.LightGray;
            tooltips.Add(effectLine);

            TooltipLine bossLine = new TooltipLine(Mod, "BossDrop", "King Slime Boss Drop");
            bossLine.OverrideColor = Color.Cyan;
            tooltips.Add(bossLine);
        }

        // NO RECIPE - Boss drop only!
    }

    public class EliteBouncingModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Expert;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Special Effect Modifier");
            typeLine.OverrideColor = Color.Gold;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Projectiles ricochet once");
            effectLine.OverrideColor = Color.LightGray;
            tooltips.Add(effectLine);

            TooltipLine bossLine = new TooltipLine(Mod, "BossDrop", "Brain of Cthulhu Boss Drop");
            bossLine.OverrideColor = Color.Cyan;
            tooltips.Add(bossLine);
        }

        // NO RECIPE - Boss drop only!
    }

    public class EliteHomingModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 7, 50, 0); // Higher value for 4-point modifier
            Item.rare = ItemRarityID.Master; // Even rarer for 4-point effects
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Special Effect Modifier");
            typeLine.OverrideColor = Color.Gold;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Slight target seeking behavior");
            effectLine.OverrideColor = Color.LightGray;
            tooltips.Add(effectLine);

            TooltipLine bossLine = new TooltipLine(Mod, "BossDrop", "Skeletron Boss Drop");
            bossLine.OverrideColor = Color.Cyan;
            tooltips.Add(bossLine);
        }

        // NO RECIPE - Boss drop only!
    }

    public class EliteLifeStealModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 7, 50, 0);
            Item.rare = ItemRarityID.Master;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Special Effect Modifier");
            typeLine.OverrideColor = Color.Gold;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "2% damage converted to health");
            effectLine.OverrideColor = Color.LightGray;
            tooltips.Add(effectLine);

            TooltipLine bossLine = new TooltipLine(Mod, "BossDrop", "Wall of Flesh Boss Drop");
            bossLine.OverrideColor = Color.Cyan;
            tooltips.Add(bossLine);
        }

        // NO RECIPE - Boss drop only!
    }

    public class EliteCritBoostModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Expert;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Special Effect Modifier");
            typeLine.OverrideColor = Color.Gold;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "+10% critical strike chance");
            effectLine.OverrideColor = Color.LightGray;
            tooltips.Add(effectLine);

            TooltipLine bossLine = new TooltipLine(Mod, "BossDrop", "Eye of Cthulhu Boss Drop");
            bossLine.OverrideColor = Color.Cyan;
            tooltips.Add(bossLine);
        }

        // NO RECIPE - Boss drop only!
    }

    public class PerfectPiercingModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 5, 0, 0); // Higher value since boss drop
            Item.rare = ItemRarityID.Expert; // Special rarity for boss drops
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Special Effect Modifier");
            typeLine.OverrideColor = Color.Gold;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Projectiles penetrate 1 enemy");
            effectLine.OverrideColor = Color.LightGray;
            tooltips.Add(effectLine);

            TooltipLine bossLine = new TooltipLine(Mod, "BossDrop", "King Slime Boss Drop");
            bossLine.OverrideColor = Color.Cyan;
            tooltips.Add(bossLine);
        }

        // NO RECIPE - Boss drop only!
    }

    public class PerfectBouncingModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Expert;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Special Effect Modifier");
            typeLine.OverrideColor = Color.Gold;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Projectiles ricochet once");
            effectLine.OverrideColor = Color.LightGray;
            tooltips.Add(effectLine);

            TooltipLine bossLine = new TooltipLine(Mod, "BossDrop", "Brain of Cthulhu Boss Drop");
            bossLine.OverrideColor = Color.Cyan;
            tooltips.Add(bossLine);
        }

        // NO RECIPE - Boss drop only!
    }

    public class PerfectHomingModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 7, 50, 0); // Higher value for 4-point modifier
            Item.rare = ItemRarityID.Master; // Even rarer for 4-point effects
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Special Effect Modifier");
            typeLine.OverrideColor = Color.Gold;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "Slight target seeking behavior");
            effectLine.OverrideColor = Color.LightGray;
            tooltips.Add(effectLine);

            TooltipLine bossLine = new TooltipLine(Mod, "BossDrop", "Skeletron Boss Drop");
            bossLine.OverrideColor = Color.Cyan;
            tooltips.Add(bossLine);
        }

        // NO RECIPE - Boss drop only!
    }

    public class PerfectLifeStealModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 7, 50, 0);
            Item.rare = ItemRarityID.Master;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Special Effect Modifier");
            typeLine.OverrideColor = Color.Gold;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "2% damage converted to health");
            effectLine.OverrideColor = Color.LightGray;
            tooltips.Add(effectLine);

            TooltipLine bossLine = new TooltipLine(Mod, "BossDrop", "Wall of Flesh Boss Drop");
            bossLine.OverrideColor = Color.Cyan;
            tooltips.Add(bossLine);
        }

        // NO RECIPE - Boss drop only!
    }

    public class PerfectCritBoostModifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Expert;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int pointCost = Content.Systems.ModifierData.GetModifierPointCost(Item.type);
            string tier = Content.Systems.ModifierData.GetModifierTier(pointCost);

            TooltipLine pointLine = new TooltipLine(Mod, "PointCost", $"Point Cost: {pointCost} ({tier})");
            pointLine.OverrideColor = pointCost switch
            {
                1 => Color.White,
                2 => Color.LightGreen,
                3 => Color.Yellow,
                4 => Color.Orange,
                _ => Color.Gray
            };
            tooltips.Add(pointLine);

            TooltipLine typeLine = new TooltipLine(Mod, "ModifierType", "Special Effect Modifier");
            typeLine.OverrideColor = Color.Gold;
            tooltips.Add(typeLine);

            TooltipLine effectLine = new TooltipLine(Mod, "Effect", "+10% critical strike chance");
            effectLine.OverrideColor = Color.LightGray;
            tooltips.Add(effectLine);

            TooltipLine bossLine = new TooltipLine(Mod, "BossDrop", "Eye of Cthulhu Boss Drop");
            bossLine.OverrideColor = Color.Cyan;
            tooltips.Add(bossLine);
        }

        // NO RECIPE - Boss drop only!
    }

    public class BasicModularComponent : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 0, 5, 0);
            Item.rare = ItemRarityID.Green;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine tierLine = new TooltipLine(Mod, "ComponentTier", "Basic Modular Component");
            tierLine.OverrideColor = Color.LightGreen;
            tooltips.Add(tierLine);

            TooltipLine usageLine = new TooltipLine(Mod, "Usage", "Used for crafting and upgrading modular weapons and modifiers");
            usageLine.OverrideColor = Color.LightGray;
            tooltips.Add(usageLine);

            TooltipLine dropLine = new TooltipLine(Mod, "DropInfo", "Drops from pre-hardmode bosses");
            dropLine.OverrideColor = Color.Cyan;
            tooltips.Add(dropLine);
        }
    }

    public class EliteModularComponent : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 0, 25, 0);
            Item.rare = ItemRarityID.Orange;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine tierLine = new TooltipLine(Mod, "ComponentTier", "Elite Modular Component");
            tierLine.OverrideColor = Color.Orange;
            tooltips.Add(tierLine);

            TooltipLine usageLine = new TooltipLine(Mod, "Usage", "Used for advanced crafting and major upgrades");
            usageLine.OverrideColor = Color.LightGray;
            tooltips.Add(usageLine);

            TooltipLine dropLine = new TooltipLine(Mod, "DropInfo", "Drops from hardmode bosses");
            dropLine.OverrideColor = Color.Cyan;
            tooltips.Add(dropLine);
        }
    }

    public class PerfectModularComponent : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Purple;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine tierLine = new TooltipLine(Mod, "ComponentTier", "Perfect Modular Component");
            tierLine.OverrideColor = Color.Purple;
            tooltips.Add(tierLine);

            TooltipLine usageLine = new TooltipLine(Mod, "Usage", "Used for ultimate crafting and perfection upgrades");
            usageLine.OverrideColor = Color.LightGray;
            tooltips.Add(usageLine);

            TooltipLine dropLine = new TooltipLine(Mod, "DropInfo", "Drops from post-Plantera bosses");
            dropLine.OverrideColor = Color.Cyan;
            tooltips.Add(dropLine);
        }
    }
}