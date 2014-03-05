using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace ObjectSongEngineMG
{
    public class OSEMap
    {
        public List<OSEPlayObject> Items;


        public OSEMap()
        {
            Items = new List<OSEPlayObject>();
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var item in Items)
            {
                item.Draw(spriteBatch);
            }
        }
    }
}
