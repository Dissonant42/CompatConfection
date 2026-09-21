using CompatConfection.Content.Items.Materials.Thorium;
using CompatConfection.Content.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CompatConfection.Content.Items.Weapons.Thorium
{
    public class JawCrusher : ModItem
    {

        public override string Texture => "CompatConfection/Content/Projectiles/gumdropplaceholder";


        public const int HoldoutDistance = 20;
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 40;
            Item.useTime = 40;

            Item.shootSpeed = 8f;
            Item.knockBack = 9f;
            Item.damage = 36;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 8;
            Item.shoot = ModContent.ProjectileType<Gobstopper>();

            Item.rare = ItemRarityID.LightRed;
            Item.value = 40000;
            Item.noUseGraphic = true;
            Item.channel = true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ModContent.ProjectileType<JawCrusherHoldout>();

            velocity = Vector2.Normalize(velocity) * HoldoutDistance;

			Projectile.NewProjectile(source, position, velocity, type, damage, knockback, Main.myPlayer);

			return false;
        }
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<PureSugar>(), 12);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
    }
}