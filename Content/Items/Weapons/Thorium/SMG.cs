using CompatConfection.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CompatConfection.Content.Items.Weapons.Thorium
{
    public class SMG : ModItem
    {
        private int ShotAmount = 0;

        public const int HoldoutDistance = 20;
        public override string Texture => "CompatConfection/Content/Items/TempItem";
        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Pink;
            Item.value = 46000;

            Item.useTime = 9;
            Item.useAnimation = 9;
            //Item.reuseDelay = 28;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item11;

            Item.DamageType = DamageClass.Ranged;
            Item.damage = 13;
            Item.knockBack = .9f;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<SMGHoldout>();
            Item.shootSpeed = 12f;
            Item.useAmmo = AmmoID.Bullet;
            //Item.channel = true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            ShotAmount++;
            if (ShotAmount == 1)
            {
                for (int i = 0; i < 2; i++)
                {
                    Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(1));
                    newVelocity *= 1f - Main.rand.NextFloat(0.15f);
                    Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
                }
            }
            if (ShotAmount == 2)
            {
                float projCount = Main.rand.Next(0, 4);
                float tenthpi = .314159265358979f;
                Vector2 Offset1 = new Vector2(velocity.X, velocity.Y);
                Offset1.Normalize();
                Offset1 *= 8f;
                float rotation = Main.rand.NextFloat(-5, 5);
                for (int i = 0; i < projCount; i++)
                {
                    float num = i - (projCount - 1f) / 2f;
                    Vector2 Offset2 = Offset1.RotatedBy((double)(tenthpi * num));
                    Projectile.NewProjectile(source, position + Offset2, velocity.RotatedBy(MathHelper.ToRadians(rotation)), type, damage, knockback, player.whoAmI);
                }
                //Projectile.NewProjectile(source, position.RotatedBy(MathHelper.ToRadians(-1)), velocity, type, damage, knockback, player.whoAmI);
                ShotAmount = 0;
            }

            //type = ModContent.ProjectileType<SMGHoldout>();
            //velocity = Vector2.Zero;
            //Projectile.NewProjectile(source, position, velocity, type, damage, knockback, Main.myPlayer);
            return false;
        }
		public override bool CanConsumeAmmo(Item ammo, Player player) {
			// This prevents the item from consuming ammo when initially used. The projectile will "spin up" and then it will consume ammo instead.
			//if (player.ItemTimeIsZero) {
			//	return false;
			//}
            if (Main.rand.NextFloat() >= 0.25f)
            {
                return false;
            }
			return true;
		}
    }
}