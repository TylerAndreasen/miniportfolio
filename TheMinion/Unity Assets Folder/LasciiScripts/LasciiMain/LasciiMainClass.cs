using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using LasciiGameObjects;
using LasciiUtility;

namespace LasciiMain
{
    class LasciiMainClass : MonoBehaviour
    {
        [SerializeField] TextAsset dotPages, dotDirectories, dotINI;
        [SerializeField] TMP_Text pageDesc, actionsDesc;
        Page[] allPages;
        DirectoryPrecursor[] preDirs;
        Directory[] allDirs;

        private int majorIndex = 12, gameMode = 0, savedPageIndicator = -1;//, gameIndex = 0;
        private readonly int PAUSE_PAGE_INT = 13, GAME_START_PAGE_INT = 0;

        private string[]
            locations = new string[] { "Cubby", "Coat", "Rug", "Pillow", "Stationary", "Mantle", "Chimney" },
            objects = new string[] { "Chest", "Document", "Feather", "Saccade", "Stave" }
        ;

        void Start()
        {
            bool methodDebug = false;
            //Debug.Log("HEYYY, add an easter egg to allow the test chamber dungeon to be included.");
            PlayerTraits.FillArrayList();
            //PlayerTraits.DEV_DisplayTraits();

            String[] pagesData = LasciiBasicTypeUtilities.CleanEmptyValues(dotPages.text.Split("\n"));
            for (int i = 0; i < pagesData.Length; i++)
            {
                pagesData[i] = pagesData[i].Trim();
            }
            if (methodDebug) { foreach (String line in pagesData) Debug.Log(line); }


            allPages = LasciiFileParser.ParsePageFile(LasciiBasicTypeUtilities.CleanEmptyValues(pagesData), "dotPages");
            if (methodDebug) { foreach (Page p in allPages) p.DEBUG(true); }
            //pageDesc.text = allPages[0].FetchPageDescription();


            String[] directoriesData = LasciiBasicTypeUtilities.CleanEmptyValues(dotDirectories.text.Split("\n"));
            for (int i = 0; i < directoriesData.Length; i++)
            {
                directoriesData[i] = directoriesData[i].Trim();
            }
            if (methodDebug) { foreach (String line in directoriesData) Debug.Log(line); }
            preDirs = LasciiFileParser.ParseDirectoryFile(LasciiBasicTypeUtilities.CleanEmptyValues(directoriesData), "dotDirectories", allPages);
            //if (methodDebug) { foreach (DirectoryPrecursor d in preDirs) d.DEBUG(true); }

            allDirs = LasciiFileParser.Page_Directory_Integration(preDirs, allPages, out allPages);

            /* for (int i = 0; i < allPages.Length; i++)
            {
                allPages[i].DEBUG();
            } */
            //allPages = LasciiObjectUtilities.CombineArrays(allPages, gameStarts);
            Debug.Log($" Total items :{allPages.Length}:.");


            string placeholder;
            pageDesc.text = allPages[majorIndex].FetchDescription(out placeholder);// + "<br>" + allPages[majorIndex].GetFullIndex();
            actionsDesc.text = placeholder;

            //In future the ini file will be responible for many things, but the need for identifying the versions of file in Java is no longer present.
            //String[] metaData = dotINI.text.Split("\n");*/


            /* foreach (LocationManager m in locationManagers)
            {
                foreach (string item in objects)
                {
                    m.PushItem(item);
                    m.PullItem(item);
                }
            } */


            Debug.Log($"#DEBUG# LasciiMainClass.Start() - COMPLETED!!! #");
        }
        bool updateDebug = true, changed = false, hidingSpotPage = false;
        private string loggedKeys = "";
        void Update()
        {
            if (changed)
            {
                string placeholder;
                pageDesc.text = allPages[majorIndex].FetchDescription(out placeholder);// + "<br>" + allPages[majorIndex].GetFullIndex();
                if (majorIndex > 2 && majorIndex < 9)
                {
                    hidingSpotPage = true;
                }
                else hidingSpotPage = false;
                actionsDesc.text = placeholder;
                changed = false;
            }

            /*
            //DEV_TEST - Remove from release
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                majorIndex = (majorIndex + 1) % allPages.Length;
                string placeholder;
                pageDesc.text = allPages[majorIndex].FetchDescription(out placeholder);// + "<br>" + allPages[majorIndex].GetFullIndex();
                actionsDesc.text = placeholder;
            }
            //*/

            /*
            Game Modes:
            
            0 - Menu
            1 - Game (You stand {location})
            */

            String userCommand = Input.inputString;

            //PAUSE MENU
            if (gameMode == 0)
            {
                if (Input.anyKeyDown)
                {
                    if (updateDebug) Debug.Log($"#DEBUG# LasciiMainClass.Update() - Gamemode 0 : Pressed :{userCommand}:");
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        /* Debug.Log();
                        if (majorIndex == 12) //On load
                        {
                            majorIndex = GAME_START_PAGE_INT;
                        } 
                        else */
                        if (majorIndex == 13) //Pause menu
                        {
                            majorIndex = savedPageIndicator; //Unpausing, returning to game play
                            gameMode = 1;
                            changed = true;
                        }
                        else
                        {
                            Debug.Log($"#DEBUG# Cannot parse an 'Escape' key rn.");
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.Backspace))
                    {
                        //DEV_TEST Use with care in the unity editor.
                        //Debug.Log("READY TO QUIT WITH BACKSPACE.");
                        Application.Quit();
                    }
                    foreach (char c in userCommand)
                    {
                        if (c == '1')
                        {
                            majorIndex = GAME_START_PAGE_INT;
                            gameMode = 1;
                            changed = true;
                        }
                        //LAST IN ELSE IF LINE
                        else
                        {
                            if (loggedKeys.Length == 5)
                            {
                                if (loggedKeys == "debug")
                                {
                                    //TODO Code to move to test Dungeon
                                    Debug.Log($"HEY, Add the code to move to the test dungeon here.");
                                    changed = true;
                                    return;
                                }
                                loggedKeys = "";
                                return;
                            }
                            else
                            {
                                loggedKeys += userCommand;
                            }
                        }
                    }
                }
            }
            //GAMEPLAY
            else if (gameMode == 1)
            {
                if (Input.anyKeyDown)
                {
                    if (updateDebug) Debug.Log($"#DEBUG# LasciiMainClass.Update() - Gamemode 1 : Pressed :{userCommand}:");
                    int parsedInt = Int32.MaxValue;

                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        savedPageIndicator = majorIndex;
                        majorIndex = PAUSE_PAGE_INT;
                        gameMode = 0;
                        changed = true;
                    }



                    foreach (char c in userCommand)
                    {
                        //DEV_TEST Remove case for 't'
                        if (c == 't')
                        {
                            PlayerTraits.DEV_DisplayTraits();
                            return;
                        }
                        else if (c == 'p')
                        {
                            Debug.Log($"#DEBUG# LasciiMainClass.Update() Current Page is :{majorIndex}:. #");
                            return;
                        }
                        else if (Input.GetKeyDown(KeyCode.Backspace))
                        {
                            //DEV_TEST Use with care in the unity editor.
                            //Debug.Log("READY TO QUIT WITH BACKSPACE.");
                            Application.Quit();
                        }
                        else if (LasciiBasicTypeUtilities.Contains(LasciiBasicTypeUtilities.DIGITS_AS_CHARS, c))
                        {
                            parsedInt = LasciiBasicTypeUtilities.ParseInt("" + c) - 1;
                            if (hidingSpotPage && parsedInt == 1)
                            {
                                //Attempt to place object
                                foreach (string s in objects)
                                {
                                    /*
                                    Get the trait of the right name
                                    test if the trait value is above 0 // == 1
                                    if so, then attempt to push the item into the location of the same name
                                    if the push is successful, set the 
                                    */
                                }
                            }
                            break;
                        }
                    }
                    LasciiGameObjects.Action[] takeableActions = allPages[majorIndex].GetTakeableActions();

                    if (parsedInt < takeableActions.Length && parsedInt > -1)
                    {
                        if (updateDebug) Debug.Log($"#DEBUG# LasciiMainClass.Update() - ");
                        LasciiGameObjects.Action a = takeableActions[parsedInt];
                        if (!a.CanTakeAction())
                        {
                            if (updateDebug) Debug.Log($"#DEBUG# LasciiMainClass.Update() - Parsed input :{parsedInt}: Cannot Take Action. #");
                            return;
                        }
                        else
                        {
                            if (a.HasEffects())
                            {
                                if (updateDebug) Debug.Log($"#DEBUG# LasciiMainClass.Update() - Action[{parsedInt}] Has Effects, Begin Applying. #");
                                if (!a.ApplyEffects()) Debug.Log($"#ERROR# LasciiMainClass.Update() - Action[{parsedInt}] Issue In applying Effect. #");
                            }
                            majorIndex = a.GetDestination().GetNumericIndex();
                            changed = true;
                            PlayerTraits.DEV_DisplayTraits();
                        }
                    }
                    else
                    {
                        if (updateDebug) Debug.Log($"#DEBUG# LasciiMainClass.Update() - Parsed input :{parsedInt}: Not In range for the possible Actions :{takeableActions.Length}:. #");
                    }
                }

            }/*
            //START UP
            else if (gameMode == 2)
            {
                if (Input.AnyKeyDown)
                {
                    foreach (char c in userCommand)
                    {
                        if ((c == '\n') || (c =='\r'))
                        {

                        }
                    }
                }
            }*/
        }
        private LocationManager[] locationManagers = new LocationManager[]
            {
                new LocationManager("Cubby", new Response[] { // A large void behind a painting, capable of hiding all objects barring the stave as it is to long to fit corner-to-corner in the space without the painting sticking out from the wall.
                    new Response("Chest", true,
                        "You heave the sturdy wooden chest the three or so feet up into the cubby in the wall and return the painting to its proper place.", // Insert Line
                        "You carefully lower the study wooden chest down from the wall cubby and replace the painting."), // Remove Line
                    new Response("Document", true,
                        "You easily place the document into the space behind the painting. Though the document looks quite small is such a place.", // Insert Line
                        "Tipping the painting aside, you easily remove the document from the hidden cubby."), // Remove Line
                    new Response("Feather", true,
                        "Gingerly, you place the rare Parakeet feather down in the cubby, and returnt the painting to its nail.", // Insert Line
                        "Carefully you extract the Parakeet feather from the cubby. Hope it didn't get dusty."), // Remove Line
                    new Response("Saccade", true,
                        "Setting aside the painting a moment, you place down and adjust the statuette. Even after returning the painting to its nail, it feels as though the eye is watching you.", // Insert Line
                        "The watched feeling returns as you approach and extract the statutette from the cubby behind the painting."), // Remove Line
                    new Response("Stave", false,
                        "After removing the painting from the wall, you find you are unable fit the stave into the cubby, even placing the end in the far top corner leaves the end of the stave poking out.", // Insert Line
                        "# ERROR # If you are reading this line, please let the developer know (Attempted to Remove Stave from the Cubby)"), // Remove Line
                }),
                new LocationManager("Coat", new Response[] {
                    new Response("Chest", false,
                        "Despite the size and softness of the cloth, you master's coat will not fully cover the chest, and the chest would certainly knock over the coat rack if you tried to hang it.", // Insert Line
                        "# ERROR # If you are reading this line, please let the developer know (Attempted to Remove Chest from the Coat)"), // Remove Line
                    new Response("Document", false,
                        "After rifling through the surprising number of pockets on your master's favored coat, you can't locate a pocket large enough and wouldn't risk folding the parchment to make it fit.", // Insert Line
                        "# ERROR # If you are reading this line, please let the developer know (Attempted to Remove Document from the Coat)"), // Remove Line
                    new Response("Feather", true,
                        "Carefully, you use the small silver hook attached to the rare feather to hang it from a loop inside your master's coat. Might he have added it for this very purpose?", // Insert Line
                        "Cautiously, you remove the "), // Remove Line
                    new Response("Saccade", false,
                        "Despite the surprising number of pockets in the hanging coat, you cannot find a pocket to fit the statuette into, which would hang all askew", // Insert Line
                        "# ERROR # If you are reading this line, please let the developer know (Attempted to Remove Saccade from the Coat)"), // Remove Line
                    new Response("Stave", true,
                        "After hooking the end of the stave on a loop within your master's coat, you readjust the coat on the rack to appear to hang more naturally.", // Insert Line
                        "Quickly, you unhook the stave from the coat's inner loop."), // Remove Line
                }),
                new LocationManager("Rug", new Response[] {
                    new Response("Chest", false,
                        "While large enough to cover the chest, the Minolal fur rug is quite stiff and rests akwardly over the chest and does not conseal it well. You decide against hiding the chest here.", // Insert Line
                        "# ERROR # If you are reading this line, please let the developer know (Attempted to Remove Chest from the Rug)"), // Remove Line
                    new Response("Document", true,
                        "Lifting up the edge of the Minolal fur rug, you slide the document under the rug. The rug should also keep it from getting dusty, handy.", // Insert Line
                        "You retireve the parchment from under the Minolal fur rug."), // Remove Line
                    new Response("Feather", false,
                        "Despite the lumps around the edge of the fur rug, the feather does not fit neatly under any of them, and would be snapped or crushed by the weight of the parts on the floor.", // Insert Line
                        "# ERROR # If you are reading this line, please let the developer know (Attempted to Remove Feather from the Rug)"), // Remove Line
                    new Response("Saccade", false,
                        "Given it's size and raised wings, the statuette is a covered, but obvious lump under the Minolal fur rug.", // Insert Line
                        "# ERROR # If you are reading this line, please let the developer know (Attempted to Remove Saccade from the Rug)"), // Remove Line
                    new Response("Stave", false,
                        "Despite the size of the rug more than covering the stave, the lump created is less than inconspicuous. ", // Insert Line
                        "# ERROR # If you are reading this line, please let the developer know (Attempted to Remove Stave from the Rug)"), // Remove Line
                }),
                new LocationManager("Pillow", new Response[] {
                    new Response("Chest", false,
                        "Looking between the chest and the pillow, you determine the pillow could fit in the chest, and not the reverse.", // Insert Line
                        "# ERROR # If you are reading this line, please let the developer know (Attempted to Remove Chest from the Pillow)"), // Remove Line
                    new Response("Document", true,
                        "Slowly, you slide the document inbetween the pillow and it case, then flip the pillow over to hide the rectangular outline visible through the pillow case.", // Insert Line
                        "Carefully, you slide the document out from between the pillow and pillow case."), // Remove Line
                    new Response("Feather", false,
                        "Despite being a matching feather bed and pillow, the Parakeet feather would be c", // Insert Line
                        "# ERROR # If you are reading this line, please let the developer know (Attempted to Remove Feather from the Pillow)"), // Remove Line
                    new Response("Saccade", true,
                        "Pulling carefully on the string in the seem of the pillow case, you open a hole just large enough to slip the statuette of Saccade into the pillow. You will want to ensure it is removed before your master next needs to sleep.", // Insert Line
                        "Again pulling gently on the string in the seem, you extract the statuette from the pillow case, and return the handful of feathers that came with it. Perhaps it is better to avoid the statuette being slept on."), // Remove Line
                    new Response("Stave", false,
                        "Upon inspection, the stave is far too long to fit within the pillow, and the mat on which your master sleeps wouldn't hide the stave either.", // Insert Line
                        "# ERROR # If you are reading this line, please let the developer know (Attempted to Remove Stave from the Pillow)"), // Remove Line
                }),
                new LocationManager("Stationary", new Response[] {
                    new Response("Chest", false,
                        "Seeing as the only unlocked stationary drawer could fit in the chest, you determine you cannot fit the chest into the stationary.",// Insert Line
                        "# ERROR # If you are reading this line, please let the developer know (Attempted to Remove Chest from the Stationary)"), // Remove Line
                    new Response("Document", true,
                        "With ease you place your master's important document into the one stationary drawer without a lock. Where did you lose the key again?", // Insert Line
                        "Picking at the edge of the thick parchment, you extract the document from the stationary drawer."), // Remove Line
                    new Response("Feather", false,
                        "Upon opening the one unlocked drawer in the stationary, you discover that it is not long enough to hold the Parakeet feather even placed diagonally", // Insert Line
                        "# ERROR # If you are reading this line, please let the developer know (Attempted to Remove Feather from the Stationary)"), // Remove Line
                    new Response("Saccade", true,
                        "Opening the one drawer without a lock, you gently rest the statuette within the stationary.", // Insert Line
                        "You gingerly retrieve the statuette from the one lockless drawer in the stationary."), // Remove Line
                    new Response("Stave", false,
                        "Upon comparison, the stave is taller than the stationary, rendering it poor hiding place for the stave.", // Insert Line
                        "# ERROR # If you are reading this line, please let the developer know (Attempted to Remove Stave from the Stationary)"), // Remove Line
                }),
                new LocationManager("Mantle", new Response[] { //The Mantle Display has a false bottom which can hide the stave or the documnent which should be rolled (in description)
                    new Response("Chest", false,
                        "Observing long, narrow sword display case and the chest that a child could hide within, you determine the chest will not fit in the mantle's display case.", // Insert Line
                        "# ERROR # If you are reading this line, please let the developer know (Attempted to Remove Chest from the Mantle)"), // Remove Line
                    new Response("Document", true,
                        "Retreving the heirloom weapon, and revealing the false bottom, you roll up the document and place it with the mantle dislpay, and return the scene to its previous state.", // Insert Line
                        "Again removing the sword, you open the false bottom to the display, and retrive the document, replacing the sword and closing the case."), // Remove Line
                    new Response("Feather", false,
                        "Upon removing the heirloom sword and opening the mantle display case's false bottom, you discover the height of the hidden compartment is not large enough to hold the height of the large, aging feather, despite being more than long enough.", // Insert Line
                        "# ERROR # If you are reading this line, please let the developer know (Attempted to Remove Feather from the Mantle)"), // Remove Line
                    new Response("Saccade", false,
                        "After removing the displayed sword and opening the ", // Insert Line
                        "# ERROR # If you are reading this line, please let the developer know (Attempted to Remove Saccade from the Mantle)"), // Remove Line
                    new Response("Stave", true,
                        "Extracing the heirloom sword, and raising the false bottom to the mantle, you place the stave into the small hollow in the bottom of the mantle display case. A home the stave has occupied before.", // Insert Line
                        "Removing your master's heirloom sword and stave from the display, you try a pose with the weapons you have seen you master take. You feel powerful, but this is no time for fantacies, you return the blade and close the case."), // Remove Line
                }),
                new LocationManager("Chimney", new Response[] {
                    new Response("Chest", true,
                        "With great effort, you lift the chest above you head and set it onto the crossbars in the chimney. And the lack of groaning metal assures you the bars will hold the chest in place.", // Insert Line
                        "With effort similar to placing the chest here, you remove the chest from the chimney's cross bars, and keep yourself from getting to soot-covered in the process."), // Remove Line
                    new Response("Document", false,
                        "While you could probably keep the document from falling, the soot-left on the crossbars would ruin the text on one side or the other, you decide against it.", // Insert Line
                        "# ERROR # If you are reading this line, please let the developer know (Attempted to Remove Document from the Chimney)"), // Remove Line
                    new Response("Feather", false,
                        "Looking up at the soot-covered crossbars of the chimney, the ancient feather seems far to old to be cleaned if it was hidden here, you decide against it.", // Insert Line
                        "# ERROR # If you are reading this line, please let the developer know (Attempted to Remove Feather from the Chimney)"), // Remove Line
                    new Response("Saccade", false,
                        "Despite your best efforts to nestle the statuette between the crossbars and the wall of the chimney, it will not remain in place.", // Insert Line
                        "# ERROR # If you are reading this line, please let the developer know (Attempted to Remove Saccade from the Chimney)"), // Remove Line
                    new Response("Stave", true,
                        "Carefully from below, you place one end of the stave on the intersection point of the crossbar, and lean the other against the surface of the chimney. Note, the ends may need a dusting later.", // Insert Line
                        "Without knocking to much ash onto yourself, you extract the stave from within the chimney."), // Remove Line
                })
            };
    }
}