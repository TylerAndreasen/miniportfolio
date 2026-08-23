using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LasciiGameObjects;
using LasciiUtility;

namespace LasciiMain
{
    class PlayerTraits
    {
        //FIELDS
        private static bool PLAYER_TRAITS_CLASS_DEBUG = false;
        private static Trait[] traits = new Trait[] 
			{
                LasciiObjectUtilities.exists,
                new Trait("Stave",0),
                new Trait("Chest",0),                  
                new Trait("Feather",0),
                new Trait("Document",0),
                new Trait("Saccade",0),
                new Trait("observedFirePlace",0)
            };
        //METHODS
        private static List<Trait> allTraits = new List<Trait>();
        public static void FillArrayList()
        {
            if (PLAYER_TRAITS_CLASS_DEBUG) Debug.Log("#DEBUG# PlayerTraits.FillArrayList() Called. #");
            foreach (Trait t in traits) allTraits.Add(t);
        }
        public static bool PushToArrayList(Trait input)
        {
            if (PLAYER_TRAITS_CLASS_DEBUG) Debug.Log("#DEBUG# PlayerTraits.PushToArrayList() Called. #");
            if (allTraits.Contains(input)) return false;
            else
            {
                allTraits.Add(input);
                return true;
            }
        }
        public static void DEV_DisplayTraits()
        {
            String output = "Traits in the [allTraits] Field";
            foreach (Trait t in allTraits)
            {
                output += "\nTrait - (String) :"+t.GetTitle()+" - Value :("+(t.HasStringValue ? "String) "+t.GetStringValue() : "Int) "+t.GetIntValue())+": ";
            }
            Debug.Log(output);
        }
        public static bool RemoveFromArrayList(Trait output)
        {
            bool methodDebug = true;
            if (methodDebug || PLAYER_TRAITS_CLASS_DEBUG) Debug.Log($"#DEBUG# PlayerTraits.RemoveTraitFromArrayList() Called, Attempting to Remove Trait with Title :{output.GetTitle()}: #");
            foreach (Trait t in allTraits)
            {
                String a = t.GetTitle(), b = output.GetTitle();
                if (methodDebug || PLAYER_TRAITS_CLASS_DEBUG) Debug.Log($"#DEBUG# PlayerTraits.RemoveTraitFromArrayList() Comparing Outgoing Trait -Title {b} to Trait -Title {a} . #");
                if ( a == b )
                {
                    if (methodDebug || PLAYER_TRAITS_CLASS_DEBUG) Debug.Log($"#DEBUG# PlayerTraits.RemoveTraitFromArrayList() - Found Trait with Title :{a}:, Removeing. #");
                    allTraits.Remove(t);
                    break;
                }
            }
            if (methodDebug || PLAYER_TRAITS_CLASS_DEBUG) Debug.Log($"#DEBUG# PlayerTraits.RemoveTraitFromArrayList() Failed to Remove Trait with Title {output.GetTitle()} . #");
            return false;
        }
        public static bool ForceTraitIntValue(String key, int value)
        {
            if (PLAYER_TRAITS_CLASS_DEBUG) Debug.Log("#DEBUG# PlayerTraits.ForceTraitIntValue() Called. #");
            foreach (Trait t in allTraits)
            {
                if (t.GetTitle() == key)
                {
                    bool overrideSuccessful = t.OverrideNewIntValue(value);
                    if (PLAYER_TRAITS_CLASS_DEBUG) Debug.Log($"#DEBUG# PlayerTraits.ForceTraitIntValue() Overriding Trait with the value :{value}:, was {(overrideSuccessful ? "SUCCESSFUL" : "NOT successful")}. #");
                    return overrideSuccessful;
                }
            }
            return false;
        }
        public static bool ForceTraitStringValue(String key, String value)
        {
            if (PLAYER_TRAITS_CLASS_DEBUG) Debug.Log("#DEBUG# PlayerTraits.ForceTraitStringValue() Called. #");
            foreach (Trait t in allTraits)
            {
                if (t.GetTitle() == key)
                {
                    return t.OverrideNewStringValue(value);
                }
            }
            return false;
        }
        public static void IncrementTraitIntValue(String key)
        {
            if (PLAYER_TRAITS_CLASS_DEBUG) Debug.Log("#DEBUG# PlayerTraits.IncrementTraitIntValue() Called. #");
            foreach (Trait t in allTraits)
            {
                if ((t.GetTitle() == key) && !t.HasStringValue)
                {
                    t.IncrementTraitIntValue();
                    return;
                }
            }
        }
        public static void IncrementTraitIntValue(String key, int value)
        {
            if (PLAYER_TRAITS_CLASS_DEBUG) Debug.Log("#DEBUG# PlayerTraits.IncrementTraitIntValue() Called. #");
            foreach (Trait t in allTraits)
            {
                if ((t.GetTitle() == key) && !t.HasStringValue)
                {
                    t.IncrementTraitIntValue(value);
                    return;
                }
            }
        }
        
        public static void DecrementTraitIntValue(String key)
        {
            if (PLAYER_TRAITS_CLASS_DEBUG) Debug.Log("#DEBUG# PlayerTraits.DecrementTraitIntValue() Called. #");
            foreach (Trait t in allTraits)
            {
                if ((t.GetTitle() == key) && !t.HasStringValue)
                {
                    t.DecrementTraitIntValue();
                    return;
                }
            }
        }

        public static void DecrementTraitIntValue(String key, int value)
        {
            if (PLAYER_TRAITS_CLASS_DEBUG) Debug.Log("#DEBUG# PlayerTraits.DecrementTraitIntValue() Called. #");
            foreach (Trait t in allTraits)
            {
                if ((t.GetTitle() == key) && !t.HasStringValue)
                {
                    t.DecrementTraitIntValue(value);
                    return;
                }
            }
        }
        public static void FloorTraitValue(String key, int floor)
        {
            if (PLAYER_TRAITS_CLASS_DEBUG) Debug.Log("#DEBUG# PlayerTraits.FloorTraitValue() Called. #");
            foreach (Trait t in allTraits)
            {
                if ((t.GetTitle() == key) && !t.HasStringValue)
                {
                    int current = t.GetIntValue();
                    if (current >= floor) return;
                    t.PushIntValue(floor);
                }
            }
        }
        public static void CeilTraitValue(String key, int ceil)
        {
            if (PLAYER_TRAITS_CLASS_DEBUG) Debug.Log("#DEBUG# PlayerTraits.CeilTraitValue() Called. #");
            foreach (Trait t in allTraits)
            {
                if ((t.GetTitle() == key) && !t.HasStringValue)
                {
                    int current = t.GetIntValue();
                    if (current <= ceil) return;
                    t.PushIntValue(ceil);
                }
            }
        }

        public static bool HasTraitCalled(String key)
        {
            bool methodDebug = false;
            if (methodDebug || PLAYER_TRAITS_CLASS_DEBUG) Debug.Log($"#DEBUG# PlayerTraits.HasTraitCalled(String :{key}:) - Called");
            foreach (Trait t in allTraits)
            {
                String title = t.GetTitle(); 
                if (methodDebug || PLAYER_TRAITS_CLASS_DEBUG) Debug.Log($"#DEBUG# PlayerTraits.HasTraitCalled(String :{key}:) - Matches Title :{title}: - :{key == title}:. #");
                if (title == key) return true;
            }
            if (methodDebug || PLAYER_TRAITS_CLASS_DEBUG) Debug.Log($"#DEBUG# PlayerTraits.HasTraitCalled(String :{key}:) - PlayerTraits does not contain a trait with the title :{key}:. #");
            return false;
        }
        public static Trait GetTraitCalled(String key)
        {
            /*
            A note about this method, or rather the original Java implementation of this method.
            In this class there are two lists of Trait Objects. One called "traits" (an Array), and one called "allTraits" (a List).
            I erronously typed "traits" where "allTraits" was in the Java version. This cost me easily 10+ hours of attempting to debug and add adding a ton of print statements trying to find this.
            I intent to leave comments in any future version of this code about this issue to both humble, remind, and reinforce this event, as it should no have occurred.
            I should have called these fields significantly different things, and should not have had the possibility of messing this up.
            */
            foreach (Trait t in allTraits)
            {
                if (t.GetTitle() == key) return t;
            }
            return LasciiObjectUtilities.exists;
        }
    }
}