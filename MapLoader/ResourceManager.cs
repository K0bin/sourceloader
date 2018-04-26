using Source.Bsp;
using Source.Common;
using Source.Mdl;
using Source.Vtf;
using SteamDatabase.ValvePak;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Source.MapLoader
{
    public class ResourceManager: IDisposable
    {
        private readonly Dictionary<string, Resource> resources = new Dictionary<string, Resource>();

        private readonly Package csgoResources;

        private readonly Stream embeddedStream;
        private readonly ZipArchive embeddedResources;

        private bool isDisposed = false;

        private static Dictionary<Type, string> paths = new Dictionary<Type, string>()
        {
            { typeof(SourceMaterial), "materials/" },
            { typeof(SourceTexture), "materials/" },
            { typeof(SourceModel), "models/" },
        };
        private static Dictionary<Type, string> fileTypes = new Dictionary<Type, string>()
        {
            { typeof(SourceMaterial), ".vmt" },
            { typeof(SourceTexture), ".vtf" },
            { typeof(SourceModel), ".mdl" },
        };

        public ResourceManager(string csgoDirectory, Map map)
        {
            csgoResources = new Package();
            var file = Path.Combine(csgoDirectory, "csgo", "pak01_dir.vpk");
            csgoResources.Read(file);

            embeddedStream = new MemoryStream(map.Lumps.GetPakFile());
            embeddedResources = new ZipArchive(embeddedStream);
        }

        ~ResourceManager()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposed)
                return;

            if (isDisposing)
            {
                embeddedStream.Close();
                embeddedStream.Dispose();
            }

            isDisposed = true;
        }

        public T Get<T>(string name) where T: Resource
        {
            if (name == null)
            {
                return null;
            }

            var nameWithType = name.ToLower();
            var path = paths[typeof(T)];
            var fileType = fileTypes[typeof(T)];
            if (!nameWithType.StartsWith(path, StringComparison.Ordinal))
            {
                nameWithType = path + nameWithType;
            }
            if (!nameWithType.EndsWith(fileType, StringComparison.Ordinal))
            {
                nameWithType = nameWithType + fileType;
            }

            if (!resources.TryGetValue(nameWithType, out Resource resource))
            {
                var data = ReadResourceFromDisk(nameWithType);
                if (data != null)
                {
                    using (var reader = new BinaryReader(new MemoryStream(data)))
                    {
                        resource = (T)Activator.CreateInstance(typeof(T), reader, data.Length);
                    }
                }
                else
                {
                    if (typeof(T) == typeof(SourceMaterial))
                    {
                        //Material not found => try to load texture and create simple material
                        var texture = Get<SourceTexture>(name);
                        if (texture == null) return null;
                        resource = new SourceMaterial(name);
                    }
                    else
                    {
                        return null;
                    }
                }
                resources[nameWithType] = resource;
            }
            if (!(resource is T))
            {
                return null;
            }
            return (T)resource;
        }

        private byte[] ReadResourceFromDisk(string name)
        {
            var entry = csgoResources.FindEntry(name);
            if (entry != null)
            {
                csgoResources.ReadEntry(entry, out byte[] data);
                return data;
            }
            else
            {
                var zipEntry = embeddedResources.GetEntry(name);
                if (zipEntry == null) return null;
                byte[] data = new byte[zipEntry.Length];
                using (var stream = zipEntry.Open())
                {
                    stream.Read(data, 0, data.Length);
                }
                return data;
            }
        }
    }
}
