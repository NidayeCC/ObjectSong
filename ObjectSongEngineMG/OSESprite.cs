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
            get;
            set;
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
            Hitbox = new OSEHitBox(_location, _size, device);
            Hitbox.Enabled = true;
        }


         public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_visible)
            {
                spriteBatch.Draw(Texture, new Rectangle(_location.X, _location.Y, _size.Width, _size.Height),
                    new Color(255, 255, 255));
                DrawHitBox(spriteBatch);
            }
        }

        //Used to debug collisions
        //Set Hitbox.Enabled to TRUE to use this method
        public void DrawHitBox(SpriteBatch spriteBatch)
        {
            if (Hitbox.Enabled)
            {
                Hitbox.Draw(spriteBatch, _location);
            }
        }


        public bool CheckForHit(OSESprite target)
        {
            var hitrect = new Rectangle(_location.X + Hitbox.Offset.X, _location.Y + Hitbox.Offset.Y, Hitbox.Size.Width, Hitbox.Size.Height);
            var targetrect = new Rectangle(target.Location.X + target.Hitbox.Offset.X, target.Location.Y + target.Hitbox.Offset.Y, target.Hitbox.Size.Width, target.Hitbox.Size.Height);
            return targetrect.Intersects(hitrect);
        }


    }
}
