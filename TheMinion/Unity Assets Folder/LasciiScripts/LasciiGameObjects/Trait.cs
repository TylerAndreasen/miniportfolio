using System;
using UnityEngine;

namespace LasciiGameObjects
{
    public class Trait
    {
        //FIELDS
        private readonly bool TRAIT_CLASS_DEBUG = false;
        private int totalTraits = 0;
        protected string title;
        protected string stringValue;
        protected int intValue;

        //CONSTRUCTORS
        public Trait(string title, int intValue)
        {
            if (TRAIT_CLASS_DEBUG) Debug.Log($"#DEBUG# Trait(String :{title}:, Value (int) :{intValue}:) - Index :{totalTraits}:. #");
            totalTraits++;
            this.title = title;
            this.intValue = intValue;
        }
        public Trait(string title, string stringValue)
        {
            if (TRAIT_CLASS_DEBUG) Debug.Log($"#DEBUG# Trait(String :{title}:, Value (int) :{stringValue}:) - Index :{totalTraits}:. #");
            totalTraits++;
            this.title = title;
            this.stringValue = stringValue;
        }

        //PROPERTIES
        public bool HasStringValue { get; private set; }

        //METHODS
        public string GetTitle() { return this.title; }
        public string GetStringValue()
        {
            if (this.HasStringValue)
            {
                return this.stringValue;
            } else
            {
                return null; //ERRORFIND : Null String related to Trait Comparison
            }
        }
        public int GetIntValue()
        {
            if (!this.HasStringValue)
            {
                return this.intValue;
            } else
            {
                return Int32.MinValue;
            }
        }
        public void DEBUG_TRAIT() {Debug.Log("#DEBUG# Trait.DEBUG_Trait() - (String) Title :"+this.title+": Value :"+(this.HasStringValue ? "(STRING) {"+this.stringValue+"}" : "(INT) {"+this.intValue+"}")+":. #");}
        public bool IncrementTraitIntValue() {return this.IncrementTraitIntValue(1);}
        public bool IncrementTraitIntValue(int In)
        {
            if (!this.HasStringValue)
            {
                this.intValue += In;
                return true;
            } else return false;
        }
        public bool DecrementTraitIntValue() {return this.DecrementTraitIntValue(1);}
        public bool DecrementTraitIntValue(int In)
        {
            if (!this.HasStringValue)
            {
                this.intValue -= In;
                return true;
            } else return false;
        }
        public bool PushIntValue(int In)
        {
            if (!this.HasStringValue)
            {
                this.intValue = In;
                return true;
            } else return false;
        }
        public bool PushStringValue(string In)
        {
            if (this.HasStringValue)
            {
                this.stringValue = In;
                return true;
            } else return false;
        }
        public bool OverrideNewStringValue(string In)
        {
            if (this.HasStringValue)
            {
                this.stringValue = In;
                return false;
            } else
            {
                this.HasStringValue = true;
                this.intValue = Int32.MinValue;
                this.stringValue = In;
                return true;
            }
        }
        public bool OverrideNewIntValue(int In)
        {
            bool methodDebug = true;
            if (methodDebug || TRAIT_CLASS_DEBUG) Debug.Log($"#DEBUG# Trait.OverrideNewIntValue(int {In}) - Called");
            if (!this.HasStringValue)
            {
                if (methodDebug || TRAIT_CLASS_DEBUG) Debug.Log($"#DEBUG# Trait.OverrideNewIntValue(int {In}) - Already Int - Current :{this.intValue}:, New :{In}:. #");
                this.intValue = In;
                return false;
            } else
            {
                if (methodDebug || TRAIT_CLASS_DEBUG) Debug.Log($"#DEBUG# Trait.OverrideNewIntValue(int {In}) - Replacing String Value - Current :{this.stringValue}:, New INT :{In}:. #");
                this.HasStringValue = false;
                this.intValue = In;
                this.stringValue = "";
                return true;
            }
        }
        public bool MeetsMinimum(int minimum)
        {
            if (!this.HasStringValue)
            {
                return (this.intValue >= minimum);
            } else 
            {
                return false;
            }
        }
        public bool ExceedsMinimum(int minimum)
        {
            if (!this.HasStringValue)
            {
                return (this.intValue > minimum);
            } else 
            {
                return false;
            }
        }
        public bool MeetsMaximum(int maximum)
        {
            if (!this.HasStringValue)
            {
                return (this.intValue <= maximum);
            } else 
            {
                return false;
            }
        }
        public bool BelowMaximum(int maximum)
        {
            if (!this.HasStringValue)
            {
                return (this.intValue < maximum);
            } else 
            {
                return false;
            }
        }
        public bool ValueEquals(int value)
        {
            if (!this.HasStringValue)
            {
                return (this.intValue == value);
            } else 
            {
                return false;
            }
        }
        public bool ValueEquals(string value)
        {
            if (!this.HasStringValue)
            {
                return (this.stringValue == value);
            } else 
            {
                return false;
            }
        }
        public bool NotEquals(int value)
        {
            if (!this.HasStringValue)
            {
                return !(this.intValue == value);
            } else 
            {
                return false;
            }
        }
        public bool NotEquals(string value)
        {
            if (!this.HasStringValue)
            {
                return !(this.stringValue == value);
            } else 
            {
                return false;
            }
        }
    }
}