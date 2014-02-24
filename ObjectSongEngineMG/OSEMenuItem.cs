using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace ObjectSongEngineMG
{
    public class OSEMenuItem : OSESpriteText
    {
        private Color _normalcolor;

        private Color _highlightcolor;

        private bool _selected;

         
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
                    TextColor = _normalcolor;
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
                    TextColor = _highlightcolor;
            }
        }


        public OSEMenuItem(String itemText, String itemAction, Int32 itemOrder, SpriteFont font)
            : base(itemText, font)
        {
            if (!String.IsNullOrEmpty(itemText))
            {
                 Action = itemAction;
                Order = itemOrder;
                _selected = false;
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
            TextColor = _selected ? _highlightcolor : _normalcolor;
        }

    }
}
