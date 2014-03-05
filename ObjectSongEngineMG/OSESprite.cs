using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ObjectSongEngineMG
{
    public class OSESprite
    {
        private Guid _id;
        private OSESize2D _size;
        private Boolean _visible;
        private OSEHitBox _hitbox;
        private OSESpriteOrientation _orientation;
        private OSELocation2D _origin;
        
        protected Texture2D Texture;
        protected OSELocation2D _location;
        protected OSELocation2D _oldlocation;


        public Guid ID
        {
            get
            {
                return _id;
            }
        }


        public Boolean Visible
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


        public OSEHitBox Hitbox
        {
            get
            {
                return _hitbox;
            }
            set
            {
                _hitbox = value;
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
                if (_hitbox != null)
                    _hitbox.Location = value;
            }
        }


        public OSELocation2D OldLocation
        {
            get
            {
                return _oldlocation;
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
                if (_hitbox != null)
                    _hitbox.Orientation = value;
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
                if (_hitbox != null)
                    _hitbox.Origin = value;
            }
        }


        public OSESprite(OSESize2D size, OSELocation2D location)
        {
            _id = Guid.NewGuid();
            _size = new OSESize2D(size);
            _location = new OSELocation2D(location);
            _oldlocation = new OSELocation2D(_location);
            _visible = true;
            _orientation = OSESpriteOrientation.Right;
            _origin = new OSELocation2D(0,0);
        }



        public void LoadTexture(GraphicsDevice device, ContentManager contentMgr, string filename)
        {
            Texture = contentMgr.Load<Texture2D>(filename);
        }


        public void CreateHitBox(GraphicsDevice device)
        {
            _hitbox = new OSEHitBox(_location, _size, device);
            _hitbox.Orientation = _orientation;
            _hitbox.Offset = new OSELocation2D(0,0);
            _hitbox.Origin = _origin;
        }


        public virtual void Update()
        {
            _hitbox.Location = _location;
        }


        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!_visible) return;

            var orientationvector = 0.0f;

            switch (_orientation)
            {
                case OSESpriteOrientation.Up:
                    orientationvector = -1.57f;
                    break;
                case OSESpriteOrientation.Down:
                    orientationvector = 1.57f;
                    break;
                case OSESpriteOrientation.Left:
                    orientationvector = 3.14f;
                    break;
            }

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.LinearWrap,
            DepthStencilState.Default, RasterizerState.CullNone);

            //spriteBatch.Draw(Texture, new Rectangle(_location.X, _location.Y, _size.Width, _size.Height),null,
            //    new Color(255, 255, 255),
            //    orientationvector, _origin.ToVector2, SpriteEffects.None, 0.0f);

            spriteBatch.Draw(Texture, _location.ToVector2, new Rectangle(0,0, _size.Width, _size.Height),
                new Color(255, 255, 255), orientationvector, _origin.ToVector2, 1, SpriteEffects.None, 0);

            spriteBatch.End();

            if (_hitbox != null)
                _hitbox.Draw(spriteBatch);
        }



        // Set Hitbox.Enabled to TRUE to use this method.
        public bool CheckForHit(OSESprite target)
        {
            if (_hitbox.Enabled && target.Hitbox.Enabled)
            {
                return _hitbox.DetectHit(target.Hitbox);
            }
            else
                return false;
        }

    }
}
