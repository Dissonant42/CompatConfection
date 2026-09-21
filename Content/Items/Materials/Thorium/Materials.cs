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
            Item.value = 2250;
            Item.rare = ItemRarityID.LightRed;
        }
        public override void AddRecipes()
		{
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
            Item.value = 3750;
            Item.rare = ItemRarityID.LightRed;
        }
        public override void AddRecipes()
		{
		}
    }
}