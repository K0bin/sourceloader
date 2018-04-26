using System;
using System.Collections.Generic;
using System.Text;

namespace Source.Vtf
{
    public struct Frame
    {
        public Face[] Faces;
        public override string ToString()
        {
            return $"Frame with {Faces.Length} Faces";
        }
    }
}
