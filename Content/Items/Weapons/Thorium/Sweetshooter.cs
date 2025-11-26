using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CompatConfection.Content.Items.Weapons.Thorium
{
    public class Sweetshooter : ModItem
    {
        private int ShotAmount = 0;
        public override string Texture => "CompatConfection/Content/Items/TempItem";
        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Pink;
            Item.value = 46000;

            Item.useTime = 6;
            Item.useAnimation = 12;
            Item.reuseDelay = 28;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item11;

            Item.DamageType = DamageClass.Ranged;
            Item.damage = 28;
            Item.knockBack = .9f;
            Item.noMelee = true;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 12f;
            Item.useAmmo = AmmoID.Bullet;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            ShotAmount++;

            if (ShotAmount == 1)
            {
                for (int i = 0; i < 2; i++)
                {
                    Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(7));
                    newVelocity *= 1f - Main.rand.NextFloat(0.15f);
                    Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
                }
            }
            if (ShotAmount == 2)
            {
                float projCount = Main.rand.Next(1, 4);
                float tenthpi = .314159265358979f;
                Vector2 Offset1 = new Vector2(velocity.X, velocity.Y);
                Offset1.Normalize();
                Offset1 *= 8f;
                for (int i = 0; i < projCount; i++)
                {
                    float num = i - (projCount - 1f) / 2f;
                    Vector2 Offset2 = Offset1.RotatedBy((double)(tenthpi * num));
                    Projectile.NewProjectile(source, position + Offset2, velocity, type, damage, knockback, player.whoAmI);
                }
                //Projectile.NewProjectile(source, position.RotatedBy(MathHelper.ToRadians(-1)), velocity, type, damage, knockback, player.whoAmI);
                ShotAmount = 0;
            }
            return false;
        }
    }
}