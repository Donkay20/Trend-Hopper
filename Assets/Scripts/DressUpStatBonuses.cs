using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DressUpStatBonuses
{
    public static double scoreMultiplier {get; set;}    //Carry-over variables to the results screen and rhythm game.
    public static double leniency {get; set;}
    public static int scoreThreshold {get; set;}

    //the variables below are obsolete, but getting rid of them causes a compile-time error as the old clothing system still uses 'em. Keeping them for now.
    public static int punkHair {get; set;}              
    public static int punkAccessory {get; set;}
    public static int punkShoe {get; set;} 
    public static int punkTop {get; set;}
    public static int punkBottom {get; set;}
    public static int y2kHair {get; set;}               
    public static int y2kAccessory {get; set;}
    public static int y2kShoe {get; set;}
    public static int y2kTop {get; set;}
    public static int y2kBottom {get; set;}
}
