using Terraria.ID;
using Terraria.ModLoader;

namespace CompatConfection.Content.Items.Weapons.Thorium
{
    public class SugarScepter : ModItem
    {
        public override string Texture => "CompatConfection/Content/Items/TempItem";

        public override void SetDefaults()
        {
            Item.damage = 80;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 30;
            Item.useTime = 30;
            Item.rare = ItemRarityID.Pink;
            Item.knockBack = 6f;
            Item.value = 46000;
            Item.UseSound = SoundID.Item43;
        }
    }
}