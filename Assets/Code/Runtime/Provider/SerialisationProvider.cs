using CodingTest_TF.Data;
using CodingTest_TF.Runtime.Serialization;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using UnityEngine;

namespace CodingTest_TF.Runtime.Provider
{
    public sealed class SerialisationProvider : AbstractProvider<SerialisationProvider>
    {
        public GameState CurrentState { get; private set; }

        public class GameState : ISerializable<GameStateMemento>
        {
            public GameState() => Debug.LogWarning("Created new GamesState");
            public GameState(GameStateMemento memento) => Deserialize(memento);

            public void Deserialize(GameStateMemento memento) => throw new NotImplementedException();
            public GameStateMemento Serialize() => throw new NotImplementedException();
        }

        [DataContract]
        public class GameStateMemento : AbstractMemento
        {
            [DataMember] public string VersionString = string.Empty;

            public GameStateMemento(string versionString) => VersionString = versionString;
        }

        public void SaveGameState()
        {
            var serializer = new DataContractJsonSerializer(typeof(GameStateMemento));

            var stream = new MemoryStream();

            var memento = CurrentState.Serialize();

            if (stream == null || memento == null)
            {
                Debug.LogError($"Something was null: {stream} / {memento}");
                return;
            }

            serializer.WriteObject(stream, memento);

            SerialisationProvider.Instance.Save(Constants.SaveDirectory, Constants.SaveGameName, stream);
        }

        private GameState LoadGameState()
        {
            var stream = SerialisationProvider.Instance.Load(Constants.SaveDirectory, Constants.SaveGameName);

            //loadedStateFromFile = true;

            if (stream == null)
                return new GameState();

            var serializer = new DataContractJsonSerializer(typeof(GameStateMemento));

            // try catch...
            var memento = (GameStateMemento)serializer.ReadObject(stream);

            // => OnLoadingCompleted?.Invoke();

            return new GameState(memento);
        }

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
