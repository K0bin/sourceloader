using Source.Bsp.LumpData;
using Source.Bsp.LumpData.GameLumps;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Source.Bsp
{
    public static class LumpArrayExtensions
    {
        public static Node[] GetNodes(this Lump[] lumps)
        {
            return (lumps[(int)LumpType.Nodes].Data as ArrayLumpData<Node>)?.Elements;
        }
        public static Leaf[] GetLeafs(this Lump[] lumps)
        {
            return (lumps[(int)LumpType.Leafs].Data as ArrayLumpData<Leaf>)?.Elements;
        }
        public static Vector3[] GetVertices(this Lump[] lumps)
        {
            return (lumps[(int)LumpType.Vertices].Data as ArrayLumpData<Vector3>)?.Elements;
        }
        public static Face[] GetFaces(this Lump[] lumps)
        {
            return (lumps[(int)LumpType.Faces].Data as ArrayLumpData<Face>)?.Elements;
        }
        public static Brush[] GetBrushes(this Lump[] lumps)
        {
            return (lumps[(int)LumpType.Brushes].Data as ArrayLumpData<Brush>)?.Elements;
        }
        public static BrushSide[] GetBrusheSides(this Lump[] lumps)
        {
            return (lumps[(int)LumpType.BrushSides].Data as ArrayLumpData<BrushSide>)?.Elements;
        }
        public static Edge[] GetEdges(this Lump[] lumps)
        {
            return (lumps[(int)LumpType.Edges].Data as ArrayLumpData<Edge>)?.Elements;
        }
        public static ushort[] GetLeafBrushes(this Lump[] lumps)
        {
            return (lumps[(int)LumpType.LeafBrushes].Data as ArrayLumpData<ushort>)?.Elements;
        }
        public static ushort[] GetLeafFaces(this Lump[] lumps)
        {
            return (lumps[(int)LumpType.LeafFaces].Data as ArrayLumpData<ushort>)?.Elements;
        }
        public static int[] GetSurfaceEdges(this Lump[] lumps)
        {
            return (lumps[(int)LumpType.SurfaceEdges].Data as ArrayLumpData<int>)?.Elements;
        }
        public static TextureData[] GetTextureData(this Lump[] lumps)
        {
            return (lumps[(int)LumpType.TextureData].Data as ArrayLumpData<TextureData>)?.Elements;
        }
        public static TextureInfo[] GetTextureInfo(this Lump[] lumps)
        {
            return (lumps[(int)LumpType.TextureInfo].Data as ArrayLumpData<TextureInfo>)?.Elements;
        }
        public static TextureDataString GetTextureDataString(this Lump[] lumps)
        {
            return (lumps[(int)LumpType.TextureStringData].Data as TextureDataString);
        }
        public static int[] GetTextureDataStringTable(this Lump[] lumps)
        {
            return (lumps[(int)LumpType.TextureDataStringTable].Data as ArrayLumpData<int>)?.Elements;
        }
        public static uint[] GetVertexNormalIndices(this Lump[] lumps)
        {
            return (lumps[(int)LumpType.VertexNormalIndices].Data as ArrayLumpData<uint>)?.Elements;
        }
        public static Vector3[] GetVertexNormals(this Lump[] lumps)
        {
            return (lumps[(int)LumpType.VertexNormals].Data as ArrayLumpData<Vector3>)?.Elements;
        }
        public static DisplacementInfo[] GetDisplacementInfos(this Lump[] lumps)
        {
            return (lumps[(int)LumpType.DisplacementInfo].Data as ArrayLumpData<DisplacementInfo>)?.Elements;
        }
        public static DisplacementTriangle[] GetDisplacementTriangles(this Lump[] lumps)
        {
            return (lumps[(int)LumpType.DisplacementTriangles].Data as ArrayLumpData<DisplacementTriangle>)?.Elements;
        }
        public static DisplacementVertex[] GetDisplacementVertices(this Lump[] lumps)
        {
            return (lumps[(int)LumpType.DisplacementVertices].Data as ArrayLumpData<DisplacementVertex>)?.Elements;
        }
        public static Model[] GetModels(this Lump[] lumps)
        {
            return (lumps[(int)LumpType.Models].Data as ArrayLumpData<Model>)?.Elements;
        }
        public static GameLump GetGame(this Lump[] lumps)
        {
            return (lumps[(int)LumpType.GameLump].Data as GameLump);
        }
        public static byte[] GetPakFile(this Lump[] lumps)
        {
            return (lumps[(int)LumpType.PakFile].Data as ArrayLumpData<byte>)?.Elements;
        }
    }
}
