using CompatConfection.Content.Items.Weapons.Thorium;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CompatConfection.Content.Projectiles
{
    public class SMGHoldout : ModProjectile
    {
        //public ref float HoldTimer => ref Projectile.ai[0];
        public ref float ShootTimer => ref Projectile.ai[0];
        public ref float ShotNumber => ref Projectile.ai[1];
        public bool shootNow;

        public override string Texture => "CompatConfection/Content/Items/TempItem";
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.hide = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            DrawOffsetX = -17;
            DrawOriginOffsetY = -4;
        }

        public override bool? CanDamage() => false;

        public override void AI()
        {
            Player plr = Main.player[Projectile.owner];
            Vector2 playerCenter = plr.RotatedRelativePoint(plr.MountedCenter);
            int useTime = plr.HeldItem.useTime;
            Item item = plr.HeldItem;

            //HoldTimer += 1f;

            ShootTimer += 1f;

            if (ShootTimer >= useTime)
            {
                ShootTimer = 0f;
                shootNow = true;

                ShotNumber += 1f;
                SoundEngine.PlaySound(SoundID.Item5, Projectile.position);
            } else shootNow = false;

            if (shootNow && Main.myPlayer == Projectile.owner)
            {
                if (plr.channel && plr.HasAmmo(plr.HeldItem) && !plr.noItems && !plr.CCed)
                {
                    float holdoutDistance = SMG.HoldoutDistance;
                    Vector2 holdoutOffset = holdoutDistance * Vector2.Normalize(Main.MouseWorld - playerCenter);
                    if (holdoutOffset.X != Projectile.velocity.X || holdoutOffset.Y != Projectile.velocity.Y)
                    {
                        Projectile.netUpdate = true;
                    }
                    ShotNumber++;

                    
                    
                    bool ammoConsumed = plr.PickAmmo(item, out int projToShoot, out float speed, out int damage, out float knockBack, out int useAmmoItemID);
                    Vector2 newVelocity = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(7));
                    newVelocity *= 1f - Main.rand.NextFloat(0.15f);
                    if (ammoConsumed)
                    {
                        if (ShotNumber == 1)
                        {
                            Projectile.NewProjectileDirect(plr.GetSource_ItemUse_WithPotentialAmmo(item, useAmmoItemID), Vector2.Normalize(Projectile.velocity), newVelocity, projToShoot, damage, knockBack, plr.whoAmI);
                        }
                        if (ShotNumber == 2)
                        {
                            for (int i = 0; i < 2; i++)
                            {
                                //newVelocity = newVelocity.RotatedByRandom(MathHelper.ToRadians(1));
                                newVelocity *= 1f - Main.rand.NextFloat(0.15f);
                                Projectile.NewProjectileDirect(plr.GetSource_ItemUse_WithPotentialAmmo(item, useAmmoItemID), Vector2.Normalize(Projectile.velocity), newVelocity, projToShoot, damage, knockBack, plr.whoAmI);
                            }
                        }
                        if (ShotNumber == 3)
                        {
                            float projCount = Main.rand.Next(1, 4);
                            float tenthpi = .314159265358979f;
                            Vector2 Offset1 = new Vector2(newVelocity.X, newVelocity.Y);
                            Offset1.Normalize();
                            Offset1 *= 8f;
                            for (int i = 0; i < projCount; i++)
                            {
                                float num = i - (projCount - 1f) / 2f;
                                Vector2 Offset2 = Offset1.RotatedBy((double)(tenthpi * num));
                                Projectile.NewProjectile(plr.GetSource_ItemUse_WithPotentialAmmo(item, useAmmoItemID), Vector2.Normalize(Projectile.velocity) + Offset2, newVelocity, projToShoot, damage, knockBack, plr.whoAmI);
                            }
                            //Projectile.NewProjectile(source, position.RotatedBy(MathHelper.ToRadians(-1)), velocity, type, damage, knockback, player.whoAmI);
                            ShotNumber = 0;
                        }
                    }
                    else Projectile.Kill();
                }
            }
            Projectile.direction = Projectile.velocity.X < 0 ? -1 : 1;
			Projectile.spriteDirection = Projectile.direction;
			plr.ChangeDir(Projectile.direction);
			plr.heldProj = Projectile.whoAmI;
			plr.SetDummyItemTime(2);
			Projectile.Center = playerCenter;
			float rotationOffset = Projectile.spriteDirection == -1 ? MathHelper.Pi : 0;
			Projectile.rotation = Projectile.velocity.ToRotation() + rotationOffset;
			plr.itemRotation = (Projectile.velocity * Projectile.direction).ToRotation();
			Projectile.timeLeft = 2;
        }
    }
}