using System;
using System.Collections.Generic;
using UnityEngine;
using LasciiUtility;

namespace LasciiGameObjects
{
    public class Page
    {
        //FIELDS
        private static readonly bool PAGE_CLASS_DEBUG_TOGGLE = false;
        private Directory directory;
        protected String[] description;
        protected String index;
        //CONSTRUCTORS
        public Page(String[] description, String index)
        {
            if (PAGE_CLASS_DEBUG_TOGGLE) Debug.Log("Page (String[] description Length :" + description.Length + ": Value 0 :" + description[0] + ":, String index Value :" + index + ":) - Creating Page Object");
            this.description = description;
            this.index = index;
        }
        //PROPERTIES
        //METHODS
        public virtual String FetchDescription(out string directoryDescription)
        {
            if (this.directory == null)  directoryDescription = "#DEBUG# No Directory Loaded. #";
            else directoryDescription = this.directory.FetchDirectoryDescription();
            bool methodDebug = false;
            if (methodDebug || PAGE_CLASS_DEBUG_TOGGLE) Debug.Log($"#DEBUG# Page.FetchPageDescription() - Description Lines :{this.description.Length}:");
            if (this.description.Length > 1)
            {
                String output = this.description[0];
                for (int i = 1; i < this.description.Length; i++)
                {
                    output = $"{output}<br>{this.description[i]}";//  output + this.description[i] + "<br>";
                }
                if (methodDebug || PAGE_CLASS_DEBUG_TOGGLE) Debug.Log($"#DEBUG# Page.FetchPageDescription() - Outputting (calc) :{output}:");
                return output;
            }

            else
            {
                if (methodDebug || PAGE_CLASS_DEBUG_TOGGLE) Debug.Log($"#DEBUG# Page.FetchPageDescription() - Outputting (desc[0]):{description[0]}:");
                return description[0];
            }
        }
        public bool PushDirectory(Directory directory)
        {
            bool methodDebug = false;
            if (PAGE_CLASS_DEBUG_TOGGLE || methodDebug) Debug.Log($"Page[{this.index}].PushDirectory(Directory {directory == null})");
            this.directory = directory;
            if (PAGE_CLASS_DEBUG_TOGGLE || methodDebug) Debug.Log($"Page[{this.index}].PushDirectory() - Directory Field is null :{this.directory == null}:");
            return true;
        }
        public void DEBUG()
        {

            Debug.Log($"#DEBUG# Page[{this.index}] - \n\tDirectory :{this.directory.DEBUG(true)}:. #");
        }
        public String DEBUG(bool unused)
        {
            return $"Page[{this.index}] - \n\tDirectory :{this.directory.DEBUG(true)}:. #";
        }
        public Action[] GetTakeableActions() { return this.directory.GetTakeableActions(); }
        public String GetFullIndex() { return this.index; }
        public int GetNumericIndex() { return LasciiBasicTypeUtilities.ParseInt(this.index); }
        public int GetTotalNextPages() { return this.directory.GetCount(); }
        public Action[] GetAllActions() { return this.directory.GetAllActions(); }
        public virtual bool IsGameStart() { return false; }
    }
}