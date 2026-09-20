using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CompatConfection;
using TheConfectionRebirth.Items;
using TheConfectionRebirth.Items.Placeable;

namespace CompatConfection.Content.Items.Materials.Thorium
{
    public class PureSugar : ModItem
    {
        public override string Texture => "CompatConfection/Content/Items/TempItem";

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 25;

        }
        public override void SetDefaults()
        {
			Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.value = 2250;
            Item.rare = ItemRarityID.LightRed;
        }
        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(3);
			recipe.AddIngredient(ModContent.ItemType<Sprinkles>(), 3);
			recipe.AddIngredient(ModContent.ItemType<SoulofDelight>(), 1);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
    }

    public class RockCandy : ModItem
    {
        public override string Texture => "CompatConfection/Content/Items/TempItem";
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 25;

        }
        public override void SetDefaults()
        {
			Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.value = 2250;
            Item.rare = ItemRarityID.LightRed;
        }
        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<Saccharite>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Creamstone>(), 8);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
    }
}