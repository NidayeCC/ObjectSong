using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace ObjectSongEngine
{
    public class OSEMenuItem : OSESprite
    {
        private readonly Guid _id;

        private Color _normalcolor;

        private Color _highlightcolor;

        private Color _currentcolor;

        private bool _selected;


        public Guid ID
        {
            get
            {
                return _id;
            }
        }


        public String Text
        {
            get;
            set;
        }


        public String Action
        {
            get;
            set;
        }


        public Int32 Order
        {
            get;
            set;
        }


        public Color NormalColor
        {
            get
            {
                return _normalcolor;
            }
            set
            {
                _normalcolor = value;
                if(!_selected)
                    _currentcolor = _normalcolor;
            }
        }


        public Color HighlightColor
        {
            get
            {
                return _highlightcolor;
            }
            set
            {
                _highlightcolor = value;
                if (_selected)
                    _currentcolor = _highlightcolor;
            }
        }


        public Color CurrentColor
        {
            get
            {
                return _currentcolor;
            }
        }


        public OSEMenuItem(Game game, String itemText, String itemAction, Int32 itemOrder, SpriteFont font)
            : base(game, new OSESize2D(1,1), new OSELocation2D(0,0))
        {
            if (!String.IsNullOrEmpty(itemText))
            {
                _id = Guid.NewGuid();
                Text = itemText;
                Action = itemAction;
                Order = itemOrder;
                _selected = false;
                _normalcolor = new Color(255, 255, 255, 255);

                var size = font.MeasureString(itemText);
                Size = new OSESize2D(size);
            }
            else
            {
                throw new Exception("OSE1000 - A Menu Item must have a text value");
            }
        }


        public bool CursorOver(OSECursor cursor)
        {
            return CheckForHit(cursor);
        }


        public void Update(OSECursor cursor)
        {
            _selected = CursorOver(cursor);
            _currentcolor = _selected ? _highlightcolor : _normalcolor;
        }


        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.DrawString(font, Text, Location.ToVector2, _currentcolor);
            Hitbox.Draw(spriteBatch, Location);
        }
    }
}
