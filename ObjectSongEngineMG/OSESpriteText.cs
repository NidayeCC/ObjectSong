using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ObjectSongEngineMG
{

    public class OSESpriteText : OSESprite
    {
        private SpriteFont _font;
        private string _text;
        private Color _textcolor;

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                ReSize(_text);
            }
        }


        public Color TextColor
        {
            get
            {
                return _textcolor;
            }
            set
            {
                _textcolor = value;
            }
        }


        public OSESpriteText(String itemText, SpriteFont font)
            : base(new OSESize2D(1, 1), new OSELocation2D(0, 0))
        {
            _textcolor = new Color(255,255,255,255);
            _font = font;
            ReSize(itemText);
            Text = itemText;
        }


        public OSESpriteText(String itemText, SpriteFont font, Color textColor)
            : base(new OSESize2D(1,1), new OSELocation2D(0,0))
        {
            _textcolor = textColor;
            _font = font;
            ReSize(itemText);
            Text = itemText;
        }


        private void ReSize(string text)
        {
            var size = _font.MeasureString(text);
            Size = new OSESize2D(size);

            if(Hitbox != null)
                Hitbox.Size = Size;
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                spriteBatch.DrawString(_font, Text, Location.ToVector2, _textcolor);
                if(Hitbox != null)
                    DrawHitBox(spriteBatch);
            }
        }
    }

}
