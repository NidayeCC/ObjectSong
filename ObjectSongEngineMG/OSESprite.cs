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
        private OSELocation2D _location;
        private Boolean _visible;
        private OSEHitBox _hitbox;

        protected Texture2D Texture;


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
            }
        }


        public OSESprite(OSESize2D size, OSELocation2D location)
        {
            _id = Guid.NewGuid();
            _size = new OSESize2D(size);
            _location = new OSELocation2D(location);       
            _visible = true;
        }


        public void LoadTexture(GraphicsDevice device, ContentManager contentMgr, string filename)
        {
            Texture = new Texture2D(device, Size.Width, Size.Height);
            Texture = contentMgr.Load<Texture2D>(filename);
        }


        public void CreateHitBox(GraphicsDevice device)
        {
            _hitbox = new OSEHitBox(_location, _size, device);
        }


        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_visible)
            {
                spriteBatch.Draw(Texture, new Rectangle(_location.X, _location.Y, _size.Width, _size.Height),
                    new Color(255, 255, 255));
                if(_hitbox != null)
                    DrawHitBox(spriteBatch);
            }
        }


        //Used to debug collisions
        //Set Hitbox.Visible to TRUE to use this method, otherwise it will not draw
        public void DrawHitBox(SpriteBatch spriteBatch)
        {
                _hitbox.Draw(spriteBatch, _location);
        }


        // Set Hitbox.Enabled to TRUE to use this method.
        public bool CheckForHit(OSESprite target)
        {
            if (_hitbox.Enabled)
            {
                var hitrect = new Rectangle(_location.X + _hitbox.Offset.X, _location.Y + _hitbox.Offset.Y, _hitbox.Size.Width, _hitbox.Size.Height);
                var targetrect = new Rectangle(target.Location.X + target.Hitbox.Offset.X, target.Location.Y + target.Hitbox.Offset.Y, target.Hitbox.Size.Width, target.Hitbox.Size.Height);
                return targetrect.Intersects(hitrect);
            }
            else
                return false;
        }


    }
}
