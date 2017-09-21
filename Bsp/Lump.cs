/*
Distroir.BSP
Copyright (C) 2017 Distroir

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
using Bsp.LumpData;
using System.IO;

namespace Bsp
{
    public struct Lump
    {
        /// <summary>
        /// Offset into file (bytes)
        /// </summary>
        public int FileOffset;
        /// <summary>
        /// Length of lump (bytes)
        /// </summary>
        public int FileLength;
        /// <summary>
        /// Lump format version
        /// </summary>
        public int Version;
        /// <summary>
        /// Lump ident code
        /// </summary>
        public int FourCC;

        public LumpType Offset;

        public LumpData.Data Data;

        public static Lump Read(BinaryReader reader, LumpType offset)
        {
            var lump = new Lump();
            lump.FileOffset = reader.ReadInt32();
            lump.FileLength = reader.ReadInt32();
            lump.Version = reader.ReadInt32();
            lump.FourCC = reader.ReadInt32();
            lump.Offset = offset;
            var position = reader.BaseStream.Position;
            reader.BaseStream.Position = lump.FileOffset;
            switch (lump.Offset)
            {
                case LumpType.Planes:
                    lump.Data = Planes.Read(reader, lump.FileLength);
                    break;
                case LumpType.Vertices:
                    lump.Data = Vertices.Read(reader, lump.FileLength);
                    break;
                case LumpType.Edges:
                    lump.Data = Edges.Read(reader, lump.FileLength);
                    break;
                case LumpType.SurfaceEdges:
                    lump.Data = SurfaceEdges.Read(reader, lump.FileLength);
                    break;
                case LumpType.Faces:
                    lump.Data = Faces.Read(reader, lump.FileLength);
                    break;
                case LumpType.OriginalFaces:
                    lump.Data = OriginalFaces.Read(reader, lump.FileLength);
                    break;
                case LumpType.Brushes:
                    lump.Data = Brushes.Read(reader, lump.FileLength);
                    break;
                case LumpType.BrushSides:
                    lump.Data = BrushSides.Read(reader, lump.FileLength);
                    break;
                case LumpType.Nodes:
                    lump.Data = Nodes.Read(reader, lump.FileLength);
                    break;
                case LumpType.Leafs:
                    lump.Data = Leafs.Read(reader, lump.FileLength);
                    break;
                case LumpType.LeafFaces:
                    lump.Data = LeafFaces.Read(reader, lump.FileLength);
                    break;
                case LumpType.LeafBrushes:
                    lump.Data = LeafBrushes.Read(reader, lump.FileLength);
                    break;
                case LumpType.TextureInfo:
                    lump.Data = TextureInfos.Read(reader, lump.FileLength);
                    break;
                case LumpType.TextureData:
                    lump.Data = TextureDatas.Read(reader, lump.FileLength);
                    break;
                case LumpType.TextureStringData:
                    lump.Data = TextureDataString.Read(reader, lump.FileLength);
                    break;
                case LumpType.TextureDataStringTable:
                    lump.Data = TextureDataStringTable.Read(reader, lump.FileLength);
                    break;
            }
            reader.BaseStream.Position = position;
            return lump;
        }
    }
}
