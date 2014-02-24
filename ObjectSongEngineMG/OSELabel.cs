using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ObjectSongEngineMG
{
    /// <summary>
    /// This is a simple label that you can place anywhere on a screen and update it
    /// directly by changing the text on the fly
    /// </summary>
    public class OSELabel : OSESpriteText
    {
        public OSELabel(String itemText, SpriteFont font)
            : base(itemText, font)
        {
        }
    }
}
