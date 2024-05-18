using CodingTest_TF.Data;
using System;
using System.IO;
using UnityEngine;

namespace CodingTest_TF.Runtime.Provider
{
    public sealed class SerialisationProvider : AbstractProvider<SerialisationProvider>
    {
        #region SaveAndLoad
        internal void Save(string directory, string file, MemoryStream stream)
        {
            if (directory == null)
                throw new NullReferenceException("directory to save to is null");

            if (!Directory.Exists(directory))
                _ = Directory.CreateDirectory(directory);

            var path = Path.Combine(directory, $"{file}.{Constants.FileEnding}");

            /// NOTE might want to protect overwriting.            
            File.WriteAllBytes(path, stream.ToArray());

            Debug.LogWarning($"Saved {file} to: {path}");
        }

        internal MemoryStream Load(string directory, string file)
        {
            if (directory == null)
                throw new FileNotFoundException("Savegame directory is null");

            if (!Directory.Exists(directory))
                return null;

            if (Directory.GetFiles(directory, $"*{file}.{Constants.FileEnding}").Length == 0)
                return null;

            var path = Path.Combine(directory, $"{file}.{Constants.FileEnding}");

            // NOTE might want to disable events during loading
            var bytes = File.ReadAllBytes(path);

            if (bytes.Length == 0)
                return null;

            var stream = new MemoryStream(bytes);

            Debug.LogWarning($"Loaded {file} from: {path}");

            return stream;
        }
        #endregion
    }
}
