using System;
using System.Collections.Generic;
using System.Text;

namespace Source.Vtf
{
    public struct Face
    {
        public Slice[] Slices;
        public override string ToString()
        {
            return $"Face with {Slices.Length} Slices";
        }
    }
}
