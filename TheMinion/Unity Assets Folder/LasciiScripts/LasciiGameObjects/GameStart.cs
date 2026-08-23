/*using System;
using UnityEngine;

namespace LasciiGameObjects
{
    public class GameStart : Page
    {
        //FIELDS
        private static readonly bool GAMEMODE_CLASS_DEBUG_TOGGLE = true;
        //CONSTRUCTOR
        public GameStart(String[] description, String index) : base(description, index)
        {
            if (GAMEMODE_CLASS_DEBUG_TOGGLE) Debug.Log($"#DEBUG# GameMode(String[{description.Length}], String {index}) - Created.");
        }
        //METHODS
        public override bool IsGameStart() { return true; }
        public override String FetchDescription(out string directoryDescription)
        {
            directoryDescription = "Press [Enter] or [1] to begin.";
            bool methodDebug = false;
            if (methodDebug || GAMEMODE_CLASS_DEBUG_TOGGLE) Debug.Log($"#DEBUG# Page.FetchPageDescription() - Description Lines :{this.description.Length}:");
            if (this.description.Length > 1)
            {
                String output = this.description[0];
                for (int i = 1; i < this.description.Length; i++)
                {
                    output = $"{output}<br>{this.description[i]}";//  output + this.description[i] + "<br>";
                }
                if (methodDebug || GAMEMODE_CLASS_DEBUG_TOGGLE) Debug.Log($"#DEBUG# Page.FetchPageDescription() - Outputting (calc) :{output}:");
                return output;
            }

            else
            {
                if (methodDebug || GAMEMODE_CLASS_DEBUG_TOGGLE) Debug.Log($"#DEBUG# Page.FetchPageDescription() - Outputting (desc[0]):{description[0]}:");
                return description[0];
            }
        }
    }
}*/