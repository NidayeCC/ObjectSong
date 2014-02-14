using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ObjectSongEngine
{
    /// <summary>
    /// Represents a single level drawable menu component
    /// </summary>
    public class OSEMenu
    {
        //private Int32 _selectedindex;
        
        private readonly Color _normalText;
        private readonly Color _highlightText;

        //private KeyboardState _oldkeyboardstate;
        //private KeyboardState _newkeyboardstate;

        private readonly SpriteFont _spriteFont;
        private readonly OSELocation2D _location2D;
        private readonly OSESize2D _margin;
        private readonly OSEMenuOrientation _orientation;
        private readonly List<OSEMenuItem> _items;
        private readonly Game _xnagame;

        private OSEMenuItem _selecteditem;


        public OSEMenuItem SelectedItem
        {
            get
            {
                return _selecteditem;
            }
        }



        public OSEMenu(Game game, SpriteFont menuFont) 
        {
            _items = new List<OSEMenuItem>();
            _spriteFont = menuFont;
            _location2D = new OSELocation2D(0, 0);
            _margin = new OSESize2D(5, 1);
            _normalText = new Color(255, 255, 255, 255);
            _highlightText = new Color(255, 0, 0, 255);
            _orientation = OSEMenuOrientation.Vertical;
            _xnagame = game;
            _selecteditem = null;
        }



        public void AddItem(String text, String action, Int32 index)
        {
            var size = _spriteFont.MeasureString(text);

            var item = new OSEMenuItem(_xnagame, text, action, index, _spriteFont)
            {
                Size = new OSESize2D(size),
                NormalColor = _normalText,
                HighlightColor = _highlightText,
                DrawHitBox = true
            };    
           
            _items.Add(item);

        }


        public void Update(OSECursor cursor)
        {
            
            
            var first = true;

            foreach (var item in _items)
            {
                var finallocation = new OSELocation2D(_location2D);

                if (!first)
                {
                    if (_orientation == OSEMenuOrientation.Horizontal)
                        finallocation.X += _margin.Width;
                    if (_orientation == OSEMenuOrientation.Vertical)
                        finallocation.Y += _margin.Height;

                    if (_orientation == OSEMenuOrientation.Vertical)
                        finallocation.Y += item.Size.Height;
                    if (_orientation == OSEMenuOrientation.Horizontal)
                        finallocation.X += item.Size.Width;
                    
                }
                first = false;
                item.Location = finallocation;
                item.Update(cursor);
            }

            CheckForHits(cursor);
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            

            foreach (var item in _items)
            {
                item.Draw(spriteBatch, _spriteFont);
            }
     
        }


        public bool CheckForHits(OSECursor cursor)
        {
            foreach (var item in _items)
            {
                if (item.CursorOver(cursor))
                {
                    _selecteditem = item;
                    return true;
                }
            }
            _selecteditem = null;

            return false;
        }
    }
}
