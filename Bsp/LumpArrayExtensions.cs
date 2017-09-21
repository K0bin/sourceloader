using Bsp.LumpData;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Bsp
{
    public static class LumpArrayExtensions
    {
        public static Node[] GetNodes(this Lump[] lumps)
        {
            return (lumps[(int)LumpType.Nodes].Data as Nodes)?.Elements;
        }
        public static Leaf[] GetLeafs(this Lump[] lumps)
        {
            return (lumps[(int)LumpType.Leafs].Data as Leafs)?.Elements;
        }
        public static Vector3[] GetVertices(this Lump[] lumps)
        {
            return (lumps[(int)LumpType.Vertices].Data as Vertices)?.Elements;
        }
        public static Face[] GetFaces(this Lump[] lumps)
        {
            return (lumps[(int)LumpType.Faces].Data as Faces)?.Elements;
        }
        public static Brush[] GetBrushes(this Lump[] lumps)
        {
            return (lumps[(int)LumpType.Brushes].Data as Brushes)?.Elements;
        }
        public static BrushSide[] GetBrusheSides(this Lump[] lumps)
        {
            return (lumps[(int)LumpType.BrushSides].Data as BrushSides)?.Elements;
        }
        public static Edge[] GetEdges(this Lump[] lumps)
        {
            return (lumps[(int)LumpType.Edges].Data as Edges)?.EdgeArr;
        }
        public static ushort[] GetLeafBrushes(this Lump[] lumps)
        {
            return (lumps[(int)LumpType.LeafBrushes].Data as LeafBrushes)?.Elements;
        }
        public static ushort[] GetLeafFaces(this Lump[] lumps)
        {
            return (lumps[(int)LumpType.LeafFaces].Data as LeafFaces)?.Elements;
        }
        public static int[] GetSurfaceEdges(this Lump[] lumps)
        {
            return (lumps[(int)LumpType.SurfaceEdges].Data as SurfaceEdges)?.Elements;
        }
        public static TextureData[] GetTextureData(this Lump[] lumps)
        {
            return (lumps[(int)LumpType.TextureData].Data as TextureDatas)?.Elements;
        }
        public static TextureInfo[] GetTextureInfo(this Lump[] lumps)
        {
            return (lumps[(int)LumpType.TextureInfo].Data as TextureInfos)?.Elements;
        }
        public static TextureDataString GetTextureDataString(this Lump[] lumps)
        {
            return (lumps[(int)LumpType.TextureStringData].Data as TextureDataString);
        }
        public static int[] GetTextureDataStringTable(this Lump[] lumps)
        {
            return (lumps[(int)LumpType.TextureDataStringTable].Data as TextureDataStringTable)?.Elements;
        }
    }
}
