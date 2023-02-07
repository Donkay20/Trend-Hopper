using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class is responsible for the results screen after winning or losing at a song.
This class takes in an array from the ScoreManager class and extracts its values to display on the results screen.
*/

public class ResultsManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int[] results;

    public TMPro.TextMeshPro perfectText;   //text fields
    public TMPro.TextMeshPro okText;
    public TMPro.TextMeshPro missText;
    public TMPro.TextMeshPro gradeText;
    
    void Start()
    {
        results = ScoreManager.Instance.getScoreLog();
        perfectText.text = results[0].ToString();   //0 is for perfect
        okText.text = results[1].ToString();        //1 is for ok
        missText.text = results[2].ToString();      //2 is for miss
        letterGrade();                              //invoke the letter grade class, dependent on the values above (may change)
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void letterGrade() {
        if (results[3] == 1) {                      //3 is for the pass/fail flag; 0 is pass and 1 is fail
            gradeText.text = "F";
        } else {                                    //if flag isn't triggered, assign a proper grade (WIP)
            gradeText.text = "A";
        }
    }
}
