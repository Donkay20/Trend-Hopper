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
    public Image healthFillGold;
    public Sprite heartGold; 
    public Sprite heartNormal;
    public GameObject heart;
    public GameObject particlePosition;
    [Space]
    public AudioSource hitSFX;
    public AudioSource midSFX;
    public AudioSource missSFX;
    [Space]
    public TMPro.TextMeshPro comboScoreText;
    public TMPro.TextMeshPro comboSplash;
    public TMPro.TextMeshPro scoreText;
    public TMPro.TextMeshPro healthText;
    public TMPro.TextMeshPro healthRequirementText;
    [Space]
    public GameObject thresholdIndicator; public Animator thresholdAnim;
    public GameObject hundredTextPrefab; public Animator hundredAnim;
    [Space]
    public GameObject dangerNotifier;
    public GameObject peakNotifier;
    [Space]
    public Animator upPress;
    public Animator downPress;
    public Animator leftPress;
    public Animator rightPress;
    public Animator MC;
    public Animator NPC;
    public Animator endCard;
    public Animator heartPump;
    public Animator transition;

    public Animator scoreAnimation;
    [Space]


    static int comboScore; 
    static float health;    
    static float maxHealth = 200; 

    public int maxCombo = 500;
    private string hundredToText;           //juice to show each 100 in score.

    private ParticleSystem leftHit;         //sfx note hit
    private ParticleSystem rightHit;
    private ParticleSystem upHit;
    private ParticleSystem downHit;

    private ParticleSystem leftGoldHit;         //sfx note hit / gold
    private ParticleSystem rightGoldHit;
    private ParticleSystem upGoldHit;
    private ParticleSystem downGoldHit;

    private bool flipped;
    private bool hitEnd; private int noteCount;

    void Start()
    {
        Debug.Log(Progress.noteLimit);

        healthRequirementText.text = DressUpStatBonuses.scoreThreshold.ToString();
        positionThresholdIndicator();

        comboScoreText.faceColor = new Color32(255, 255, 255, 70); //last value is opacity
        comboSplash.faceColor = new Color32(255, 255, 255, 90);

        Instance = this;
        comboScore = 0;

        leftHit = GameObject.Find("LeftArrowSFX").GetComponent<ParticleSystem>();
        rightHit = GameObject.Find("RightArrowSFX").GetComponent<ParticleSystem>();
        upHit = GameObject.Find("UpArrowSFX").GetComponent<ParticleSystem>();
        downHit = GameObject.Find("DownArrowSFX").GetComponent<ParticleSystem>();

        leftGoldHit = GameObject.Find("LeftArrowSFX_Gold").GetComponent<ParticleSystem>();
        rightGoldHit = GameObject.Find("RightArrowSFX_Gold").GetComponent<ParticleSystem>();
        upGoldHit = GameObject.Find("UpArrowSFX_Gold").GetComponent<ParticleSystem>();
        downGoldHit = GameObject.Find("DownArrowSFX_Gold").GetComponent<ParticleSystem>();

        Instance.noteCount = 0;
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
            case null:
                health = 150;
                break;
        }
        Instance.UpdateHealthUI();
    }

    //below: perfect/ok/miss methods, doing different things in each one.

    public static void Hit()
    {
        Instance.noteCount++;
        Debug.Log(Instance.noteCount);
        Instance.flipMC();
        Progress.score += 100; comboScore++; Progress.hitCount++;
        Instance.hitSFX.Play();

        //SFX Arrow hits
        if (Input.GetKey(KeyCode.LeftArrow)){
            if (health == maxHealth) {
                Instance.leftGoldHit.Play();
            } else {
                Instance.leftHit.Play();
            }
        }

        if (Input.GetKey(KeyCode.RightArrow)){
            if (health == maxHealth) {
                Instance.rightGoldHit.Play();
            } else {
                Instance.rightHit.Play();
            }
        }
         
        if (Input.GetKey(KeyCode.UpArrow)){
            if (health == maxHealth) {
                Instance.upGoldHit.Play();
            } else {
                Instance.upHit.Play();
            }
        }
        
        if (Input.GetKey(KeyCode.DownArrow)){
            if (health == maxHealth) {
                Instance.downGoldHit.Play();
            } else {
                Instance.downHit.Play();
            }
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

        if (health == maxHealth) {
            Instance.heartPump.SetTrigger("goldpump");
        } else {
            Instance.heartPump.SetTrigger("pump");
        }

        Instance.UpdateHealthUI();
    }

    public static void OK()
    {
        Instance.midSFX.Play();
        Instance.noteCount++;
        Debug.Log(Instance.noteCount);
        Instance.flipMC();
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

        if (health == maxHealth) {
            Instance.heartPump.SetTrigger("goldpump");
        } else {
            Instance.heartPump.SetTrigger("pump");
        }
        
        Instance.UpdateHealthUI();
    }

    public static void Miss()   //missing a note that would cause you to drop to 0 health or below triggers the fail trigger and loads the results cutscene.
                                //this code will be edited to include a delay to display a "failure" graphic before a transition happens.
    {
        comboScore = 0; Progress.missCount++; Instance.missSFX.Play();
        Instance.heartPump.SetTrigger("miss");

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
            Instance.StartCoroutine(LoadLevel(1));
        }
        Instance.noteCount++;
        Debug.Log(Instance.noteCount);
    }

    // Update is called once per frame
    void Update()
    {   
        if (comboScore >= 50) {
            scoreAnimation.SetBool("hit50", true);
        } else {
            scoreAnimation.SetBool("hit50", false);
        }


        if (Input.GetKeyDown(KeyCode.LeftArrow)){
            leftPress.SetTrigger("trigger");
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)){
            rightPress.SetTrigger("trigger");
        }
         
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            upPress.SetTrigger("trigger");
        }
        
        if (Input.GetKeyDown(KeyCode.DownArrow)){
            downPress.SetTrigger("trigger");
        }

        health = Mathf.Clamp(health,0,200);

        if (health < maxHealth/4) {
            dangerNotifier.SetActive(true);
        } else {
            dangerNotifier.SetActive(false);
        }

        if (health == maxHealth) {
            heart.GetComponent<SpriteRenderer>().sprite = heartGold;
            healthFillGold.fillAmount = healthFill.fillAmount;
            DressUpStatBonuses.peaking = true;
            peakNotifier.SetActive(true);
        } else {
            heart.GetComponent<SpriteRenderer>().sprite = heartNormal;
            healthFillGold.fillAmount = 0;
            DressUpStatBonuses.peaking = false;
            peakNotifier.SetActive(false);
        }

        comboScoreText.text = comboScore.ToString();
        
        if (health < DressUpStatBonuses.scoreThreshold) {
            Progress.notEnoughHealth = true;
            thresholdAnim.SetBool("pass", false);
        } else {
            Progress.notEnoughHealth = false;
            thresholdAnim.SetBool("pass", true);
        }

        scoreText.text = Progress.score.ToString();
        healthText.text = health.ToString();

        if(Instance.noteCount == Progress.noteLimit) {          //once the song is over (as in, once the notes hit/missed match the total notes in the song)
                                                                //note: noteLimit is determined in the SongManager class. 
                if (Progress.hitCount == Progress.noteLimit) {  //perfect
                    endCard.SetBool("pp", true);
                } else if ((Progress.hitCount + Progress.okCount) == Progress.noteLimit) {  
                                                                //full combo
                    endCard.SetBool("fc", true);
                } else {
                    endCard.SetBool("clear", true);             //basic clear
                }
                Invoke(nameof(delayResults), 9.0f);
        }
    }

    public void UpdateHealthUI() {
        float hpFraction = health / maxHealth;
        healthFill.fillAmount = hpFraction;
    }

    public void positionThresholdIndicator() {
        switch (DressUpStatBonuses.scoreThreshold) {
            case 125: //0.625
                thresholdIndicator.transform.position = new Vector3(-1.02f, 5.47f, 0f);
                break;
            case 130: //0.65
                thresholdIndicator.transform.position = new Vector3(-0.69f, 5.47f, 0f);
                break;
            case 135: //0.675
                thresholdIndicator.transform.position = new Vector3(-0.362f, 5.47f, 0f);
                break;
            case 140: //0.7
                thresholdIndicator.transform.position = new Vector3(-0.033f, 5.47f, 0f);
                break;
            case 145: //0.725
                thresholdIndicator.transform.position = new Vector3(0.298f, 5.47f, 0f);
                break;
            case 150: //0.75
                thresholdIndicator.transform.position = new Vector3(0.627f, 5.47f, 0f);
                break;  
        }
    }

    public void hundredIndicator() {
        if (hundredTextPrefab) {
            hundredTextPrefab.GetComponentInChildren<TextMesh>().text = hundredToText;
            hundredAnim.SetTrigger("hundredHit");
        }
    }

    public void flipMC() {
        NPC.SetTrigger("trigger");
        switch (flipped) {
            case true:
                MC.SetBool("flip", false);
                flipped = false;
                break;
            case false:
                MC.SetBool("flip", true);
                flipped = true;
                break;
        }
    }

    public void delayResults() {
        endCard.SetBool("pp", false); endCard.SetBool("fc", false); endCard.SetBool("clear", false);
        Instance.StartCoroutine(LoadLevel(1));
    }

    public void alterParticle(bool tick) {

    }

    public static IEnumerator LoadLevel(int id) {
        Instance.transition.SetBool("exit", true);
        yield return new WaitForSeconds(1f);
        switch(id) {
            case 1:
                SceneManager.LoadScene("Results");
                break;
        }   
    }
}