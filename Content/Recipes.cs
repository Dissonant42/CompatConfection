using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CompatConfection.Content
{
    public class Recipes : ModSystem
    {
        public override void AddRecipes()
        {
            if (ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod) == true)
            {
                Recipe SugarScepter = Recipe.Create(ModContent.ItemType<Items.Weapons.Thorium.SugarScepter>(), 1);
                SugarScepter.AddIngredient<TheConfectionRebirth.Items.Placeable.NeapoliniteBar>(12);
                SugarScepter.AddTile(TileID.MythrilAnvil);
                SugarScepter.Register();
            }
        }
    }
}