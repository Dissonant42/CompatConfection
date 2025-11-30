using System;
using CompatConfection.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CompatConfection.Content.Items.Weapons.Thorium
{
    public class SugarScepter : ModItem
    {
        public override string Texture => "CompatConfection/Content/Items/TempItem";
        public int GumdropPair = 0;
        public override void SetDefaults()
        {
            Item.damage = 80;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 30;
            Item.useTime = 42;
            Item.useAnimation = 42;
            Item.rare = ItemRarityID.Pink;
            Item.knockBack = 6f;
            Item.value = 46000;
            Item.UseSound = SoundID.Item43;
            Item.shoot = ModContent.ProjectileType<Gumdrop>();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float SpawnAngle = MathHelper.ToRadians(Main.rand.Next(0, 180));
            Vector2 MouseLoc = Main.MouseWorld;
            float SpawnDistance = 500f;

            Vector2 Spawn1 = MouseLoc + new Vector2(MathF.Cos(SpawnAngle) * SpawnDistance, MathF.Sin(SpawnAngle) * SpawnDistance);
            Vector2 Spawn2 = MouseLoc + new Vector2(MathF.Cos(SpawnAngle) * -SpawnDistance, MathF.Sin(SpawnAngle) * -SpawnDistance);

            Projectile Gumdrop1 = Projectile.NewProjectileDirect(source, Spawn1, Vector2.Zero, type, damage, knockback, player.whoAmI, GumdropPair, 0, 0);
            Projectile Gumdrop2 = Projectile.NewProjectileDirect(source, Spawn2, Vector2.Zero, type, damage, knockback, player.whoAmI, GumdropPair, 0, 0);
            Gumdrop1.rotation = SpawnAngle + MathHelper.PiOver2;
            Gumdrop2.rotation = SpawnAngle - MathHelper.PiOver2;


            GumdropPair++;
            return false;
        }
    }
}