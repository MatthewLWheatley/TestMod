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
            recipe.AddIngredient(ItemID.DirtBlock, 1); // Replace with proper crafting materials
            recipe.Register();
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
            recipe.AddIngredient(ItemID.DirtBlock, 1);
            recipe.Register();
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
            recipe.AddIngredient(ItemID.DirtBlock, 1);
            recipe.Register();
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
            recipe.AddIngredient(ItemID.DirtBlock, 1);
            recipe.Register();
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
            recipe.AddIngredient(ItemID.DirtBlock, 1); // Replace with proper crafting materials
            recipe.Register();
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

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DirtBlock, 1);
            recipe.Register();
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
            recipe.AddIngredient(ItemID.DirtBlock, 1);
            recipe.Register();
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
            recipe.AddIngredient(ItemID.DirtBlock, 1);
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
            recipe.AddIngredient(ItemID.DirtBlock, 1); // Replace with proper crafting materials
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
            recipe.AddIngredient(ItemID.DirtBlock, 1);
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
            recipe.AddIngredient(ItemID.DirtBlock, 1);
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
            recipe.AddIngredient(ItemID.DirtBlock, 1);
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
            recipe.AddIngredient(ItemID.DirtBlock, 1);
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
            recipe.AddIngredient(ItemID.DirtBlock, 1);
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
            recipe.AddIngredient(ItemID.DirtBlock, 1);
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
            recipe.AddIngredient(ItemID.DirtBlock, 1);
            recipe.Register();
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
            recipe.AddIngredient(ItemID.DirtBlock, 1);
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
            recipe.AddIngredient(ItemID.DirtBlock, 1);
            recipe.Register();
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
            recipe.AddIngredient(ItemID.DirtBlock, 1);
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
            recipe.AddIngredient(ItemID.DirtBlock, 1);
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
            recipe.AddIngredient(ItemID.DirtBlock, 1);
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
            recipe.AddIngredient(ItemID.DirtBlock, 1);
            recipe.Register();
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
            recipe.AddIngredient(ItemID.DirtBlock, 1);
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
            recipe.AddIngredient(ItemID.DirtBlock, 1);
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
            recipe.AddIngredient(ItemID.DirtBlock, 1);
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
            recipe.AddIngredient(ItemID.DirtBlock, 1);
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
            recipe.AddIngredient(ItemID.DirtBlock, 1);
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
            recipe.AddIngredient(ItemID.DirtBlock, 1);
            recipe.Register();
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
            recipe.AddIngredient(ItemID.DirtBlock, 1);
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
            recipe.AddIngredient(ItemID.DirtBlock, 1);
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
            recipe.AddIngredient(ItemID.DirtBlock, 1);
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
            recipe.AddIngredient(ItemID.DirtBlock, 1);
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
            recipe.AddIngredient(ItemID.DirtBlock, 1);
            recipe.Register();
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
            recipe.AddIngredient(ItemID.DirtBlock, 1);
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
            recipe.AddIngredient(ItemID.DirtBlock, 1);
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
            recipe.AddIngredient(ItemID.DirtBlock, 1);
            recipe.Register();
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
            recipe.AddIngredient(ItemID.DirtBlock, 1);
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
            recipe.AddIngredient(ItemID.DirtBlock, 1);
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
            recipe.AddIngredient(ItemID.DirtBlock, 1);
            recipe.Register();
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