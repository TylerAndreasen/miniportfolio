using System;
using UnityEngine;

namespace LasciiGameObjects
{
    public class DirectoryPrecursor
    {
        //FIELDS

        private static readonly bool DP_CLASS_DEBUG_TOGGLE = false;
        private int index;
        private Action[] actions;
        //CONSTRUCTORS
        /**
         */
        public DirectoryPrecursor(int directoryIndex, Action[] actions)
        {
            this.index = directoryIndex;
            this.actions = actions;

            if (DP_CLASS_DEBUG_TOGGLE)
            {
                for (int i = 0; i < this.actions.Length; i++)
                {
                    if (this.actions[i] == null)
                    {
                        Debug.Log("DP Action[" + i + "] is null");
                    }
                }
            }
        }
        //PROPERTIES
        //METHODS
        public int GetIndex() { return this.index; }
        public Action[] GetActions() { return this.actions; }

        public void DEBUG()
        {
            Debug.Log("#DEBUG# DirectoryPrecursor.DEBUG() - DP Index :" + this.index + ": Action Count :" + this.actions.Length + ":");
        }
    }
}