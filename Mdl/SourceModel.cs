using Source.Common;
using Source.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Source.Mdl
{
    public class SourceModel : Resource
    {
        public Header Header
        {
            get; private set;
        }

        public SourceModel(BinaryReader reader)
        {
            Header = reader.ReadStruct<Header>();
        }
    }
}
