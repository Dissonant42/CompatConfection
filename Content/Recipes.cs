using CompatConfection.Content.Items.Materials.Thorium;
using CompatConfection.Content.Items.Weapons.Thorium;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheConfectionRebirth.Items;
using TheConfectionRebirth.Items.Placeable;

namespace CompatConfection.Content
{
    public class Recipes : ModSystem
    {
        public override void AddRecipes()
        {
            if (ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod) == true)
            {
                Recipe SugarScepter = Recipe.Create(ModContent.ItemType<SugarScepter>(), 1);
                SugarScepter.AddIngredient<NeapoliniteBar>(12);
                SugarScepter.AddTile(TileID.MythrilAnvil);
                SugarScepter.Register();

                Recipe SMG = Recipe.Create(ModContent.ItemType<SMG>());
                SMG.AddIngredient(ModContent.ItemType<NeapoliniteBar>(), 12);
                SMG.AddTile(TileID.MythrilAnvil);
                SMG.Register();

                Recipe Geode = Recipe.Create(ModContent.ItemType<RockCandy>());
                Geode.AddIngredient(ModContent.ItemType<Saccharite>(), 1);
                Geode.AddIngredient(ModContent.ItemType<Creamstone>(), 8);
                Geode.AddTile(TileID.Anvils);
                Geode.Register();

                Recipe Charm = Recipe.Create(ModContent.ItemType<PureSugar>(), 3);
                Charm.AddIngredient(ModContent.ItemType<Sprinkles>(), 3);
                Charm.AddIngredient(ModContent.ItemType<SoulofDelight>(), 1);
                Charm.AddTile(TileID.Anvils);
                Charm.Register();
            }
        }
    }
}