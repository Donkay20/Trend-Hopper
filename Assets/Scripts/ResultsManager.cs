using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultsManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int[] results;

    public TMPro.TextMeshPro perfectText;
    public TMPro.TextMeshPro okText;
    public TMPro.TextMeshPro missText;
    public TMPro.TextMeshPro gradeText;
    
    void Start()
    {
        results = ScoreManager.Instance.getScoreLog();
        perfectText.text = results[0].ToString();
        okText.text = results[1].ToString();
        missText.text = results[2].ToString();
        letterGrade();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void letterGrade() {
        if (results[3] == 1) {
            gradeText.text = "F";
        } else {
            gradeText.text = "A";
        }
    }
}
