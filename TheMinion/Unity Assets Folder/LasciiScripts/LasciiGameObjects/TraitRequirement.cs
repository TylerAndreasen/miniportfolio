using System;
using UnityEngine;
using UnityEngine.Assertions;
using LasciiUtility;

namespace LasciiGameObjects
{
    public class TraitRequirement : Trait
    {
        //FIELDS
        private static readonly bool TRAIT_REQUIREMENT_CLASS_DEBUG = false;
        private bool[] controlBooleans;
        private static readonly bool[] defaultBooleans = { false, true, true, false, true };

        //CONSTRUCTORS
        public TraitRequirement(string title, int intvalue) : base(title, intvalue)
        {
            this.SetLEG(defaultBooleans);
        }
        public TraitRequirement(string title, string stringvalue) : base(title, stringvalue)
        {
            this.SetLEG(defaultBooleans);
        }
        public TraitRequirement(string title, int intvalue, bool[] contronBooleans) : base(title, intvalue)
        {
            this.SetLEG(contronBooleans);
        }
        public TraitRequirement(string title, string stringvalue, bool[] contronBooleans) : base(title, stringvalue)
        {
            this.SetLEG(contronBooleans);
        }
        //PROPERTIES
        //METHODS
        /*
            debug statment
            
            easy refs for the trait string value flag and title
            
            return false if the title of the trait does not match the TR

            assert that the trait and TR SVF's match, with a clear debug statement

            else
                if string value
                    assert equals flag XOR (^) not equals flag
                    if not equals flag, not equals
                    else equals
                else 
                    if not flag, not equals

                    else if less than
                        if equals, less than or equals
                        else 
                            less than
                    
                    else if greater than
                        if equals, greater than or equals
                        else
                            greater than
                    *else returns false
            
            
            */
        public bool Compare(Trait trait)
        {
            bool methodDebug = false;
            if (methodDebug || TRAIT_REQUIREMENT_CLASS_DEBUG) Debug.Log("#DEBUG# TraitRequirement.compare(Trait :" + trait.GetTitle() + ":) on TR :" + this.title + ":. #");
            bool oHasStringValue = trait.HasStringValue;
            String oTitle = trait.GetTitle();
            if (methodDebug || TRAIT_REQUIREMENT_CLASS_DEBUG) Debug.Log($"#DEBUG# TraitRequirement.compare(Trait :{trait.GetTitle()}:) on TR :{this.title}:, Trait title :{oTitle}:, Matches :{this.title == oTitle}:. #");
            if (this.title != oTitle) return false;
            //No mismatches between trait and TR
            Assert.IsTrue(this.HasStringValue == oHasStringValue,
                "#ERROR# TraitRequirement.compare(Trait) recieved a Trait with the Value Type :"
                        + (oHasStringValue ? ("String: value :" + trait.GetStringValue()) : ("int: value :" + trait.GetIntValue()))
                        + ":\nwhile this TraitRequirement has the Value Type :"
                        + (this.HasStringValue ? ("String: value :" + this.GetStringValue()) : ("int: value :" + this.GetIntValue()))
                        + ": . #"
            );

            if (methodDebug || TRAIT_REQUIREMENT_CLASS_DEBUG) Debug.Log("#DEBUG# TraitRequirement.compare(Trait :" + trait.GetTitle() + ":) on TR :" + this.title + ":, Value Type :" + (oHasStringValue ? "String" : "INT") + ":. #");
            if (oHasStringValue) //String value
            {
                Assert.IsTrue(this.controlBooleans[1] ^ this.controlBooleans[3], $"#ERROR# TraitRequirement.compare(Trait) has Equals flag :{this.controlBooleans[1]}: and Not Equals Flag :{this.controlBooleans[3]}: These must pass an XOR test and currently do not."); //Neither == or !=
                if (this.controlBooleans[3])
                {
                    if (methodDebug || TRAIT_REQUIREMENT_CLASS_DEBUG) Debug.Log("#DEBUG# TraitRequirement.compare(Trait :" + trait.GetTitle() + ":) on TR :" + this.title + ":, Value Type :" + (oHasStringValue ? "String" : "INT") + ":, Comparison (Not Equals). #");
                    return trait.NotEquals(this.stringValue); // !=
                }
                else
                {

                    if (methodDebug || TRAIT_REQUIREMENT_CLASS_DEBUG) Debug.Log("#DEBUG# TraitRequirement.compare(Trait :" + trait.GetTitle() + ":) on TR :" + this.title + ":, Value Type :" + (oHasStringValue ? "String" : "INT") + ":, Comparison (ValueEquals). #");
                    /*
                    For posterity, I spent a good couple of hours confused by this one.
                    The method Equals is an existing virtual method in the Object class within C#.
                    Attempting to use the Equals method while passing in a string was obviously never going to work.
                    I even knew about this problem before and called the methods within the Action class "ValueEquals()".
                    */
                    return trait.ValueEquals(this.stringValue); //==
                }
            }
            else
            {
                if (this.controlBooleans[3]) //!=
                {
                    return trait.NotEquals(this.intValue);
                }
                else if (this.controlBooleans[0]) // <
                {
                    if (this.controlBooleans[1])
                    {
                        if (methodDebug || TRAIT_REQUIREMENT_CLASS_DEBUG) Debug.Log("#DEBUG# TraitRequirement.compare(Trait :" + trait.GetTitle() + ":) on TR :" + this.title + ":, Value Type :" + (oHasStringValue ? "String" : "INT") + ":, Comparison (Meets Maximum). #");
                        return trait.MeetsMaximum(this.intValue); // <=
                    }
                    else
                    {
                        if (methodDebug || TRAIT_REQUIREMENT_CLASS_DEBUG) Debug.Log("#DEBUG# TraitRequirement.compare(Trait :" + trait.GetTitle() + ":) on TR :" + this.title + ":, Value Type :" + (oHasStringValue ? "String" : "INT") + ":, Comparison (Below Maximum). #");
                        return this.BelowMaximum(this.intValue); // <
                    }
                }
                else if (this.controlBooleans[2]) // >
                {
                    if (this.controlBooleans[1])
                    {

                        if (methodDebug || TRAIT_REQUIREMENT_CLASS_DEBUG) Debug.Log("#DEBUG# TraitRequirement.compare(Trait :" + trait.GetTitle() + ":) on TR :" + this.title + ":, Value Type :" + (oHasStringValue ? "String" : "INT") + ":, Comparison (Meets Minimum). #");
                        return trait.MeetsMinimum(this.intValue); // >=
                    }
                    else
                    {

                        if (methodDebug || TRAIT_REQUIREMENT_CLASS_DEBUG) Debug.Log("#DEBUG# TraitRequirement.compare(Trait :" + trait.GetTitle() + ":) on TR :" + this.title + ":, Value Type :" + (oHasStringValue ? "String" : "INT") + ":, Comparison (Exceeds Minimum). #");
                        return trait.ExceedsMinimum(this.intValue); // >
                    }
                }
                else if (this.controlBooleans[1]) // ==
                {

                    if (methodDebug || TRAIT_REQUIREMENT_CLASS_DEBUG) Debug.Log("#DEBUG# TraitRequirement.compare(Trait :" + trait.GetTitle() + ":) on TR :" + this.title + ":, Value Type :" + (oHasStringValue ? "String" : "INT") + ":, Comparison (ValueEquals). #");
                    return trait.ValueEquals(this.intValue);
                }
                else
                {

                    if (methodDebug || TRAIT_REQUIREMENT_CLASS_DEBUG) Debug.Log("#DEBUG# TraitRequirement.compare(Trait :" + trait.GetTitle() + ":) on TR :" + this.title + ":, Value Type :" + (oHasStringValue ? "String" : "INT") + ":, Comparison (Invalid Typeif (methodDebug) ). #");
                    return false;
                }
            }

        }
        public static bool[] CopyDefaultBooleans()
        {
            bool[] output = new bool[defaultBooleans.Length];
            for (int i = 0; i < output.Length; i++)
                output[i] = defaultBooleans[i];
            return output;
        }
        public static bool[] CopyDefaultBooleans(bool hideFlag)
        {
            bool[] output = new bool[defaultBooleans.Length];
            for (int i = 0; i < output.Length; i++)
                output[i] = defaultBooleans[i];
            output[4] = hideFlag;
            return output;
        }

        private void SetLEG(bool[] controlBooleans)
        {
            if (TRAIT_REQUIREMENT_CLASS_DEBUG) Debug.Log("#DEBUG# TraitRequirement.setLEG(boolean [" + LasciiBasicTypeUtilities.FlagsAsDigits(controlBooleans) + "]) - TR Created. ");
            Assert.IsTrue(controlBooleans.Length == 5);
            /* /		else if (controlBooleans[0] && controlBooleans[2])
            //		{
            //			this.applyDefaultBooleans(controlBooleans);
            //		}*/
            this.controlBooleans = controlBooleans;
            if (TRAIT_REQUIREMENT_CLASS_DEBUG)
            {
                String output = "";
                foreach (bool b in controlBooleans)
                {
                    if (b) output += "1";
                    else output += "0";
                }
                Debug.Log("#DEBUG# TraitRequirement.setLEG(boolean[]). Final is :" + output + ":. #");
            }
        }

        public bool DisplayDispiteComparisonFailure(Trait Trait)
        {
            bool compares = this.Compare(Trait);
            if (compares) return true;
            return !this.controlBooleans[4];
        }
        public bool ShowActionRegardlessOfLackingTrait() { return !this.controlBooleans[4]; }
        public void DEBUG()
        {
            Debug.Log($"#DEBUG# TraitRequirement - Title :{this.title}:\tValue :" + (base.HasStringValue ? this.stringValue : this.intValue) + ": Flags - :" + LasciiBasicTypeUtilities.FlagsAsDigits(this.controlBooleans) + ": #");
        }
        public string DEBUG(bool unused)
        {
            return ($"#DEBUG# TraitRequirement - Title :{this.title}:\tValue :" + (base.HasStringValue ? this.stringValue : this.intValue) + ": Flags - :" + LasciiBasicTypeUtilities.FlagsAsDigits(this.controlBooleans) + ": #");
        }
    }
}