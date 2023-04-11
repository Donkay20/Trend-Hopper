using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectManagerV2 : MonoBehaviour
{
    private int phase;              //stores the phase; phase 1 = song select / phase 2 = difficulty select / phase 3 = continue choice (optional).
    private int selectedLevel;      //stores the level selected. value is from 1-3.
    private string difficulty;      //stores the difficulty chosen. values are easy/normal/hard.
    private int prevLevel;          //var for menu movement management. values are from 1-3.
    private int continueChoices;    //var controlling menu for resuming from dress-up / beginning when you've seen the intro at least once. value is either 1 or 2.

    public KeyCode inputUp;
    public KeyCode inputDown;
    public KeyCode inputRight;
    public KeyCode inputLeft;       //controls
    public KeyCode cheatButton;     //debug button.
    [Space]
    public AudioSource preview1; public AudioSource preview2; public AudioSource preview3; //preview music when hovering over a song
    public AudioSource changeSong; public AudioSource changeDifficulty; public AudioSource confirm;
    [Space]
    public Animator backgrounds;
    public Animator punkAlbum; public Animator popAlbum; public Animator funkAlbum;
    public Animator difficultySelection;
    public Animator genre;
    public Animator phaseCategory;
    public Animator phase1Navigation;
    public Animator upArrow; public Animator downArrow;     //various menu animations
    [Space]
    public Animator day2;
    public GameObject day2Lock;
    public Animator day3;
    public GameObject day3Lock;     //restrictive indicators to let the user know they haven't done the stage yet
    [Space]
    public GameObject phoneDay1;
    public GameObject phoneDay2;
    public GameObject phoneDay3;    //the little text in the top-left of the screen denoting what day it is
    [Space]
    public Animator continueController; //the pop-up when you can continue from the beginning or dress-up, provided you've seen the intro before.

    void Start()
    {   
        //initialization
        phase = 1; selectedLevel = 1; continueChoices = 1; preview1.Play();
        difficulty = "normal"; Progress.difficulty = "normal";

        if (Progress.levelOneCleared) {
            day2Lock.SetActive(false);
        }
        if (Progress.levelTwoCleared) {
            day3Lock.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(inputUp)) {
            switch (phase) {
                case 1:
                    switch(selectedLevel) {
                        case 1:
                            break;
                        case 2:
                            prevLevel = selectedLevel;
                            selectedLevel--;
                            break;
                        case 3:
                            prevLevel = selectedLevel;
                            selectedLevel--;
                            break;
                    }
                    updatePhaseOne();
                    break;

                case 2:
                    switch (difficulty) {
                    case "hard":
                        difficulty = "easy";
                        break;
                    case "normal":
                        difficulty = "hard";
                        break;
                    case "easy":
                        difficulty = "normal";
                        break;
                    }
                    Progress.difficulty = difficulty;
                    updatePhaseTwo();
                    break;

                case 3:
                    switch (continueChoices) {
                        case 1:
                            continueChoices = 2;
                            continueController.SetBool("swap", true);
                            break;
                        case 2:
                            continueChoices = 1;
                            continueController.SetBool("swap", false);
                            break;
                    }
                    break;
            }
        }

        if (Input.GetKeyDown(inputDown)) {
            switch (phase) {
                case 1:
                    switch(selectedLevel) {
                        case 1:
                            prevLevel = selectedLevel;
                            selectedLevel++;
                            break;
                        case 2:
                            prevLevel = selectedLevel;
                            selectedLevel++;
                            break;
                        case 3:
                            break;
                    }
                    updatePhaseOne();
                    break;
                case 2:
                    switch (difficulty) {
                        case "hard":
                            difficulty = "normal";
                            break;
                        case "normal":
                            difficulty = "easy";
                            break;
                        case "easy":
                            difficulty = "hard";
                            break;
                    }
                    Progress.difficulty = difficulty;
                    updatePhaseTwo();
                    break;
                case 3:
                    switch (continueChoices) {
                        case 1:
                            continueChoices = 2;
                            continueController.SetBool("swap", true);
                            break;
                        case 2:
                            continueChoices = 1;
                            continueController.SetBool("swap", false);
                            break;
                    }
                    break;
            }
        }

        if (Input.GetKeyDown(inputRight)) {
            switch(phase) {
                case 1: //if you're on song select, go to stage select (provided you've already cleared the previous stage)
                    switch(selectedLevel) {
                        case 1:
                            phase = 2;
                            break;
                        case 2:
                            if(Progress.levelOneCleared) {
                                phase = 2;
                            }
                            break;
                        case 3:
                            if(Progress.levelTwoCleared) {
                                phase = 2;
                            }
                            break;
                    }
                    //If the phase was successfully moved from 1 to 2 upon hitting right when it was phase 1 (as in, if the stage wasn't locked).
                    if (phase == 2) {
                        phaseCategory.SetBool("Phase2", true);
                        phase1Navigation.SetBool("selectingLevel", false);
                        difficultySelection.SetBool("difficultyActive", true);
                        switch(selectedLevel) {
                            case 1:
                                punkAlbum.SetBool("1IsSelected", true);
                                break;
                            case 2:
                                popAlbum.SetBool("2IsSelected", true);
                                break;
                            case 3:
                                funkAlbum.SetBool("3IsSelected", true);
                                break;
                        }
                    }
                    break;

                case 2:
                    switch(selectedLevel) {
                        case 1:
                            if (Progress.day1IntroSeen) {
                                Progress.lastLevel = "dayOneIntro";
                                continueController.SetBool("appear", true);
                                phase = 3;
                            } else {
                                confirm.Play();
                                SceneManager.LoadScene("Dialogue_Day1");
                            }
                            
                            break;
                        case 2:
                            if (Progress.day2IntroSeen) {
                                continueController.SetBool("appear", true);
                                phase = 3;
                            } else {
                                confirm.Play();
                                Progress.lastLevel = "dayTwoIntro";
                                SceneManager.LoadScene("Dialogue_Day2");
                            }
                            
                            break;
                        case 3:
                            if (Progress.day3IntroSeen) {
                                continueController.SetBool("appear", true);
                                phase = 3;
                            } else {
                                confirm.Play();
                                Progress.lastLevel = "dayThreeIntro";
                                SceneManager.LoadScene("Dialogue_Day3");
                            }
                            break;
                    }
                    break;

                case 3:
                    switch(selectedLevel) {
                        //continueChoices: 1 = yes, skip right to dress up scene / 2 = no = play the dialogue normally
                        case 1:
                            if(continueChoices == 1) {
                                confirm.Play();
                                Progress.lastLevel = "dayOneIntro";
                                SceneManager.LoadScene("DressUpV2");
                            } else {
                                confirm.Play();
                                SceneManager.LoadScene("Dialogue_Day1");
                            }
                            break;
                        case 2:
                            if(continueChoices == 1) {
                                confirm.Play();
                                Progress.lastLevel = "dayTwoIntro";
                                SceneManager.LoadScene("DressUpV2");
                            } else {
                                confirm.Play();
                                SceneManager.LoadScene("Dialogue_Day2");
                            }
                            break;
                        case 3:
                            //todo
                            if(continueChoices == 1) {
                                confirm.Play();
                                Progress.lastLevel = "dayThreeIntro";
                                SceneManager.LoadScene("DressUpV2");
                            } else {
                                confirm.Play();
                                SceneManager.LoadScene("Dialogue_Day3");
                            }
                            break;
                    }
                    break;
            }
             
        }
        if (Input.GetKeyDown(inputLeft)) {    
            switch(phase) {
                case 1:
                    //todo add animation
                    SceneManager.LoadScene("TitleScreen");
                    break;

                case 2:
                    phase = 1;
                    phaseCategory.SetBool("Phase2", false);
                    phase1Navigation.SetBool("selectingLevel", true);
                    difficultySelection.SetBool("difficultyActive", false);
                    switch(selectedLevel) {
                        case 1:
                            punkAlbum.SetBool("1IsSelected", false);
                            break;
                        case 2:
                            popAlbum.SetBool("2IsSelected", false);
                            break;
                        case 3:
                            funkAlbum.SetBool("3IsSelected", false);
                            break;
                        }
                    break;

                case 3:
                    continueController.SetBool("appear", false);
                    continueController.SetBool("swap", false);
                    continueChoices = 1;
                    phase = 2;
                    break;
            }
        }

        if (Input.GetKeyDown(cheatButton)) {
            //Debugging purposes.
            //Progress.levelOneCleared = true;
            //Progress.levelTwoCleared = true;
            //Progress.levelThreeCleared = true;
            //Progress.day1IntroSeen = true;
            //Progress.day2IntroSeen = true;
            //Progress.day3IntroSeen = true;
            //day2Lock.SetActive(false);
            //day3Lock.SetActive(false);
        }
    }

    private void updatePhaseOne() {
        /*
        First, change the background by setting the irrelevant ones off, then the relevant one on.
        Then, adjust the album. Set the irrelevant one off, then set the relevant one on. Moving to the second album from the first/third is a bit more involved since it has two possible directions of travel.
        Then, adjust the genre. Set the previous one off, then the relevant one on.
        Then, adjust the tooltip on the top status bar of the phone; because it doesn't have an animation we can just set them active/inactive.
        Then, adjust the lock. If we hover on a stage we don't have access to, set the status to true, otherwise we'll make it false (on relevant selections; frex. we don't need to touch 3 when going from 2 -> 1.)
        Then, adjust the navigation arrows. Disable them if we can't go up/down any more, and enable them if we can.
        Then, adjust the song previews that are playing. Turn the relevant ones on and the irrelevant ones off.
        */
        changeSong.Play();
        switch(selectedLevel) {
            case 1:
                backgrounds.SetBool("2", false); backgrounds.SetBool("1", true);
                punkAlbum.SetBool("1IsFocus", true); 
                popAlbum.SetBool("2To3", false); popAlbum.SetBool("1To2", false); popAlbum.SetBool("2To1", true);
                genre.SetBool("2", false); genre.SetBool("1", true);
                phoneDay2.SetActive(false); phoneDay1.SetActive(true);
                day2.SetBool("day2Focus", false);
                upArrow.SetBool("upIsActive", false);
                preview1.Play(); preview2.Stop();
                break;
            case 2:
                backgrounds.SetBool("1", false); backgrounds.SetBool("3", false); backgrounds.SetBool("2", true);
                punkAlbum.SetBool("1IsFocus", false); funkAlbum.SetBool("3IsFocus", false);
                popAlbum.SetBool("2To3", false); popAlbum.SetBool("2To1", false); popAlbum.SetBool("3To2", false); popAlbum.SetBool("1To2", false); popAlbum.SetBool(prevLevel + "To" + selectedLevel, true);
                genre.SetBool("1", false); genre.SetBool("3", false); genre.SetBool("2", true);
                phoneDay1.SetActive(false); phoneDay3.SetActive(false); phoneDay2.SetActive(true);
                day3.SetBool("day3Focus", false); day2.SetBool("day2Focus", true);
                upArrow.SetBool("upIsActive", true); downArrow.SetBool("downIsActive", true);
                if (Progress.levelOneCleared) { preview2.Play(); }
                preview1.Stop(); preview3.Stop();
                break;
            case 3:
                backgrounds.SetBool("2", false); backgrounds.SetBool("3", true);
                funkAlbum.SetBool("3IsFocus", true); 
                popAlbum.SetBool("1To2", false); popAlbum.SetBool("3To2", false); popAlbum.SetBool("2To1", false); popAlbum.SetBool("2To3", true);
                genre.SetBool("2", false); genre.SetBool("3", true);
                phoneDay2.SetActive(false); phoneDay3.SetActive(true);
                day2.SetBool("day2Focus", false); day3.SetBool("day3Focus", true);
                downArrow.SetBool("downIsActive", false);
                if (Progress.levelTwoCleared) { preview3.Play(); }
                preview2.Stop();
                break;
        }
    }

    private void updatePhaseTwo() {
        changeDifficulty.Play();
        //Set the right difficulty active, set the others inactive.
        switch (difficulty) {
            case "hard":
                difficultySelection.SetBool("normal", false); difficultySelection.SetBool("easy", false); difficultySelection.SetBool("hard", true);
                break;
            case "normal":
                difficultySelection.SetBool("hard", false); difficultySelection.SetBool("easy", false); difficultySelection.SetBool("normal", true);
                break;
            case "easy":
                difficultySelection.SetBool("normal", false); difficultySelection.SetBool("hard", false); difficultySelection.SetBool("easy", true);
                break;
        }
    }
}

