using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ObjectSongEngineMG
{
    /// <summary>
    /// Represents a single level drawable menu component
    /// </summary>
    public class OSEMenu
    {
        private readonly Color _normalText;
        private readonly Color _highlightText;

        private readonly SpriteFont _spriteFont;
        private OSELocation2D _location2D;
        private readonly OSESize2D _margin;
        private readonly OSEMenuOrientation _orientation;
        private readonly List<OSEMenuItem> _items;

        private OSEMenuItem _selecteditem;


        public OSEMenuItem SelectedItem
        {
            get
            {
                return _selecteditem;
            }
        }


        public OSELocation2D Location
        {
            get
            {
                return _location2D;
            }
            set
            {
                _location2D = value;
            }
        }


        public OSEMenu(SpriteFont menuFont) 
        {
            _items = new List<OSEMenuItem>();
            _spriteFont = menuFont;
            _location2D = new OSELocation2D(0, 0);
            _margin = new OSESize2D(5, 1);
            _normalText = new Color(255, 255, 255, 255);
            _highlightText = new Color(255, 0, 0, 255);
            _orientation = OSEMenuOrientation.Vertical;
            _selecteditem = null;
        }


        public void EnableHitBox(GraphicsDevice device)
        {
            foreach (OSEMenuItem item in _items)
            {
                item.EnableHitBox(device);
            }
        }


        public void AddItem(String text, String action, Int32 index)
        {
            var item = new OSEMenuItem(text, action, index, _spriteFont)
            {
                NormalColor = _normalText,
                HighlightColor = _highlightText     
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
