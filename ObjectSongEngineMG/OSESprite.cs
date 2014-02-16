using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ObjectSongEngineMG
{
    public class OSESprite
    {
        private OSESize2D _size;
        private OSELocation2D _location;

        protected Texture2D Texture;
              
        protected Game XNAGame;

        public OSEHitBox Hitbox
        {
            get;
            set;
        }


        public bool DrawHitBox
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
               
                Texture = new Texture2D(XNAGame.GraphicsDevice, Size.Width, Size.Height);
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


        public OSESprite(Game game, OSESize2D size, OSELocation2D location)
        {
            _size = size;
            XNAGame = game;
            _location = location;

            Hitbox = new OSEHitBox(game, new OSELocation2D(0,0), _size);
            DrawHitBox = false;
        }


        public void LoadTexture(ContentManager contentMgr, string filename)
        {
            Texture = contentMgr.Load<Texture2D>(filename);
        }

  
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle(_location.X, _location.Y, _size.Width, _size.Height), new Color(255, 255, 255));
            if (DrawHitBox)
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
