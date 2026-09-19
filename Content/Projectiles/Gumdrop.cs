using System;
using CompatConfection.Common;
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

            Projectile.extraUpdates = 1;

            DrawOffsetX = -12;
            DrawOriginOffsetX = 00;
            DrawOriginOffsetY = 0;
        }
        public override void AI()
        {
            Projectile.frame = (int)Projectile.ai[2];
            Vector2 Accel = new Vector2((float)Math.Cos(Projectile.rotation - MathHelper.PiOver2), (float)Math.Sin(Projectile.rotation - MathHelper.PiOver2)) * 0.2f;
            if (Projectile.ai[1] == 1)
            {
                if (Projectile.ai[0] == 0)
                {
                    if (!(Projectile.velocity == Vector2.Zero))
                    {
                        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
                    }
                    Projectile.velocity = new Vector2((float)Math.Cos(Projectile.rotation - MathHelper.PiOver2), (float)Math.Sin(Projectile.rotation - MathHelper.PiOver2)) * 5;
                }
                if (Projectile.ai[0] <= 10 && Projectile.alpha > 25)
                {
                    Projectile.alpha -=25;
                }
                if (Projectile.ai[2] < 3)
                {
                    if (Projectile.ai[0] < 25 || Projectile.ai[0] > 31)
                    {
                        Projectile.velocity -=Accel;
                    }
                }
                else Projectile.velocity *= 0.95f;
                if (Projectile.ai[0] == 30)
                {
                    Projectile.ai[2] = 1;
                    Projectile.velocity = -18 * Accel;
                }
                if (Projectile.ai[0] == 40)
                {
                    Projectile.ai[2] = 2;
                }
                if (Projectile.ai[0] == 89)
                {
                    Projectile.velocity *= 0.1f;
                    Projectile.ai[2] = 3;
                    SoundEngine.PlaySound(in SoundID.NPCHit1, Projectile.position);

                    if (Projectile.localAI[0] == 1)
                    {
                        Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), new Vector2(Projectile.localAI[1], Projectile.localAI[2]), Vector2.Zero, ModContent.ProjectileType<Gumdrop>(), (int)(1.33333 * Projectile.damage), Projectile.knockBack, Projectile.owner, Projectile.rotation, 2, 0);
                    }
                }
                if (Projectile.ai[0] == 95)
                {
                    Projectile.ai[2] = 3;
                    Projectile.velocity *= 0;
                    Projectile.damage = 0;
                }
                if (Projectile.ai[0] == 100)
                {
                    Projectile.ai[2] = 4;
                }
                if (Projectile.ai[0] > 100)
                {
                    Projectile.alpha += 25;
                }
                if (Projectile.ai[0] == 110)
                {
                    Projectile.Kill();
                }
                Projectile.ai[0]++;
            }
            else if (Projectile.ai[1] == 2)
            {
                Projectile.damage = (int)(Projectile.damage * 0.97);

                if ( Projectile.ai[2] >= 24)
                {
                    Projectile.Kill();
                }
                Projectile.ai[2]++;
            }
            else if (Projectile.ai[1] == 0)
            {
                float SpawnAngle = MathHelper.ToRadians(Main.rand.Next(0, 180));
                Vector2 MouseLoc = Main.player[Projectile.owner].GetModPlayer<CursorPlayer>().mouseloc;
                float SpawnDistance = 500f;

                Vector2 Spawn1 = MouseLoc + new Vector2(MathF.Cos(SpawnAngle) * SpawnDistance, MathF.Sin(SpawnAngle) * SpawnDistance);
                Vector2 Spawn2 = MouseLoc + new Vector2(MathF.Cos(SpawnAngle) * -SpawnDistance, MathF.Sin(SpawnAngle) * -SpawnDistance);

                Projectile Gumdrop1 = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Spawn1, Vector2.Zero, ModContent.ProjectileType<Gumdrop>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 1, 0);
                Projectile Gumdrop2 = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Spawn2, Vector2.Zero, ModContent.ProjectileType<Gumdrop>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 1, 0);
                Gumdrop1.rotation = SpawnAngle + MathHelper.PiOver2;
                Gumdrop2.rotation = SpawnAngle - MathHelper.PiOver2;
                
                Gumdrop1.localAI[0] = 1;
                Gumdrop1.localAI[1] = MouseLoc.X;
                Gumdrop1.localAI[2] = MouseLoc.Y;

                Gumdrop2.localAI[0] = 0;

                Gumdrop1.netUpdate = true;
                Gumdrop2.netUpdate = true;

                Projectile.active = false;
            }
        }
        public override void PostDraw(Color lightColor)
        {
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.ai[1] == 2)
            {
                Projectile.damage = (int)(Projectile.damage * 0.85);
            }
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float ShockwaveSize = 30f * Projectile.scale + 12f * Projectile.ai[2];


            if (targetHitbox.IntersectsConeFastInaccurate(Projectile.Center, ShockwaveSize, 0, MathHelper.Pi) && Projectile.ai[1] == 2)
            {
                return true;
            }

            return base.Colliding(projHitbox, targetHitbox);
        }
    }
}