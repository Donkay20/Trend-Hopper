using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress
{
    public string lastLevel {get; set;}
    public bool failedOnce {get; set;}
    public bool dressUpTutorialSeen {get; set;}

    public bool day1BeforeDialogueSeen {get; set;}

    public bool levelOneCleared {get; set;}
    public bool levelTwoClared {get; set;}
    public bool levelThreeCleared {get; set;}

    public int levelOneHighScore {get; set;}
    public int levelTwoHighScore {get; set;}
    public int levelThreeHighScore {get; set;}
}
