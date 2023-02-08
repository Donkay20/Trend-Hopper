using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
This class is responsible for the score; this includes the amt. of perfect/ok/miss notes in any given song.
*/

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public AudioSource hitSFX;
    public AudioSource missSFX;

    public TMPro.TextMeshPro comboScoreText;
    public TMPro.TextMeshPro comboSplash;
    public TMPro.TextMeshPro scoreText;
    public TMPro.TextMeshPro healthText;
    public TMPro.TextMeshPro debugPerfectText;
    public TMPro.TextMeshPro debugOKText;
    public TMPro.TextMeshPro debugMissText;
    //public GameObject bonusBox;

    static int comboScore;
    static int score;
    static int health;
    static int debugPerfectValue;
    static int debugOKValue;
    static int debugMissValue;
    static int failCheck;
    public int maxCombo = 500;

    private int[] comboLog = new int[5]; //array for score & pass/fail value
    // 0 = perfect / 1 = ok / 2 = bad / 3 = pass/fail
    // 1 = ok
    // 2 = bad
    // 3 = pass/fail check; 0 is pass, 1 is fail

    void Start()
    {
        comboScoreText.faceColor = new Color32(255, 255, 255, 70); //last value is opacity
        comboSplash.faceColor = new Color32(255, 255, 255, 90);
        //bonusBox.renderer.material.color.a = 0.5f;
        Instance = this;
        comboScore = 0;
        score = 0;
        debugPerfectValue = 0;
        debugOKValue = 0;
        debugMissValue = 0;
        failCheck = 0;
        health = 100;
    }

    //below: perfect/ok/miss methods, doing different things in each one.

    public static void Hit()
    {
        score += 100;
        comboScore += 1;
        debugPerfectValue += 1;
        Instance.hitSFX.Play();
        if (health >= 199) {
            health = 200;
        } else {
            health += 2;
        }
    }

    public static void OK()
    {
        score += 50;
        comboScore += 1;
        debugOKValue += 1;
         if (health >= 200) {
            health = 200;
        } else {
            health += 1;
        }
    }

    public static void Miss()   //missing a note that would cause you to drop to 0 health or below triggers the fail trigger and loads the results cutscene.
                                //this code will be edited to include a delay to display a "failure" graphic before a transition happens.
    {
        comboScore = 0;
        debugMissValue += 1;
        Instance.missSFX.Play();
        health -= 5;
        if (health <= 0) {
            failCheck = 1;
            SceneManager.LoadScene("Results");
            //Application.Quit();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (comboScore >= 2 && comboScore <= 9) {   //ngl I was kinda bored, might remove this later
            if (comboScore == 2) {
                comboScoreText.text = "two";
            }
            if (comboScore == 3) {
                comboScoreText.text = "three";
            }
            if (comboScore == 4) {
                comboScoreText.text = "four";
            }
            if (comboScore == 5) {
                comboScoreText.text = "five";
            }
            if (comboScore == 6) {
                comboScoreText.text = "six";
            }
            if (comboScore == 7) {
                comboScoreText.text = "seven";
            }
            if (comboScore == 8) {
                comboScoreText.text = "eight";
            }
            if (comboScore == 9) {
                comboScoreText.text = "nine";
            }
        } else {
            comboScoreText.text = comboScore.ToString();
        }
        //comboScoreText.text = comboScore.ToString();
        scoreText.text = score.ToString();
        healthText.text = health.ToString();
        debugPerfectText.text = debugPerfectValue.ToString();
        debugOKText.text = debugOKValue.ToString();
        debugMissText.text = debugMissValue.ToString();
    }

    public int[] getScoreLog() {        //builds the info needed to send to the results screen. may or may not need to reset its values for multiple levels but we'll see idk
        comboLog[0] = debugPerfectValue;
        comboLog[1] = debugOKValue;
        comboLog[2] = debugMissValue;
        comboLog[3] = failCheck;
        comboLog[4] = score;
        return comboLog;
    }
}
