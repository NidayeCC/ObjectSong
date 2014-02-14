using Microsoft.Xna.Framework;
using ObjectSongEngine;

namespace ApocaLand
{
    public class Tile : OSESprite
    {
        public Tile(Game game, int width, int height)
            : base(game, new OSESize2D(width, height), new OSELocation2D(0,0))
        {

        }
    }
}
