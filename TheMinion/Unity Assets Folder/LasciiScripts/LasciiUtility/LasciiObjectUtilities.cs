using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LasciiGameObjects;

namespace LasciiUtility
{
    public class LasciiObjectUtilities
    {
        public static readonly Trait exists = new Trait("exists", 1), keyTrait = new Trait("playerHasKey", 1);
        public static readonly TraitRequirement existsTR = new TraitRequirement("exists", 1, new bool[] { false, true, false, false, false });

        public static Object[] ShortenToMax(Object[] bank, int max)
        {
            if (bank.Length < max) return bank;
            Object[] output = new Object[max];
            for (int i = 0; i < max; i++)
                output[i] = bank[i];
            return output;
        }
        public static bool All_TRs_Show_On_Lacking(TraitRequirement[] bank)
        {
            foreach (TraitRequirement tr in bank)
            {
                if (tr == null) continue;
                if (!tr.ShowActionRegardlessOfLackingTrait()) return false;
            }
            return true;
        }
        public static Page[] CombineArrays(Page[] first, Page[] second)
        {
            List<Page> bucket = new List<Page>();
            foreach (Page p in first) bucket.Add(p);
            foreach (Page p in second) bucket.Add(p);
            Page[] output = new Page[bucket.Count];
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = bucket[i];
            }
            return output;
        }
    }
}