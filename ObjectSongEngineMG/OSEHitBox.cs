using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ObjectSongEngine
{
    public class OSEHitBox
    {

        private OSESize2D _size;
        private OSELocation2D _location;
        private Rectangle _hitrect;
        private readonly Texture2D _pixeltex;
        private readonly Color _pixelcolor;
        


        public OSESize2D Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
            }
        }


        public OSELocation2D Location
        {
            get
            {
                return _location;
            }
            set
            {
                _location = value;
            }
        }


        public OSEHitBox(Game game, OSELocation2D location, OSESize2D size)
        {
            _location = location;
            _size = size;

            _pixeltex = new Texture2D(game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            _pixeltex.SetData(new[] { Color.White });

            _pixelcolor = new Color(255,255,255,255);
        }


        public bool CheckForHit(OSEHitBox target, OSELocation2D myLocation)
        {
            _hitrect = new Rectangle(myLocation.X + _location.X, myLocation.Y + _location.Y, _size.Width, _size.Height);
            var targetrect = new Rectangle(target._location.X, target._location.Y, target._size.Width, target._size.Height);
            return targetrect.Intersects(_hitrect);
        }


        public virtual void Draw(SpriteBatch spriteBatch, OSELocation2D location)
        {
            // Draw top line
            spriteBatch.Draw(_pixeltex, new Rectangle(location.X + _location.X, location.Y + _location.Y, _size.Width, 1), _pixelcolor);

            // Draw left line
            spriteBatch.Draw(_pixeltex, new Rectangle(location.X + _location.X, location.Y + _location.Y, 1, _size.Height), _pixelcolor);

            // Draw right line
            spriteBatch.Draw(_pixeltex, new Rectangle((location.X + _location.X + _size.Width - 1),location.Y + _location.Y, 1, _size.Height), _pixelcolor);

            // Draw bottom line
            spriteBatch.Draw(_pixeltex, new Rectangle(location.X + _location.X, location.Y + _location.Y + _size.Height - 1, _size.Width, 1), _pixelcolor);
  
        }
    }
}
