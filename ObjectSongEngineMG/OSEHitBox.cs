using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ObjectSongEngineMG
{
    public class OSEHitBox
    {

        private OSESize2D _size;
        private OSELocation2D _offset;
        private Texture2D _pixeltex;
        private Color _pixelcolor;
        private bool _enabled;
        private bool _visible;


        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;
            }
        }


        public bool Visible
        {
            get
            {
                return _visible;
            }
            set
            {
                _visible = value;
            }
        }


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


        public OSELocation2D Offset
        {
            get
            {
                return _offset;
            }
            set
            {
                _offset = value;
            }
        }


        public OSEHitBox(OSELocation2D offset, OSESize2D size, GraphicsDevice graphics)
        {
            _offset = new OSELocation2D(offset);
            _size = new OSESize2D(size.Width, size.Height);
            Initialize(graphics);
            _enabled = true;
            _visible = false;
        }


        public void Initialize(GraphicsDevice device)
        {
            _pixeltex = new Texture2D(device, 1, 1, false, SurfaceFormat.Color);
            _pixeltex.SetData(new[] { Color.White });
            _pixelcolor = new Color(255, 255, 255, 255);
        }


        public virtual void Draw(SpriteBatch spriteBatch, OSELocation2D location)
        {
            if (_visible)
            {
                // Draw top line
                spriteBatch.Draw(_pixeltex, new Rectangle(location.X + _offset.X, location.Y + _offset.Y, _size.Width, 1), _pixelcolor);

                // Draw left line
                spriteBatch.Draw(_pixeltex, new Rectangle(location.X + _offset.X, location.Y + _offset.Y, 1, _size.Height), _pixelcolor);

                // Draw right line
                spriteBatch.Draw(_pixeltex, new Rectangle((location.X + _offset.X + _size.Width - 1), location.Y + _offset.Y, 1, _size.Height), _pixelcolor);

                // Draw bottom line
                spriteBatch.Draw(_pixeltex, new Rectangle(location.X + _offset.X, location.Y + _offset.Y + _size.Height - 1, _size.Width, 1), _pixelcolor);
            }
        }
    }
}
