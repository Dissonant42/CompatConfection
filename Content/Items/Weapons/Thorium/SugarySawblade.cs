using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CompatConfection;

namespace CompatConfection.Content.Items.Weapons.Thorium
{
    public class SugarySawblade : ModItem
    {
        public override string Texture => "CompatConfection/Content/Items/TempItem";

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;

        }
        public override void SetDefaults()
        {
            Item.damage = 16;
            Item.DamageType = DamageClass.Ranged;
			Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.knockBack = 3f;
            Item.value = 15;
            Item.shootSpeed = 5;
            Item.rare = ItemRarityID.Green;
            Item.shoot = ModContent.ProjectileType<Projectiles.SugarySawblade>();
            if (ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod) == true)
            {
                Item.ammo = thoriumMod.Find<ModItem>("Sawblade").Type;
            }

        }
    }
}