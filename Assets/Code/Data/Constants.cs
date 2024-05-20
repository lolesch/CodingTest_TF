using UnityEngine;

namespace CodingTest.Data
{
    public static class Constants
    {
        public static readonly string SaveDirectory = Application.persistentDataPath;

        public static readonly string FileEnding = "sav";

        public static readonly float TooltipDelay = .5f;
        public static readonly float TooltipDelayAfterInteraction = 2f;
        public static Color ButtonRed => new(1f, .2f, .2f);
        public static Color ButtonGreen => new(0f, .8f, .2f);
        public static Color ButtonBlue => new(0f, .5f, 1f);
    }
}
