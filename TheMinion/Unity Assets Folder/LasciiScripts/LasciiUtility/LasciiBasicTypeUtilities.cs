using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using LasciiGameObjects;

namespace LasciiUtility
{
    class LasciiBasicTypeUtilities
    {
        //FIELDS
        public static readonly char[] DIGITS_AS_CHARS = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        public static readonly string[] DIGITS_AS_STRINGS = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        
        //METHODS
        public static string[] CleanEmptyValues(string[] input)
        {
            bool methodDebug = false;
            if (methodDebug) Debug.Log($"#DEBUG# LasciiBasicTypeUtilities.CleanEmptyValues(string :{input}:) - Called. #");
            List<string> bucket = new List<string>();
            foreach (string line in input)
            {
                if (line != "") bucket.Add(line);
            }
            string[] output = new string[bucket.Count];
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = bucket[i];
            }
            return output;
        }
        public static string FlagsAsDigits(bool[] Input)
        {
            string output = "";
            foreach (bool b in Input)
            {
                output += (b ? '1' : '0');
            }
            return output;
        }
        public static bool[] SubArray(bool[] bank, int inLength)
        {
            if (inLength >= bank.Length) return bank;
            bool[] output = new bool[inLength];
            for (int i = 0; i < inLength; i++)
            {
                output[i] = bank[i];
            }
            return output;
        }
        public static int ParseInt(string line)
        {
            bool methodDebug = false;
            if (methodDebug) Debug.Log("#DEBUG# LasciiBasicTypeUtilities.parseInt(string :"+line+":) Begin Parse. #");
            string number = "";
            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (Contains(DIGITS_AS_CHARS, c)) number += c;
            }
            if (methodDebug) Debug.Log("#DEBUG# LasciiBasicTypeUtilities.parseInt(string :"+line+") Parsed Number :"+number+":. #");
            if (number == "")
            {
                if (methodDebug) Debug.Log("#DEBUG#  LasciiBasicTypeUtilities.parseInt(string :"+line+":) Returning Integer Maximum. #");
                return Int32.MaxValue;
            } else
            {
                int output = Int32.Parse(number, System.Globalization.NumberStyles.Any);
                if (methodDebug) Debug.Log("#DEBUG# LasciiBasicTypeUtilities.parseInt(string :"+line+":) Returning :"+output+":. #");
                return output;
            }
        }
        public static int[] ParseInts(string[] lines)
        {
            List<int> calc = new List<int>();
            foreach (string line in lines)
            {
                int tester = ParseInt(line);
                if (tester != Int32.MaxValue) calc.Add(tester);
            }
            int[] output = new int[calc.Count];
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = calc[i];
            }
            return output;
        }
        public static bool Contains(string[] bank, string key)
        {
            foreach (string c in bank)
            {
                if (c == key) return true;
            }
            return false;
        }
        public static bool Contains(char[] bank, char key)
        {
            foreach (char c in bank)
            {
                if (c == key) return true;
            }
            return false;
        }
        public static int IndexOf(string[] bank, string key)
        {
            for (int i = 0; i < bank.Length; i++)
            {
                if (bank[i] == key) return i;
            }
            return -1;
        }
        public static int Total(bool[] bank)
        {
            int output = 0;
            foreach (bool b in bank)
            {
                if (b) output++;
            }
            return output;
        }
        public static string ScrubParan(string line)
        {
            bool methodDebug = false;
            if (methodDebug) Debug.Log($"#DEBUG# LasciiBasicTypeUtilities.ScrubParan(string) INPUT :{line}: - Length :{line.Length}:. #");
            line = line.Trim();
            if (methodDebug) Debug.Log($"#DEBUG# LasciiBasicTypeUtilities.ScrubParan(string) No Whitespace :{line}: - Length :{line.Length}:. #");
            if (line[0] == '(')
            {
                line = line.Substring(1); //Remove the '(' only if it is present.
            }
            if (line[line.Length-1] == ')')
            {
                line = line.Substring(0,line.Length-1); //Remove the '(' only if it is present.

            }
            
            if (methodDebug) Debug.Log($"#DEBUG# LasciiBasicTypeUtilities.ScrubParan(string) OUTPUT :{line}:");
            return line;
        }
        public static string ScrubBrackets(string line)
        {
            bool methodDebug = false;
            if (methodDebug) Debug.Log($"#DEBUG# LasciiBasicTypeUtilities.ScrubBrackets(string) INPUT :{line}: - Length :{line.Length}:. #");
            line = line.Trim();
            if (methodDebug) Debug.Log($"#DEBUG# LasciiBasicTypeUtilities.ScrubBrackets(string) No Whitespace :{line}: - Length :{line.Length}:. #");
            //if (methodDebug) Debug.Log($"#DEBUG# LasciiBasicTypeUtilities.ScrubBrackets(string) INPUT :{line}:");
            if (line[0] == '[')
            {
                line = line.Substring(1); //Remove the '(' only if it is present.
            }
            if (line[line.Length-1] == ']')
            {
                line = line.Substring(0,line.Length-1); //Remove the '(' only if it is present.
            }
            if (methodDebug) Debug.Log($"#DEBUG# LasciiBasicTypeUtilities.ScrubBrackets(string) OUTPUT :{line}:");
            return line;
        }
    }
}