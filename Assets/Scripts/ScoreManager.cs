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

    void Start()
    {
        Instance = this;
        comboScore = 0;
        score = 0;
        debugPerfectValue = 0;
        debugOKValue = 0;
        debugMissValue = 0;
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
            Application.Quit();
        }
    }

    // Update is called once per frame
    void Update()
    {
        comboScoreText.text = comboScore.ToString();
        scoreText.text = score.ToString();
        healthText.text = health.ToString();
        debugPerfectText.text = debugPerfectValue.ToString();
        debugOKText.text = debugOKValue.ToString();
        debugMissText.text = debugMissValue.ToString();
    }
}
