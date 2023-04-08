using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Progress
{
    public static string lastLevel {get; set;}
    public static bool dressUpTutorialSeen {get; set;}

    public static bool day1BeforeDialogueSeen {get; set;}

    public static string difficulty {get; set;}


    public static int peakNotes {get; set;}
    public static int hitCount {get; set;}
    public static int okCount {get; set;}
    public static int missCount {get; set;}
    public static bool failed {get; set;}
    public static bool notEnoughHealth {get; set;}
    public static int score {get; set;}
    public static int highestCombo {get; set;}
    public static int noteLimit {get; set;}

    public static bool day1IntroSeen {get; set;}
    public static bool day2IntroSeen {get; set;}
    public static bool day3IntroSeen {get; set;}

    public static bool levelOneCleared {get; set;}
    public static bool levelTwoCleared {get; set;}
    public static bool levelThreeCleared {get; set;}

    public static int chosenHair {get; set;}
    public static int chosenTop {get; set;}
    public static int chosenBottom {get; set;}
    public static int chosenShoe {get; set;}
    public static int chosenAccessory{get; set;}
}
