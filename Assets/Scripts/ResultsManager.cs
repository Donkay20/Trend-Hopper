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
    private int[] results;
    private int totalNotes;
    private double finalScoreAfterMultiplier;
    private int highestPossibleScore;

    public TMPro.TextMeshPro perfectText;   //text fields
    public TMPro.TextMeshPro okText;
    public TMPro.TextMeshPro missText;
    public TMPro.TextMeshPro gradeText;
    public TMPro.TextMeshPro totalNotesText;
    public TMPro.TextMeshPro highestComboText;
    public TMPro.TextMeshPro finalScoreText;
    public TMPro.TextMeshPro finalScoreAfterMultiplierText;
    public TMPro.TextMeshPro multiplierText;
    public TMPro.TextMeshPro status;
    
    void Start()
    {   
        results = ScoreManager.Instance.getScoreLog();
        perfectText.text = results[0].ToString();           //0 is for perfect
        okText.text = results[1].ToString();                //1 is for ok
        missText.text = results[2].ToString();              //2 is for miss
        finalScoreText.text = results[4].ToString();        //4 is for score
        totalNotes = results[0] + results[1] + results[2];  totalNotesText.text = totalNotes.ToString();
        highestComboText.text = results[5].ToString();      //5 is for the score record
        multiplierText.text = DressUpStatBonuses.scoreMultiplier.ToString();
        finalScoreAfterMultiplierText.text = (results[4]*DressUpStatBonuses.scoreMultiplier).ToString();
        finalScoreAfterMultiplier = (results[4]*DressUpStatBonuses.scoreMultiplier);
        if (totalNotes == (results[0] + results[1])) {      //if only perfect and ok notes are hit
            status.text = "Full Combo!";
        } else if (totalNotes == results[0]) {              //if only perfect notes are hit
            status.text = "Pitch Perfect!!";
        }
        highestPossibleScore = (totalNotes*100) * 2;        //highest possible score from the song
        letterGrade();                                      //invoke the letter grade class, dependent on the values above (may change)
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void letterGrade() {
        if (results[3] == 1) {                      //3 is for the pass/fail flag; 0 is pass and 1 is fail
            gradeText.text = "F";
        } else {                                    //if flag isn't triggered, assign a proper grade
            if (finalScoreAfterMultiplier == highestPossibleScore) {
                gradeText.text = "S";
            } else if (finalScoreAfterMultiplier < highestPossibleScore && finalScoreAfterMultiplier >= highestPossibleScore*0.8) {
                gradeText.text = "A";
            } else if (finalScoreAfterMultiplier < highestPossibleScore*0.8 && finalScoreAfterMultiplier >= highestPossibleScore*0.6) {
                gradeText.text = "B";
            } else if (finalScoreAfterMultiplier < highestPossibleScore*0.6 && finalScoreAfterMultiplier >= highestPossibleScore*0.4) {
                gradeText.text = "C";
            } else if (finalScoreAfterMultiplier < highestPossibleScore*0.4 && finalScoreAfterMultiplier >= 0) {
                gradeText.text = "D";
            }
        }
    }
}
