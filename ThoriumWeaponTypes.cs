using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace CompatConfection
{
    public class WeaponTypes : ModSystem
    {
        static bool thoriumEnabled;
        public override void PostSetupContent()
        {

            

        }
        public override void SetStaticDefaults()
        {
            ItemID.Sets.Spears[ModLoader.GetMod("TheConfectionRebirth").Find<ModItem>("SpearofCavendes").Type] = true;
            ItemID.Sets.Spears[ModLoader.GetMod("TheConfectionRebirth").Find<ModItem>("CookieCrumbler").Type] = true;
            thoriumEnabled = ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod);
            if (thoriumEnabled)
            {
                thoriumMod.Call("AddFlailProjectileID", ModLoader.GetMod("TheConfectionRebirth").Find<ModProjectile>("CreamofKickin").Type);
            }
        }
    }
}