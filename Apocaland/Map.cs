using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApocaLand
{
    public class OrthoMap2D
    {
        private MapPiece[,] pieces;

        public OrthoMap2D(Int32 XSize, Int32 YSize)
        {
            pieces = new MapPiece[XSize, YSize];    
        }

    }

}
