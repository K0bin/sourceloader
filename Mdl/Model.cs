using System;
using System.Collections.Generic;
using System.Text;

namespace Source.Mdl
{
    public class Model
    {
        public List<Mesh> Meshes
        {
            get; private set;
        } = new List<Mesh>();
    }
}
