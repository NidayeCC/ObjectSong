using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ObjectSongEngineMG
{
    public class OSEHitBox
    {

        private OSESize2D _size;
        private OSELocation2D _location;
        private OSELocation2D _origin;
        private OSELocation2D _offset;
        private OSESpriteOrientation _orientation;
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


        public OSESpriteOrientation Orientation
        {
            get
            {
                return _orientation;
            }
            set
            {
                _orientation = value;
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


        public OSELocation2D Origin
        {
            get
            {
                return _origin;
            }
            set
            {
                _origin = value;
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


        public OSEHitBox(OSELocation2D location, OSESize2D size, GraphicsDevice graphics)
        {
            _location = new OSELocation2D(location);
            _origin = new OSELocation2D(0,0);
            _offset = new OSELocation2D(0,0);
            _orientation = OSESpriteOrientation.Right;
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


        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!_visible) return;

            var finalx = _location.X + _offset.X - _origin.X;
            var finaly = _location.Y + _offset.Y - _origin.Y;

            // Draw top line
            spriteBatch.Draw(_pixeltex, new Rectangle(finalx, finaly, _size.Width, 1), _pixelcolor);

            // Draw left line
            spriteBatch.Draw(_pixeltex, new Rectangle(finalx, finaly, 1, _size.Height), _pixelcolor);

            // Draw right line
            spriteBatch.Draw(_pixeltex, new Rectangle(finalx + _size.Width - 1, finaly, 1, _size.Height), _pixelcolor);

            // Draw bottom line
            spriteBatch.Draw(_pixeltex, new Rectangle(finalx, finaly + _size.Height - 1, _size.Width, 1), _pixelcolor);
        }


        public bool DetectHit(OSEHitBox targetHitBox)
        {
            if (_enabled)
            {
                var finalx = _location.X + _offset.X - _origin.X;
                var finaly = _location.Y + _offset.Y - _origin.Y;

                var tfinalx = targetHitBox.Location.X + targetHitBox.Offset.X - targetHitBox.Origin.X;
                var tfinaly = targetHitBox.Location.Y + targetHitBox.Offset.Y - targetHitBox.Origin.Y;

                var hitrect = new Rectangle(finalx, finaly, _size.Width, _size.Height);
                var targetrect = new Rectangle(tfinalx, tfinaly, targetHitBox.Size.Width, targetHitBox.Size.Height);

                return targetrect.Intersects(hitrect);
            }
            else
                return false;
        }
    }
}
