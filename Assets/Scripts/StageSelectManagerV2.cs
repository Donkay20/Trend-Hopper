using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectManagerV2 : MonoBehaviour
{
    // Start is called before the first frame update

    private int phase;
    private int selectedLevel;
    private string difficulty;
    private int prevLevel;

    public KeyCode inputUp;
    public KeyCode inputDown;
    public KeyCode inputRight;
    public KeyCode inputLeft;
    public KeyCode cheatButton;
    [Space]
    public AudioSource preview1; public AudioSource preview2; //public AudioSource preview3;
    [Space]
    public Animator backgrounds;
    public Animator punkAlbum; public Animator popAlbum; public Animator funkAlbum;
    public Animator difficultySelection;
    public Animator genre;
    public Animator phaseCategory;
    public Animator phase1Navigation;
    public Animator upArrow; public Animator downArrow;
    [Space]
    public Animator day2;
    public GameObject day2Lock;
    public Animator day3;
    public GameObject day3Lock;
    [Space]
    public GameObject phoneDay1;
    public GameObject phoneDay2;
    public GameObject phoneDay3;

    void Start()
    {   
        phase = 1;
        selectedLevel = 1;
        difficulty = "normal";

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
                    updatePhaseTwo();
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
                    updatePhaseTwo();
                    break;
            }
        }

        if (Input.GetKeyDown(inputRight)) {
            switch(phase) {
                case 1:
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
                            //todo: edit for if the player has played the day 1/2/3 intro yet
                            if (difficulty == "normal") {
                                SceneManager.LoadScene("Dialogue_Day1");
                            }
                            break;
                        case 2:
                            if (difficulty == "normal") {
                                SceneManager.LoadScene("Dialogue_Day2");
                            }
                            break;
                        //case 3: todo
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
            }
        }

        if (Input.GetKeyDown(cheatButton)) {
            //Debugging purposes.
            Progress.levelOneCleared = true;
            Progress.levelTwoCleared = true;
            Progress.levelThreeCleared = true;
            day2Lock.SetActive(false);
            day3Lock.SetActive(false);
        }
    }

    private void updatePhaseOne() {
        /*
        First, change the background by setting the irrelevant ones off, then the relevant one on.

        Then, adjust the album. Set the irrelevant one off, then set the relevant one on.
        Moving to the second album from the first/third is a bit more involved since it has two possible directions of travel.

        Then, adjust the genre. Set the previous one off, then the relevant one on.

        Then, adjust the tooltip on the top status bar of the phone; because it doesn't have an animation we can just set them active/inactive.

        Then, adjust the lock. If we hover on a stage we don't have access to, set the status to true, otherwise we'll make it false (on relevant selections; frex. we don't need to touch 3 when going from 2 -> 1.)

        Then, adjust the navigation arrows. Disable them if we can't go up/down any more, and enable them if we can.
        */
        switch(selectedLevel) {
            case 1:
                backgrounds.SetBool("2", false); backgrounds.SetBool("1", true);
                punkAlbum.SetBool("1IsFocus", true); 
                popAlbum.SetBool("2To3", false); popAlbum.SetBool("1To2", false); popAlbum.SetBool("2To1", true);
                genre.SetBool("2", false); genre.SetBool("1", true);
                phoneDay2.SetActive(false); phoneDay1.SetActive(true);
                day2.SetBool("day2Focus", false);
                upArrow.SetBool("upIsActive", false);
                break;
            case 2:
                backgrounds.SetBool("1", false); backgrounds.SetBool("3", false); backgrounds.SetBool("2", true);
                punkAlbum.SetBool("1IsFocus", false); funkAlbum.SetBool("3IsFocus", false);
                popAlbum.SetBool("2To3", false); popAlbum.SetBool("2To1", false); popAlbum.SetBool("3To2", false); popAlbum.SetBool("1To2", false); popAlbum.SetBool(prevLevel + "To" + selectedLevel, true);
                genre.SetBool("1", false); genre.SetBool("3", false); genre.SetBool("2", true);
                phoneDay1.SetActive(false); phoneDay3.SetActive(false); phoneDay2.SetActive(true);
                day3.SetBool("day3Focus", false); day2.SetBool("day2Focus", true);
                upArrow.SetBool("upIsActive", true); downArrow.SetBool("downIsActive", true);
                break;
            case 3:
                backgrounds.SetBool("2", false); backgrounds.SetBool("3", true);
                funkAlbum.SetBool("3IsFocus", true); 
                popAlbum.SetBool("1To2", false); popAlbum.SetBool("3To2", false); popAlbum.SetBool("2To1", false); popAlbum.SetBool("2To3", true);
                genre.SetBool("2", false); genre.SetBool("3", true);
                phoneDay2.SetActive(false); phoneDay3.SetActive(true);
                day2.SetBool("day2Focus", false); day3.SetBool("day3Focus", true);
                downArrow.SetBool("downIsActive", false);
                break;
        }
    }

    private void updatePhaseTwo() {
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

