using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bsp.LumpData
{
    public class Faces: Data
    {
        public Face[] Elements;
        private Faces() { }

        public static Faces Read(BinaryReader reader, int length)
        {
            var size = length / Face.SIZE;
            var start = reader.BaseStream.Position;

            var faces = new Faces();
            faces.Elements = new Face[size];
            for (int i = 0; i < size; i++)
            {
                faces.Elements[i] = Face.Read(reader);
            }
            return faces;
        }
    }

    public class OriginalFaces : Data
    {
        public Face[] FacesArr;
        private OriginalFaces() { }

        public static OriginalFaces Read(BinaryReader reader, int length)
        {
            var size = length / Face.SIZE;
            var start = reader.BaseStream.Position;

            var faces = new OriginalFaces();
            faces.FacesArr = new Face[size];
            for (int i = 0; i < size; i++)
            {
                faces.FacesArr[i] = Face.Read(reader);
            }
            return faces;
        }
    }

    public class Face
    {
        public const int SIZE = 56;

        public ushort PlaneNumber;
        public byte Side;
        public bool IsOnNode;
        public int FirstEdge;
        public short EdgesCount;
        public short TextureInfo;
        public short DisplacementInfo;
        public short SurfaceFogVolumeId;
        public byte[] Styles = new byte[4];
        public int LightOffset;
        public float Area;
        public int[] LightmapTextureMinsInLuxels = new int[2];
        public int[] LightmapTextureSizeInLuxels = new int[2];
        public int originalFace;
        public ushort PrimitivesCount;
        public ushort FirstPrimitiveId;
        public uint SmoothingGroup;

        private Face() { }

        public static Face Read(BinaryReader reader)
        {
            var face = new Face();
            face.PlaneNumber = reader.ReadUInt16();
            face.Side = reader.ReadByte();
            face.IsOnNode = reader.ReadByte() == 1;
            face.FirstEdge = reader.ReadInt32();
            face.EdgesCount = reader.ReadInt16();
            face.TextureInfo = reader.ReadInt16();
            face.DisplacementInfo = reader.ReadInt16();
            face.SurfaceFogVolumeId = reader.ReadInt16();
            face.Styles[0] = reader.ReadByte();
            face.Styles[1] = reader.ReadByte();
            face.Styles[2] = reader.ReadByte();
            face.Styles[3] = reader.ReadByte();
            face.LightOffset = reader.ReadInt32();
            face.Area = reader.ReadSingle();
            face.LightmapTextureMinsInLuxels[0] = reader.ReadInt32();
            face.LightmapTextureMinsInLuxels[1] = reader.ReadInt32();
            face.LightmapTextureSizeInLuxels[0] = reader.ReadInt32();
            face.LightmapTextureSizeInLuxels[1] = reader.ReadInt32();
            face.originalFace = reader.ReadInt32();
            face.PrimitivesCount = reader.ReadUInt16();
            face.FirstPrimitiveId = reader.ReadUInt16();
            face.SmoothingGroup = reader.ReadUInt32();
            return face;
        }
    }
}
