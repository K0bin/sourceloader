using Source.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace Source.Bsp.LumpData.GameLumps
{
    public class StaticProps: LumpData
    {
        public const string IdName = "sprp";

        public List<string> Models
        {
            get; private set;
        }

        public List<ushort> Leafs
        {
            get; private set;
        }

        public List<StaticProp> Props
        {
            get; private set;
        }

        public StaticProps(BinaryReader reader, long length, int version)
        {
            var startPosition = reader.BaseStream.Position;

            var modelCount = reader.ReadInt32();
            Models = new List<string>(modelCount);
            for (var i = 0; i < modelCount; i++)
            {
                var bytes = reader.ReadBytes(128);
                string entry = Encoding.ASCII.GetString(bytes);
                Models.Add(entry.Replace("\0", string.Empty).Trim());
            }

            var leafCount = reader.ReadInt32();
            Leafs = new List<ushort>(leafCount);
            for (var i = 0; i < leafCount; i++)
            {
                Leafs.Add(reader.ReadUInt16());
            }

            var remainingLength = length - reader.BaseStream.Position + startPosition;
            var propCount = reader.ReadInt32();
            Props = new List<StaticProp>(propCount);
            for (int i = 0; i < propCount; i++)
            {
                Props.Add(StaticProp.Read(reader, version));
            }
        }

        private int TryRead(BinaryReader reader, int length, int propCount)
        {
            var start = reader.BaseStream.Position;
            try
            {
                for (int i = 0; i < propCount; i++)
                {
                    var pos = reader.BaseStream.Position;
                    reader.BaseStream.Position += 24;
                    var index = reader.ReadUInt16();
                    var model = Models[Props[i].PropType];
                    reader.BaseStream.Position = pos + length;
                }
                return length;
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("unsuccessful using: "+length);
                reader.BaseStream.Position = start;
                return TryRead(reader, length + 1, propCount);
            }
        }
    }

    [Flags]
    public enum StaticPropFlags : byte
    {
        Fades = 1,
        UseLightingOrigin = 2,
        NoDraw = 4,
        IgnoreNormals = 8,
        NoShadow = 0x10,
        Unused = 0x20,
        NoPerVertexLighting = 0x40,
        NoSelfShadowing = 0x80
    }

    public struct StaticProp
    {
        //v4+
        public Vector3 Origin;
        public Vector3 Angles;
        public ushort PropType;
        public ushort FirstLeaf;
        public ushort LeafCount;
        [MarshalAs(UnmanagedType.U1)]
        public bool Solid;
        public StaticPropFlags Flags;
        public int Skin;
        public float FadeMinDistance;
        public float FadeMaxDistance;
        public Vector3 LightingOrigin;
        //v5+
        public float ForcedFadeScale;
        //v6 & v7
        public ushort MinDxLevel;
        public ushort MaxDxLevel;
        //v8+
        public byte MinCpuLevel;
        public byte MaxCpuLevel;
        public byte MinGpuLevel;
        public byte MaxGpuLevel;
        //v7+
        public uint DiffuseModulation;
        //v10+
        public float Unknown;
        //v9+
        [MarshalAs(UnmanagedType.U4)]
        public bool DisableX360;

        public static StaticProp Read(BinaryReader reader, int version)
        {
            var start = reader.BaseStream.Position;
            var staticProp = new StaticProp();
            staticProp.Origin = reader.ReadStruct<Vector3>();
            staticProp.Angles = reader.ReadStruct<Vector3>();
            staticProp.PropType = reader.ReadUInt16();
            staticProp.FirstLeaf = reader.ReadUInt16();
            staticProp.LeafCount = reader.ReadUInt16();
            staticProp.Solid = reader.ReadByte() == 1;
            staticProp.Flags = (StaticPropFlags)reader.ReadByte();
            staticProp.Skin = reader.ReadInt32();
            staticProp.FadeMinDistance = reader.ReadSingle();
            staticProp.FadeMaxDistance = reader.ReadSingle();
            staticProp.LightingOrigin = reader.ReadStruct<Vector3>();

            if (version >= 5)
            {
                staticProp.ForcedFadeScale = reader.ReadSingle();
            }
            if (version == 6 || version == 7)
            {
                staticProp.MinDxLevel = reader.ReadUInt16();
                staticProp.MaxDxLevel = reader.ReadUInt16();
            }
            if (version >= 8)
            {
                staticProp.MinCpuLevel = reader.ReadByte();
                staticProp.MaxCpuLevel = reader.ReadByte();
                staticProp.MinGpuLevel = reader.ReadByte();
                staticProp.MaxGpuLevel = reader.ReadByte();
            }
            if (version >= 7)
            {
                staticProp.DiffuseModulation = reader.ReadUInt32();
            }
            if (version >= 10)
            {
                staticProp.Unknown = reader.ReadSingle();
            }
            if (version >= 9)
            {
                staticProp.DisableX360 = reader.ReadInt32() == 1;
            }
            var structLength = reader.BaseStream.Position - start;
            return staticProp;
        }
    }
}
