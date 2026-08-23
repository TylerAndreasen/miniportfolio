using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using LasciiUtility;

namespace LasciiGameObjects
{
    public class Directory
    {
        //FIELDS
        private static readonly bool DIRECTORY_CLASS_DEBUG = false;
        private Page assignedPage;
        private Action[] actions;
        private int directoryIndex;
        //CONSTRUCTORS
        public Directory(Page assignedPage, int directoryIndex, Action[] actions)
        {
            if (DIRECTORY_CLASS_DEBUG) Debug.Log("#DEBUG# Directory(Page Index :" + assignedPage.GetFullIndex() + ":, Directory Index :" + directoryIndex + ":, Action Count :" + actions.Length + ":) ");
            Assert.IsTrue(actions.Length > 0);
            this.assignedPage = assignedPage;
            this.directoryIndex = directoryIndex;
            this.actions = actions;
        }
        //PROPERTIES
        //METHODS
        public String FetchDirectoryDescription()
        {
            
            bool methodDebug = false;
            if (methodDebug) Debug.Log($"#DEBUG# Directory.FetchDirectoryDescription() - Begin. #");
            String output = "Enter your selection:<br><br>";
            String[] lines = this.GetAllowableActionDescriptions();
            if (lines.Length == 1 && lines[0] == "") return "";
            if (methodDebug) Debug.Log($"#DEBUG# Directory.FetchDirectoryDescription() - Calculated Lines to output :{lines.Length}:. #");
            int i = 1;
            foreach (String line in lines)
            {
                if (methodDebug) Debug.Log($"#DEBUG# Directory.FetchDirectoryDescription() - Pushing :{line}: to output for display. #");
                output = output + (i++) + ". " + line + "<br>";
            }
            if (methodDebug) Debug.Log($"#DEBUG# Directory.FetchDirectoryDescription() - Returning :{output}:. #");
            return output;
        }
        public void DEBUG()
        {
            String output = "";
            for (int i = 0; i < this.actions.Length; i++)
            {
                output += "\n\t\tAction[" + i + "] - \n\t\t:" + actions[i].DEBUG(true) + ":";
            }
            Debug.Log(output);
        }
        public String DEBUG(bool unused)
        {
            String output = "";
            for (int i = 0; i < this.actions.Length; i++)
            {
                output += "\n\t\tAction[" + i + "] - \n\t\t:" + actions[i].DEBUG(true) + ":";
            }
            return output;
        }
        public Action[] GetTakeableActions()
        {
            List<Action> bucket = new List<Action>();
            Action[] output;
            foreach (Action a in this.actions)
            {
                if (a.CanShowAction())
                {
                    bucket.Add(a);
                }
            }
            output = new Action[bucket.Count];
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = bucket[i];
            }
            return output;
        }
        public String[] GetAllowableActionDescriptions()
        {
            bool methodDebug = false;
            if (methodDebug) Debug.Log($"#DEBUG# Directory.GetAllowableActionDescriptions() - Begin - Total Actions :{this.actions.Length}:, Example {this.actions[0].DEBUG(true)}. #");
            List<String> bucket = new List<String>();
            for (int i = 0; i < this.actions.Length; i++)
            {
                if (methodDebug) Debug.Log($"#DEBUG# Directory.GetAllowableActionDescriptions() - Testing Action[{i}] Desc :{this.actions[i].GetDescription()}: - Can Show :{this.actions[i].CanShowAction()}: - First TR Name :{this.actions[i].GetTR()[0].GetTitle()}:. #");
                if (this.actions[i].CanShowAction())
                {
                    if (methodDebug) Debug.Log($"#DEBUG# Directory.GetAllowableActionDescriptions() - Adding Action[{i}] to method ouput. Current Length :{bucket.Count}:. #");
                    bucket.Add(this.actions[i].GetDescription() + (DIRECTORY_CLASS_DEBUG ? (" " + this.actions[i].GetDestination().GetNumericIndex() + ".") : ""));
                }
            }
            if (bucket.Count == 0) return new String[] {"Apologies, an error has occurred. Try restarting the game."};
            String[] output = new String[bucket.Count];
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = bucket[i];
            }
            if (methodDebug) Debug.Log($"#DEBUG# Directory.GetAllowableActionDescriptions() - Returning String Count :{output.Length}:. #");
            return output;
        }
        public int GetCount() { return this.actions.Length; }
        public Page GetNextPage(String index)
        {
            int parsed = LasciiBasicTypeUtilities.ParseInt(index);
            if (!(parsed < this.actions.Length)) return this.assignedPage;
            else if (!this.actions[parsed].CanTakeAction()) return this.assignedPage;
            else return this.actions[parsed].GetDestination();
        }
        public int GetDirectoryIndex() { return directoryIndex; }
        public Action[] GetAllActions() { return this.actions; }
    }
}