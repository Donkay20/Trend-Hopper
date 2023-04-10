using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
This class is responsible for the results screen after winning or losing at a song.
This class takes in an array from the ScoreManager class and extracts its values to display on the results screen.
*/

public class ResultsManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int totalNotes;
    private double finalScoreAfterMultiplier;
    private int highestPossibleScore;
    private string grade;
    private bool high;

    public KeyCode input;
    [Space]
    public AudioSource levelOneTune;
    public AudioSource levelTwoTune;
    public AudioSource levelThreeTune;
    [Space]
    public TMPro.TextMeshPro perfectText;  
    public TMPro.TextMeshPro okText;
    public TMPro.TextMeshPro missText;
    public TMPro.TextMeshPro status;
    [Space]
    public TMPro.TextMeshPro totalNotesText;
    public TMPro.TextMeshPro highestComboText;
    public TMPro.TextMeshPro finalScoreAfterMultiplierText;
    [Space]
    public TMPro.TextMeshPro multiplierText;                                                                //text fields to influence
    public TMPro.TextMeshPro leniencyText;
    public TMPro.TextMeshPro coolnessText;
    public TMPro.TextMeshPro peakText;
    [Space]
    public Sprite difficultyEasy; public Sprite difficultyNormal; public Sprite difficultyHard;             //difficulty assets
    public GameObject difficultyIcon;                                                                       //the gameobject to influence in terms of the difficulty displayed
    public Sprite lv1; public Sprite lv2; public Sprite lv3;                                                //level assets
    public GameObject levelIcon;                                                                            //the gameobject to influence in terms of the stage
    public Sprite gradeF; public Sprite gradeD; public Sprite gradeC; 
    public Sprite gradeB; public Sprite gradeA; public Sprite gradeS;                                       //grade assets
    public GameObject gradeIcon;                                                                            //the gameobject to influence in terms of the grade
    [Space]
    public GameObject mcDisplay;    //the gameobject to influence
    public Sprite successSprite;
    public Sprite failSprite;       //sprites for when the mc has a good or bad score
    [Space]
    public GameObject appliedHair; 
    public GameObject appliedTop; 
    public GameObject appliedBottom; 
    public GameObject appliedShoe; 
    public GameObject appliedAccessory; //the clothes that go on the mc 
    [Space]
    public Sprite[] hairStorageSuccess = new Sprite[9];
    public Sprite[] topStorageSuccess = new Sprite[9];
    public Sprite[] bottomStorageSuccess = new Sprite[9];
    public Sprite[] shoeStorageSuccess = new Sprite[9];
    public Sprite[] accessoryStorageSuccess = new Sprite[9];    //storage for the clothes upon getting a good grade
    [Space]
    public Sprite[] hairStorageFailure = new Sprite[9];
    public Sprite[] topStorageFailure = new Sprite[9];
    public Sprite[] bottomStorageFailure = new Sprite[9];
    public Sprite[] shoeStorageFailure = new Sprite[9];
    public Sprite[] accessoryStorageFailure = new Sprite[9];    //storage for the clothes upon getting a bad grade
    
    void Start()
    {   
        switch(Progress.lastLevel) {    //determining the uhhhh the level display
            case "dayOne":
                levelIcon.GetComponent<SpriteRenderer>().sprite = lv1;
                levelOneTune.Play();
                break;
            case "dayTwo":
                levelIcon.GetComponent<SpriteRenderer>().sprite = lv2;
                levelTwoTune.Play();
                break;
            case "dayThree":
                levelIcon.GetComponent<SpriteRenderer>().sprite = lv3;
                levelThreeTune.Play();
                break;
            case null:
                levelIcon.GetComponent<SpriteRenderer>().sprite = lv3;
                break;
        }

        switch (Progress.difficulty) {  //determining the difficulty display
            case "easy":
                difficultyIcon.GetComponent<SpriteRenderer>().sprite = difficultyEasy;
                break;
            case "normal":
                difficultyIcon.GetComponent<SpriteRenderer>().sprite = difficultyNormal;
                break;
            case "hard":
                difficultyIcon.GetComponent<SpriteRenderer>().sprite = difficultyHard;
                break;
            case null:
                difficultyIcon.GetComponent<SpriteRenderer>().sprite = difficultyHard;
                break;
        }

        finalScoreAfterMultiplier = ((int) Progress.score * DressUpStatBonuses.scoreMultiplier + (Progress.peakNotes*50));

        perfectText.text = Progress.hitCount.ToString();          
        okText.text = Progress.okCount.ToString();                
        missText.text = Progress.missCount.ToString();                  
        totalNotes = Progress.hitCount + Progress.okCount + Progress.missCount;
        totalNotesText.text = totalNotes.ToString();
        highestComboText.text = Progress.highestCombo.ToString();      

        multiplierText.text = DressUpStatBonuses.scoreMultiplier.ToString() + "x";
        leniencyText.text = "+" + DressUpStatBonuses.leniencyValue.ToString() + " ms";
        coolnessText.text = DressUpStatBonuses.scoreThreshold.ToString() + " req.";
        peakText.text = Progress.peakNotes.ToString();

        finalScoreAfterMultiplierText.text = finalScoreAfterMultiplier.ToString();

        if (totalNotes == (Progress.hitCount + Progress.okCount)) {      //if only perfect and ok notes are hit
            status.text = "Full combo!";
        } 
        
        if (totalNotes == Progress.hitCount) {              //if only perfect notes are hit
            status.text = "Peak performance!";
        }

        highestPossibleScore = (totalNotes*100) * 2;        //highest possible score from the song, this can be alleviated with gold notes.
        letterGrade();                                      //invoke the letter grade class, dependent on the values above (may change)

        switch (grade) {
            case "S":
                mcDisplay.GetComponent<SpriteRenderer>().sprite = successSprite;
                high = true;
                break;
            case "A":
                mcDisplay.GetComponent<SpriteRenderer>().sprite = successSprite;
                high = true;
                break;
            case "B":
                mcDisplay.GetComponent<SpriteRenderer>().sprite = successSprite;
                high = true;
                break;
            case "C":
                mcDisplay.GetComponent<SpriteRenderer>().sprite = failSprite;
                high = false;
                break;
            case "D":
                mcDisplay.GetComponent<SpriteRenderer>().sprite = failSprite;
                high = false;
                break;
            case "F":
                mcDisplay.GetComponent<SpriteRenderer>().sprite = failSprite;
                high = false;
                break;
        }
        //debugging cause ada is cringe
        mcDisplay.GetComponent<SpriteRenderer>().sprite = failSprite;
        high = false;
        //debugging cause ada is cringe
        appliedClothing();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(input)) {   //dependent on the grade received, load either pass or fail scene.
            switch(Progress.lastLevel) {
                case "dayOne":
                    if (grade == "F") {
                        SceneManager.LoadScene("Dialogue_Day1FailStage");
                    } else {
                        SceneManager.LoadScene("Dialogue_Day1PassStage");
                    }
                    break;
                case "dayTwo":
                    if (grade == "F") {
                        SceneManager.LoadScene("Dialogue_Day2FailStage");
                    } else {
                        SceneManager.LoadScene("Dialogue_Day2PassStage");
                    }
                    break;
                case "dayThree":
                    if (grade == "F") {
                        SceneManager.LoadScene("Dialogue_Day3FailStage");
                    } else {
                        SceneManager.LoadScene("Dialogue_Day3PassStage");
                    }
                    break;
            }
        }
    }

    private void letterGrade() {
        if (grade == null) {
            gradeIcon.GetComponent<SpriteRenderer>().sprite = gradeF;
        }
        if (Progress.failed) {                      //if the fail flag was triggered, give auto F
            grade = "F";
            gradeIcon.GetComponent<SpriteRenderer>().sprite = gradeF;
        } else {                                    //if flag isn't triggered, assign a proper grade
            if (finalScoreAfterMultiplier >= highestPossibleScore) {
                grade = "S";
                gradeIcon.GetComponent<SpriteRenderer>().sprite = gradeS;
            } else if (finalScoreAfterMultiplier < highestPossibleScore && finalScoreAfterMultiplier >= highestPossibleScore*0.8) {
                grade = "A";
                gradeIcon.GetComponent<SpriteRenderer>().sprite = gradeA;
            } else if (finalScoreAfterMultiplier < highestPossibleScore*0.8 && finalScoreAfterMultiplier >= highestPossibleScore*0.6) {
                grade = "B";
                gradeIcon.GetComponent<SpriteRenderer>().sprite = gradeB;
            } else if (finalScoreAfterMultiplier < highestPossibleScore*0.6 && finalScoreAfterMultiplier >= highestPossibleScore*0.4) {
                grade = "C";
                gradeIcon.GetComponent<SpriteRenderer>().sprite = gradeC;
            } else if (finalScoreAfterMultiplier < highestPossibleScore*0.4 && finalScoreAfterMultiplier >= 0) {
                grade = "D";
                gradeIcon.GetComponent<SpriteRenderer>().sprite = gradeD;
            }
        }
    }

    private void appliedClothing() { //fuck you Jaden (and maybe Ada)
        switch(high) {
            case true:
                appliedHair.GetComponent<SpriteRenderer>().sprite = hairStorageSuccess[Progress.chosenHair];
                appliedTop.GetComponent<SpriteRenderer>().sprite = topStorageSuccess[Progress.chosenTop];
                appliedBottom.GetComponent<SpriteRenderer>().sprite = bottomStorageSuccess[Progress.chosenBottom];
                appliedShoe.GetComponent<SpriteRenderer>().sprite = shoeStorageSuccess[Progress.chosenShoe];
                appliedAccessory.GetComponent<SpriteRenderer>().sprite = accessoryStorageSuccess[Progress.chosenAccessory];
                break;
            case false:
                appliedHair.GetComponent<SpriteRenderer>().sprite = hairStorageFailure[Progress.chosenHair];
                appliedTop.GetComponent<SpriteRenderer>().sprite = topStorageFailure[Progress.chosenTop];
                appliedBottom.GetComponent<SpriteRenderer>().sprite = bottomStorageFailure[Progress.chosenBottom];
                appliedShoe.GetComponent<SpriteRenderer>().sprite = shoeStorageFailure[Progress.chosenShoe];
                appliedAccessory.GetComponent<SpriteRenderer>().sprite = accessoryStorageFailure[Progress.chosenAccessory];
                break;
        }
    }
}