using CompatConfection.Common;
using CompatConfection.Content.Items.Weapons.Thorium;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CompatConfection.Content.Projectiles
{
    public class JawCrusherHoldout : ModProjectile
    {

        public override string Texture => "CompatConfection/Content/Projectiles/gumdropplaceholder";


        public ref float HoldTimer => ref Projectile.ai[1];
        public ref float Charge => ref Projectile.ai[2];

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 1;
        }
        public override void SetDefaults()
        {
			//Projectile.width = 22;
			//Projectile.height = 22;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.hide = true;
			//Projectile.usesOwnerLight = true;
			//Projectile.drawLayer = ProjectileDrawLayerID.HeldProj; // Draws over the player's body and under the player's hands
			Projectile.DamageType = DamageClass.Magic;
			Projectile.ignoreWater = true;

			DrawOffsetX = -17;//
			DrawOriginOffsetY = -4;//
        }

		public override bool? CanDamage() => false;

        public override void AI() {
            Player player = Main.player[Projectile.owner];
            Vector2 pCenter = player.RotatedRelativePoint(player.MountedCenter);
            int chargeTime = player.HeldItem.useTime;

            if (Charge < 4)
            {
                HoldTimer ++;
            }

            if (HoldTimer >= chargeTime)
            {
                HoldTimer = 0;
                Charge ++;
                SoundEngine.PlaySound(SoundID.DD2_FlameburstTowerShot, Projectile.position);

                for (int i = 0; i < 360; i += 2)
                {
                    var dust = Dust.NewDustDirect(Projectile.position, 8, 8, DustID.WhiteTorch, 5, 0);
                    dust.velocity = dust.velocity.RotatedBy(MathHelper.ToRadians(i));
                }
            }


            if (player.channel && !player.noItems && !player.CCed)
            {
                
                float holdoutDistance = JawCrusher.HoldoutDistance * Projectile.scale;
                Vector2 holdoutOffset = holdoutDistance * Vector2.Normalize(player.GetModPlayer<CursorPlayer>().mouseloc - pCenter);
                if (holdoutOffset.X != Projectile.velocity.X || holdoutOffset.Y != Projectile.velocity.Y)
                {
                    Projectile.netUpdate = true;
                }
            }
            else
            {
                Projectile.Kill();
                //#ADD GOBSTOPPER SPAWN CODE HERE
            }

            Projectile.direction = Projectile.velocity.X < 0 ? -1 : 1;
            Projectile.spriteDirection = Projectile.direction;
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.SetDummyItemTime(2);
            Projectile.Center = player.Center;
            float rotationOffset = Projectile.spriteDirection == -1 ? MathHelper.Pi : 0;
            Projectile.rotation = Projectile.velocity.ToRotation() + rotationOffset;
            player.itemRotation = (Projectile.velocity * Projectile.direction).ToRotation();
            Projectile.timeLeft = 2;

        }

    }
}