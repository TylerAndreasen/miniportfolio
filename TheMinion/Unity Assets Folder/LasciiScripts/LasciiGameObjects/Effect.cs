using System;
using UnityEngine;
using UnityEngine.Assertions;
using LasciiUtility;
using LasciiMain;

namespace LasciiGameObjects
{
    public class Effect
    {
        //FIELDS
        private static readonly bool EFFECT_CLASS_DEBUG_TOGGLE = false, EFFECT_CONSTRUCTOR_DEBUG_TOGGLE = false;

        private static readonly String[] validEffectTypes =
        {					//Stars mean implemented*
            "new", 			// Adds a new Trait*
            "remove", 		// Remove existing Trait*
            "floor", 		// Meets some minimum*
            "ceiling", 		// Is not above some maximum*
            "increment", 	// Add the integer value*
            "decrement", 	// Subtract the integer value*
            "force" 		//Forces a value. Useful for resetting*
		};

        private string effectType, title, stringValue = null;
        private int intValue = -1;
        public bool HasStringValue { get; private set; }
        public bool isEffectUsable;
        //CONSTRUCTORS
        private Effect(String effectType, string title)
        {
            if (EFFECT_CLASS_DEBUG_TOGGLE || EFFECT_CONSTRUCTOR_DEBUG_TOGGLE) Debug.Log("#DEBUG# private Effect(String :" + effectType + ":, String :" + title + ":) - Creating Effect. #");
            this.title = title;
            if (LasciiBasicTypeUtilities.Contains(validEffectTypes, effectType))
            {
                this.effectType = effectType;
                this.isEffectUsable = true;
                if (EFFECT_CLASS_DEBUG_TOGGLE || EFFECT_CONSTRUCTOR_DEBUG_TOGGLE) Debug.Log("#DEBUG# private Effect(String :" + this.effectType + ":, String :" + this.title + ":) - Effect of a valid type :" + effectType + ":. #");
            }
            else
            {

                this.effectType = "none";
                this.isEffectUsable = false;
                if (EFFECT_CLASS_DEBUG_TOGGLE || EFFECT_CONSTRUCTOR_DEBUG_TOGGLE) Debug.Log("#DEBUG# private Effect(String :" + this.effectType + ":, String :" + this.title + ":) - Effect of an invalid type :" + effectType + ":. #");
            }
        }
        public Effect(String effectType, String title, int intValue) : this(effectType, title)
        {
            this.intValue = intValue;
            if (EFFECT_CLASS_DEBUG_TOGGLE || EFFECT_CONSTRUCTOR_DEBUG_TOGGLE) Debug.Log("#DEBUG# Effect(String :" + effectType + ":, String :" + title + ":, int :" + intValue + ":) - Creating Effect. #");
        }
        public Effect(String effectType, String title, String stringValue) : this(effectType, title)
        {
            this.stringValue = stringValue;
            this.HasStringValue = true;
            if (EFFECT_CLASS_DEBUG_TOGGLE || EFFECT_CONSTRUCTOR_DEBUG_TOGGLE) Debug.Log("#DEBUG# Effect(String :" + this.effectType + ":, String :" + this.title + ":, String :" + this.stringValue + ":) - Creating Effect. #");
        }
        //PROPERTIES
        //METHODS
        public void DEBUG()
        {
            String output = "Effect Type :" + this.effectType + ":\n\t\t\t\tEffect Title :" + this.title + ":";
            output += "\n\t\t\t\tEffect Value Type :" + (this.HasStringValue ? "String" : "int") + ":";
            output += "\n\t\t\t\tEffect Value :" + (this.isEffectUsable ? (this.HasStringValue ? this.stringValue : this.intValue) : "Effect Unusable, See Listed Types in Effect.cs.") + ":";
            Debug.Log(output);
        }
        public string DEBUG(bool unused)
        {
            String output = "Effect Type :" + this.effectType + ":\n\t\t\t\tEffect Title :" + this.title + ":";
            output += "\n\t\t\t\tEffect Value Type :" + (this.HasStringValue ? "String" : "int") + ":";
            output += "\n\t\t\t\tEffect Value :" + (this.isEffectUsable ? (this.HasStringValue ? this.stringValue : this.intValue) : "Effect Unusable, See Listed Types in Effect.cs.") + ":";
            return output;
        }
        public bool IsEffectUsable()
        {
            return this.isEffectUsable;
        }
        public String GetEffectType() { return this.effectType; }
        public String GetTitle() { return this.title; }
        public int GetIntValue()
        {
            Assert.IsTrue(this.isEffectUsable);
            Assert.IsTrue(this.HasStringValue);
            return this.intValue;
        }
        public String GetStringValue()
        {
            Assert.IsTrue(this.isEffectUsable);
            Assert.IsTrue(!this.HasStringValue);
            return this.stringValue;

        }
        public static bool IsEffectTypeValid(String key)
        {
            return LasciiBasicTypeUtilities.Contains(validEffectTypes, key);
        }
        public bool ApplyEffect(bool overwrite)
        {
            bool methodDebug = true;
            if (methodDebug || EFFECT_CLASS_DEBUG_TOGGLE)
                Debug.Log($"#DEBUG# Begin Applying - Effect({this.effectType}) Title :{this.title}:, Value :{(this.HasStringValue ? this.stringValue : this.intValue)}: . #");


            /**
		     * 		NEW
		     *		NEW
		     *		NEW
		     */

            if (this.effectType == validEffectTypes[0]) //"new"
            {
                if (methodDebug) Debug.Log($"#DEBUG# Effect of Type :{this.effectType}:. #");
                if (!PlayerTraits.HasTraitCalled(this.title))// || overwrite)
                {
                    if (this.HasStringValue)
                    {
                        bool flag = PlayerTraits.PushToArrayList(new Trait(this.title, this.stringValue));
                        if (methodDebug)
                        Debug.Log($"#DEBUG# Effect.ApplyEffect({overwrite}) Effect({this.effectType}) - Pushed Trait(String :{this.title}:, String :{this.stringValue}:) - Call was {(flag ? "SUCCESSFUL" : "NOT successful")}. #");
                        return true;
                    }
                    else
                    {
                        bool flag = PlayerTraits.PushToArrayList(new Trait(this.title, this.intValue));
                        if (methodDebug)
                        Debug.Log($"#DEBUG# Effect.ApplyEffect({overwrite}) Effect({this.effectType}) - Pushed Trait(String  :{this.title}:, int :{this.intValue}:)  - Call was {(flag ? "SUCCESSFUL" : "NOT successful")}. #");
                        return true;
                    }
                }
                else
                {
                    if (methodDebug || EFFECT_CLASS_DEBUG_TOGGLE)
                    Debug.Log($"#ERROR# Effect.ApplyEffect(boolean overwrite :{overwrite}:) - Failed to Apply Effect({this.effectType}). [Player has Effect of matching Title :{PlayerTraits.HasTraitCalled(this.title)}:] . #");
                    return false;
                }
            }

            /**
		 * 		REMOVE
		 *		REMOVE
		 *		REMOVE
		 */

            else if (this.effectType == (validEffectTypes[1])) // "remove"
            {
                if (methodDebug) Debug.Log($"#DEBUG# Effect of Type :{this.effectType}:. #");
                if (PlayerTraits.HasTraitCalled(this.title))
                {
                    if (this.HasStringValue)
                    {
                        bool flag = PlayerTraits.RemoveFromArrayList(new Trait(this.title, this.stringValue));
                        if (methodDebug) Debug.Log($"#DEBUG# Effect.ApplyEffect({overwrite}) Effect({this.effectType}) Removed Trait(String :{this.title}:, String :{this.stringValue}:) - Call was {(flag ? "SUCCESSFUL" : "NOT successful")}. #");
                        return true;
                    }
                    else
                    {
                        PlayerTraits.RemoveFromArrayList(new Trait(this.title, this.intValue));
                        if (methodDebug) Debug.Log($"#DEBUG# Effect.ApplyEffect({overwrite}) Effect({this.effectType}) Removed Trait(String :{this.title}. int :{this.intValue}:) Successfully. #");
                        return true;
                    }
                }
                else
                {
                    if (methodDebug || EFFECT_CLASS_DEBUG_TOGGLE)
                        Debug.Log($"#ERROR# Effect.ApplyEffect(boolean overwrite :{overwrite}:) - Failed to Apply Effect({this.effectType}). [Player has Effect of matching Title :{PlayerTraits.HasTraitCalled(this.title)}:] . #");
                    return false;
                }
            }

            /**
             * 		FORCE
             *		FORCE
             *		FORCE
             */

            else if (this.effectType == (validEffectTypes[6])) // "force"
            {
                if (methodDebug) Debug.Log("#DEBUG# Effect of Type :" + this.effectType + ":, Title :" + this.title + ":");
                if (PlayerTraits.HasTraitCalled(this.title))
                {
                    if (this.HasStringValue)
                    {
                        bool flag =  PlayerTraits.ForceTraitStringValue(this.title, this.stringValue);
                        if (methodDebug) 
                        Debug.Log($"#DEBUG# Effect.ApplyEffect({overwrite}) Effect({this.effectType}) Pushed Trait({this.title}, {this.stringValue}) - Call was {(flag ? "SUCCESSFUL" : "NOT successful")}. #");
                        return true;
                    }
                    else
                    {
                        bool flag = PlayerTraits.ForceTraitIntValue(this.title, this.intValue);
                        if (methodDebug)
                        Debug.Log($"#DEBUG# Effect.ApplyEffect({overwrite}) Effect:{this.effectType}: Pushed Trait({this.title}, {this.intValue}) - Call was {(flag ? "SUCCESSFUL" : "NOT successful")}. #");
                        return true;
                    }
                }
                else
                {
                    if (methodDebug) Debug.Log("#ERROR# Failed to find Trait with Title :" + this.title + ":");
                    return false;
                }
            }


            else if (this.HasStringValue)
            {
                if (methodDebug) Debug.Log("#DEBUG# Effect.ApplyEffect(" + overwrite + ") - Effect{" + this.effectType + "} Failed to Apply Effect as this Effect has Value Type :" + (this.HasStringValue ? "String" : "int") + ":. #");
                return false;
            }

            /**
             * 		FLOOR
             *		FLOOR
             *		FLOOR
             */

            else if (this.effectType == (validEffectTypes[2])) // "floor"
            {
                if (methodDebug) Debug.Log("#DEBUG# Effect of Type :" + this.effectType + ":. #");
                if (PlayerTraits.HasTraitCalled(this.title))
                {
                    Trait subject = PlayerTraits.GetTraitCalled(this.title);
                    if (this.HasStringValue)
                    {
                        if (methodDebug) Debug.Log("#DEBUG# Effect.ApplyEffect(" + overwrite + ") Effect{" + this.effectType + "}  Failed to Floor Trait(String :" + subject.GetTitle()
                        + ":, Value (String) :" + subject.GetStringValue()
                        + ":, Has String Value (boolean) :" + subject.HasStringValue + ":). #");
                        return false;
                    }
                    else
                    {
                        PlayerTraits.FloorTraitValue(this.title, this.intValue);
                        if (methodDebug) Debug.Log("#DEBUG# Effect.ApplyEffect(" + overwrite + ") Effect{" + this.effectType + "} Floored Trait(String :" + subject.GetTitle() + ", int :" + subject.GetIntValue() + ":) Successfully. #");
                        return true;
                    }
                }
                else
                {
                    if (methodDebug || EFFECT_CLASS_DEBUG_TOGGLE)
                        Debug.Log("#ERROR# Effect.ApplyEffect(boolean overwrite :" + overwrite + ":) - Failed to Apply Effect{" + this.effectType + "}. [Player has Effect of matching Title :" + PlayerTraits.HasTraitCalled(this.title) + ":] . #");
                    return false;
                }
            }

            /**
             * 		CEILING
             *		CEILING
             *		CEILING
             */

            else if (this.effectType == (validEffectTypes[3])) // "ceiling"
            {
                if (methodDebug) Debug.Log("#DEBUG# Effect of Type :" + this.effectType + ":. #");
                if (PlayerTraits.HasTraitCalled(this.title))
                {
                    Trait subject = PlayerTraits.GetTraitCalled(this.title);
                    if (this.HasStringValue)
                    {
                        if (methodDebug) Debug.Log("#DEBUG# Effect.ApplyEffect(" + overwrite + ") Effect{" + this.effectType + "}  Failed to Apply Ceiling Trait(String :" + subject.GetTitle()
                        + ":, Value (String) :" + subject.GetStringValue()
                        + ":, Has String Value (boolean) :" + subject.HasStringValue + ":). #");
                        return false;
                    }
                    else
                    {
                        PlayerTraits.CeilTraitValue(this.title, this.intValue);
                        if (methodDebug) Debug.Log("#DEBUG# Effect.ApplyEffect(" + overwrite + ") Effect{" + this.effectType + "} Applied Ceiling to Trait(String :" + subject.GetTitle() + ", int :" + subject.GetIntValue() + ":) Successfully. #");
                        return true;
                    }
                }
                else
                {
                    if (methodDebug || EFFECT_CLASS_DEBUG_TOGGLE)
                        Debug.Log("#ERROR# Effect.ApplyEffect(boolean overwrite :" + overwrite + ":) - Failed to Apply Effect{" + this.effectType + "}. [Player has Effect of matching Title :" + PlayerTraits.HasTraitCalled(this.title) + ":] . #");
                    return false;
                }
            }

            /**
             * 		INCREMENT
             *		INCREMENT
             *		INCREMENT
             */

            if (this.effectType == (validEffectTypes[4])) // "increment"
            {
                if (methodDebug) Debug.Log("#DEBUG# Effect of Type :" + this.effectType + ":. #");
                if (PlayerTraits.HasTraitCalled(this.title))
                {
                    Trait subject = PlayerTraits.GetTraitCalled(this.title);
                    if (this.HasStringValue)
                    {
                        if (methodDebug) Debug.Log("#DEBUG# Effect.ApplyEffect(" + overwrite + ") Effect{" + this.effectType + "}  Failed to Increment Trait(String :" + subject.GetTitle() + ":, Value (String) :" + subject.GetStringValue() + ":, Has String Value (boolean) :" + subject.HasStringValue + ":). #");
                        return false;
                    }
                    else
                    {
                        PlayerTraits.IncrementTraitIntValue(this.title, this.intValue);
                        if (methodDebug) Debug.Log("#DEBUG# Effect.ApplyEffect(" + overwrite + ") Effect{" + this.effectType + "} Incremented Trait(String :" + subject.GetTitle() + ", int :" + subject.GetIntValue() + ":) Successfully. #");
                        return true;
                    }
                }
                else
                {
                    if (methodDebug || EFFECT_CLASS_DEBUG_TOGGLE)
                        Debug.Log("#ERROR# Effect.ApplyEffect(boolean overwrite :" + overwrite + ":) - Failed to Apply Effect{" + this.effectType + "}. [Player has Effect of matching Title :" + PlayerTraits.HasTraitCalled(this.title) + ":] . #");
                    return false;
                }
            }

            /**
             * 		DECREMENT
             *		DECREMENT
             *		DECREMENT
             */

            if (this.effectType == (validEffectTypes[5])) // "decrement"
            {
                if (methodDebug) Debug.Log("#DEBUG# Effect of Type :" + this.effectType + ":. #");
                if (PlayerTraits.HasTraitCalled(this.title))
                {
                    Trait subject = PlayerTraits.GetTraitCalled(this.title);
                    if (this.HasStringValue)
                    {
                        if (methodDebug) Debug.Log("#DEBUG# Effect.ApplyEffect(" + overwrite + ") Effect{" + this.effectType + "}  Failed to Decrement Trait(String :" + subject.GetTitle() + ":, Value (String) :" + subject.GetStringValue() + ":, Has String Value (boolean) :" + subject.HasStringValue + ":). #");
                        return false;
                    }
                    else
                    {
                        PlayerTraits.DecrementTraitIntValue(this.title, this.intValue);
                        if (methodDebug) Debug.Log("#DEBUG# Effect.ApplyEffect(" + overwrite + ") Effect{" + this.effectType + "} Decremented Trait(String :" + subject.GetTitle() + ", int :" + subject.GetIntValue() + ":) Successfully. #");
                        return true;
                    }
                }
                else
                {
                    if (methodDebug || EFFECT_CLASS_DEBUG_TOGGLE)
                        Debug.Log("#ERROR# Effect.ApplyEffect(boolean overwrite :" + overwrite + ":) - Failed to Apply Effect{" + this.effectType + "}. [Player has Effect of matching Title :" + PlayerTraits.HasTraitCalled(this.title) + ":] . #");
                    return false;
                }
            }

            /**
             * 		END
             *		END
             *		END
             */

            else
            {
                if (methodDebug || EFFECT_CLASS_DEBUG_TOGGLE)
                    Debug.Log("#ERROR# Effect.ApplyEffect(boolean overwrite :" + overwrite + ":) - Failed to Apply Effect{" + this.effectType + "}. [Player has Effect of matching Title :" + PlayerTraits.HasTraitCalled(this.title) + ":] . #");
                return false;
            }
        }
    }
}