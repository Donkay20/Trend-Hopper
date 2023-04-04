using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
This class is responsible for the score; this includes the amt. of perfect/ok/miss notes in any given song.
*/

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public Image healthFill;
    [Space]
    public AudioSource hitSFX;
    public AudioSource missSFX;
    [Space]
    public TMPro.TextMeshPro comboScoreText;
    public TMPro.TextMeshPro comboSplash;
    public TMPro.TextMeshPro scoreText;
    public TMPro.TextMeshPro healthText;
    public TMPro.TextMeshPro healthRequirementText;
    [Space]
    public GameObject thresholdIndicator;
    public GameObject hundredTextPrefab;
    [Space]
    public GameObject dangerNotifier;
    public GameObject peakNotifier;

    static int comboScore; //can stay
    static float health;    //can stay
    static float maxHealth = 200; //can stay

    public int maxCombo = 500;
    private string hundredToText;        //juice to show each 100 in score.

    public ParticleSystem leftHit; //sfx note hit
    public ParticleSystem rightHit;
    public ParticleSystem upHit;
    public ParticleSystem downHit;

    void Start()
    {
        healthRequirementText.text = DressUpStatBonuses.scoreThreshold.ToString();
        thresholdIndicator = Instantiate(thresholdIndicator); positionThresholdIndicator();

        comboScoreText.faceColor = new Color32(255, 255, 255, 70); //last value is opacity
        comboSplash.faceColor = new Color32(255, 255, 255, 90);

        Instance = this;

        comboScore = 0;

        leftHit = GameObject.Find("LeftArrowSFX").GetComponent<ParticleSystem>();
        rightHit = GameObject.Find("RightArrowSFX").GetComponent<ParticleSystem>();
        upHit = GameObject.Find("UpArrowSFX").GetComponent<ParticleSystem>();
        downHit = GameObject.Find("DownArrowSFX").GetComponent<ParticleSystem>();
       

        Progress.peakNotes = 0;
        Progress.score = 0;
        Progress.hitCount = 0;
        Progress.okCount = 0;
        Progress.missCount = 0;
        Progress.failed = false;
        Progress.highestCombo = 0;

        switch (Progress.difficulty) {
            case "easy":
                health = 100;
                break;
            case "normal":
                health = 80;
                break;
            case "hard":
                health = 60;
                break;
        }
        Instance.UpdateHealthUI();
    }

    //below: perfect/ok/miss methods, doing different things in each one.

    public static void Hit()
    {
        Progress.score += 100; comboScore++; Progress.hitCount++;
        Instance.hitSFX.Play();

        //SFX Arrow hits
        if (Input.GetKey(KeyCode.LeftArrow)){
            Instance.leftHit.Play();
        }

        if (Input.GetKey(KeyCode.RightArrow)){
            Instance.rightHit.Play();
        }
         
        if (Input.GetKey(KeyCode.UpArrow)){
            Instance.upHit.Play();
        }
        
        if (Input.GetKey(KeyCode.DownArrow)){
            Instance.downHit.Play();
        }

        if (comboScore % 100 == 0) {
            Instance.hundredToText = comboScore.ToString();
            Instance.hundredIndicator();
        }

        if (comboScore > Progress.highestCombo) {
            Progress.highestCombo = comboScore;
        }

        if (health == maxHealth) {
            Progress.peakNotes++;
        }

        if (health >= 199) {
            health = 200;
        } else {
            switch (Progress.difficulty) {
            case "easy":
                health += 4;
                break;
            case "normal":
                health += 3;
                break;
            case "hard":
                health += 2;
                break;
            }
        }
        Instance.UpdateHealthUI();
    }

    public static void OK()
    {
        Progress.score += 50; comboScore++; Progress.okCount++;

        if (comboScore % 100 == 0) {
            Instance.hundredToText = comboScore.ToString();
            Instance.hundredIndicator();
        }

        if (comboScore > Progress.highestCombo) {
            Progress.highestCombo = comboScore;
        }

        if (health == maxHealth) {
            Progress.peakNotes++;
        }

         if (health >= 200) {
            health = 200;
        } else {
            switch (Progress.difficulty) {
            case "easy":
                health += 3;
                break;
            case "normal":
                health += 2;
                break;
            case "hard":
                health += 1;
                break;
        }
        }

        Instance.UpdateHealthUI();
    }

    public static void Miss()   //missing a note that would cause you to drop to 0 health or below triggers the fail trigger and loads the results cutscene.
                                //this code will be edited to include a delay to display a "failure" graphic before a transition happens.
    {
        comboScore = 0; Progress.missCount++; Instance.missSFX.Play();

        switch (Progress.difficulty) {
            case "easy":
                health -= 3;
                break;
            case "normal":
                health -= 4;
                break;
            case "hard":
                health -= 5;
                break;
        }

        Instance.UpdateHealthUI();

        if (health <= 0) {
            Progress.failed = true;
            SceneManager.LoadScene("Results");
        }
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health,0,200);

        if (health < maxHealth/4) {
            dangerNotifier.SetActive(true);
        } else {
            dangerNotifier.SetActive(false);
        }

        if (health == maxHealth) {
            DressUpStatBonuses.peaking = true;
            peakNotifier.SetActive(true);
        } else {
            DressUpStatBonuses.peaking = false;
            peakNotifier.SetActive(false);
        }

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
        if (health < DressUpStatBonuses.scoreThreshold) {
            Progress.notEnoughHealth = true;
        }
        if (health >= DressUpStatBonuses.scoreThreshold) {
            Progress.notEnoughHealth = false;
        }

        scoreText.text = Progress.score.ToString();
        healthText.text = health.ToString();
    }

    public void UpdateHealthUI() {
        float hpFraction = health / maxHealth;
        healthFill.fillAmount = hpFraction;
    }

    public void positionThresholdIndicator() {
        switch (DressUpStatBonuses.scoreThreshold) {
            case 125: //0.625
                thresholdIndicator.transform.position = new Vector3(-1.02f, 4.58f, 0f);
                break;
            case 130: //0.65
                thresholdIndicator.transform.position = new Vector3(-0.69f, 4.58f, 0f);
                break;
            case 135: //0.675
                thresholdIndicator.transform.position = new Vector3(-0.362f, 4.58f, 0f);
                break;
            case 140: //0.7
                thresholdIndicator.transform.position = new Vector3(-0.033f, 4.58f, 0f);
                break;
            case 145: //0.725
                thresholdIndicator.transform.position = new Vector3(0.298f, 4.58f, 0f);
                break;
            case 150: //0.75
                thresholdIndicator.transform.position = new Vector3(0.627f, 4.58f, 0f);
                break;  
        }
    }

    public void hundredIndicator() {
        if (hundredTextPrefab) {
            GameObject prefab = Instantiate(hundredTextPrefab);
            prefab.GetComponentInChildren<TextMesh>().text = hundredToText;
        }
    }
}
