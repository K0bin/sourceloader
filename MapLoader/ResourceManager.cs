using Source.Bsp;
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
        private readonly Package csgoResources;

        private readonly Stream embeddedStream;
        private readonly ZipArchive embeddedResources;

        private bool isDisposed = false;

        public byte[] this[string name]
        {
            get
            {
                return ReadResource(name);
            }
        }

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

        private byte[] ReadResource(string name)
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
