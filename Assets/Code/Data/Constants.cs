using UnityEngine;

namespace CodingTest.Data
{
    public static class Constants
    {
        public static readonly string SaveDirectory = Application.persistentDataPath;

        //public static readonly string SaveGameName = "SaveGame";

        public static readonly string FileEnding = "sav";

        public static readonly float TooltipDelay = .5f;
        public static readonly float TooltipDelayAfterInteraction = 2f;
    }
}
