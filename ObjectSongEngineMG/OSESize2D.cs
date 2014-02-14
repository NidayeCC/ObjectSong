using System;
using Microsoft.Xna.Framework;

namespace ObjectSongEngine
{
    /// <summary>
    /// Represents a size in 2D space as two 32-bit Integers, Width and Height
    /// </summary>
    public class OSESize2D
    {
        private Int32 _width;
        private Int32 _height;

        public Int32 Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }

        public Int32 Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }


        public OSESize2D()
        {
            _width = 0;
            _height = 0;
        }


        public OSESize2D(Vector2 sizeVector2)
        {
            _width = Convert.ToInt32(sizeVector2.X);
            _height = Convert.ToInt32(sizeVector2.Y);
        }

        public OSESize2D(Int32 itemWidth, Int32 itemHeight)
        {
            _width = itemWidth;
            _height = itemHeight;
        }


        public void Update(Int32 itemWidth, Int32 itemHeight)
        {
            _width = itemWidth;
            _height = itemHeight;
        }


        public void Update(Vector2 sizeVector2)
        {
            _width = Convert.ToInt32(sizeVector2.X);
            _height = Convert.ToInt32(sizeVector2.Y);
        }


        public void Initialize()
        {
            _width = 0;
            _height = 0;
        }
    }
}
