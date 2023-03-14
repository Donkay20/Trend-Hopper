using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Progress
{
    public static string lastLevel {get; set;}
    public static bool failedOnce {get; set;}
    public static bool dressUpTutorialSeen {get; set;}

    public static bool day1BeforeDialogueSeen {get; set;}

    public static bool levelOneCleared {get; set;}
    public static bool levelTwoClared {get; set;}
    public static bool levelThreeCleared {get; set;}

    public static int levelOneHighScore {get; set;}
    public static int levelTwoHighScore {get; set;}
    public static int levelThreeHighScore {get; set;}

    public static int chosenHair {get; set;}
    public static int chosenTop {get; set;}
    public static int chosenBottom {get; set;}
    public static int chosenShoe {get; set;}
    public static int chosenAccessory{get; set;}
}
