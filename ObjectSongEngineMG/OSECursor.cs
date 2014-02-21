using Microsoft.Xna.Framework.Graphics;

namespace ObjectSongEngineMG
{
    public class OSECursor : OSESprite
    {

        public OSECursor(OSESize2D size, OSELocation2D location)
            : base(size, location)
        {

        }

        public new void EnableHitBox(GraphicsDevice device)
        {
            base.EnableHitBox(device);
        }
    }
}
