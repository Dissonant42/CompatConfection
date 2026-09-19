using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CompatConfection.Common
{
    public class CursorPlayer : ModPlayer
    {
        public float mouseX;
        public float mouseY;

        public Vector2 mouseloc;

        public override void PreUpdate()
        {
            mouseX = Main.MouseWorld.X;
            mouseY = Main.MouseWorld.Y;
            mouseloc = new Vector2(mouseX, mouseY);
        }
    }
}