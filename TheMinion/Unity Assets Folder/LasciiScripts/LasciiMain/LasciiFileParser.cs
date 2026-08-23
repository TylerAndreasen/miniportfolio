using System;
using UnityEngine;
using UnityEngine.Assertions;
using LasciiGameObjects;
using LasciiUtility;

namespace LasciiMain
{
    class LasciiFileParser
    {

        //FIELDS
        //private static Page[] allPages;
        //METHODS
        //public static void FlushTempPagePointers() { allPages = null; }


        public static Directory[] Page_Directory_Integration(DirectoryPrecursor[] preDirs, Page[] pages, out Page[] pagesReturn)
        {
            bool methodDebug = false;
            if (methodDebug) Debug.Log($"#DEBUG# LasciiFileParser.Page_Directory_Integration(DP[] preDirs :{preDirs.Length}:, Page[] pages :{pages.Length}:).#");
            Assert.IsTrue(preDirs.Length == pages.Length, $"#ERROR# LasciiFileParser.page_Directory_Integration() - DirectoryPrecursor Count :{preDirs.Length}: and Page Object Count :{pages.Length}: do not match. #");
            Directory[] output = new Directory[preDirs.Length];

            for (int i = 0; i < preDirs.Length; i++)
            {
                DirectoryPrecursor direc = preDirs[i];
                int direcIndex = direc.GetIndex();
                Page assigned = pages[direcIndex];
                LasciiGameObjects.Action[] actions = direc.GetActions();
                if (methodDebug) Debug.Log((actions[i] == null ? $"Actions[{i} are NULL" : $"Actions[{i}] are NOT NULL"));
                Directory instance = new Directory(assigned, direcIndex, actions);
                assigned.PushDirectory(instance);
                output[i] = instance;
            }
            pagesReturn = pages;
            return output;
        }

        public static Page[] ParsePageFile(String[] fileLines, string fileLoc)
        {
            bool methodDebug = false;
            if (methodDebug) Debug.Log($"#DEBUG# LasciiFileParser.ParsePageFile(String[] fileLines :{fileLines.Length}:, String :{fileLoc}:) - Called. #");
            Assert.IsTrue(fileLines.Length > 0);
            Page[] output = new Page[fileLines.Length];
            for (int i = 0; i < fileLines.Length; i++)
            {
                if (methodDebug) Debug.Log($"#DEBUG# LasciiFileParser.ParsePageFile(String[] fileLines :{fileLines.Length}:, String :{fileLoc}:) - Begin parsing page line with {fileLines[i]}");
                output[i] = ParseLineToPage(fileLines[i], fileLoc);
                if (methodDebug) Debug.Log($"#DEBUG# LasciiFileParserLasciiFileParser.ParsePageFile(String[] fileLines :{fileLines.Length}:, String :{fileLoc}:) - Is output[{i}] null :{output[i] == null}: ");
            }
            return output;
        }

        private static Page ParseLineToPage(String lineIn, string fileLoc)
        {
            bool methodDebug = false;
            if (methodDebug) Debug.Log($"#DEBUG# LasciiFileParser.ParseLineToPage(String lineIn :{lineIn}:, String :{fileLoc}:) - Called. #");
            String[] segments = LasciiBasicTypeUtilities.CleanEmptyValues(lineIn.Split(":", StringSplitOptions.RemoveEmptyEntries));
            Assert.IsTrue(segments.Length == 2, $"#ERROR# LasciiFileParser.ParseLineToPage(String :{lineIn}:, String :{fileLoc}:) - Insuffecient Data to create Page. Parts of Line = :{segments.Length}:. #");
            String pageIndex = segments[0];
            String[] descParts = LasciiBasicTypeUtilities.CleanEmptyValues(segments[1].Split(")", StringSplitOptions.RemoveEmptyEntries));
            if (methodDebug) Debug.Log($"#DEBUG# LasciiFileParser.ParseLineToPage(String lineIn :{lineIn}:, String :{fileLoc}:) - Calculated there to be a total of {descParts.Length} Description Parts. #");
            for (int i = 0; i < descParts.Length; i++)
            {
                descParts[i] = LasciiBasicTypeUtilities.ScrubParan(descParts[i]);
                if (methodDebug) Debug.Log($"#DEBUG# LasciiFileParser.ParseLineToPage(String lineIn :{lineIn}:, String :{fileLoc}:) - Calculated Description Part [{i}] to be :{descParts[i]}:");
            }
            /* Page output = (Page) ScriptableObject.CreateInstance(typeof(Page));
            output.PushData(descParts, pageIndex); */
            Page output = new Page(descParts, pageIndex);
            return output;
        }

        public static DirectoryPrecursor[] ParseDirectoryFile(String[] fileLines, string fileLoc, Page[] allPages)
        {
            bool methodDebug = false;
            if (methodDebug) Debug.Log($"#DEBUG# LasciiFileParserParseDirectoryFile(String[] fileLines > :{fileLines.Length}:, String :{fileLoc}:)");
            Assert.IsTrue(fileLines.Length > 0, $"#ERROR# LasciiFileParserParseDirectoryFile(String[] fileLines > :{fileLines.Length}:, String :{fileLoc}:) - No file lines passed into method. #");
            DirectoryPrecursor[] output = new DirectoryPrecursor[fileLines.Length];
            for (int i = 0; i < fileLines.Length; i++)
            {
                output[i] = ParseLineToDirectory(fileLines[i], fileLoc, i, allPages);
            }
            return output;
        }
        private static DirectoryPrecursor ParseLineToDirectory(String lineIn, string fileLoc, int directoryIndex, Page[] allPages)
        {
            bool methodDebug = false;
            /**This is used to separate sections of each line based on the ':' char
             * */
            String[] segments = lineIn.Split(":", StringSplitOptions.RemoveEmptyEntries);
            if (methodDebug)
            {
                foreach (String peice in segments) 
                    Debug.Log($"#DEBUG# LasciiFilePrecursor.ParseLineToDirectory(String :{lineIn}:, String :{fileLoc}:, int :{directoryIndex}:) - Directory Line Part (segements[]) :{peice}:. #");
            }
            int selfIndex = LasciiBasicTypeUtilities.ParseInt(segments[0]);
            Assert.IsTrue(selfIndex != Int32.MaxValue, $"#ERROR# LasciiFileParser.ParseLineToDirectory() - File Loaded with Line :{selfIndex}: without Digit in lead position. Lead Position found to be :{segments[0]}: File Location :{fileLoc}:. #");
            Assert.IsTrue(segments.Length >= 2, $"#ERROR# LasciiFileParser.ParseLineToDirectory() - File Loaded with Line :{directoryIndex}: without suffecient data :{segments[0]}:. File Location:{fileLoc}. #");


            LasciiGameObjects.Action[] actionsOut = new LasciiGameObjects.Action[segments.Length - 1];
            if (methodDebug) Debug.Log($"#ERROR# LasciiFilePrecursor.ParseLineToDirectory(String :{lineIn}:, String :{fileLoc}:, int :{directoryIndex}:) - LasciiGameObjects.Action[] actionsOut created with Length :{actionsOut.Length}:. #");

            for (int i = 0; i < actionsOut.Length; i++)
            {
                String[] actionParts = segments[i + 1].Split(";", StringSplitOptions.RemoveEmptyEntries);
                
                if (methodDebug) Debug.Log($"#DEBUG# LasciiFilePrecursor.ParseLineToDirectory(String :{lineIn}:, String :{fileLoc}:, int :{directoryIndex}:) - String[] actionParts created with Length :" + actionParts.Length + ":");

                Assert.IsTrue((actionParts.Length == 3 || actionParts.Length == 4), $"#ERROR# LasciiFilePrecursor.ParseLineToDirectory(String :{lineIn}:, String :{fileLoc}:, int :{directoryIndex}:) - Found :{actionParts.Length}: parts in an LasciiGameObjects.Action definition from file :{fileLoc}:. #");


                String desc = LasciiBasicTypeUtilities.ScrubParan(actionParts[0]);
                if (methodDebug) Debug.Log($"#DEBUG# LasciiFilePrecursor.ParseLineToDirectory(String :{lineIn}:, String :{fileLoc}:, int :{directoryIndex}:) - Action Description :{desc}:. #");
                Page dest;
                /* if (actionParts[1][1] == 'g')
                {
                    dest = LasciiMainClass.gameStarts[LasciiBasicTypeUtilities.ParseInt(LasciiBasicTypeUtilities.ScrubParan(actionParts[1]))];
                }
                else */ dest = allPages[LasciiBasicTypeUtilities.ParseInt(LasciiBasicTypeUtilities.ScrubParan(actionParts[1]))];
                TraitRequirement[] trs = ParseTraitRequirementsFromLine(LasciiBasicTypeUtilities.ScrubBrackets(actionParts[2]), fileLoc);
                bool show = LasciiObjectUtilities.All_TRs_Show_On_Lacking(trs);
                if (actionParts.Length == 4) //Has Effects
                {
                    Debug.Log($"#DEBUG# LasciiFileParser.ParseLineToDirectory() - Begin parse :{actionParts[3]}: into effects. #");
                    Effect[] effectsOut = ParseEffects(actionParts[3], directoryIndex);
                    actionsOut[i] = new LasciiGameObjects.Action(desc, trs, show, effectsOut, dest);
                }
                else
                {
                    actionsOut[i] = new LasciiGameObjects.Action(desc, trs, show, dest);
                }
            }
            return new DirectoryPrecursor(selfIndex, actionsOut);
        }
        private static Effect[] ParseEffects(String lineIn, int directoryIndex)
        {
            bool methodDebug = false;
            if (methodDebug) Debug.Log("#DEBUG# LasciiFileParser.ParseEffects(String :" + lineIn + ": int :" + directoryIndex + ":) - Begin Parsing");
            String effects = LasciiBasicTypeUtilities.ScrubBrackets(lineIn); //No more brackets on the string
            if (methodDebug) Debug.Log($"#DEBUG# LasciiFileParser.ParseEffects(String :{lineIn}: int :{directoryIndex}:) - ?Without Brackets :{effects}:");
            String[] effectList = effects.Split(")", StringSplitOptions.RemoveEmptyEntries); //Split the string into a list
            if (methodDebug)
            {
                Debug.Log("#DEBUG# LasciiFileParser.ParseEffects(String :" + lineIn + ":, int :" + directoryIndex + ":) - Showing Split of String intput [" + effectList.Length + "]");
                foreach (String e in effectList)
                    Debug.Log("\t\t\t" + e);
            }
            for (int i = 0; i < effectList.Length; i++)
            {
                effectList[i] = LasciiBasicTypeUtilities.ScrubParan(effectList[i]); //No more Parans on any element
                                                                                    //if (methodDebug)Debug.Log("\t\t"+effectList[i]);
            }
            Effect[] output = new Effect[effectList.Length];
            for (int i = 0; i < output.Length; i++)
            {
                if (methodDebug)
                {
                    Debug.Log($"#DEBUG# LasciiFileParser.ParseEffects(String :{lineIn}:, int :{directoryIndex}:) - Effect[{i}] - :{effectList[i]}:, Split(',') Result Length :{effectList[i].Split(",", StringSplitOptions.RemoveEmptyEntries).Length}:");
                }
                String[] effectParts = effectList[i].Split(","/*, StringSplitOptions.RemoveEmptyEntries*/);
                if (methodDebug)
                {
                    foreach (String peice in effectParts)
                    {
                        Debug.Log($"#DEBUG# LasciiFileParser.ParseEffects(String :{lineIn}:, int :{directoryIndex}:) - Effect[{i}] Part :{peice}:");
                    }
                }
                bool effectValueIsString = false;

                Assert.IsTrue(effectParts.Length == 3, "#ERROR# LasciiFileParser.ParseEffects(String) - Directory[" + directoryIndex + "] Effect[" + i + "] Total parts of an Effect from file found to be :" + effectParts.Length + ":. Should always be three #");
                Assert.IsTrue(Effect.IsEffectTypeValid(effectParts[0]), "#ERROR# LasciiFileParser.ParseEffects(String) - Attempted to create effect with invalid type :" + effectParts[0] + ": for Directory :" + directoryIndex + ":. See Effect.validEffectTypes[] for full list of valid types. #");
                Assert.IsTrue(effectParts[1] != "", "#ERROR# LasciiFileParser.ParseEffects(String) - Attempted to create Effect with the title:" + effectParts[1] + ": (empty String) for Directory :" + directoryIndex + ":. #");

                effectValueIsString = DetermineEffectValueType(effectParts[2], directoryIndex);
                if (methodDebug) Debug.Log("#DEBUG# LasciiFileParser.ParseEffects(String, int) - Found Effect Value type to be :" + (effectValueIsString ? "String" : "int") + ":. #");
                //Previously, the below statements if & else were one statement which either always passed a String version of effectParts[2] to the constructor, or attempted to use the (conditional ? returnOnTrue : returnOnFalse) syntax, which the constructor/Java/IDE cannot handle, dispite it working in theory.

                if (effectValueIsString)
                {
                    if (methodDebug) Debug.Log("#DEBUG# LasciiFileParser.ParseEffects(String, int) - Creating new Effect with (String :" + effectParts[0] + ":, String:" + effectParts[1] + ":, String:" + effectParts[2] + ":) . #");
                    output[i] = new Effect(effectParts[0], effectParts[1], effectParts[2]);
                }
                else
                {
                    int pointer = LasciiBasicTypeUtilities.ParseInt(effectParts[2]);
                    if (methodDebug) Debug.Log("#DEBUG# LasciiFileParser.ParseEffects(String, int) - Creating new Effect with (String :" + effectParts[0] + ":, String:" + effectParts[1] + ":, >>> INT <<<:" + pointer + ":) . #");
                    output[i] = new Effect(effectParts[0], effectParts[1], pointer);
                }
            }
            return output;
        }
        private static bool DetermineEffectValueType(String value, int directoryIndex)
        {
            Assert.IsTrue(value != "");
            int cSharpInt;
            return !(Int32.TryParse(value, out cSharpInt));
        }
        private static TraitRequirement[] ParseTraitRequirementsFromLine(String lineIn, String fileLoc)
        {
            bool methodDebug = false;
            if (methodDebug) Debug.Log($"#DEBUG# ParseTraitRequirementsFromLine(String :{lineIn}:, String :{fileLoc}:)");
            String[] lineTraits = lineIn.Split(")", StringSplitOptions.RemoveEmptyEntries);
            if (methodDebug)
            {
                for (int i = 0; i < lineTraits.Length; i++)
                    Debug.Log("#DEBUG# LasciiFileParser.ParseTraitRequirementsFromLine(String :" + lineTraits[i] + ":) - Index :" + i + ":");
            }
            for (int i = 0; i < lineTraits.Length; i++)
            {
                lineTraits[i] = LasciiBasicTypeUtilities.ScrubParan(lineTraits[i]); //FINDERROR Trait being checked for lost first letter of its title.
            }
            TraitRequirement[] output = new TraitRequirement[lineTraits.Length];
            for (int i = 0; i < output.Length; i++)
            {
                if (lineTraits[i] == (""))// || lineTraits[i] == ("exists,1,=,false"))
                {
                    output[i] = LasciiObjectUtilities.existsTR;
                }
                else
                {
                    bool localDebug = false;
                    String[] traitParts = lineTraits[i].Split(",", StringSplitOptions.RemoveEmptyEntries);
                    if (methodDebug) Debug.Log("#DEBUG# LasciiFileParser.ParseTraitRequirementsFromLine(String :" + lineIn + ":) - TR[" + i + "] Trait Parts :" + traitParts.Length + ":. #");
                    if (methodDebug)
                    {
                        Debug.Log("#DEBUG# LasciiFileParser.ParseTraitRequirementsFromLine(String :" + lineIn + ":) - TraitRequirement[" + i + "] of out :" + output.Length + ": - Data After Split(','). #");
                        for (int j = 0; j < traitParts.Length; j++)
                            Debug.Log("#DEBUG# LasciiFileParser.ParseTraitRequirementsFromLine(String) - Data[" + j + "] :" + traitParts[j] + ":. #");
                    }
                    if (traitParts.Length == 2)
                    {
                        if (methodDebug) Debug.Log("#DEBUG# LasciiFileParser.ParseTraitRequirementsFromLine(String:" + lineIn + ":) - Creating new TR[" + i + "] - Parts = 2 #");
                        if (localDebug) Debug.Log("\n\n#DEBUG# LasciiFileParser.ParseTraitRequirementsFromLine(String:"+lineIn+":) - Creating new TR[" + i + "] (traitParts[0] :" + traitParts[0] + ":, traitParts[1] :" + traitParts[1] + ":,) . #");
                        output[i] = new TraitRequirement(traitParts[0], LasciiBasicTypeUtilities.ParseInt(traitParts[1]));
                    }
                    else if (traitParts.Length == 3)
                    {
                        if (methodDebug) Debug.Log("#DEBUG# LasciiFileParser.ParseTraitRequirementsFromLine(String:" + lineIn + ":) - Creating new TR[" + i + "] - Parts = 3 #");
                        if (localDebug) Debug.Log("\n\n#DEBUG# LasciiFileParser.ParseTraitRequirementsFromLine(String:" + lineIn + ":) - Creating new TR[" + i + "] (traitParts[0] :" + traitParts[0] + ":, traitParts[1] :" + traitParts[1] + ":, traitParts[2] :" + traitParts[2] + ":) . #");
                        output[i] = new TraitRequirement(traitParts[0], LasciiBasicTypeUtilities.ParseInt(traitParts[1]), ParseTraitRequirementConditions(traitParts[2], fileLoc));
                    }
                    else if (traitParts.Length == 4)
                    {
                        if (methodDebug) Debug.Log("#DEBUG# LasciiFileParser.ParseTraitRequirementsFromLine(String:" + lineIn + ":) - Creating new TR[" + i + "] - Parts = 4 #");
                        if (localDebug) Debug.Log("\n\n#DEBUG# ********************************************    LasciiFileParser.ParseTraitRequirementsFromLine(String:" + lineIn + ":) - Creating new TR[" + i + "] (traitParts[0] :" + traitParts[0] + ":, traitParts[1] :" + traitParts[1] + ":, traitParts[2] :" + traitParts[2] + ":, traitParts[3] :" + traitParts[3] + ":) . #");
                        output[i] = new TraitRequirement(traitParts[0], LasciiBasicTypeUtilities.ParseInt(traitParts[1]), ParseTraitRequirementConditions(traitParts[2], traitParts[3], fileLoc));
                    }

                }
                //if (methodDebug) Debug.Log(output[i].DEBUG());
            }
            /*
            if (methodDebug)
            {
                foreach (TraitRequirement tr in output)
                    Debug.Log(tr.DEBUG(true));
            }
            */
            return output;
        }

        private static String[] TR_comparisons = { "<", "=", ">", "!" };
        private static bool[] ParseTraitRequirementConditions(String lineIn, string fileLoc)
        {
            bool methodDebug = false;
            if (methodDebug) Debug.Log($"#DEBUG# LasciiFileParser.ParseTraitRequirementConditions(String :{lineIn}:, String :{fileLoc}:) - Begin Parsing. #");
            bool[] output = TraitRequirement.CopyDefaultBooleans();
            for (int i = 0; i < 3; i++)
            {
                if (lineIn.Contains(TR_comparisons[i])) output[i] = true; else output[i] = false;
            }
            if (lineIn.Contains(TR_comparisons[3]))
            {
                if (methodDebug) Debug.Log($"#DEBUG# LasciiFileParser.ParseTraitRequirementConditions(String :{lineIn}:, String :{fileLoc}:) - TR is NOT EQUALS. #");
                for (int i = 0; i < 3; i++)
                {
                    output[i] = false;
                }
                output[3] = true;
            }
            else
            {
                if (methodDebug) Debug.Log($"#DEBUG# LasciiFileParser.ParseTraitRequirementConditions(String :{lineIn}:, String :{fileLoc}:) - TR is normal. #");
                output[3] = false;
            }
            return output;
        }
        private static bool[] ParseTraitRequirementConditions(String lineIn, String showFlag, String fileLoc)
        {
            bool methodDebug = false;
            if (methodDebug) Debug.Log($"#DEBUG# LasciiFileParser.ParseTraitRequirementConditions(String :{lineIn}:, String :{showFlag}:, String :{fileLoc}:) - Begin Parsing  ");
            bool[] output = TraitRequirement.CopyDefaultBooleans();
            if (methodDebug) Debug.Log($"#DEBUG# LasciiFileParser.ParseTraitRequirementConditions(String, String, String) -  Bool[] length :{output.Length}: - Current Output :{LasciiBasicTypeUtilities.FlagsAsDigits(output)}:. #");
            for (int i = 0; i < TR_comparisons.Length; i++)
            {
                
                if (lineIn.Contains(TR_comparisons[i]))
                {
                    output[i] = true;
                }
                else output[i] = false;
                if (methodDebug) Debug.Log($"#DEBUG# LasciiFileParser.ParseTraitRequirementConditions(String, String, String) -  Index :{i}:, Maximum :{TR_comparisons.Length}: - Value :{output[i]}: #");
            }
            if (showFlag == "true") output[4] = true;
            else output[4] = false; //Set the Hide bit to true only if the read in string is true
            if (methodDebug) Debug.Log($"#DEBUG# LasciiFileParser.ParseTraitRequirementConditions(String :{lineIn}:, String :{showFlag}:, String :{fileLoc}:) - Output :{LasciiBasicTypeUtilities.FlagsAsDigits(output)}:. #");
            return output;
        }
    }
}