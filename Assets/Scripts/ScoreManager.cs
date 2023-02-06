using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public AudioSource hitSFX;
    public AudioSource missSFX;

    public TMPro.TextMeshPro comboScoreText;
    public TMPro.TextMeshPro scoreText;
    public TMPro.TextMeshPro healthText;
    public TMPro.TextMeshPro debugPerfectText;
    public TMPro.TextMeshPro debugOKText;
    public TMPro.TextMeshPro debugMissText;

    static int comboScore;
    static int score;
    static int health;
    static int debugPerfectValue;
    static int debugOKValue;
    static int debugMissValue;
    static int failCheck;

    private int[] comboLog = new int[4]; //array for score & pass/fail value
    // 0 = perfect / 1 = ok / 2 = bad / 3 = pass/fail
    // 1 = ok
    // 2 = bad
    // 3 = pass/fail check; 0 is pass, 1 is fail

    void Start()
    {
        comboScoreText.faceColor = new Color32(255, 255, 255, 30); //last value is opacity
        Instance = this;
        comboScore = 0;
        score = 0;
        debugPerfectValue = 0;
        debugOKValue = 0;
        debugMissValue = 0;
        failCheck = 0;
        health = 100;
    }

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

    public static void Miss()
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
        if (comboScore >= 2 && comboScore <= 9) {
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

    public int[] getScoreLog() {
        comboLog[0] = debugPerfectValue;
        comboLog[1] = debugOKValue;
        comboLog[2] = debugMissValue;
        comboLog[3] = failCheck;
        return comboLog;
    }
}
