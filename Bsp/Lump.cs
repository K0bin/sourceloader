﻿/*
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
using CsgoDemoRenderer.Bsp.LumpData;
using System.IO;
using System.Numerics;

namespace CsgoDemoRenderer.Bsp
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

        public LumpData.LumpData Data;

        public override string ToString() => $"Lump {Data?.ToString() ?? ""}".Trim();

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
                    lump.Data = new ArrayLumpData<LumpData.Plane>(reader, lump.FileLength);
                    break;
                case LumpType.Vertices:
                    lump.Data = new ArrayLumpData<Vector3>(reader, lump.FileLength);
                    break;
                case LumpType.Edges:
                    lump.Data = new ArrayLumpData<Edge>(reader, lump.FileLength);
                    break;
                case LumpType.SurfaceEdges:
                    lump.Data = new ArrayLumpData<int>(reader, lump.FileLength);
                    break;
                case LumpType.Faces:
                    lump.Data = new ArrayLumpData<Face>(reader, lump.FileLength);
                    break;
                case LumpType.OriginalFaces:
                    lump.Data = new ArrayLumpData<Face>(reader, lump.FileLength);
                    break;
                case LumpType.Brushes:
                    lump.Data = new ArrayLumpData<Brush>(reader, lump.FileLength);
                    break;
                case LumpType.BrushSides:
                    lump.Data = new ArrayLumpData<BrushSide>(reader, lump.FileLength);
                    break;
                case LumpType.Nodes:
                    lump.Data = new ArrayLumpData<Node>(reader, lump.FileLength);
                    break;
                case LumpType.Leafs:
                    lump.Data = new ArrayLumpData<Leaf>(reader, lump.FileLength);
                    break;
                case LumpType.LeafFaces:
                    lump.Data = new ArrayLumpData<ushort>(reader, lump.FileLength);
                    break;
                case LumpType.LeafBrushes:
                    lump.Data = new ArrayLumpData<ushort>(reader, lump.FileLength);
                    break;
                case LumpType.TextureInfo:
                    lump.Data = new ArrayLumpData<TextureInfo>(reader, lump.FileLength);
                    break;
                case LumpType.TextureData:
                    lump.Data = new ArrayLumpData<TextureData>(reader, lump.FileLength);
                    break;
                case LumpType.TextureStringData:
                    lump.Data = new TextureDataString(reader, lump.FileLength);
                    break;
                case LumpType.TextureDataStringTable:
                    lump.Data = new ArrayLumpData<int>(reader, lump.FileLength);
                    break;
                case LumpType.DisplacementVertices:
                    lump.Data = new ArrayLumpData<DisplacementVertex>(reader, lump.FileLength);
                    break;
                case LumpType.DisplacementTriangles:
                    lump.Data = new ArrayLumpData<DisplacementTriangle>(reader, lump.FileLength);
                    break;
                case LumpType.DisplacementInfo:
                    lump.Data = new ArrayLumpData<DisplacementInfo>(reader, lump.FileLength);
                    break;
            }
            reader.BaseStream.Position = position;
            return lump;
        }
    }
}