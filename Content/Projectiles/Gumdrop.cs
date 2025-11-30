using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CompatConfection.Content.Projectiles
{
    public class Gumdrop : ModProjectile
    {
        public override string Texture => "CompatConfection/Content/Projectiles/gumdropplaceholder";

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 6;
        }

        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            
            Projectile.frameCounter = 0;
            Projectile.DamageType = DamageClass.Magic;

            Projectile.friendly = true;
            Projectile.localNPCHitCooldown = 10;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
        }
        public override void AI()
        {
            Projectile.frame = (int)Projectile.ai[2];
            Vector2 Accel = new Vector2((float)Math.Cos(Projectile.rotation - MathHelper.PiOver2), (float)Math.Sin(Projectile.rotation - MathHelper.PiOver2)) * 0.2f;

            if (Projectile.localAI[0] == 0)
            {
                Projectile.velocity = new Vector2((float)Math.Cos(Projectile.rotation - MathHelper.PiOver2), (float)Math.Sin(Projectile.rotation - MathHelper.PiOver2)) * 5;
            }
            if (Projectile.localAI[0] <= 10 && Projectile.alpha > 25)
            {
                Projectile.alpha -=25;
            }
            if (Projectile.ai[2] < 3)
            {
                if (Projectile.localAI[0] < 25 || Projectile.localAI[0] > 31)
                {
                    Projectile.velocity -=Accel;
                }
            }
            else Projectile.velocity *= 0.97f;
            if (Projectile.localAI[0] == 30)
            {
                Projectile.ai[2] = 1;
                Projectile.velocity = -18 * Accel;
            }
            if (Projectile.localAI[0] == 40)
            {
                Projectile.ai[2] = 2;
            }
            if (Projectile.localAI[0] == 89)
            {
                Projectile.velocity *= 0.1f;
                Projectile.ai[2] = 3;
                SoundEngine.PlaySound(in SoundID.NPCHit1, Projectile.position);
            }
            if (Projectile.localAI[0] == 95)
            {
                Projectile.ai[2] = 3;
            }
            if (Projectile.localAI[0] == 100)
            {
                Projectile.ai[2] = 4;
            }
            if (Projectile.localAI[0] > 100)
            {
                Projectile.alpha += 25;
            }
            if (Projectile.localAI[0] == 110)
            {
                Projectile.Kill();
            }
            Projectile.localAI[0]++;
        }
        public override void PostDraw(Color lightColor)
        {
        }
    }
}