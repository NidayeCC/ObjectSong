using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ObjectSongEngineMG
{
    /// <summary>
    /// Represents a location in 2D space as two 32-bit Integers, X and Y
    /// </summary>
    public class OSELocation2D
    {
        private Int32 _xloc;
        private Int32 _yloc;

        public Int32 X
        {
            get
            {
                return _xloc;
            }
            set
            {
                _xloc = value;
            }
        }


        public Int32 Y
        {
            get
            {
                return _yloc;
            }
            set
            {
                _yloc = value;
            }
        }


        public Vector2 ToVector2
        {
            get
            {
                return new Vector2(_xloc, _yloc);
            }
        }


        public OSELocation2D()
        {
            _xloc = 0;
            _yloc = 0;
        }


        public OSELocation2D(Int32 xLoc, Int32 yLoc)
        {
            _xloc = xLoc;
            _yloc = yLoc;
        }


        public OSELocation2D(MouseState mouseState)
        {
            _xloc = mouseState.X;
            _yloc = mouseState.Y;
        }


        public OSELocation2D(OSELocation2D location)
        {
            _xloc = location.X;
            _yloc = location.Y;
        }


        public OSELocation2D(Vector2 location)
        {
            _xloc = Convert.ToInt32(location.X);
            _yloc = Convert.ToInt32(location.Y);
        }


        public void Update(MouseState mouseState)
        {
            _xloc = mouseState.X;
            _yloc = mouseState.Y;
        }


        public void Update(Int32 xLoc, Int32 yLoc)
        {
            _xloc = xLoc;
            _yloc = yLoc;
        }


        public void Update(Vector2 locationVector2)
        {
            _xloc = Convert.ToInt32(locationVector2.X);
            _yloc = Convert.ToInt32(locationVector2.Y);
        }
    }
}
