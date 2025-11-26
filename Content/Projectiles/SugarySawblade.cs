using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CompatConfection.Content.Projectiles
{
    public class SugarySawblade : ModProjectile
    {
        public override string Texture => "CompatConfection/Content/Items/TempItem";

        public override void SetDefaults()
        {
            if (ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod) == true)
            {
                Projectile.CloneDefaults(thoriumMod.Find<ModProjectile>("SawbladeLight").Type);
            }
            Projectile.idStaticNPCHitCooldown = 20;
            Projectile.usesIDStaticNPCImmunity = true;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }
            if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!target.friendly)
            {
                target.AddBuff(ModLoader.GetMod("TheConfectionRebirth").Find<ModBuff>("SacchariteAmmoInjection").Type, 240);
                float victim = target.whoAmI;
                Projectile Shard = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), target.Center - Projectile.velocity * 1f, (target.Center - Projectile.Center) * 0.75f, ModLoader.GetMod("TheConfectionRebirth").Find<ModProjectile>("SacchariteBullet").Type, 1, 0, Projectile.owner, 15, -1, 0);
                Shard.ai[1] = victim;
            }
        }
    }
}