using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DressUpStatBonuses
{
    public static double scoreMultiplier {get; set;}    //carry-over variables to the results screen and rhythm game I guess
    public static double leniency {get; set;}
    public static int scoreThreshold {get; set;}

    public static int punkHair {get; set;}              //I need to find a better way to do this, but these carry variables to the rhythm game so we know what clothing to put on the mc
    public static int punkAccessory {get; set;}
    public static int punkShoe {get; set;} 
    public static int punkTop {get; set;}
    public static int punkBottom {get; set;}
}
